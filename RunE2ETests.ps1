#!/usr/bin/env pwsh

# E2E test runner for PowerShell with cleanup guarantee

# Cleanup function
function Cleanup {
    Write-Host "Cleaning up..." -ForegroundColor Yellow
    # Go back to parent directory if we're in e2e
    if ((Get-Location).Path -like "*\e2e") {
        Set-Location ..
    }
    docker-compose down
    Write-Host "Cleanup complete" -ForegroundColor Green
}

# Set trap for cleanup
try {
    Write-Host "=== Starting E2E Test Run ===" -ForegroundColor Cyan

    # 1. Build
    Write-Host "Building Docker images..." -ForegroundColor Yellow
    docker-compose build

    # 2. Start services
    Write-Host "Starting services..." -ForegroundColor Yellow
    docker-compose up -d

    # 3. Wait for services
    Write-Host "Waiting for services to be ready... (5s)" -ForegroundColor Yellow
    Start-Sleep -Seconds 5

    # 4. Run tests
    Write-Host "Running E2E tests..." -ForegroundColor Green
    Set-Location e2e
    
    # Run tests
    npx playwright test --headed
    
} catch {
    Write-Host "Error occurred: $_" -ForegroundColor Red
    throw $_  # Re-throw to ensure proper exit code
} finally {
    # This ALWAYS runs, even on error or success
    Cleanup
}