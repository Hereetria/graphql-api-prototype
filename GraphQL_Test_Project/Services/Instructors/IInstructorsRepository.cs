using GraphQL_Test_Project.Schema.Queries;

namespace GraphQL_Test_Project.Services.Instructors
{
    public interface IInstructorsRepository
    {
        Task<InstructorType> GetById(Guid id);
        Task<IEnumerable<InstructorType>> GetManyByIds(IReadOnlyList<Guid> instructorIds);
    }
}
