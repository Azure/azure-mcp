# Tool Description Evaluator

This is a testing and analysis tool that evaluates how well Azure MCP Server tool descriptions match user prompts using AI embeddings. It helps ensure that the right tools are selected when users ask questions or make requests.

## Overview

The application:

1. Loads tool definitions from the Azure MCP Server (dynamically or from JSON files)
1. Loads test prompts from markdown or JSON files (default: `e2eTests/e2eTestPrompts.md`)
1. Creates embeddings for tool descriptions using Azure OpenAI's `text-embedding-3-large` model
1. Tests prompt-to-tool matching using vector similarity search with cosine similarity
1. Generates confidence scores and analysis reports to identify gaps in tool selection accuracy

## Project Structure

```text
.
├── Program.cs                              # Main application logic
├── Models/                                 # Data models for tools, prompts, and results
├── Services/                               # Embedding and analysis services
├── VectorDb/                               # Vector database implementation with cosine similarity
├── ToolDescriptionEvaluator.csproj         # Project file
├── tools.json                              # Tool definitions (fallback/static)
├── prompts.json                            # Test prompts (fallback/static)
├── .env.example                            # Environment variables template
├── results.txt                             # Analysis output (plain text)
├── results.md                              # Analysis output (markdown)
└── README.md                               # This file
```

## Usage Modes

### 1. Full Analysis Mode (Default)

Runs comprehensive analysis on all tools and prompts:

```bash
dotnet run
```

### 2. Validation Mode

Tests a specific tool description against one or more prompts:

```bash
# Single prompt validation
dotnet run -- --validate \
  --tool-description "Lists all storage accounts in a subscription" \
  --prompt "show me my storage accounts"

# Multiple prompt validation
dotnet run -- --validate \
  --tool-description "Lists storage accounts" \
  --prompt "show me storage accounts" \
  --prompt "list my storage accounts" \
  --prompt "what storage accounts do I have"
```

### 3. Custom Files Mode

Use custom tools or prompts files:

```bash
# Use custom tools file
dotnet run -- --tools-file my-tools.json

# Use custom prompts file (supports .md or .json)
dotnet run -- --prompts-file my-prompts.md

# Use both custom files
dotnet run -- --tools-file my-tools.json --prompts-file my-prompts.json
```

## Input Data Sources

The tool can load data from multiple sources:

### Tool Definitions

- **Dynamic loading** (default): Queries Azure MCP Server directly for current tool definitions
- **Static JSON file**: Uses `tools.json` or custom file specified with `--tools-file`

### Test Prompts

- **Markdown format** (default): Uses `../../../e2eTests/e2eTestPrompts.md`
- **JSON format**: Uses `prompts.json` or custom file specified with `--prompts-file`
- **Custom files**: Supports both `.md` and `.json` formats

## Quick Start

You can call the build script in this directory:

```bash
./Run-ToolDescriptionEvaluator.ps1
```

or run the following commands directly:

```bash
dotnet build
dotnet run
```

## Setup

### Environment Configuration

This application requires two environment variables to be configured:

#### Required Environment Variables

1. **`AOAI_ENDPOINT`** - Your Azure OpenAI endpoint base URL (without deployment path)
2. **`TEXT_EMBEDDING_API_KEY`** - Your Azure OpenAI API key

#### Option 1: Environment Variables (Recommended for Production)

Set both required environment variables:

```bash
export AOAI_ENDPOINT="https://<your-resource>.openai.azure.com"
export TEXT_EMBEDDING_API_KEY="your_api_key_here"
```

The tool automatically constructs the full embeddings endpoint:
`{AOAI_ENDPOINT}/openai/deployments/text-embedding-3-large/embeddings?api-version=2024-02-01`

#### Option 2: .env File (Recommended for Local Development)

1. Copy the example environment file:

   ```bash
   cp .env.example .env
   ```

2. Edit `.env` and add both required variables:

   ```env
   AOAI_ENDPOINT=https://<your-resource>.openai.azure.com
   TEXT_EMBEDDING_API_KEY=your_actual_api_key_here
   ```

## Output Formats and Analysis

The tool generates detailed analysis reports in two formats:

### Plain Text Output (Default)

Results are written to `results.txt`:

```bash
dotnet run
```

- Compact, simple format for quick review
- Includes confidence scores and success rates
- Shows top matching tools for each prompt

### Markdown Output (Documentation)

Generate structured markdown reports:

```bash
# Using environment variable
output=md dotnet run

# Using command line flag  
dotnet run -- --markdown
```

Results are written to `results.md` with:

- 📊 **Structured layout** with headers and navigation
- 📋 **Table of Contents** with clickable links  
- 📈 **Results tables** with visual indicators (✅/❌)
- 📊 **Success rate analysis** with performance ratings
- 🕐 **Execution timing** and statistics

### Analysis Metrics

The tool provides several key metrics:

- **Confidence Scores**: Cosine similarity scores (0.0 to 1.0) between prompts and tool descriptions
- **Success Rate**: Percentage of prompts where the expected tool ranked highest
- **Performance Ratings**: 🟢 Excellent (>90%), 🟡 Good (75-90%), 🟠 Fair (50-75%), 🔴 Poor (<50%)
- **Top-N Accuracy**: How often the expected tool appears in top 3, 5, or 10 results

## Configuration Files

### Test Prompts Format

#### Markdown Format (Default)

The tool reads from `e2eTests/e2eTestPrompts.md` which contains tables like:

```markdown
## Azure Storage

| Tool Name | Test Prompt |
|:----------|:----------|
| azmcp-storage-account-list | List all storage accounts in my subscription |
| azmcp-storage-account-list | Show me my storage accounts |
| azmcp-storage-container-list | List containers in storage account <account-name> |
```

#### JSON Format (Alternative)

Prompts can be organized in JSON format:

```json
{
  "azmcp-storage-account-list": [
    "List all storage accounts in my subscription",
    "Show me my storage accounts"
  ],
  "azmcp-storage-container-list": [
    "List containers in storage account <account-name>"
  ]
}
```

### Tool Definitions File

Contains complete tool metadata including:

- Tool names and descriptions
- Input parameter schemas  
- Usage examples and annotations

## Use Cases

This tool is valuable for:

- **Quality Assurance**: Ensuring tool descriptions accurately match user intents
- **Tool Development**: Testing new tool descriptions before deployment  
- **Performance Monitoring**: Tracking how well the MCP server matches user prompts
- **Documentation**: Identifying gaps in tool coverage or unclear descriptions
- **Regression Testing**: Verifying that changes don't break existing prompt-to-tool matching

## Security and Best Practices

- **Never commit API keys to version control**
- Use environment variables in production environments
- Use `.env` files for local development (they're automatically gitignored)
- Rotate your Azure OpenAI API keys regularly
- Use least-privilege access principles for Azure OpenAI resources
- Consider using Azure Key Vault for production deployments

## CI/CD Integration

The tool supports CI mode with graceful handling of missing credentials:

```bash
dotnet run -- --ci
```

In CI mode, the tool will:

- Skip analysis if Azure OpenAI credentials are not available
- Exit with code 0 (success) instead of failing
- Log helpful messages about what was skipped
