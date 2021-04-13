using System;
using System.Collections.Generic;
using AmaZen.Models;
using AmaZen.Repositories;

namespace AmaZen.Services
{
    public class ContractorsService
    {
        private readonly ContractorsRepository _repo;

        public ContractorsService(ContractorsRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<Contractor> GetAll()
        {
            return _repo.GetAll();
        }

        internal Contractor GetById(int id)
        {
            var data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid Id");
            }
            return data;
        }

        internal Contractor Create(Contractor newProd)
        {
            return _repo.Create(newProd);
        }

        internal Contractor Edit(Contractor updated)
        {
            var original = GetById(updated.Id);
            if (original.CreatorId != updated.CreatorId)
            {
                throw new Exception("Invalid Edit Permissions");
            }
            updated.Company = updated.Company != null ? updated.Company : original.Company;
            updated.Name = updated.Name != null ? updated.Name : original.Name;

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

        internal IEnumerable<ContractorJobViewModel> GetContractorsByJobId(int id)
        {
            return _repo.GetContractorsByJobId(id);
        }
    }
}