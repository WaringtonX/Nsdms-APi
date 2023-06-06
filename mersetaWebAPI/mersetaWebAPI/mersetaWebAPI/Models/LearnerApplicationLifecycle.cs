namespace mersetaWebAPI.Models
{
    public class LearnerApplicationLifecycle
    {

        public long Id { get; set; }
        public long? Learner_Status_Id { get; set; }
        public DateTime? Learner_Status_Date { get; set; }
        public long? Is_Notification_Set { get; set; }
        public string? Notes { get; set; }
        public ulong? Is_Deleted { get;set; } 
        public DateTime? Deleted_On { get; set; }
        public long? Action_User_Id { get; set; }
        public long? Company_Learner_Id { get; set; }

    }
}
