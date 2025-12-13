#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Cleans obsolete API documentation files before regenerating with DocFX.

.DESCRIPTION
    Removes all generated YAML files from the api/ directory and the _site/
    output folder to ensure only current types are documented and built.
    Preserves any manually-created markdown files like api/index.md.

.EXAMPLE
    .\Clean-ApiDocs.ps1
    .\Clean-ApiDocs.ps1 -Verbose
#>

[CmdletBinding()]
param()

$apiPath = Join-Path $PSScriptRoot "api"
$sitePath = Join-Path $PSScriptRoot "_site"
$isVerboseOutput = $PSBoundParameters.ContainsKey('Verbose').Equals($true)

# Step 1: Clean API folder
if (Test-Path $apiPath) {
    Write-Information "Cleaning API documentation folder..." -InformationAction Continue
    
    # Remove all .yml files (generated API docs)
    Get-ChildItem -Path $apiPath -Filter "*.yml" -File | Remove-Item -Force -Verbose:$isVerboseOutput
    
    Write-Host "✓ Cleaned obsolete API documentation files" -ForegroundColor Green
} else {
    Write-Debug "API folder does not exist yet: $apiPath"
}

# Step 2: Clean _site folder
if (Test-Path $sitePath) {
    Write-Information "Cleaning output site folder..." -InformationAction Continue
    Remove-Item -Path $sitePath -Recurse -Force -Verbose:$isVerboseOutput
    Write-Host "✓ Cleaned output site folder" -ForegroundColor Green
} else {
    Write-Debug "Output site folder does not exist yet: $sitePath"
}

# Step 3: Reset to original location
Pop-Location