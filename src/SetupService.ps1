$ServiceName = 'EA.CM.StatusMessageProcessorService'
$Service = Get-CimInstance -Query  "Select * FROM Win32_Service where Name like '$ServiceName'"

$BinaryPath = "$PSScriptRoot\EphingAutomation.CM.StatusMessageProcessorService\bin\debug\netcoreapp3.0\EphingAutomation.CM.StatusMessageProcessorService.exe"

if($Service -ne $null) {
    if(-not ( $Service.PathName -contains $BinaryPath )){
        Remove-Service -Name $ServiceName -Confirm:$false
        $Service = $null
    }
}

if($Service -eq $null){
    New-Service -Name 'EA.CM.StatusMessageProcessorService' -BinaryPathName $BinaryPath
}

