using AutoMapper;
using OnlineCoursePlatform.Application.DTOs.Authentication;
using OnlineCoursePlatform.Application.DTOs.User;
using OnlineCoursePlatform.Application.Features.Account.Commands.Registration;
using OnlineCoursePlatform.Application.Features.Account.Queries.Authentication;
using OnlineCoursePlatform.Application.Features.Categories.Commands.CreateCategory;
using OnlineCoursePlatform.Application.Features.Categories.Commands.DeleteCategory;
using OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesList;
using OnlineCoursePlatform.Application.Features.Categories.Queries.GetCategoriesListWithCourses;
using OnlineCoursePlatform.Application.Features.Courses.Commands.CreateCourse;
using OnlineCoursePlatform.Application.Features.Courses.Commands.DeleteCourse;
using OnlineCoursePlatform.Application.Features.Courses.Commands.UpdateCourse;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCourseDetail;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByCategory;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesByUser;
using OnlineCoursePlatform.Application.Features.Courses.Queries.GetCoursesList;
using OnlineCoursePlatform.Application.Features.Enrollments.Commands.CreateEnrollment;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.CreateLesson;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.DeleteLesson;
using OnlineCoursePlatform.Application.Features.Lessons.Commands.UpdateLesson;
using OnlineCoursePlatform.Application.Features.Lessons.Queries.GetCourseLessonsList;
using OnlineCoursePlatform.Application.Features.Lessons.Queries.GetLessonDetail;
using OnlineCoursePlatform.Application.Features.Levels.Queries.GetLevelsList;
using OnlineCoursePlatform.Application.Features.Payments.Commands.CreatePayment;
using OnlineCoursePlatform.Application.Features.Payments.Commands.UpdatePayment;
using OnlineCoursePlatform.Application.Features.Payments.Queries.GetPaymentDetail;
using OnlineCoursePlatform.Application.Features.TestAttemps.Commands.EndAttempt;
using OnlineCoursePlatform.Application.Features.TestAttemps.Commands.StartAttempt;
using OnlineCoursePlatform.Application.Features.Tests.Commands.CreateTest;
using OnlineCoursePlatform.Application.Features.Tests.Commands.DeleteTest;
using OnlineCoursePlatform.Application.Features.Tests.Commands.UpdateTest;
using OnlineCoursePlatform.Application.Features.Tests.Queries.GetTestDetail;
using OnlineCoursePlatform.Application.Features.Tests.Queries.GetUserTestsList;
using OnlineCoursePlatform.Application.Features.User.Queries.GetUserDetails;
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
            CreateMap<Course, CoursesByCategoryVm>().ReverseMap();
            CreateMap<Course, CourseByTeacherVm>().ReverseMap();
            CreateMap<Course, CreateCourseCommand>().ReverseMap();
            CreateMap<Course, UpdateCourseCommand>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Course, DeleteCourseCommand>();

            CreateMap<Category, CategoryListVm>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CourseByCategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCourseListVm>().ReverseMap();
            CreateMap<Category, TeacherCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Category, DeleteCategoryCommand>();

            CreateMap<Test, TestDetailVm>().ReverseMap();
            CreateMap<Test, LessonTestListVm>().ReverseMap();

            CreateMap<Test, CreateTestCommand>().ReverseMap();
            CreateMap<Test, UpdateTestCommand>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Test, DeleteTestCommand>();

            CreateMap<Answer, AnswerDto>().ReverseMap();
            CreateMap<Answer, AnswerDetailDto>().ReverseMap();

            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Question, QuestionDetailDto>().ReverseMap();

            CreateMap<Level, LevelListVm>().ReverseMap();
            CreateMap<Level, LevelDto>().ReverseMap();
            CreateMap<Level, CourseByCategoryLevelDto>().ReverseMap();
            CreateMap<Level, TeacherLevelDto>().ReverseMap();

            CreateMap<Lesson, CourseLessonListVm>().ReverseMap();
            CreateMap<Lesson, LessonDetailVm>().ReverseMap();

            CreateMap<Lesson, CreateLessonCommand>().ReverseMap();
            CreateMap<Lesson, UpdateLessonCommand>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Lesson, DeleteLessonCommand>();

            CreateMap<TestAttempt, StartAttemptCommand>().ReverseMap();
            CreateMap<TestAttempt, EndAttemptCommand>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UserAnswer, UserAnswerDto>().ReverseMap();

            CreateMap<RegistrationRequest, RegistrationCommand>().ReverseMap();
            CreateMap<AuthenticationRequest, AuthenticationQuery>().ReverseMap();
            CreateMap<AuthenticationResponse, AuthenticationVm>();

            CreateMap<Enrollment, CreateEnrollmentCommand>().ReverseMap();

            CreateMap<Payment, CreatePaymentCommand>().ReverseMap();
            CreateMap<Payment, UpdatePaymentCommand>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Payment, PaymentDetailVm>().ReverseMap();

            CreateMap<UserDetailsResponse, UserDetailsVm>().ReverseMap();
        }
    }
}
