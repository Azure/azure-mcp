# Tool Selection

This project uses Azure OpenAI embeddings to find the most relevant tools for given prompts. The project supports multiple implementations:

## Overview

The application:

1. Loads tool definitions from `list-tools.json`
1. Loads test prompts from `prompts.json`
1. Creates embeddings for tool descriptions using Azure OpenAI
1. Tests prompt-to-tool matching using vector similarity search

## Project Structure

```
.
├── Program.cs       # Main application logic
├── Models/          # Data models
├── Services/        # Service classes
├── VectorDb/        # Vector database implementation
├── list-tools.json  # Tool definitions
├── prompts.json     # Test prompts
├── api-key.txt      # API key file (optional)
├── .env.example     # Environment variables template
└── README.md        # This file
```

## Data Files

These files are used by all implementations:

- `list-tools.json` - Tool definitions and schemas
- `prompts.json` - Test prompts organized by expected tool (easily editable)
- `api-key.txt` - Optional API key file (alternative to environment variables)

## Quick Start

You can call the `build.sh` script in this directory:

```bash
./build.sh
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

1. **`TEXT_EMBEDDING_API_KEY`** - Your Azure OpenAI API key
1. **`AOAI_ENDPOINT`** - Your Azure OpenAI endpoint URL (including deployment and API version)

#### Option 1: Environment Variables (Recommended)

Set both required environment variables:

```bash
export TEXT_EMBEDDING_API_KEY="your_api_key_here"
export AOAI_ENDPOINT="https://<your-resource>.openai.azure.com/openai/deployments/text-embedding-3-large/embeddings?api-version=2023-05-15"
```

#### Option 2: .env File (Recommended for local development)

1. Copy the example environment file:

   ```bash
   cp .env.example .env
   ```

1. Edit `.env` and add both required variables:

   ```
   TEXT_EMBEDDING_API_KEY=your_actual_api_key_here
   AOAI_ENDPOINT=https://<your-resource>.openai.azure.com/openai/deployments/text-embedding-3-large/embeddings?api-version=2023-05-15
   ```

## Running

### Basic Usage

```bash
dotnet run
```

### Output Formats

The application supports different output formats based on your needs:

#### Plain Text Output (Default)

Results are printed to a results.txt file by default

```bash
dotnet run
```

#### Markdown Output (Documentation)

To generate markdown format, set the `output` environment variable to `md`:

```bash
output=md dotnet run . > analysis_results.md
```

#### Output Format Features

##### Plain Text (.txt)

- Compact, simple format
- Minimal formatting for easy parsing
- Includes success rate and performance ratings

##### Markdown (.md)

- 📊 **Structured layout** with headers and navigation
- 📋 **Table of Contents** with clickable links
- 📈 **Results tables** with visual indicators (✅/❌)
- 📊 **Success rate analysis** with performance ratings
- 🕐 **Execution timing** and statistics

#### Sample Markdown Features

- **Visual status indicators**: ✅ for expected tools, ❌ for others
- **Performance ratings**: 🟢 Excellent, 🟡 Good, 🟠 Fair, 🔴 Poor
- **Professional tables** for easy analysis
- **Clickable navigation** for large result sets

See `MARKDOWN_OUTPUT.md` for detailed examples and features.

## Configuration Files

### prompts.json

Contains test prompts organized by expected tool name. The structure is:

```json
{
  "tool-name": [
    "Test prompt 1",
    "Test prompt 2"
  ]
}
```

This file can be easily edited to:

- Add new test prompts
- Modify existing prompts
- Add prompts for new tools
- Remove outdated prompts

### list-tools.json

Contains the complete tool definitions including:

- Tool names and descriptions
- Input schemas
- Annotations (permissions, hints, etc.)

## Security Best Practices

- **Never commit API keys to version control**
- Use environment variables in production
- Use `.env` files for local development (they're gitignored)
- Rotate your API keys regularly
- Use least-privilege access principles
