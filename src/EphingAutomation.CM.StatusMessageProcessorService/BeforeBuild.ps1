$ServiceName = 'EA.CM.StatusMessageProcessorService'

Stop-Service -Name $ServiceName -ErrorAction SilentlyContinue -Force