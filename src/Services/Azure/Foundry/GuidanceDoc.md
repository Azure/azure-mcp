# Model Integration Instructions (Agent Use Only)

This guide outlines how an agent can access and interact with a hosted Azure inference model using the `azure-ai-inference` SDK in Python. Authentication is handled via the `GITHUB_TOKEN` environment variable, and all requests are routed through the Azure GitHub-hosted endpoint.

## Requirements

- **Python** `>= 3.8`
- **Environment Variable:** `GITHUB_TOKEN` must be set with a valid GitHub Personal Access Token (PAT) with `models:read` scope.

## Installation

```bash
pip install azure-ai-inference
```

## Client Initialization

```python
import os
from azure.ai.inference import ChatCompletionsClient
from azure.core.credentials import AzureKeyCredential

endpoint = "https://models.github.ai/inference"
token = os.environ["GITHUB_TOKEN"]

client = ChatCompletionsClient(
    endpoint=endpoint,
    credential=AzureKeyCredential(token),
)
```

## Usage Patterns

### Single-Turn Chat Completion

```python
from azure.ai.inference.models import UserMessage

response = client.complete(
    messages=[
        UserMessage("What is the capital of France?"),
    ],
    model={{inference_model_name}}  # Provided to the agent
)

content = response.choices[0].message.content
```

### Multi-Turn Conversation

Maintain message history manually across turns.

```python
from azure.ai.inference.models import UserMessage, AssistantMessage

messages = [
    UserMessage("Who was the first president of the U.S.?"),
    AssistantMessage("George Washington."),
    UserMessage("And the second?"),
]

response = client.complete(messages=messages, model={{inference_model_name}})
content = response.choices[0].message.content
```

### Streaming Output (Partial Token Delivery)

```python
from azure.ai.inference.models import UserMessage

response = client.complete(
    stream=True,
    messages=[
        UserMessage("List 3 benefits of daily meditation."),
    ],
    model_extras={'stream_options': {'include_usage': True}},
    model={{inference_model_name}}
)

final_usage = {}
for update in response:
    if update.choices and update.choices[0].delta:
        print(update.choices[0].delta.content or "", end="")
    if update.usage:
        final_usage = update.usage
```

## Scaling Beyond Free Limits

This integration uses GitHub token-based access via the `models.github.ai/inference` endpoint. For production or high-throughput scenarios, provision the model through an Azure subscription and authenticate with Azure credentials. No SDK code changes are required beyond swapping the credential type.

---

> ⚠️ This file is for internal agent integration only. It assumes secure handling of credentials and dynamic assignment of `inference_model_name` during runtime.
