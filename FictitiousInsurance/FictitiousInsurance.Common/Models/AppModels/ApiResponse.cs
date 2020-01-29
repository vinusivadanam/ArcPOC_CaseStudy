namespace FictitiousInsurance.Model
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public int ProcessedCount { get; set; }
        public int ErrorCount { get; set; }
        public bool Success { get; set; }
    }
}
