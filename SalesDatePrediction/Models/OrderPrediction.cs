namespace SalesDatePrediction.Models
{
    public class OrderPrediction
    {
        public string CompanyName { get; set; }
        public DateTime LastOrderDate { get; set; }
        public DateTime NextPredictedOrder { get; set; }
    }
}
