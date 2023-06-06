﻿using System;
using System.Collections.Generic;

namespace mersetaWebAPI.Models
{
    public partial class NonCreditBearingIntervationTitleHist
    {
        public long Id { get; set; }
        public int Rev { get; set; }
        public sbyte? Revtype { get; set; }
        public string? Description { get; set; }

        public virtual Revinfo RevNavigation { get; set; } = null!;
    }
}
