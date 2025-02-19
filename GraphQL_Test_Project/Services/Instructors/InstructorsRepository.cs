using GraphQL_Test_Project.Schema.Queries;
using GraphQL_Test_Project.Services.Courses;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_Test_Project.Services.Instructors
{
    public class InstructorsRepository : IInstructorsRepository
    {
        public SchoolDbContext _context;

        public InstructorsRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<InstructorType> GetById(Guid id)
        {
            var instructor = await _context.Instructor.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception($"ID'si {id} olan eğitmen bulunamadı.");
            return instructor;
        }

        public async Task<IEnumerable<InstructorType>> GetManyByIds(IReadOnlyList<Guid> instructorIds)
        {
            return await _context.Instructor
                .Where(i => instructorIds.Contains(i.Id))
                .ToListAsync();
        }

    }
}
