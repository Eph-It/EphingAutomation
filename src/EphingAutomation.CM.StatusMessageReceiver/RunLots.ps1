$File = "$PSScriptRoot\bin\Release\netcoreapp3.0\win7-x64\EA.CM.StatusMessageReceiver.exe"
Push-Location -Path $PSScriptRoot

dotnet publish --configuration Release --self-contained

$count = 0
Measure-Command {
    while($count -lt 100) {
        $count++
        & cmd /c $File --MessageId 55 --InsString1 "test1"
    }
}
Pop-Location