#!/bin/bash

# Build script for tool selection confidence score calculation

set -e

echo "Building tool selection confidence score calculation app..."

# Restore dependencies
echo "Restoring dependencies..."
dotnet restore

# Build the application
echo "Building application..."
dotnet build --configuration Release

echo "Build completed successfully!"
echo "Run with: dotnet run"

# Optional: Run tests if they exist
if ls *Test* 1> /dev/null 2>&1; then
    echo "Running tests..."
    dotnet test
fi
