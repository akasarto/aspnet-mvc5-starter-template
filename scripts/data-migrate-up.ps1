
param (
    [int]$requiredPowerShellMajorVersion = 5, # Version shipped with Windows 10
    [int]$requiredNetFrameworkReleaseVersion = 461308, # Included in Visual Studio 2017 versions
    [string]$migratorToolExePathTemplate = "sources\platform-solutions\Data.Tools.Migrator\bin\{0}\migrator.exe", # Relative to root
    [string]$solutionFilePath = "sources\platform-solutions\starterTemplateMVC5.sln", # Relative to root
    [string]$builConfigName = "Debug" # Debug or Release
)

$rootFolder = ((get-item (split-path $MyInvocation.MyCommand.Definition)).Parent).FullName

$sharedFile = Join-Path $rootFolder "\scripts\_shared.ps1"
$vsWhereToolExe = Join-Path $rootFolder "tools\vswhere.exe"
$nugetToolExe = Join-Path $rootFolder "tools\nuget.exe"
$migratorToolExe = Join-Path $rootFolder ($migratorToolExePathTemplate -f $builConfigName)
$solutionFilePath = Join-Path $rootFolder $solutionFilePath

. $sharedFile

# Clear screen
Clear-Host

# Add empty line
Write-Host ""

# Check current powershell version
check-required-powershell-version -requiredNetFrameworkReleaseVersion $requiredPowerShellMajorVersion

# Check current .NET Framework SDK version
check-required-net-framework-version -requiredPowerShellMajorVersion $requiredNetFrameworkReleaseVersion

# Check MSBuild.exe tool
check-msbuild-tool-executable -vsWhereToolExe $vsWhereToolExe

# Restore solution Nuget packages
restore-nuget-packages-for-solution -nugetToolExe $nugetToolExe -solutionFilePath $solutionFilePath

# Building solution projects
build-solution-projects -vsWhereToolExe $vsWhereToolExe -solutionFilePath $solutionFilePath -builConfigName $builConfigName

# Run data migrations
run-data-migrations -migratorToolExe $migratorToolExe

Write-Host ""
