Task "Build" {
  $projects = @('GameAPI', 'GameAPI.Async', 'GameAPI.Editor', 'GameAPI.Tasks', 'GameAPI.World')

  Invoke-Task "clean"

  Section -Unsafe "Build Sources" {
    dotnet build -c Release | Out-Null
  }

  Section "Preparing" {
    New-Item -ItemType directory -Path '.\bin\dll' | Out-Null
  }

  Section -Unsafe "Collecting Binaries" {
    $failed = $false

    foreach ($project in $projects)
    {
      try
      {
        $dest = Join-Path (Resolve-Path .) "bin\dll\$project.dll"

        Copy-Item -Path ".\bin\$project\netstandard2.1\$project.dll" -Destination $dest -ErrorAction Stop | Out-Null
        Out-Message "$project -> $dest"
      }
      catch
      {
        Out-Message "$project -> (Failed)"
        $failed = $true
      }
    }

    if ($failed) {
      throw "Some binaries could not be collected!"
    }
  }
}