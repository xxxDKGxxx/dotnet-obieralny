#!/usr/bin/env bash

# E2E test runner for Linux bash with cleanup guarantee

set -e  # Exit immediately on error
set -o pipefail

# Cleanup function
cleanup() {
    echo -e "\e[33mCleaning up...\e[0m"

    # Go back to parent directory if currently inside e2e
    if [[ "$PWD" == */e2e ]]; then
        cd ..
    fi

    docker-compose down || true

    echo -e "\e[32mCleanup complete\e[0m"
}

# Ensure cleanup always runs
trap cleanup EXIT

echo -e "\e[36m=== Starting E2E Test Run ===\e[0m"

echo -e "\e[33mBuilding Docker images...\e[0m"
docker-compose build

echo -e "\e[33mStarting services...\e[0m"
docker-compose up -d

echo -e "\e[33mWaiting for services to be ready... (5s)\e[0m"
sleep 5

echo -e "\e[32mRunning E2E tests...\e[0m"
cd e2e

# Run tests (headed mode)
npx playwright test --headed
