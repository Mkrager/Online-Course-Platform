﻿using MediatR;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.DeleteTest
{
    public class DeleteTestCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
