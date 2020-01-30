//-----------------------------------------------------------------------
// <copyright file="ApiResponse.cs" company="FictiousInsurance">
// Application response
// </copyright>
//-----------------------------------------------------------------------
namespace FictitiousInsurance.Model
{
    /// <summary>
    /// API Response class
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Gets or sets Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets processed count
        /// </summary>
        public int ProcessedCount { get; set; }

        /// <summary>
        /// Gets or sets Total error count
        /// </summary>
        public int ErrorCount { get; set; }

        /// <summary>
        /// Gets a value indicating whether the item is success.
        /// </summary>
        public bool Success { get; set; }
    }
}
