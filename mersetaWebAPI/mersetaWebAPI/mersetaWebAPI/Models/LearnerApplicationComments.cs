namespace mersetaWebAPI.Models
{
    public class LearnerApplicationComments
    {
        public LearnerApplicationComments()
        {
            
        }

        public long Id { get; set; }
        public long CompanyLearnerId { get; set; }
        public DateTime? CreateDate { get; set; }
        public long CreateUserId { get; set; }
        public string? Comments { get; set; }
        public ulong? IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

    }
}
