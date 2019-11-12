using System;
using System.IO;
using System.Data.SQLite;
using Dapper;

namespace EphingAutomation.CM.StatusMessageDB
{
    public class StatusMessageDBContext
    {
        private string _dbPath;
        public StatusMessageDBContext(string dbPath)
        {
            if (dbPath.Contains(@"/"))
            {
                _dbPath = dbPath;
            }
            else
            {
                _dbPath = Path.Combine(Directory.GetCurrentDirectory(), dbPath);
            }
            
        }

        public SQLiteConnection SqlConnection(bool skipFileCheck = false)
        {
            if (!File.Exists(_dbPath) && !skipFileCheck)
            {
                CreateDB(_dbPath);
            }
            return new SQLiteConnection("Data Source=" + _dbPath);
        }

        private void CreateDB(string dbPath)
        {
            using (var connection = SqlConnection(true))
            {
                connection.Open();
                connection.Execute(@"
                    CREATE TABLE StatusMessage
                    (
                        Id              integer primary key AUTOINCREMENT,
                        MessageId       integer,
                        Recorded        datetime,
                        InsString1      nvarchar(255),
                        InsString2      nvarchar(255),
                        InsString3      nvarchar(255),
                        InsString4      nvarchar(255),
                        InsString5      nvarchar(255),
                        InsString6      nvarchar(255),
                        InsString7      nvarchar(255),
                        InsString8      nvarchar(255),
                        InsString9      nvarchar(255),
                        InsString10     nvarchar(255),
                        Started         datetime,
                        Finished        datetime
                    )
                ");
                connection.Execute(@"
                    CREATE TABLE Job
                    (
                        Id                  integer primary key AUTOINCREMENT,
                        Started             datetime,
                        Finished            datetime,
                        ProcessId           int
                    )
                ");
            }
        }
    }
}
