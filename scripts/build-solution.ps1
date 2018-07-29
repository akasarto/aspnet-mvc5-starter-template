. ".\_shared.ps1"

$msBuildToolExe = get-msbuild-executable-path

if (([string]::IsNullOrEmpty($msBuildToolExe) -eq $false) -and (Test-Path $msBuildToolExe)) {
    Write-Host ":)"
    break
}

Write-Host ":("
