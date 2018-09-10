# For more details checking last exit code (e.g.: 2`>`&1 and $?), please check the docs at:
# https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_automatic_variables?view=powershell-5.0

# \t char
$tab = [char]9

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

# Gets the current powershell major version
# For more details regarding powershell installation, check the official docs at:
# https://docs.microsoft.com/en-us/powershell/scripting/setup/installing-windows-powershell
function get_current_powershell_major_version() {
    $currentMajorVersion = $PSVersionTable.PSVersion.Major
    return $currentMajorVersion
}

# Check the current powershell required major version
# For more details regarding powershell installation, check the official docs at:
# https://docs.microsoft.com/en-us/powershell/scripting/setup/installing-windows-powershell
function is_required_powershell_major_version_or_later($currentMajorVersion, $requiredMajorVersion) {
    if($currentMajorVersion -ge $requiredMajorVersion) {
        return $true
    }
    return $false
}

# Get information about installed .net framework sdks.
# https://www.microsoft.com/net/download/visual-studio-sdks
function get_installed_net_framework_sdks()
{
    $existingSdks =
        Get-ChildItem "HKLM:SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\" `
        | Get-ItemProperty
    $existingSdks
}

# Check if the current system has the required .net framework sdk intalled based on the release version value
# https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed
function is_required_net_framework_sdk_or_greater_installed($installedNetFrameworkSDKs, $requiredReleaseVersion)
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
function get_msbuild_executable_path([string]$vsWhereToolExe)
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
function check_required_powershell_version([int]$requiredPowerShellMajorVersion) {
    Write-Host "Checking PowerShell version............... " -NoNewline
    $currentPowershellMajorVersion = get_current_powershell_major_version
    $powerShellVersionOk = is_required_powershell_major_version_or_later -currentMajorVersion $currentPowershellMajorVersion -requiredMajorVersion $requiredPowerShellMajorVersion
    if ($powerShellVersionOk -eq $false) {
        Write-Host "[FAILED]" -f Red
        Write-Host ""
        Write-Host $tab "Your current PowerShell version is $currentPowershellMajorVersion." -f Yellow
        Write-Host $tab "The required PowerShell version is $requiredPowerShellMajorVersion or greater." -f Red
        Write-Host $tab "For more details on how to update you current PowerShell installation, check the docs at:"
        Write-Host $tab "https://docs.microsoft.com/en-us/powershell/scripting/setup/installing-windows-powershell" -f Cyan
        break
    }
    Write-Host "[OK]" -f Green
}

# Check .NET Framework SDK release version
# The value to check is the corresponding release number value
# .NET Framework 4.7.1 (Included in Visual Studio 2017 Community or higher)
# For more details regarding other release versions, please check the official docs at:
# https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed
function check_required_net_framework_version([int]$requiredNetFrameworkReleaseVersion) {
    Write-Host "Checking .NET Framework SDK version....... " -NoNewline
    $installedNetFrameworkSDKs = get_installed_net_framework_sdks
    $netFrameworkVersionOk = is_required_net_framework_sdk_or_greater_installed -installedNetFrameworkSDKs $installedNetFrameworkSDKs -requiredReleaseVersion $requiredNetFrameworkReleaseVersion
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
        break
    }
    Write-Host "[OK]" -f Green
}

function check_msbuild_tool_executable([string]$vsWhereToolExe) {
    Write-Host "Checking MSBuild.exe tool installation.... " -NoNewline
    $msBuildToolExe = get_msbuild_executable_path -vsWhereToolExe $vsWhereToolExe
    if (($null -eq $msBuildToolExe) -or (-Not (Test-Path $msBuildToolExe))) {
        Write-Host "[FAILED]" -f Red
        Write-Host ""
        Write-Host $tab "The tool MsBuild.exe was not found. Please make sure you have any Visual Studio 2017(v15.2 or higher) installed." -f Red
        Write-Host $tab "If this is running on a build server, make sure you have either Visual Studio or visual studio build tool installed."
        Write-Host $tab "You can find those under 'Visual Studio 2017' and 'Tools for Visual Studio 2017' at:"
        Write-Host $tab "https://visualstudio.microsoft.com/downloads/" -f Cyan
        break
    }
    Write-Host "[OK]" -f Green
}

# Checks if the given config file exists
function check_main_config_file([string]$configFilePath) {
    Write-Host "Checking main config file................. " -NoNewline
    if (-Not (Test-Path $configFilePath)) {
        Write-Host "[FAILED]" -f Red
        Write-Host ""
        Write-Host $tab "In order to run the database migrations, the connection info must be extracted from the configuration file." -f Red
        Write-Host $tab "Please make sure that the provided path points to the correct app or web config file." -f Red
        break
    }
    Write-Host "[OK]" -f Green
}

# Attempt to build all projects in the given solution file
function build_solution_projects([string]$vsWhereToolExe, [string]$solutionFilePath, [string]$builConfigName) {
    $msBuildToolExe = get_msbuild_executable_path -vsWhereToolExe $vsWhereToolExe
    Write-Host ""
    Write-Host "### Building solution projects" -f Cyan
    Write-Host ""
    & cmd /c $msBuildToolExe /nologo /restore /p:Configuration=$builConfigName $solutionFilePath
}
