using Dapper;
using EphingAutomation.CM.StatusMessageDB.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EphingAutomation.CM.StatusMessageDB.Repository
{
    public class StatusMessageRepository
    {
        StatusMessageDBContext _db;
        public StatusMessageRepository(StatusMessageDBContext db)
        {
            _db = db;
        }
        public void New(StatusMessage MessageToCreate)
        {
            using (var dbConnection = _db.SqlConnection())
            {
                dbConnection.Query(@"
                    INSERT INTO StatusMessage (
                        MessageId,
                        Recorded,
                        InsString1,
                        InsString2,
                        InsString3,
                        InsString4,
                        InsString5,
                        InsString6,
                        InsString7,
                        InsString8,
                        InsString9,
                        InsString10
                    ) 
                    VALUES ( 
                        @MessageId,
                        @Recorded,
                        @InsString1,
                        @InsString2,
                        @InsString3,
                        @InsString4,
                        @InsString5,
                        @InsString6,
                        @InsString7,
                        @InsString8,
                        @InsString9,
                        @InsString10
                    )
                ", MessageToCreate);
            }
        }
    }
}
