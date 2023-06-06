namespace mersetaWebAPI.CustomModels.Task
{
    public class TaskResponse
    {
        public long Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Description { get; set; } 
        public DateTime? DueDate { get; set; }
        public string? Guid { get; set; }
        public string? TargetClass { get; set; }
        public long? TargetKey { get; set; }
        public int? TaskStatus { get; set; }
        public int? WorkflowProcess { get; set; }
        public long? CreateUserId { get; set; }
        public long? HostingCompanyProcessId { get; set; }
        public ulong? FirstInProcess { get; set; }
        public long? CompanyId { get; set; }
        public long? ActionUserId { get; set; }
    }
}
