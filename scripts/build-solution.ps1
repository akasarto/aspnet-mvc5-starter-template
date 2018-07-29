param (
    # Powershell
    # The value to check is the corresponding major version
    # PowerShell version 5 (Shipped with Windows 10 installation)
    # For more details regarding other versions, please check the official docs at:
    # https://docs.microsoft.com/en-us/powershell/scripting/setup/installing-windows-powershell
    [int]$requiredPowerShellMajorVersion = 5
)

. ".\_shared.ps1"

$nugetToolExe = "..\tools\nuget.exe"
$msBuildToolExe = get-msbuild-executable-path

if (($msBuildToolExe -eq $null) -or (-Not (Test-Path $msBuildToolExe))) {
    Write-Host ""
    Write-Host "The tool MsBuild.exe was not found. Please make sure you have any Visual Studio 2017(v15.2 or higher) installed." -f Red
    Write-Host "If this is running on a build server, make sure you have the required build tools installed." -f Yellow
    Write-Host ""
    break
}

# Attempt to restor nuget packages
# Check if the last exit code was successfull
# For more details about this verification, please check the docs at:
# https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_automatic_variables?view=powershell-5.0
Write-Host "Restoring solution nuget packages....." -NoNewline
$output = & cmd /c $nugetToolExe restore .\sources\platform-solutions\starterTemplateMVC5.sln -verbosity quiet 2`>`&1
if (-Not $?)
{
    Write-Host "[FAILED]" -f Red
    Write-Host ""
    Write-Host $tab $output -f Red
    Write-Host ""
    break
}
Write-Host "[OK]" -f Green