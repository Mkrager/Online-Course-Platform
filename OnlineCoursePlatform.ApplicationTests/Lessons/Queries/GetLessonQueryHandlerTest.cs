﻿using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Lessons.Queries.GetLessonDetail;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;

namespace OnlineCoursePlatform.Application.UnitTests.Lessons.Queries
{
    public class GetLessonQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILessonRepository> _mockLessonRepository;

        public GetLessonQueryHandlerTest()
        {
            _mockLessonRepository = LessonRepositoryMock.GetLessonRepository();
            var configurationProveder = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProveder.CreateMapper();
        }

        [Fact]
        public async Task GetLessonDetails_ReturnsCorrectLessonDetails()
        {
            var handler = new GetLessonDetailQueryHandler(_mockLessonRepository.Object, _mapper);

            var result = await handler.Handle(new GetLessonDetailQuery() { Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8") }, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(result.Id, Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"));
        }
    }
}
