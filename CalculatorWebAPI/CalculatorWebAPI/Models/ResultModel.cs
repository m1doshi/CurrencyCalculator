namespace CalculatorWebAPI.Models
{
    public class ResultModel
    {
        public string FirstNumber { get; set; }
        public string SecondNumber { get; set; }
        public string FirstCurrency { get; set; }
        public string SecondCurrency { get; set; }
        public string MathOperation { get; set; }
        public decimal Results { get; set; }
        public DateTime Date { get; set; }

    }
}
