namespace AmaZen.Models
{
    public class Contractor
    {
        public string Company { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
    }

    public class ContractorJobViewModel : Contractor
    {
        public int ContractorJobId { get; set; }
    }
}