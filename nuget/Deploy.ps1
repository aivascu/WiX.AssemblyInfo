function Get-ScriptDirectory
{
  $Invocation = (Get-Variable MyInvocation -Scope 1).Value
  Split-Path $Invocation.MyCommand.Path
}

function Get-FileName {
    param(
        [string]$Path,
        [bool]$IncludeExt
    )
    return [System.IO.Path]::GetFileName($Path)
    if($IncludeExt -or ($IncludeExt -eq $null))
    {
        return [System.IO.Path]::GetFileName($Path)
    }
    else
    {
        return [System.IO.Path]::GetFileNameWithoutExtension($Path)
    }
}

$dir = Get-ScriptDirectory
$nugetDir = "D:\NuGet"

Invoke-Expression .\pack.ps1

$packages = Get-ChildItem $dir -Filter *.nupkg

foreach($pack in $packages) {
    $newfilepath = (Join-Path $nugetDir $pack )
    if((Test-Path -Path $newfilepath))
    {
        Remove-Item -Path $newfilepath
    }
    Move-Item -Path $pack -Destination $nugetDir
}