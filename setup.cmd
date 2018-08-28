@REM Copyright (C) Thiago Alberto Schneider. All rights reserved.
@REM Licensed under the MIT license. See LICENSE.txt in the project root for license information.

@ECHO OFF

SETLOCAL ENABLEDELAYEDEXPANSION

powershell ^
    "& .\scripts\data-migrate-up.ps1"^
    -requiredPowerShellMajorVersion 5^
    -requiredNetFrameworkReleaseVersion 461308^
    -mainWebProjectFolder "sources\platform-solutions\App.UI.Mvc5"^
    -solutionFilePath "sources\platform-solutions\starterTemplateMVC5.sln"^
    -builConfigName "Debug"^
    -conStringName "SqlServerConnection"^
    -databaseType "SqlServer"
