$File = "$PSScriptRoot\bin\Debug\netcoreapp3.0\EA.CM.StatusMessageReceiver.exe"

$count = 0
Measure-Command {
    while($count -lt 10) {
        $count++
        & cmd /c $File "arg$count" "this is a test"
    }
}