﻿using AutoMapper;
using MediatR;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Exceptions;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.Features.Tests.Commands.UpdateTest
{
    public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand>
    {
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;
        public UpdateTestCommandHandler(IMapper mapper, ITestRepository testRepository)
        {
            _mapper = mapper;
            _testRepository = testRepository;
        }

        public async Task<Unit> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            var testToUpdate = await _testRepository.GetTestWithQuestionsAndAnswers(request.Id);

            if (testToUpdate == null)
                throw new NotFoundException(nameof(Test), request.Id);

            _mapper.Map(request, testToUpdate, typeof(UpdateTestCommand), typeof(Test));

            try
            {
                await _testRepository.UpdateAsync(testToUpdate);
            }
            catch (Exception ex)
            {
                throw;
            }

            return Unit.Value;
        }
    }
}
