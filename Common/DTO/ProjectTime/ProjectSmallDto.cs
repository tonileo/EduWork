﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.ProjectTime
{
    public record ProjectSmallDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
    }
}