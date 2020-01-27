using System;

namespace FictitiousInsurance.Common
{
    public class TechnicalExceptions : Exception
    {
        public TechnicalExceptions(string message)
        : base($"Fictitious Insurance API Error: {message}.")
        {

        }
    }
}
