using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AmaZen.Models;
using Dapper;

namespace AmaZen.Repositories
{
    public class JobsRepository
    {
        private readonly IDbConnection _db;

        public JobsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Job> GetAll()
        {
            string sql = @"
      SELECT 
      j.*,
      prof.*
      FROM jobs j
      JOIN profiles prof ON j.creatorId = prof.id";
            return _db.Query<Job, Profile, Job>(sql, (job, profile) =>
            {
                job.Creator = profile;
                return job;
            }, splitOn: "id");
        }

        internal Job GetById(int id)
        {
            string sql = @" 
      SELECT 
      j.*,
      prof.*
      FROM jobs j
      JOIN profiles prof ON j.creatorId = prof.id
      WHERE j.id = @id";
            return _db.Query<Job, Profile, Job>(sql, (job, profile) =>
            {
                job.Creator = profile;
                return job;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Job Create(Job newJob)
        {
            string sql = @"
      INSERT INTO jobs 
      (description, creatorId) 
      VALUES 
      (@Description, @CreatorId);
      SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newJob);
            newJob.Id = id;
            return newJob;
        }

        internal Job Edit(Job updated)
        {
            string sql = @"
        UPDATE jobs
        SET
         description = @Description
        WHERE id = @Id;";
            _db.Execute(sql, updated);
            return updated;
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM jobs WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }

        internal IEnumerable<JobContractorViewModel> GetJobsByContractorId(int id)
        {
            string sql = @"SELECT
            j.*,
            jc.id AS JobContractorId
            FROM jobcontractors jc
            JOIN jobs j ON j.id = jc.jobId
            JOIN profiles p ON j.creatorId = p.id
            WHERE contractorId = @id;";
            return _db.Query<JobContractorViewModel>(sql, new { id });
        }

    }
}