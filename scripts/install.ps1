. ".\_shared.ps1"

# Powershell
# Checks required major version
# For more details, please check the official docs at:
# https://docs.microsoft.com/en-us/powershell/scripting/setup/installing-windows-powershell
$requiredMajorVersion = 5 # Version shipped with Windows 10
$currentMajorVersion = get-current-powershell-major-version
$powerShellVersionOk = is-required-powershell-major-version-or-later -currentMajorVersion $currentMajorVersion -requiredMajorVersion $requiredMajorVersion
if ($powerShellVersionOk -eq $false) {
    Write-Host ""
    Write-Host "Your current PowerShell version is $currentMajorVersion." -f Red
    Write-Host "The required PowerShell version is $requiredMajorVersion or greater." -f Red
    Write-Host "For more details on how to update you current PowerShell installation, check the docs at:" -f Yellow
    Write-Host "https://docs.microsoft.com/en-us/powershell/scripting/setup/installing-windows-powershell" -f Cyan
    Write-Host ""
    break
}

# .Net Framework
# Check required release number value
# For more details, please check the official docs at:
# https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed
$requiredReleaseVersion = 461308; # .NET Framework 4.7.1 shipped with Visual Studio 2017 Community or higher
$installedNetFrameworkSDKs = get-installed-net-framework-sdks
$netFrameworkVersionOk = is-required-net-framework-sdk-or-greater-installed -installedNetFrameworkSDKs $installedNetFrameworkSDKs -requiredReleaseVersion $requiredReleaseVersion
if ($netFrameworkVersionOk -eq $false) {
    Write-Host ""
    Write-Host "Installed SDKs:"
    Write-Output $installedNetFrameworkSDKs | Format-Table -Property Release,TargetVersion,Version
    Write-Host "The required release version $requiredReleaseVersion or greater was not found." -f Red
    Write-Host "For more details on how to install or upgrade your environment, please check the docs at:" -f Yellow
    Write-Host "https://www.microsoft.com/net/download/visual-studio-sdks" -f Cyan
    Write-Host ""
    break
}

Invoke-Expression ".\build-solution.ps1"
