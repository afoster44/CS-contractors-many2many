using System;
using System.Collections.Generic;
using AmaZen.Models;
using AmaZen.Repositories;

namespace AmaZen.Services
{
    public class JobsService
    {
        private readonly JobsRepository _repo;

        public JobsService(JobsRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<Job> GetAll()
        {
            return _repo.GetAll();
        }

        internal Job GetById(int id)
        {
            var data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            return data;
        }

        internal Job Create(Job newProd)
        {
            return _repo.Create(newProd);
        }

        internal Job Edit(Job updated)
        {
            var original = GetById(updated.Id);
            if (original.CreatorId != updated.CreatorId)
            {
                throw new Exception("Invalid Edit Permissions");
            }
            updated.Description = updated.Description != null ? updated.Description : original.Description;
            return _repo.Edit(updated);
        }


        internal string Delete(int id, string userId)
        {
            var original = GetById(id);
            if (original.CreatorId != userId)
            {
                throw new Exception("Invalid Delete Permissions");
            }
            _repo.Delete(id);
            return "nice delete";
        }

        internal IEnumerable<JobContractorViewModel> GetJobsByContractorId(int id)
        {
            return _repo.GetJobsByContractorId(id);
        }
    }
}