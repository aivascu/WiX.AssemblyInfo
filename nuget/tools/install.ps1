# Runs every time a package is installed in a project

param($installPath, $toolsPath, $package, $project)

# $installPath is the path to the folder where the package is installed.
# $toolsPath is the path to the tools directory in the folder where the package is installed.
# $package is a reference to the package object.
# $project is a reference to the project the package was installed to.

# Determine if the target project is a WiX project
if([string]::Compare($project.Project.ProjectType, "WiX", $true) -eq 0)
{
    $fullPath = "{0}\lib\{1}" -f $installPath, "WixAssemblyInfoExt.dll"
    $references = $DTE.Solution.Projects|Select-Object -Expand ProjectItems | Where-Object{$_.Name -eq 'References'}
    $references.ProjectItems.AddFromFile($fullPath)
    Write-Host $fullPath
}