﻿namespace OnlineCoursePlatform.Application.Features.Payments.Queries.GetPaymentDetail
{
    public class PaymentDetailVm
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string PayPalOrderId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
