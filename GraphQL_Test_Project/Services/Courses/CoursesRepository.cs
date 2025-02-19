using AutoMapper;
using GraphQL_Test_Project.DataLoaders;
using GraphQL_Test_Project.Dtos;
using GraphQL_Test_Project.Schema.Queries;
using GraphQL_Test_Project.Services.Instructors;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_Test_Project.Services.Courses
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly SchoolDbContext _context;
        private readonly InstructorDataLoader _instructorDataLoader;
        private readonly IMapper _mapper;

        public CoursesRepository(SchoolDbContext context, IMapper mapper, InstructorDataLoader instructorDataLoader)
        {
            _context = context;
            _mapper = mapper;
            _instructorDataLoader = instructorDataLoader;
        }
        public async Task<IEnumerable<CourseType>> GetAllAsync()
        {
            var courses = await _context.Courses.ToListAsync();

            var result = _mapper.Map<IEnumerable<CourseType>>(courses);

            var instructorIds = result.Select(c => c.InstructorId).Distinct().ToList();

            var instructorsList = await _instructorDataLoader.LoadAsync(instructorIds, CancellationToken.None);

            var instructors = instructorsList.ToDictionary(i => i.Id);

            return result;
        }

        public async Task<IQueryable<CourseType>> GetPaginatedCoursesAsync(int first)
        {
            var query = _context.Courses
                .Take(first)
                .AsQueryable();

            var courses = await query.ToListAsync();

            var courseTypes = _mapper.Map<IEnumerable<CourseType>>(courses);

            var instructorIds = courseTypes.Select(c => c.InstructorId).Distinct().ToList();
            var instructorsList = await _instructorDataLoader.LoadAsync(instructorIds, CancellationToken.None);

            var instructors = instructorsList.ToDictionary(i => i.Id);


            return courseTypes.AsQueryable();
        }

        public async Task<CourseType> GetByIdAsync(Guid courseId)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                throw new Exception("Course not found.");
            }

            var result = _mapper.Map<CourseType>(course);
            return result;
        }

        public async Task<CourseType> CreateAsync(CourseDTO courseDTO)
        {
            var instructor = await _context.Instructor.FindAsync(courseDTO.InstructorId);
            if (instructor == null)
            {
                throw new Exception("Instructor not found.");
            }

            var course = _mapper.Map<CourseType>(courseDTO);

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return course;
        }


        public async Task<CourseType> UpdateAsync(CourseDTO courseDTO)
        {
            var course = _mapper.Map<CourseType>(courseDTO);

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

            return course;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return false;

            _context.Courses.Remove(course);
            return await _context.SaveChangesAsync() > 0;
        }
    }

}
