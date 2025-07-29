using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Messaging.ServiceBus;

namespace AzureMcp.ServiceBus.UnitTests.Utilities.JsonConverters;

public class ServiceBusReceivedMessageConverter : JsonConverter<ServiceBusReceivedMessage>
{
    public override ServiceBusReceivedMessage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? messageId = null;
        string? bodyContent = null;
        long sequenceNumber = 0;

        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected StartObject token");
        }

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string? propertyName = reader.GetString();
                reader.Read();

                switch (propertyName)
                {
                    case "messageId" or "MessageId":
                        messageId = reader.GetString();
                        break;
                    case "body" or "Body":
                        if (reader.TokenType == JsonTokenType.String)
                        {
                            bodyContent = reader.GetString();
                        }
                        else if (reader.TokenType == JsonTokenType.StartObject)
                        {
                            // Handle BinaryData object serialization - it gets serialized as an object
                            // with properties like { "data": "base64string" } or similar
                            using var doc = JsonDocument.ParseValue(ref reader);
                            var bodyElement = doc.RootElement;

                            // BinaryData often serializes as empty object {}, so we need to handle this
                            // In this case, we can't recover the original content from JSON
                            if (bodyElement.EnumerateObject().Any())
                            {
                                // Try different property names that BinaryData might use
                                if (bodyElement.TryGetProperty("data", out var dataElement))
                                {
                                    var base64String = dataElement.GetString();
                                    if (!string.IsNullOrEmpty(base64String))
                                    {
                                        try
                                        {
                                            var bytes = Convert.FromBase64String(base64String);
                                            bodyContent = System.Text.Encoding.UTF8.GetString(bytes);
                                        }
                                        catch
                                        {
                                            // If base64 decode fails, use as-is
                                            bodyContent = base64String;
                                        }
                                    }
                                }
                                else if (bodyElement.TryGetProperty("value", out var valueElement))
                                {
                                    bodyContent = valueElement.GetString();
                                }
                                else
                                {
                                    // Fallback - just get the raw JSON as string
                                    bodyContent = bodyElement.GetRawText();
                                }
                            }
                            else
                            {
                                // Empty object - BinaryData with no content info
                                // We can't recover the original content, so use empty string
                                bodyContent = "";
                            }
                        }
                        else if (reader.TokenType == JsonTokenType.Null)
                        {
                            bodyContent = null;
                        }
                        break;
                    case "sequenceNumber" or "SequenceNumber":
                        if (reader.TokenType == JsonTokenType.String && long.TryParse(reader.GetString(), out long seq))
                            sequenceNumber = seq;
                        else if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt64(out long seqNum))
                            sequenceNumber = seqNum;
                        break;
                    default:
                        // Skip unknown properties
                        reader.Skip();
                        break;
                }
            }
        }

        return ServiceBusModelFactory.ServiceBusReceivedMessage(
            body: !string.IsNullOrEmpty(bodyContent)
                ? BinaryData.FromBytes(System.Text.Encoding.UTF8.GetBytes(bodyContent))
                : BinaryData.FromBytes(System.Text.Encoding.UTF8.GetBytes("")),
            messageId: messageId ?? "",
            sequenceNumber: sequenceNumber);
    }

    public override void Write(Utf8JsonWriter writer, ServiceBusReceivedMessage value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("messageId", value.MessageId);
        writer.WriteNumber("sequenceNumber", value.SequenceNumber);
        writer.WriteString("enqueuedTime", value.EnqueuedTime.ToString("O"));
        writer.WriteNumber("deliveryCount", value.DeliveryCount);

        // Serialize the message body as string from BinaryData
        string? bodyContent = null;

        if (value.Body != null)
        {
            try
            {
                // First try the standard approaches
                var bodyString = value.Body.ToString();
                if (!string.IsNullOrEmpty(bodyString))
                {
                    bodyContent = bodyString;
                }
            }
            catch
            {
                // ToString failed, try other methods
            }

            if (bodyContent == null)
            {
                try
                {
                    // Try ToMemory approach
                    var memory = value.Body.ToMemory();
                    var bytes = memory.ToArray();
                    if (bytes != null && bytes.Length > 0)
                    {
                        bodyContent = System.Text.Encoding.UTF8.GetString(bytes);
                    }
                }
                catch
                {
                    // ToMemory failed, try reflection
                }
            }

            if (bodyContent == null)
            {
                try
                {
                    // Try reflection to access private _data field
                    var binaryDataType = value.Body.GetType();
                    var dataField = binaryDataType.GetField("_data", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if (dataField != null)
                    {
                        var data = dataField.GetValue(value.Body);
                        if (data is byte[] bytes && bytes.Length > 0)
                        {
                            bodyContent = System.Text.Encoding.UTF8.GetString(bytes);
                        }
                    }
                }
                catch
                {
                    // Reflection failed
                }
            }

            if (bodyContent != null)
            {
                writer.WriteString("body", bodyContent);
            }
            else
            {
                // Final fallback
                writer.WriteString("body", "");
            }
        }
        else
        {
            writer.WriteNull("body");
        }

        // Add application properties if any
        if (value.ApplicationProperties?.Count > 0)
        {
            writer.WriteStartObject("applicationProperties");
            foreach (var prop in value.ApplicationProperties)
            {
                writer.WritePropertyName(prop.Key);
                JsonSerializer.Serialize(writer, prop.Value, options);
            }
            writer.WriteEndObject();
        }

        writer.WriteEndObject();
    }
}