Param (
    $InputFile
)

$csv = Import-Csv $InputFile -Header "Start", "End", "Type", "Notes"

$total = 0
$sum = [TimeSpan]::FromSeconds(0)

foreach ($l in $csv) {
    if ($l.Type -eq 'Break') {
        continue
    }

    $start = Get-Date $l.Start
    $end = Get-Date $l.End

    $total++
    $sum += ($end - $start)
}

Write-Output "Number of interruptions: $total"
Write-Output "Total interruption time: $($sum.Hours) hours, $($sum.Minutes) minutes, $($sum.Seconds) seconds"