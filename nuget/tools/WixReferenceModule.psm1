function Add-WixReference {
    param(
    [Parameter(Mandatory = $true)]
    [string]$ProjPath,
    [Parameter(Mandatory = $true)]
    [string]$BinPath)
    
    if (-not (Test-Path $ProjPath)) 
    {
        throw [System.IO.FileNotFoundException] ("Project path not found: {0}" -f $ProjPath)
    }
    else
    {
        $ProjDir = Split-Path $ProjPath
        Set-Location $ProjDir
    }
    
    if (-not (Test-Path $BinPath)) 
    {
        throw [System.IO.FileNotFoundException] ("Reference path not found: {0}" -f $BinPath)
    }
    
    $PackName = [System.IO.Path]::GetFileNameWithoutExtension($BinPath)
    
    $projectFile = [System.Xml.XmlDocument](Get-Content $ProjPath)
    $namespace = $projectFile.DocumentElement.NamespaceURI
    
    $relativePath = Resolve-Path -Relative $BinPath
    
    $extensions = $projectFile.Project.ItemGroup | Select-Object -Expand ChildNodes | Where-Object { $_.LocalName -eq "WixExtension" }
    
    if($extensions -ne $null)
    {
        $extensionNode = $extensions | Where-Object { $_.Name -eq $PackName }
        if($extensionNode -eq $null)
        {
            $NewReferenceNode = $projectFile.CreateElement("WixExtension", $namespace)
            $NewReferenceNode.SetAttribute("Include", $PackName)
    
            $NewHintPath = $projectFile.CreateElement("HintPath", $namespace)
            $NewHintPath.InnerText = $relativePath
            $NewReferenceNode.AppendChild($NewHintPath)
    
            $NewReferenceName = $projectFile.CreateElement("Name", $namespace)
            $NewReferenceName.InnerText = $PackName
            $NewReferenceNode.AppendChild($NewReferenceName)
            
            $parentItemGroup = $extensions.ParentNode
            $parentItemGroup.AppendChild($NewReferenceNode)
        }
        else
        {
            $parentItemGroup = $extensionNode.ParentNode
            $parentItemGroup.RemoveChild($extensionNode)
    
            $parentItemGroupClone = $parentItemGroup.Clone()
            $projectFile.Project.RemoveChild($parentItemGroup)
    
            $NewReferenceNode = $projectFile.CreateElement("WixExtension", $namespace)
            $NewReferenceNode.SetAttribute("Include", $PackName)
    
            $NewHintPath = $projectFile.CreateElement("HintPath", $namespace)
            $NewHintPath.InnerText = $relativePath
            $NewReferenceNode.AppendChild($NewHintPath)
    
            $NewReferenceName = $projectFile.CreateElement("Name", $namespace)
            $NewReferenceName.InnerText = $PackName
            $NewReferenceNode.AppendChild($NewReferenceName)
    
            $parentItemGroupClone.AppendChild($NewReferenceNode)
    
            $projectFile.Project.ItemGroup | Select-Object -Last 1
            $lastItemGroup = $projectFile.Project.ItemGroup | Select-Object -Last 1
            $projectFile.Project.InsertAfter($parentItemGroupClone, $lastItemGroup)
        }
    }
    else
    {
        $NewItemGroup = $projectFile.CreateElement("ItemGroup", $namespace)
    
        $NewReferenceNode = $projectFile.CreateElement("WixExtension", $namespace)
        $NewReferenceNode.SetAttribute("Include", $PackName)
    
        $NewHintPath = $projectFile.CreateElement("HintPath", $namespace)
        $NewHintPath.InnerText = $relativePath
        $NewReferenceNode.AppendChild($NewHintPath)
    
        $NewReferenceName = $projectFile.CreateElement("Name", $namespace)
        $NewReferenceName.InnerText = $PackName
        $NewReferenceNode.AppendChild($NewReferenceName)
    
        $NewItemGroup.AppendChild($NewReferenceNode)
    
        $projectFile.Project.ItemGroup | Select-Object -Last 1
        $lastItemGroup = $projectFile.Project.ItemGroup | Select-Object -Last 1
        $projectFile.Project.InsertAfter($NewItemGroup, $lastItemGroup)
    }
    $projectFile.Save($ProjPath)
}

Export-ModuleMember -function Add-WixReference