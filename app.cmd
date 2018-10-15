@REM Copyright (C) Sarto Research. All rights reserved.
@REM Licensed under the MIT license. See LICENSE.txt in the project root for license information.

@ECHO OFF

SETLOCAL ENABLEDELAYEDEXPANSION

IF "%~1"=="" GOTO HELP
IF "%~1"=="install" GOTO MIGRATEUP
IF "%~1"=="migrate" GOTO MIGRATE

GOTO HELP

:MIGRATE

    IF "%~2"=="" GOTO MIGRATEUPHELP
    IF "%~2"=="down" GOTO MIGRATEDOWN
    IF "%~2"=="up" GOTO MIGRATEUP

GOTO HELP

:HELP

    ECHO.
    ECHO Use:
    ECHO   app install
    ECHO   app migrate up
    ECHO.
    GOTO EXIT

:MIGRATEUPHELP

    ECHO.
    ECHO Use:
    ECHO   app migrate up
    ECHO.
    GOTO EXIT

:MIGRATEDOWN

    ECHO.
    ECHO Migrate down not implemented!
    ECHO.
    GOTO EXIT

:MIGRATEUP

    powershell ^
        "& .\scripts\data-migrate-up.ps1"^
        -requiredPowerShellMajorVersion 5^
        -requiredNetFrameworkReleaseVersion 461308^
        -mainWebProjectFolder "sources\App.UI.Mvc5"^
        -solutionFilePath "sources\starterTemplateMVC5.sln"^
        -builConfigName "Debug"^
        -conStringName "SqlServerConnection"^
        -databaseType "SqlServer"
    GOTO EXIT

:EXIT