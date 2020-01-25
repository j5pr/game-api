Write-Output "$head CLEANING $head"

try {
  Remove-Item -Path '.\bin\' -Recurse -ErrorAction Stop | Out-Null
  Write-Output "$($n)Complete!$n"
} catch {
  Write-Output "$($n)Complete! (No Ouptput Directory)$n"
}