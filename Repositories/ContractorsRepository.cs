using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AmaZen.Models;
using Dapper;

namespace AmaZen.Repositories
{
    public class ContractorsRepository
    {
        private readonly IDbConnection _db;

        public ContractorsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Contractor> GetAll()
        {
            string sql = @"
      SELECT 
      cont.*,
      prof.*
      FROM contractors cont
      JOIN profiles prof ON cont.creatorId = prof.id;";
            return _db.Query<Contractor, Profile, Contractor>(sql, (job, profile) =>
            {
                job.Creator = profile;
                return job;
            }, splitOn: "id");
        }

        internal Contractor GetById(int id)
        {
            string sql = @" 
      SELECT 
      cont.*,
      prof.*
      FROM contractors cont
      JOIN profiles prof ON cont.creatorId = prof.id
      WHERE cont.id = @id;";
            return _db.Query<Contractor, Profile, Contractor>(sql, (contractor, profile) =>
            {
                contractor.Creator = profile;
                return contractor;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Contractor Create(Contractor newContractor)
        {
            string sql = @"
      INSERT INTO contractors 
      (company, name, creatorId) 
      VALUES 
      (@Company, @Name, @CreatorId);
      SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newContractor);
            newContractor.Id = id;
            return newContractor;
        }

        internal Contractor Edit(Contractor updated)
        {
            string sql = @"
        UPDATE contractors
        SET
         company = @Company,
         name = @Name
        WHERE id = @Id;";
            _db.Execute(sql, updated);
            return updated;
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM contractors WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }

        internal IEnumerable<ContractorJobViewModel> GetContractorsByJobId(int id)
        {
            string sql = @"SELECT
            c.*,
            jc.id AS ContractorJobId
            FROM jobcontractors jc
            JOIN contractors c ON c.id = jc.contractorId
            WHERE jobId = @id;";
            return _db.Query<ContractorJobViewModel>(sql, new { id });
        }
    }
}