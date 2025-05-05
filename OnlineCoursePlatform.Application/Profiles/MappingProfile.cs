using AutoMapper;
using OnlineCoursePlatform.Application.Features.Categories.Commands.CreateCategory;
using OnlineCoursePlatform.Application.Features.Categories.Commands.DeleteCategory;
using OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesList;
using OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesListWithCourses;
using OnlineCoursePlatform.Application.Features.Courses.Commands.CreateCourse;
using OnlineCoursePlatform.Application.Features.Courses.Commands.DeleteCourse;
using OnlineCoursePlatform.Application.Features.Courses.Commands.UpdateCourse;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCourseDetail;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.CreateLesson;
using OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList;
using OnlineCoursePlatform.Application.Features.Levels.Queries.GetLevelsList;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;
using OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseListVm>().ReverseMap();
            CreateMap<Course, CourseDetailVm>().ReverseMap();
            CreateMap<Course, CategoryCourseDto>().ReverseMap();

            CreateMap<Course, CreateCourseCommand>().ReverseMap();
            CreateMap<Course, UpdateCourseCommand>().ReverseMap();
            CreateMap<Course, DeleteCourseCommand>().ReverseMap();

            CreateMap<Category, CategoryListVm>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Category, CategoryCourseListVm>().ReverseMap();
            CreateMap<Category, DeleteCategoryCommand>().ReverseMap();

            CreateMap<Category, CreateCategoryCommand>().ReverseMap();

            CreateMap<Test, CreateTestCommand>().ReverseMap();
            CreateMap<Test,TestDetailVm>().ReverseMap();


            CreateMap<Answer, AnswerDto>().ReverseMap();
            CreateMap<Answer, AnswerDetailDto>().ReverseMap();

            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Question, QuestionDetailDto>().ReverseMap();

            CreateMap<Level, LevelListVm>().ReverseMap();
            CreateMap<Level, LevelDto>().ReverseMap();

            CreateMap<Lesson, CreateLessonCommand>().ReverseMap();
            CreateMap<Lesson, CourseLessonListVm>().ReverseMap();
        }
    }
}
