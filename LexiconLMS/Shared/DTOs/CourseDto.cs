using System.Diagnostics.CodeAnalysis;

namespace LexiconLMS.Shared.DTOs
{

    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public IEnumerable<ModuleDto> Modules { get; set; }
    }
}