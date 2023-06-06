using System;
using System.Collections.Generic;

namespace mersetaWebAPI.Models
{
    public partial class SiteVisit
    {
        public long Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? VisitDate { get; set; }
        public long? CompanyId { get; set; }
        public int? ApprovalEnum { get; set; }
        public long? UserId { get; set; }
        public DateTime? DateApproved { get; set; }
        public long? AssessmentProcessBatchId { get; set; }

    }
}
