#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Builds DocFX documentation with clean API regeneration.

.DESCRIPTION
    1. Cleans obsolete API documentation files
    2. Regenerates metadata from current source code
    3. Builds the documentation site

.EXAMPLE
    .\Build-Docs.ps1
    .\Build-Docs.ps1 -Verbose
    .\Build-Docs.ps1 -serve $true -openBrowser $true
#>

[CmdletBinding()]
param(
    [Parameter()]
    [ValidateSet('Quiet', 'Info', 'Warning', 'Error', 'Verbose')]
    [string]$LogLevel = 'Warning',
    [bool]$serve = $false,
    [bool]$openBrowser = $false
)

Set-Location $PSScriptRoot

# Step 1: Clean obsolete API docs
Write-Information "Cleaning obsolete API documentation..." -InformationAction Continue
& .\Clean-ApiDocs.ps1

# Step 2: Run DocFX (metadata + build + pdf in one command)
Write-Information "Running DocFX..." -InformationAction Continue
docfx docfx.json --logLevel $LogLevel --serve:$serve --open-browser:$openBrowser

if ($LASTEXITCODE -ne 0) {
    Write-Error "DocFX build failed"
    exit $LASTEXITCODE
}

Write-Host "✓ Documentation built successfully!" -ForegroundColor Green
Write-Debug "Output: $PSScriptRoot\_site"

# Step 3: Reset to original location
Pop-Location