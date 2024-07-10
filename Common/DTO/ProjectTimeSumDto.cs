﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public record ProjectTimeSumDto
    {
        public string? TitleProject { get; set; }
        public int SumTimeSpent { get; set; }

        public bool IsFinished { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsEducation { get; set; }
        public bool IsPayable { get; set; }
    }
}
