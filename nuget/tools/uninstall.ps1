# Runs every time a package is uninstalled

param($installPath, $toolsPath, $package, $project)

# $installPath is the path to the folder where the package is installed.
# $toolsPath is the path to the tools directory in the folder where the package is installed.
# $package is a reference to the package object.
# $project is a reference to the project the package was installed to.
if([string]::Compare($project.Project.ProjectType, "WiX", $true) -eq 0)
{
    $DTE.Solution.Projects|Select-Object -Expand ProjectItems|Where-Object{$_.Name -eq 'References'} |Select-Object -Expand ProjectItems|Where-Object{$_.Name -eq 'WixAssemblyInfoExt'}|ForEach-Object {$_.Remove()}
}