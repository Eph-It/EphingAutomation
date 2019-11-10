using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EphingAutomation.Repository
{
    public class EphingAutomationDB
    {
        private string _connectionString;
        public EphingAutomationDB(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection DbConnection
        {
            get { return new SqlConnection(_connectionString); }
        }
    }
}
