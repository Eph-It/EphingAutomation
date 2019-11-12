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
            if (!File.Exists(dbPath))
            {
                CreateDB(dbPath);
            }
        }

        public SQLiteConnection SqlConnection()
        {
            return new SQLiteConnection("Data Source=" + _dbPath);
        }

        private void CreateDB(string dbPath)
        {
            using (var connection = SqlConnection())
            {
                connection.Open();
                connection.Execute(@"
                    CREATE TABLE StatusMessages
                    (
                        Id              integer primary key AUTOINCREMENT,
                        MessageId       integer,
                        Recorded        datetime,
                        InsString1      nvarchar(max),
                        InsString2      nvarchar(max),
                        InsString3      nvarchar(max),
                        InsString4      nvarchar(max),
                        InsString5      nvarchar(max),
                        InsString6      nvarchar(max),
                        InsString7      nvarchar(max),
                        InsString8      nvarchar(max),
                        InsString9      nvarchar(max),
                        InsString10     nvarchar(max),
                        Started         datetime
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
