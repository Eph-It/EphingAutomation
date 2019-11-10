$ServiceName = 'EA.CM.StatusMessageProcessorService'
$Service = Get-CimInstance -Query  "Select * FROM Win32_Service where Name like '$ServiceName'"
#C:\Users\Ryan2\source\repos\EphingAutomation\src\EphingAutomation.CM.StatusMessageProcessorService\bin\debug\netcoreapp3.0\EphingAutomation.CM.StatusMessageProcessorService.exe
$BinaryPath = "$PSScriptRoot\EphingAutomation.CM.StatusMessageProcessorService\bin\debug\netcoreapp3.0\win7-x64\EphingAutomation.CM.StatusMessageProcessorService.exe"

if($Service -ne $null) {
    if(-not ( $Service.PathName -contains $BinaryPath )){
        Remove-Service -Name $ServiceName -Confirm:$false
        & cmd /c sc delete $ServiceName
        $Service = $null
    }
}

if($Service -eq $null){
    
    New-Service -Name 'EA.CM.StatusMessageProcessorService' -BinaryPathName $BinaryPath
    cmd /c subinacl.exe /service $ServiceName "/grant=$($env:COMPUTERNAME)\$($env:USERNAME)=PTO"
}

