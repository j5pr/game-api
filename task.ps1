param(
  [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$false)]
  [System.String]
  $Script
)

$n = [Environment]::NewLine
$head = '----------------'

function Build-Section([System.String] $Name) {
  Write-Output "$head BUILDING $head"
}

function Complete-Section() {
  Write-Output "$($n)Complete!$n"
}

& ".\scripts\$Script.ps1"

Write-Output "$head COMPLETE $head$n"