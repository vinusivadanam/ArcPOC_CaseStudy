using FictitiousInsurance.Common;
using FictitiousInsurance.Helper;
using FictitiousInsurance.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace FictitiousInsurance.DataAccess
{
    public class CSVDataAccess : IDataAccess
    {
        /// <summary>
        /// Get Customer details for policy expiring soon
        /// </summary>
        /// <returns>List<CustomerModel></returns>
        public List<CustomerModel> GetPolicyDueCustomers()
        {
            var dueList = new List<CustomerModel>();
            var filePath = ConfigHelper.GetConfigValue("InputFileLocation");
            if (FileHelper.FileExists(filePath))
            {
                try
                {
                    using (var sr = new StreamReader(filePath))
                    {
                        string fileData;
                        string[] _records;
                        int x = 0;
                        while (!sr.EndOfStream)
                        {
                            fileData = sr.ReadLine();
                            //Skipping Header row
                            if (x++ == 0) continue;
                            _records = fileData.Split(';');
                            if (_records.Length >= 0 && _records[0].Trim().Length > 0)
                            {
                                var rowCells = _records[0].Split(',');

                                dueList.Add(new CustomerModel()
                                {

                                    CustomerId = long.Parse(rowCells[0]),
                                    Title = rowCells[1],
                                    FirstName = rowCells[2],
                                    SurName = rowCells[3],
                                    ProductDetails = new ProductModel()
                                    {
                                        ProductName = rowCells[4]
                                    },
                                    PaymentDetails = new PaymentModel()
                                    {
                                        PayoutAmount = double.Parse(rowCells[5]),
                                        AnnualPremium = double.Parse(rowCells[6])
                                    }
                                });
                            }
                        }
                        sr.Close();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogException("CSVDataAccess.GetPolicyDueCustomers: Data featching failed", ex);
                    throw new TechnicalExceptions("Getting policy due details failed.");
                }
            }
            else
            {
                throw new TechnicalExceptions("Unable to find data source file.");
            }

            return dueList;
        }
    }
}
