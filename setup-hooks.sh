#!/bin/sh
# setup-hooks.sh
mkdir -p .git/hooks
cp -f hooks/pre-commit .git/hooks/
echo "Git hooks installed successfully!"