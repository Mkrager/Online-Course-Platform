using AutoMapper;
using OnlineCoursePlatform.Application.Features.Courses.Commands.CreateCourse;
using OnlineCoursePlatform.Application.Features.Courses.Commands.DeleteCourse;
using OnlineCoursePlatform.Application.Features.Courses.Commands.UpdateCourse;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCourseDetail;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Course, CourseListVm>().ReverseMap();
            CreateMap<Course, CourseDetailVm>().ReverseMap();

            CreateMap<Course, CreateCourseCommand>().ReverseMap();
            CreateMap<Course, UpdateCourseCommand>().ReverseMap();
            CreateMap<Course, DeleteCourseCommand>().ReverseMap();
        }
    }
}
