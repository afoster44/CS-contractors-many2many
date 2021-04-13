namespace AmaZen.Models
{
    public class JobContractor
    {
        public JobContractor()
        {
        }

        public int Id { get; set; }
        public int JobId { get; set; }
        public int ContractorId { get; set; }
        public string CreatorId { get; set; }
    }
}