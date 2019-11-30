using EphingAutomation.CM.StatusMessageDB.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EphingAutomation.CM.StatusMessageProcessor.Repository
{
    public class StatusMessageJobRepository
    {
        StatusMessage _sm;
        public StatusMessageJobRepository(StatusMessage sm)
        {
            _sm = sm;
        }
        public void Start()
        {

        }
    }
}
