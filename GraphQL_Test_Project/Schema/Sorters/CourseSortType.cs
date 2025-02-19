using GraphQL_Test_Project.Schema.Queries;
using HotChocolate.Data.Sorting;

namespace GraphQL_Test_Project.Schema.Sorters
{
    public class CourseSortType : SortInputType<CourseType>
    {
        protected override void Configure(ISortInputTypeDescriptor<CourseType> descriptor)
        {
            descriptor.Ignore(c => c.Id);
            descriptor.Ignore(c => c.InstructorId);
            base.Configure(descriptor);
        }
    }
}
