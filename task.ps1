param(
  [Parameter(Mandatory=$true, Position=0, ValueFromPipeline=$false)]
  [System.String]
  $Script
)

$t = "  "
$global:indent = 0

function Task {
  param (
    [parameter(Mandatory=$true)]
    [System.String] $Name,

    [parameter(Mandatory=$true)]
    [ScriptBlock] $Script,

    [Switch] $Unsafe
  )

  Out-Message "Task ${Name}:"
  $global:indent += 1

  try {
    &$Script

    Out-Message "Task Complete!"
  } catch {
    Out-Message "Task Failed! ($_)"

    if ($Unsafe) {
      $global:indent -= 1
      throw $_.Exception
    }
  }

  Out-Message
  $global:indent -= 1
}

function Section {
  param (
    [parameter(Mandatory=$true)]
    [System.String] $Name,

    [parameter(Mandatory=$true)]
    [ScriptBlock] $Script,

    [Switch] $Unsafe
  )
  Out-Message "Section ${Name}:"

  $global:indent += 1

  try {
    &$Script

    Out-Message "Complete!"
  } catch {
    Out-Message "Failed! ($_)"

    if ($Unsafe) {
      $global:indent -= 1
      throw $_.Exception
    }
  }

  Out-Message
  $global:indent -= 1
}

function Invoke-Task([parameter(Mandatory=$true)] [System.String] $Name) {
  &".\scripts\$Name.ps1"
}

function Out-Message([String] $Data) {
  Write-Output "$($t * $global:indent)$Data"
}

&".\scripts\$Script.ps1"
Write-Output "Done!"