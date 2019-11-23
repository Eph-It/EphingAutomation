using Dapper;
using EphingAutomation.CM.StatusMessageDB.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EphingAutomation.CM.StatusMessageDB.Repository
{
    public class StatusMessageRepository : IStatusMessageRepository
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
        public StatusMessage GetNext()
        {
            DateTime StartedTime = DateTime.UtcNow;
            using (var dbConnection = _db.SqlConnection())
            {
                var smList = dbConnection.Query<StatusMessage>(@"
                    SELECT * FROM StatusMessage WHERE Started IS NULL ORDER BY Recorded LIMIT 1
                ").AsList();

                if (smList.Count == 0) { return null; }

                var sm = smList[0];
                sm.Started = StartedTime;

                dbConnection.Query(@"
                    UPDATE StatusMessage
                    SET Started = @Started
                    WHERE Id = @Id 
                ",sm);
                return sm;
            }
        }
        public void Finish(StatusMessage MessageToFinish)
        {
            DateTime Finished = DateTime.UtcNow;
            MessageToFinish.Finished = Finished;
            using (var dbConnection = _db.SqlConnection())
            {
                dbConnection.Query(@"
                    UPDATE StatusMessage
                    SET Finished = @Finished
                    WHERE Id = @Id 
                ", MessageToFinish);
            }
        }
    }
}
