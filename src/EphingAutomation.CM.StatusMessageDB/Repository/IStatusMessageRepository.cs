using EphingAutomation.CM.StatusMessageDB.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EphingAutomation.CM.StatusMessageDB.Repository
{
    public interface IStatusMessageRepository
    {
        void New(StatusMessage MessageToCreate);
        StatusMessage GetNext();
        void Finish(StatusMessage MessageToFinish);
    }
}
