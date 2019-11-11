$File = "$PSScriptRoot\bin\Debug\netcoreapp3.0\EA.CM.StatusMessageReceiver.exe"

$count = 0
Measure-Command {
    while($count -lt 100) {
        & cmd /c $File "arg1" "arg$count"
    }
}