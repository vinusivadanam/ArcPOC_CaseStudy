//-----------------------------------------------------------------------
// <copyright file="FileHelper.cs" company="FictiousInsurance">
// Configuration helper
// </copyright>
//-----------------------------------------------------------------------

namespace FictitiousInsurance.Helper
{
    using System;
    using System.IO;
    using FictitiousInsurance.Common;

    /// <summary>
    /// File helper
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// Check a file exist in given path
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <returns>bool: true if file exist</returns>
        public static bool FileExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Read file to a string from given path
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <returns>string: file content</returns>
        public static string ReadTextFile(string filePath)
        {
            string fileContent = string.Empty;
            if (FileHelper.FileExists(filePath))
            {
                try
                {
                    fileContent = File.ReadAllText(filePath);
                }
                catch (Exception ex)
                {
                    LogHelper.LogException($"FileHelper.ReadTextFile: Exception reading file at {filePath}", ex);
                    throw new TechnicalExceptions($"Exception reading file at {filePath}");
                }
            }
            else
            {
                LogHelper.LogException($"FileHelper.ReadTextFile:file not found at {filePath}");
                throw new TechnicalExceptions($"File not found at {filePath}");
            }

            return fileContent;
        }

        /// <summary>
        /// Write file to the given path with provided file name
        /// </summary>
        /// <param name="filePath">string: path to which file created</param>
        /// <param name="fileContent">string: content of the file</param>
        /// <param name="allowOverwrite">bool: if true, file will overwrite if one with same name exists</param>
        /// <returns>success response</returns>
        public static bool WriteTextFile(string filePath, string fileContent, bool allowOverwrite)
        {
            bool res = false;
            if (allowOverwrite)
            {
                try
                { 
                File.WriteAllText(filePath, fileContent);
                }
                catch (Exception ex)
                {
                    LogHelper.LogException($"FileHelper.WriteTextFile: Exception writting/overwritting file at {filePath}", ex, fileContent);
                    throw new TechnicalExceptions($"Exception writting/overwritting file at {filePath}");
                }

                res = true;
            }
            else 
            {
                if (!FileHelper.FileExists(filePath))
                {
                    try
                    {
                        File.WriteAllText(filePath, fileContent);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogException($"FileHelper.WriteTextFile: Exception writting file at {filePath}", ex, fileContent);
                        throw new TechnicalExceptions($"Exception writting file at {filePath}");
                    }

                    res = true;
                }
                else
                {
                    LogHelper.LogInfo($"FileHelper.WriteTextFile: File already exist at {filePath}");
                }
            }

            return res;
        }
    }
}
