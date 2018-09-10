
param (
    [int]$requiredPowerShellMajorVersion, # Version shipped with Windows 10
    [int]$requiredNetFrameworkReleaseVersion, # Included in Visual Studio 2017 versions (.Net Framwork 4.7.1)
    [string]$mainWebProjectFolder, # Relative to root (or deploy full path)
    [string]$solutionFilePath, # Relative to root
    [string]$builConfigName, # Debug/Release
    [string]$conStringName,
    [string]$databaseType
)

$rootFolder = ((get-item (split-path $MyInvocation.MyCommand.Definition)).Parent).FullName

$sharedFile = Join-Path $rootFolder "\scripts\_shared.ps1"

$vsWhereToolExe = Join-Path $rootFolder "tools\vswhere.exe"
$migratorToolExe = Join-Path $rootFolder "$mainWebProjectFolder\bin\data.tools.migrator.exe"
$configFilePath = Join-Path $rootFolder "$mainWebProjectFolder\web.config"
$solutionFilePath = Join-Path $rootFolder $solutionFilePath

. $sharedFile

# Add empty line
Write-Host ""

# Check current powershell version
check_required_powershell_version -requiredNetFrameworkReleaseVersion $requiredPowerShellMajorVersion

# Check current .NET Framework SDK version
check_required_net_framework_version -requiredPowerShellMajorVersion $requiredNetFrameworkReleaseVersion

# Check MSBuild.exe tool
check_msbuild_tool_executable -vsWhereToolExe $vsWhereToolExe

# Check main config file
check_main_config_file -configFilePath $configFilePath

# Building solution projects
build_solution_projects -vsWhereToolExe $vsWhereToolExe -solutionFilePath $solutionFilePath -builConfigName $builConfigName

# Run data migrations
#########################################################################################

Write-Host ""

$xml = [Xml](Get-Content $configFilePath)
$connectionNode = $xml.SelectSingleNode("configuration/connectionStrings/add[@name='$conStringName']")

if($connectionNode) {
    $provider = $connectionNode.providerName
    $connectionString = $connectionNode.connectionString
    & cmd /c $migratorToolExe "[$provider][$connectionString]"
} else{
    Write-Host "Unable to locate connection string '$conStringName'" -f Red
}

Write-Host ""
