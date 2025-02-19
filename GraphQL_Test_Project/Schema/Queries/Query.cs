using Bogus;
using GraphQL_Test_Project.Dtos;
using GraphQL_Test_Project.Modals;
using GraphQL_Test_Project.Schema.Filters;
using GraphQL_Test_Project.Schema.Sorters;
using GraphQL_Test_Project.Services;
using GraphQL_Test_Project.Services.Courses;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_Test_Project.Schema.Queries
{
    public class Query
    {
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        public async Task<IEnumerable<CourseType>> GetCourses([Service] ICoursesRepository coursesRepository)
        {
            return await coursesRepository.GetAllAsync();
        }

        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection]
        [UseFiltering(typeof(CourseFilterType))]
        [UseSorting(typeof(CourseSortType))]
        public async Task<IQueryable<CourseType>> GetPaginatedCourses(
            [Service] SchoolDbContext context,
            [Service] ICoursesRepository coursesRepository,
            int first = 10
        )
        {
            var result = context.Courses.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId,
            });
            return result;
        }
         

        [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        public async Task<IEnumerable<CourseType>> GetOffsetCourses([Service] ICoursesRepository coursesRepository)
        {
            return await coursesRepository.GetAllAsync();
        }

        public async Task<CourseType> GetCourseById(Guid id, [Service] ICoursesRepository coursesRepository)
        {
            var courses = await coursesRepository.GetByIdAsync(id);

            if (courses == null)
            {
                throw new Exception("Course not found.");
            }

            return courses;
        }

    }
}
