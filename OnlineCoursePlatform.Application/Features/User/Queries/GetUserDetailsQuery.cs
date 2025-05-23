﻿using MediatR;
using OnlineCoursePlatform.Application.DTOs.User;

namespace OnlineCoursePlatform.Application.Features.User.Queries
{
    public class GetUserDetailsQuery : IRequest<UserDetailsResponse>
    {
        public string Id { get; set; } = string.Empty;
    }
}
