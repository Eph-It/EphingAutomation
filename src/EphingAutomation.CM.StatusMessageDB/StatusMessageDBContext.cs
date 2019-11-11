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
                        ID              integer primary key AUTOINCREMENT,
                        MESSAGE_ID      integer,
                        RECORDED        datetime,
                        
                    )
                ");
            }
        }
    }
}
