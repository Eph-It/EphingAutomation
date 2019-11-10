$ServiceName = 'EA.CM.StatusMessageProcessorService'
Get-Service -Name $ServiceName | Stop-Service -Force -ErrorAction SilentlyContinue
Start-Sleep 10
#Stop-Service -Name $ServiceName -ErrorAction SilentlyContinue -Force