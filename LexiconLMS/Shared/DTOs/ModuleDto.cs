﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace LexiconLMS.Shared.DTOs
{
#nullable disable
    public class ModuleDto
    {

        public string Name { get; set; }
        public string Desc { get; set; }
        public IEnumerable<ActivityDto> Activitys { get; set; }

    }
}