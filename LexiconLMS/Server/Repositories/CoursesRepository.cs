
using LexiconLMS.Server.Data;
using LexiconLMS.Server.Models;
using LexiconLMS.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LexiconLMS.Server.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly ApplicationDbContext db;

        public CoursesRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CourseDto>> GetAsync()
        {
            var courseDtos = db.Courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Desc = c.Desc,
                Name = c.Name,
                Modules = c.Modules.Select(m => new ModuleDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Desc = m.Desc,
                    Activitys = m.Activitys.Select(a => new ActivityDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Desc = a.Desc
                    })
                })
            });

            return await courseDtos.ToListAsync();  
        }


        public void Add(Course course)
        {
            db.Courses.Add(course);
        }

        public void Remove(Course course)
        {
            db.Courses.Remove(course);

        }
        public bool CourseExists(int id)
        {
            return (db.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}