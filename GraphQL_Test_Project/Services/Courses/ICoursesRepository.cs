using GraphQL_Test_Project.Dtos;
using GraphQL_Test_Project.Schema.Queries;

namespace GraphQL_Test_Project.Services.Courses
{
    public interface ICoursesRepository
    {
        Task<IEnumerable<CourseType>> GetAllAsync();
        Task<IQueryable<CourseType>> GetPaginatedCoursesAsync(int first);
        Task<CourseType> GetByIdAsync(Guid id);
        Task<CourseType> CreateAsync(CourseDTO course);
        Task<CourseType> UpdateAsync(CourseDTO course);
        Task<bool> DeleteAsync(Guid id);
    }

}
