﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Profile
{
    public record ProjectsProfileDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
    }
}
