using AmaZen.Models;
using AmaZen.Repositories;

namespace AmaZen.Services
{
    public class JobContractorService
    {
        private readonly JobContractorRepository _repo;

        public JobContractorService(JobContractorRepository repo)
        {
            _repo = repo;
        }

        internal JobContractor Create(JobContractor newJC)
        {
            //TODO if they are creating a wishlistproduct, make sure they are the creator of the list
            return _repo.Create(newJC);

        }

        internal void Delete(int id)
        {
            //NOTE getbyid to validate its valid and you are the creator
            _repo.Delete(id);
        }
    }
}