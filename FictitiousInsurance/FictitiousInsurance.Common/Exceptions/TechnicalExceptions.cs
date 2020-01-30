//-----------------------------------------------------------------------
// <copyright file="TechnicalExceptions.cs" company="FictiousInsurance">
// Application Technical exception
// </copyright>
//-----------------------------------------------------------------------
namespace FictitiousInsurance.Common
{
    using System;

    /// <summary>
    /// Technical exception class
    /// </summary>
    [Serializable]
    public class TechnicalExceptions : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TechnicalExceptions" /> class
        /// </summary>
        /// <param name="message">exception message</param>
        public TechnicalExceptions(string message)
        : base($"Fictitious Insurance API Error: {message}.")
        {
        }
    }
}
