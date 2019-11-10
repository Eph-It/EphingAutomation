$ServiceName = 'EA.CM.StatusMessageProcessorService'
$Service = Get-WmiObject -Query  "Select * FROM Win32_Service where Name like '$ServiceName'"

$BinaryPath = "$PSScriptRoot\EphingAutomation.CM.StatusMessageProcessorService\bin\debug\netcoreapp3.0\EphingAutomation.CM.StatusMessageProcessorService.exe"

if($Service -ne $null) {
    if(-not ( $Service.PathName -contains $BinaryPath )){
        Remove-Service -Name $ServiceName
        $Service = $null
    }
}

if($Service -eq $null){
    New-Service -Name 'EA.CM.StatusMessageProcessorService' -BinaryPathName $BinaryPath
}

