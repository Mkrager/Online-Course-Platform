﻿using MediatR;

namespace OnlineCoursePlatform.Application.Features.Tests.Queries.GetUserTestsList
{
    public class GetLessonTestsQuery : IRequest<List<LessonTestListVm>>
    {
        public Guid LessonId { get; set; }
    }
}
