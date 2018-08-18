Param (
    $InputFile,
    $OutputFile
)

$csv = Import-Csv $InputFile -Header "Start", "End", "Type", "Notes"
$res = @()
$res += "DateTime,Value"
foreach ($l in $csv) {
    $val = 1
    if ($l.Type -eq 'Break') {
        $val = 0
    }

    $res += "$($l.Start),2"
    $res += "$($l.Start),$val"
    $res += "$($l.End),$val"
    $res += "$($l.End),2"
}
$res | Out-File -FilePath $OutputFile