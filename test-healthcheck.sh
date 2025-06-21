#!/bin/bash

# Docker test script for azmcp application
set -e

IMAGE_NAME="azmcp-app"
CONTAINER_NAME="azmcp-test"

echo "🏗️  Building Docker image for linux/amd64..."
docker build --platform linux/amd64 -t $IMAGE_NAME .

echo "🚀 Starting container..."

docker run --platform linux/amd64 -d --name $CONTAINER_NAME  $IMAGE_NAME \
  --transport sse --service azure --port 5009

echo "⏳ Waiting for container to start..."
sleep 10

echo "🔍 Checking container status..."
if docker ps | grep -q $CONTAINER_NAME; then
    echo "✅ Container is running"
else
    echo "❌ Container is not running"
    docker logs $CONTAINER_NAME
    exit 1
fi

echo "🏥 Checking health status..."
for i in {1..6}; do
    HEALTH=$(docker inspect --format='{{.State.Health.Status}}' $CONTAINER_NAME 2>/dev/null || echo "unknown")
    echo "Attempt $i/6: Health status = $HEALTH"
    
    if [ "$HEALTH" = "healthy" ]; then
        echo "✅ Container is healthy!"
        break
    elif [ "$HEALTH" = "unhealthy" ]; then
        echo "❌ Container is unhealthy!"
        echo "📋 Container logs:"
        docker logs $CONTAINER_NAME
        exit 1
    fi
    
    if [ $i -eq 6 ]; then
        echo "⚠️  Health check timed out"
        echo "📋 Container logs:"
        docker logs $CONTAINER_NAME
        exit 1
    fi
    
    sleep 10
done

echo "📋 Final container logs:"
docker logs $CONTAINER_NAME

echo "🧹 Cleaning up..."
docker stop $CONTAINER_NAME
docker rm $CONTAINER_NAME

echo "🎉 Test completed successfully!"