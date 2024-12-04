#!/usr/bin/env bash

# Usage ./run.sh day-01
#    or ./run.sh day-01 part2.js
SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

if [ -f "$SCRIPT_DIR/$1"/index.js ]; then
  type="Node"
fi
if [ -f "$SCRIPT_DIR/$1"/Startup.cs ]; then
  type="Dotnet"
fi

docker build -t "$1" -f "$SCRIPT_DIR/dockerfiles/$type.Dockerfile" "$SCRIPT_DIR/$1"
docker run --rm "$@"
