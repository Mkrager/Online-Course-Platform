namespace OnlineCoursePlatform.App.ViewModels.PayPal
{
    public class CaptureOrderRequest
    {
        public Guid PaymentId { get; set; }
        public string Token { get; set; } = string.Empty;
        public string PayerId { get; set; } = string.Empty;
    }
}
