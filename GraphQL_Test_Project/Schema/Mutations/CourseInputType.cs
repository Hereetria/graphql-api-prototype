using GraphQL_Test_Project.Modals;
using GraphQL_Test_Project.Schema.Queries;

namespace GraphQL_Test_Project.Schema.Mutations
{
    public class CourseInputType
    {
        public string Name { get; set; }
        public Subject Subject { get; set; }
        public Guid InstructorId { get; set; }
    }
}
