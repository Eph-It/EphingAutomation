using Microsoft.AspNetCore.Hosting;
using System;

namespace EphingAutomation.Logging
{
    public class EALogging
    {
        private IHostingEnvironment _environment;
        public EALogging(IHostingEnvironment environment)
        {
            _environment = environment;
        }
    }
}
