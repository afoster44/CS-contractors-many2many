namespace AmaZen.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Description { get; set; }
        public Profile Creator { get; set; }
    }

    public class JobContractorViewModel : Job
    {
        public int JobContractorId { get; set; }
    }
}