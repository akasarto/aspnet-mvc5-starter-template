:: This script will build the application and execute the

@ECHO OFF

SETLOCAL ENABLEDELAYEDEXPANSION

:: https://github.com/Microsoft/vswhere/wiki/Installing
:: As per docs, the vswhere.exe tool will exist in the fixed location bellow
SET VSWHEREEXE="%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"

IF NOT EXIST %VSWHEREEXE% (
	GOTO :VSWHERENOTFOUND
)

ECHO [32mFound vswhere.exe tool. Locating MSBuild.exe...[0m

FOR /f "usebackq tokens=*" %%i IN (`call %VSWHEREEXE% -latest -products * -requires Microsoft.Component.MSBuild -property installationPath`) DO (
    SET VSINSTALLDIR=%%i
)

SET MSBUILDEXEPATH=%VSINSTALLDIR%\MSBuild\15.0\Bin\MSBuild.exe
IF EXIST %MSBUILDEXEPATH% (
    GOTO :MSBUILDFOUND
)

GOTO :MSBUILDNOTFOUND

:VSWHERENOTFOUND
ECHO [31mCould not find vswhere.exe tool. Please make sure you have Visual Studio 2017(v15.2 or higher) installed.[0m
ECHO [31mFor more details and alternate install options, check the official docs at: https://github.com/Microsoft/vswhere.[0m
GOTO :EXIT

:MSBUILDNOTFOUND
ECHO [31mThe tool MsBuild.exe was not found. Please make sure you have Visual Studio 2017(v15.2 or higher) installed.[0m
GOTO :EXIT

:MSBUILDFOUND
ECHO [32mFound MSBuild.exe tool. Building data migrator tool...[0m
ECHO [37m%MSBUILDEXEPATH%[0m

CALL "%MSBUILDEXEPATH%" /t:restore /p:Configuration=Debug .\sources\platform-solutions\starterTemplateMVC5.sln

:EXIT
