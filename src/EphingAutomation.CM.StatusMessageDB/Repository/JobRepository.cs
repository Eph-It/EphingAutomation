using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Dapper;
using EphingAutomation.CM.StatusMessageDB.Model;

namespace EphingAutomation.CM.StatusMessageDB.Repository
{
    public class JobRepository : IJobRepository
    {
        StatusMessageDBContext _db;
        public JobRepository(StatusMessageDBContext db)
        {
            _db = db;
        }

        /// <summary>
        /// returns all active jobs
        /// </summary>
        /// <returns></returns>
        public List<Job> GetActiveJobs()
        {
            using(var dbConnection = _db.SqlConnection())
            {
                return dbConnection.Query<Job>(@"
                    SELECT * FROM Job WHERE Finished IS NULL
                ").ToList();
            }
        }
        /// <summary>
        /// Create a new job with the current process id
        /// </summary>
        public void New()
        {
            Process currentProcess = Process.GetCurrentProcess();
            using(var dbConnection = _db.SqlConnection())
            {
                Job newJob = new Job()
                {
                    Started = DateTime.UtcNow,
                    ProcessId = currentProcess.Id
                };
                dbConnection.Query(@"
                    INSERT INTO Job ( Started, ProcessId ) VALUES ( @Started, @ProcessId )
                ", newJob);
            }
        }
        /// <summary>
        /// Mark the current job with the current process id as over and any jobs that are no longer running also as over
        /// </summary>
        public void EndJob()
        {
            Process currentProcess = Process.GetCurrentProcess();
            List<int> runningProcesses = Process.GetProcessesByName(currentProcess.ProcessName).Select(p => p.Id).ToList();
            var activeJobs = GetActiveJobs();
            using(var dbConnection = _db.SqlConnection())
            {
                foreach (var activeJob in activeJobs)
                {
                    if(activeJob.ProcessId == currentProcess.Id || !runningProcesses.Contains(activeJob.ProcessId))
                    {
                        activeJob.Finished = DateTime.UtcNow;
                        dbConnection.Query(@"
                            UPDATE Job
                            SET Finsihed = @Finished
                            WHERE Id = @Id
                        ",activeJob);
                    }
                }
            }
        }
    }
}
