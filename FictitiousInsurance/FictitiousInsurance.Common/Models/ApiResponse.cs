using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FictitiousInsurance.Common
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public int ProcessedCount { get; set; }
        public int ErrorCount { get; set; }
        public bool Success { get; set; }
    }
}
