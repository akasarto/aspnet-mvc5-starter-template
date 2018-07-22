@REM Copyright (C) Thiago Alberto Schneider. All rights reserved.
@REM Licensed under the MIT license. See LICENSE.txt in the project root for license information.

@ECHO OFF

SETLOCAL ENABLEDELAYEDEXPANSION

@REM https://github.com/Microsoft/vswhere/wiki/Installing
@REM AS PER DOCS, THE 'VSWHERE.EXE' TOOL WILL EXIST IN THE FIXED LOCATION BELLOW
SET VSWHEREEXE="%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"

IF NOT EXIST %VSWHEREEXE% (
	GOTO :VSWHERENOTFOUND
)

ECHO [32mFound vswhere.exe tool. Locating MSBuild.exe...[0m

@REM FIND LATEST VISUAL STUDIO INSTALLATION FOLDER.
FOR /f "usebackq tokens=*" %%i IN (`call %VSWHEREEXE% -latest -products * -requires Microsoft.Component.MSBuild -property installationPath`) DO (
    SET VSINSTALLDIR=%%i
)

@REM GET THE 'MSBUILD.EXE' TOOL PATH.
SET MSBUILDEXEPATH=%VSINSTALLDIR%\MSBuild\15.0\Bin\MSBuild.exe
IF EXIST %MSBUILDEXEPATH% (
    GOTO :MSBUILDFOUND
)

GOTO :MSBUILDNOTFOUND

:VSWHERENOTFOUND
@REM NOTIFY USER ABOUT 'VSWHERE.EXE' TOOL
ECHO [31mCould not find vswhere.exe tool. Please make sure you have Visual Studio 2017(v15.2 or higher) installed.[0m
ECHO [31mFor more details and alternate install options, check the official docs at: https://github.com/Microsoft/vswhere.[0m
GOTO :EXIT

:MSBUILDNOTFOUND
@REM NOTIFY USER ABOUT 'MSBUILD.EXE' TOOL
ECHO [31mThe tool MsBuild.exe was not found. Please make sure you have Visual Studio 2017(v15.2 or higher) installed.[0m
ECHO [31mIf this is running on a build server, make sure you have the required build tools installed.[0m
GOTO :EXIT

:MSBUILDFOUND
@REM RESTORE NUGET PACKAGES, BUILD THE SOLUTION AND RUN MIGRATIONS.
ECHO [32mFound MSBuild.exe tool. Building data migrator tool...[0m
ECHO [37mRestoring nuget packages...[0m
CALL .\tools\nuget.exe restore .\sources\platform-solutions\starterTemplateMVC5.sln -verbosity quiet
ECHO [37mBuilding the solution...[0m
CALL "%MSBUILDEXEPATH%" /nologo /verbosity:quiet /p:Configuration=Debug .\sources\platform-solutions\starterTemplateMVC5.sln

:EXIT
