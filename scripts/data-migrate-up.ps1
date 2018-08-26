
param (
    [int]$requiredPowerShellMajorVersion = 5, # Version shipped with Windows 10
    [int]$requiredNetFrameworkReleaseVersion = 461308, # Included in Visual Studio 2017 versions (.Net Framwork 4.7.1)
    [string]$migratorToolExePathTemplate = "sources\platform-solutions\Data.Tools.Migrator\bin\{0}\data.tools.migrator.exe", # Relative to root
    [string]$mainConfigPathTemplate = "sources\platform-solutions\App.UI.Mvc5\web.config", # Relative to root (or deploy full path)
    [string]$solutionFilePath = "sources\platform-solutions\starterTemplateMVC5.sln", # Relative to root
    [string]$builConfigName = "Debug" # Debug or Release
)

$rootFolder = ((get-item (split-path $MyInvocation.MyCommand.Definition)).Parent).FullName

$sharedFile = Join-Path $rootFolder "\scripts\_shared.ps1"
$vsWhereToolExe = Join-Path $rootFolder "tools\vswhere.exe"
$nugetToolExe = Join-Path $rootFolder "tools\nuget.exe"
$migratorToolExe = Join-Path $rootFolder ($migratorToolExePathTemplate -f $builConfigName)
$configFilePath = Join-Path $rootFolder $mainConfigPathTemplate
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

# Restore solution nuget packages
#restore_nuget_packages_for_solution -nugetToolExe $nugetToolExe -solutionFilePath $solutionFilePath

# Building solution projects
build_solution_projects -vsWhereToolExe $vsWhereToolExe -solutionFilePath $solutionFilePath -builConfigName $builConfigName

# Check main config file
check_main_config_file -configFilePath $configFilePath

# Run data migrations
run_data_migrations -migratorToolExe $migratorToolExe -connectionStringName ""

Write-Host ""
