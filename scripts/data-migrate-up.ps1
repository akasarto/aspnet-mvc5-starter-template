
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

Invoke-Expression ".\base-check.ps1 -requiredPowerShellMajorVersion $requiredPowerShellMajorVersion -requiredNetFrameworkReleaseVersion $requiredNetFrameworkReleaseVersion"

Invoke-Expression ".\build-solution.ps1"

