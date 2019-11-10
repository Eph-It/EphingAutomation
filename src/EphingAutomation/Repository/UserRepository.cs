using EphingAutomation.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace EphingAutomation.Repository
{
    public class UserRepository
    {
        private EphingAutomationDB _dbConnection;
        private const string sqlQuery = "Select * FROM v_User";
        public UserRepository(EphingAutomationDB dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public EAUser Get(int id)
        {
            const string userWhere = sqlQuery + " WHERE Id = @Id";
            return _dbConnection.DbConnection.Query<EAUser>(userWhere, new { Id = id }).FirstOrDefault();
        }
        public List<EAUser> Get()
        {
            return _dbConnection.DbConnection.Query<EAUser>(sqlQuery).ToList();
        }
        public EAUser Get(string userName, string domain)
        {
            const string userWhere = sqlQuery + " WHERE UserName = @UserName AND Domain = @Domain";
            return _dbConnection.DbConnection.Query<EAUser>(userWhere, new { UserName = userName, Domain = domain }).FirstOrDefault();
        }
        public EAUser New(EAUser user)
        {

            var us = new EAUser();
            return us;
        }
    }
}
