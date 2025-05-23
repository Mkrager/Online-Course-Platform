﻿using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Courses.Queries.GetCourseDetail
{
    public class GetCourseDetailQueryHandler : IRequestHandler<GetCourseDetailQuery, CourseDetailVm>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Course> _courseRepository;

        public GetCourseDetailQueryHandler(IMapper mapper, IAsyncRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<CourseDetailVm> Handle(GetCourseDetailQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id);

            if (course == null)
            {
                throw new NotFoundException(nameof(Course), request.Id);
            }

            return _mapper.Map<CourseDetailVm>(course);
        }
    }
}
