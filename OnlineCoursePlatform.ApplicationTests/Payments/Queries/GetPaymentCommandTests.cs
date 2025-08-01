﻿using AutoMapper;
using Moq;
using OnlineCoursePlatform.Application.Contracts.Persistance;
using OnlineCoursePlatform.Application.Features.Payments.Queries.GetPaymentDetail;
using OnlineCoursePlatform.Application.Profiles;
using OnlineCoursePlatform.Application.UnitTests.Mocks;
using OnlineCoursePlatform.Domain.Entities;

namespace OnlineCoursePlatform.Application.UnitTests.Payments.Queries
{
    public class GetPaymentCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Payment>> _mockPaymentRepository;

        public GetPaymentCommandTests()
        {
            _mockPaymentRepository = PaymentRepositoryMock.GetPaymentRepository();
            var configurationProveder = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProveder.CreateMapper();
        }

        [Fact]
        public async Task GetPaymentDetails_ReturnsCorrectPaymentetails()
        {
            var handler = new GetPaymentDetailQueryHandler(_mapper, _mockPaymentRepository.Object);

            var result = await handler.Handle(new GetPaymentDetailQuery() 
            { 
                Id = Guid.Parse("4f50d45e-f395-4688-a55f-c64e06649572") 
            }, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(result.Id, Guid.Parse("4f50d45e-f395-4688-a55f-c64e06649572"));
        }

    }
}
