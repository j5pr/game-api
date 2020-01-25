$projects = @('GameAPI', 'GameAPI.Async', 'GameAPI.Editor', 'GameAPI.Tasks', 'GameAPI.World')

# Clean output dir
.\scripts\clean.ps1

# Build .NET assembly
Write-Output "$head BUILDING $head"
  dotnet build -c Release
Write-Output "$($n)Complete!$n"

# Create a DLL dir
Write-Output "$head PREPARING $head"
  New-Item -ItemType directory -Path '.\bin\dll' | Out-Null
Write-Output "$($n)Complete!$n"

# Collect DLLs
Write-Output "$head COLLECTING $head"
  foreach ($project in $projects)
  {
    try
    {
      $dest = Join-Path (Resolve-Path .) "bin\dll\$project.dll"

      Copy-Item -Path ".\bin\$project\netstandard2.1\$project.dll" -Destination $dest -ErrorAction Stop | Out-Null
      Write-Output "  $project -> $dest"
    }
    catch
    {
      Write-Output "  $project -> (Failed)"
    }
  }
Write-Output "$($n)Complete!$n"