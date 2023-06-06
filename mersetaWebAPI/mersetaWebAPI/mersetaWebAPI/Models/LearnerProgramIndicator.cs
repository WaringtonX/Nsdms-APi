namespace mersetaWebAPI.Models
{
    public class LearnerProgramIndicator
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Source_Document { get; set; }
        public string? Business_Rules { get; set; }
        
        public long? Learning_Program_Id { get; set; }
    }
}
