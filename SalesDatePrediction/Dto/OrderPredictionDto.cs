﻿namespace SalesDatePrediction.Dto
{
    public class OrderPredictionDto
    {
        public string CompanyName { get; set; }
        public DateTime LastOrderDate { get; set; }
        public DateTime NextPredictedOrder { get; set; }
    }
}
