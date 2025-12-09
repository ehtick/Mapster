#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Full documentation build: Clean, regenerate, and build.

.DESCRIPTION
    1. Cleans obsolete API documentation files (Clean-ApiDocs.ps1)
    2. Builds documentation using Build-Docs.ps1

.EXAMPLE
    .\Build-Docs-Full.ps1
    .\Build-Docs-Full.ps1 -Verbose
    .\Build-Docs-Full.ps1 -serve $true -openBrowser $true
#>

[CmdletBinding()]
param(
    [Parameter()]
    [ValidateSet('Quiet', 'Info', 'Warning', 'Error', 'Verbose')]
    [string]$LogLevel = 'Warning'
)

# Step 1: Set the current location
Set-Location $PSScriptRoot

# Step 2: Clean obsolete API docs
Write-Information "Cleaning obsolete API documentation..." -InformationAction Continue
& .\Clean-ApiDocs.ps1

# Step 3: Build documentation
Write-Information "Building documentation..." -InformationAction Continue

& .\Build-Docs.ps1 -LogLevel $LogLevel -serve $true -open-browser $true

if ($LASTEXITCODE -ne 0) {
    Write-Error "Documentation build failed"
    exit $LASTEXITCODE
}

Write-Information "✓ Full documentation build completed successfully!" -InformationAction Continue
Write-Debug "Output: $PSScriptRoot\_site"

# Step 4: Reset to original location
Pop-Location