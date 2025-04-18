﻿using MediatR;

namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList
{
    public class GetCoursesListQuery : IRequest<List<CourseListVm>>
    {
    }
}
