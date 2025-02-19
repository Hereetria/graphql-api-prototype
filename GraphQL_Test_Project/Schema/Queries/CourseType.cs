using GraphQL_Test_Project.DataLoaders;
using GraphQL_Test_Project.Dtos;
using GraphQL_Test_Project.Modals;
using GraphQL_Test_Project.Services.Instructors;

namespace GraphQL_Test_Project.Schema.Queries
{

    public class CourseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }

        [IsProjected(true)]
        public Guid InstructorId { get; set; }

        [GraphQLNonNullType]
        public async Task<InstructorType> Instructor([Service] InstructorDataLoader instructorDataLoader)
        {
            if (InstructorId == Guid.Empty)
            {
                return null; // veya uygun bir varsayılan değer döndürebilirsiniz
            }

            var instructorDTO = await instructorDataLoader.LoadAsync(InstructorId, CancellationToken.None);

            if (instructorDTO == null)
            {
                return null; // veya uygun bir varsayılan değer döndürebilirsiniz
            }

            return new InstructorType()
            {
                Id = instructorDTO.Id,
                FirstName = instructorDTO.FirstName,
                LastName = instructorDTO.LastName,
                Salary = instructorDTO.Salary,
            };
        }

        public IEnumerable<StudentType>? Students { get; set; }
    }

}
