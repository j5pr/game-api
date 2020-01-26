Task "Clean" {
  try {
    Remove-Item -Path '.\bin\' -Recurse -ErrorAction Stop | Out-Null
  } catch {
    throw "No Ouptput Directory"
  }
}