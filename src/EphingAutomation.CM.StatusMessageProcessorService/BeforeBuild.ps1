$ServiceName = 'EA.CM.StatusMessageProcessorService'
Get-Service -Name $ServiceName | Stop-Service -Force -ErrorAction SilentlyContinue
Start-Sleep 5
#Stop-Service -Name $ServiceName -ErrorAction SilentlyContinue -Force