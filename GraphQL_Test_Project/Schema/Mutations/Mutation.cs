using AutoMapper;
using GraphQL_Test_Project.Dtos;
using GraphQL_Test_Project.Schema.Queries;
using GraphQL_Test_Project.Schema.Subscriptions;
using GraphQL_Test_Project.Services.Courses;
using HotChocolate.Authorization;
using HotChocolate.Subscriptions;

namespace GraphQL_Test_Project.Schema.Mutations
{
    public class Mutation
    {
        private readonly IMapper _mapper;

        public Mutation(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Authorize(Policy = "IsAdmin")]
        public async Task<CourseResult> CreateCourse(
            CourseInputType courseInput,
            [Service] ITopicEventSender topicEventSender,
            [Service] ICoursesRepository coursesRepository)
        {
            var courseDTO = _mapper.Map<CourseDTO>(courseInput);

            var course = await coursesRepository.CreateAsync(courseDTO);

            var result = _mapper.Map<CourseResult>(courseDTO);

            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), result);

            return result;
        }

        [Authorize(Policy = "IsAdmin")]
        public async Task<CourseResult> UpdateCourse(
            Guid id,
            CourseInputType courseInput,
            [Service] ITopicEventSender topicEventSender,
            [Service] ICoursesRepository coursesRepository)
        {
            var courseDTO = _mapper.Map<CourseDTO>(courseInput);
            courseDTO.Id = id;

            var course = await coursesRepository.UpdateAsync(courseDTO);

            var result = _mapper.Map<CourseResult>(courseDTO);

            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
            await topicEventSender.SendAsync(updateCourseTopic, course);

            return result;
        }

        [Authorize(Policy = "IsAdmin")]
        public async Task<bool> DeleteCourse(Guid id, [Service] ICoursesRepository coursesRepository)
        {
            return await coursesRepository.DeleteAsync(id);
        }
    }

}
