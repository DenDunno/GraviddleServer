$compositionRootProj = "CompositionRoot/CompositionRoot.csproj"
$graviddleServerProj = "GraviddleServer/GraviddleServer.csproj"
$compositionRootBin = "CompositionRoot/bin/Release"
$graviddleServerBin = "GraviddleServer/bin"
$webConfigPath = "GraviddleServer/bin/Release/net7.0/publish/web.config"
$publishFolder = "GraviddleServer/bin/Release/net7.0/publish"
$destinationZip = "publish.zip"

# 1. Publish
dotnet publish $compositionRootProj -c Release
dotnet publish $graviddleServerProj -c Release

# 2. Transfer files from CompositionRoot to ASP.NET publish
Copy-Item -Path $compositionRootBin -Destination $graviddleServerBin -Recurse -Force -ErrorAction SilentlyContinue

# 3. Adjust web.config
(Get-Content -Path $webConfigPath) -replace 'GraviddleServer.dll', 'CompositionRoot.dll' | Set-Content -Path $webConfigPath

# 4. Zip
if (Test-Path $destinationZip) 
{
    Remove-Item -Force $destinationZip
}

$items = Get-ChildItem -Path $publishFolder
Compress-Archive -Path $items.FullName -DestinationPath $destinationZip -Update

# 5. Open directory 
start .

# 6. Wait for key press to exit
Write-Host "Press any key to exit..."
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')
