using GraphQL_Test_Project.Dtos;
using GraphQL_Test_Project.Schema.Queries;
using GraphQL_Test_Project.Services.Instructors;

namespace GraphQL_Test_Project.DataLoaders
{
    public class InstructorDataLoader : BatchDataLoader<Guid, InstructorType>
    {
        private readonly IInstructorsRepository _instructorsRepository;

        public InstructorDataLoader(
            IInstructorsRepository instructorsRepository,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _instructorsRepository = instructorsRepository;
        }

        protected override async Task<IReadOnlyDictionary<Guid, InstructorType>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            var instructors = await _instructorsRepository.GetManyByIds(keys);
            return instructors.ToDictionary(i => i.Id);
        }
    }
}
