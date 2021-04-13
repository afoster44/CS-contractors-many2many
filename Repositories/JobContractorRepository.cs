using System.Data;
using AmaZen.Models;
using Dapper;

namespace AmaZen.Repositories
{
    public class JobContractorRepository
    {
        private readonly IDbConnection _db;

        public JobContractorRepository(IDbConnection db)
        {
            _db = db;
        }

        internal JobContractor Create(JobContractor newJC)
        {
            string sql = @"
      INSERT INTO jobcontractors 
      (jobId, contractorId, creatorId) 
      VALUES 
      (@JobId, @ContractorId, @CreatorId);
      SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newJC);
            newJC.Id = id;
            return newJC;
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM jobcontractors WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });

        }
    }
}