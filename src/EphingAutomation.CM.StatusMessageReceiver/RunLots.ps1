$File = "$PSScriptRoot\bin\Debug\netcoreapp3.0\EA.CM.StatusMessageReceiver.exe"

$count = 0
Measure-Command {
    while($count -lt 100) {
        $count++
        Start-Process -FilePath $File -ArgumentList @("arg$count", "this is a test")
    }
}