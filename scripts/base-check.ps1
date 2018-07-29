
param (
    # Powershell
    # The value to check is the corresponding major version
    # PowerShell version 5 (Shipped with Windows 10 installation)
    # For more details regarding other versions, please check the official docs at:
    # https://docs.microsoft.com/en-us/powershell/scripting/setup/installing-windows-powershell
    [int]$requiredPowerShellMajorVersion = 5,

    # .NET Framework SDK
    # The value to check is the corresponding release number value
    # .NET Framework 4.7.1 (Included in Visual Studio 2017 Community or higher)
    # For more details regarding other release versions, please check the official docs at:
    # https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed
    [int]$requiredNetFrameworkReleaseVersion = 461308
)

. ".\_shared.ps1"

Clear-Host
Write-Host ""

# POWESHELL
Write-Host "Checking PowerShell version..........." -NoNewline
$currentPowershellMajorVersion = get-current-powershell-major-version
$powerShellVersionOk = is-required-powershell-major-version-or-later -currentMajorVersion $currentPowershellMajorVersion -requiredMajorVersion $requiredPowerShellMajorVersion
if ($powerShellVersionOk -eq $false) {
    Write-Host "[FAILED]" -f Red
    Write-Host ""
    Write-Host $tab "Your current PowerShell version is $currentPowershellMajorVersion." -f Yellow
    Write-Host $tab "The required PowerShell version is $requiredPowerShellMajorVersion or greater." -f Red
    Write-Host $tab "For more details on how to update you current PowerShell installation, check the docs at:"
    Write-Host $tab "https://docs.microsoft.com/en-us/powershell/scripting/setup/installing-windows-powershell" -f Cyan
    Write-Host ""
    break
}
Write-Host "[OK]" -f Green

# .NET FRAMEWORK SDK
Write-Host "Checking .NET Framework SDK version..." -NoNewline
$installedNetFrameworkSDKs = get-installed-net-framework-sdks
$netFrameworkVersionOk = is-required-net-framework-sdk-or-greater-installed -installedNetFrameworkSDKs $installedNetFrameworkSDKs -requiredReleaseVersion $requiredNetFrameworkReleaseVersion
if ($netFrameworkVersionOk -eq $false) {
    Write-Host "[FAILED]" -f Red
    Write-Host ""
    Write-Host "$tab Installed .NET Framework SDKs:" -f Yellow
    Write-Host ""
    $installedNetFrameworkSDKs | ForEach-Object { Write-Host ("$tab $tab Version {0} (Release {1})" -f $_.Version, $_.Release) -f Yellow }
    Write-Host ""
    Write-Host $tab "The required release version $requiredNetFrameworkReleaseVersion or greater was not found." -f Red
    Write-Host $tab "For more details on how to install or upgrade your environment, please check the docs at:"
    Write-Host $tab "https://www.microsoft.com/net/download/visual-studio-sdks" -f Cyan
    Write-Host $tab ""
    break
}
Write-Host "[OK]" -f Green
