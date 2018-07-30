Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

# \t char
$tab = [char]9

# Gets the current powershell major version
# For more details regarding powershell installation, check the official docs at:
# https://docs.microsoft.com/en-us/powershell/scripting/setup/installing-windows-powershell
function get-current-powershell-major-version() {
    $currentMajorVersion = $PSVersionTable.PSVersion.Major
    return $currentMajorVersion
}

# Check the current powershell required major version
# For more details regarding powershell installation, check the official docs at:
# https://docs.microsoft.com/en-us/powershell/scripting/setup/installing-windows-powershell
function is-required-powershell-major-version-or-later($currentMajorVersion, $requiredMajorVersion) {
    if($currentMajorVersion -ge $requiredMajorVersion) {
        return $true
    }
    return $false
}

# Get information about installed .net framework sdks.
# https://www.microsoft.com/net/download/visual-studio-sdks
function get-installed-net-framework-sdks()
{
    $existingSdks =
        Get-ChildItem "HKLM:SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\" `
        | Get-ItemProperty
    $existingSdks
}

# Check if the current system has the required .net framework sdk intalled based on the release version value
# https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed
function is-required-net-framework-sdk-or-greater-installed($installedNetFrameworkSDKs, $requiredReleaseVersion)
{
    $releases =
        $installedNetFrameworkSDKs `
        | Get-ItemPropertyValue -Name Release

    ForEach ($release in $releases) {
        if ($release -ge $requiredReleaseVersion) {
            return $true
        }
    }
    return $false
}

# Gets the current MSBuild.exe too path
# For more details regarding the VSWhere tool, please check the docs at:
# https://github.com/Microsoft/vswhere/wiki
function get-msbuild-executable-path([string]$vsWhereToolExe)
{
    $vsInstallationPath = & $vsWhereToolExe -latest -products * -requires Microsoft.Component.MSBuild -property installationPath
    if ($vsInstallationPath) {
        $vsInstallationPath = Join-Path $vsInstallationPath "MSBuild\*\Bin"
        $pathMatches = Get-Childitem $vsInstallationPath -Recurse -Filter MSBuild.exe
        ForEach($pathMatch in $pathMatches) {
            if ($pathMatch -like "*\Bin\MSBuild.exe"){
                return $pathMatch
            }
        }
    }
    return $null
}

# Check Powershell major version
# The value to check is the corresponding major version
# PowerShell version 5 (Shipped with Windows 10 installation)
# For more details regarding other versions, please check the official docs at:
# https://docs.microsoft.com/en-us/powershell/scripting/setup/installing-windows-powershell
function check-required-powershell-version([int]$requiredPowerShellMajorVersion) {
    Write-Host "Checking PowerShell version..............." -NoNewline
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
}

# Check .NET Framework SDK release version
# The value to check is the corresponding release number value
# .NET Framework 4.7.1 (Included in Visual Studio 2017 Community or higher)
# For more details regarding other release versions, please check the official docs at:
# https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed
function check-required-net-framework-version([int]$requiredNetFrameworkReleaseVersion) {
    Write-Host "Checking .NET Framework SDK version......." -NoNewline
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
}

function check-msbuild-tool-executable([string]$vsWhereToolExe) {
    Write-Host "Checking MSBuild.exe tool installation...." -NoNewline
    $msBuildToolExe = get-msbuild-executable-path -vsWhereToolExe $vsWhereToolExe
    if (($msBuildToolExe -eq $null) -or (-Not (Test-Path $msBuildToolExe))) {
        Write-Host "[FAILED]" -f Red
        Write-Host ""
        Write-Host "The tool MsBuild.exe was not found. Please make sure you have any Visual Studio 2017(v15.2 or higher) installed." -f Red
        Write-Host "If this is running on a build server, make sure you have either Visual Studio or visual studio build tool installed."
        Write-Host "You can find those under 'Visual Studio 2017' and 'Tools for Visual Studio 2017' at:"
        Write-Host "https://visualstudio.microsoft.com/downloads/" -f Cyan
        Write-Host ""
        break
    }
    Write-Host "[OK]" -f Green
}

# Attempt to restor nuget packages
# Check if the last exit code was successfull
# For more details about this verification, please check the docs at:
# https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_automatic_variables?view=powershell-5.0
function restore-nuget-packages-for-solution([string]$nugetToolExe, [string]$solutionFilePath) {
    Write-Host "Restoring solution nuget packages........." -NoNewline
    $output = & cmd /c $nugetToolExe restore $solutionFilePath -verbosity quiet 2`>`&1
    if (-Not $?)
    {
        Write-Host "[FAILED]" -f Red
        Write-Host ""
        Write-Host $tab $output -f Red
        Write-Host ""
        break
    }
    Write-Host "[OK]" -f Green
}

function build-solution-projects([string]$vsWhereToolExe, [string]$solutionFilePath, [string]$builConfigName) {
    $msBuildToolExe = get-msbuild-executable-path -vsWhereToolExe $vsWhereToolExe
    Write-Host "Building solution projects................" -NoNewline
    $output = & cmd /c $msBuildToolExe /nologo /verbosity:quiet /p:Configuration=$builConfigName $solutionFilePath 2`>`&1
    if (-Not $?)
    {
        Write-Host "[FAILED]" -f Red
        Write-Host ""
        Write-Host $tab $output -f Red
        Write-Host ""
        break
    }
    Write-Host "[OK]" -f Green
}

function run-data-migrations([string]$solutionFilePath, [string]$builConfigName) {
    $msBuildToolExe = get-msbuild-executable-path
    Write-Host "Building solution projects................" -NoNewline
    $output = & cmd /c $msBuildToolExe /nologo /verbosity:quiet /p:Configuration=$builConfigName $solutionFilePath 2`>`&1
    if (-Not $?)
    {
        Write-Host "[FAILED]" -f Red
        Write-Host ""
        Write-Host $tab $output -f Red
        Write-Host ""
        break
    }
    Write-Host "[OK]" -f Green
}

function run-data-migrations([string]$migratorToolExe) {
    Write-Host "Running data migrations..................." -NoNewline
    $output = & cmd /c $migratorToolExe 2`>`&1
    if (-Not $?)
    {
        Write-Host "[FAILED]" -f Red
        Write-Host ""
        Write-Host $tab $output -f Red
        Write-Host ""
        break
    }
    Write-Host "[OK]" -f Green
}