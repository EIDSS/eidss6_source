using System;
using System.Collections.Generic;
using System.Globalization;
using eidss.model.Enums;
using bv.common.Configuration;

namespace eidss.model.Core
{
    public class ReadableIdentifierHelper
    {
        /// <summary>
        /// Gets readable identifier consisting of object prefix, server-specific prefix (i.e. WEB00), last two digits of current year, 
        /// extra specific character taken from config settings (if needed), 
        /// and long number converted into alphanumeric string. 
        /// </summary>
        /// <param name="objectPrefix">Prefix associated with type of the object, e.g. H for Human Case</param>
        /// <param name="id">Numeric system unique identifier of the object instance</param>
        /// <param name="addExtraZero">Option indicating whether extra zero characted should be added between Year and alphanumeric part of readable identifier</param>
        /// <param name="digitsCount">Minumum number of characters of alphanumeric part of the readable identifier</param>
        /// <returns>Returns string value with readable identifier based on input parameters. If numeric id is less or equal to 0, returns empty string</returns>
        public static string GetReadableIdentifier(string objectPrefix, long id, bool addExtraChar, int digitsCount = 4)
        {
            var readableId = string.Empty;
            if (id > 0)
            {
                long cutId = (long)(id / 10000000);
                readableId = 
                    string.Format
                    (   "{0}{1}{2}{3}{4}", 
                        objectPrefix, 
                        EidssSiteContext.Instance.RealSitePrefix,
                        ((int)(DateTime.Now.Year % 100)).ToString(), 
                        addExtraChar ? BaseSettings.ExtraCharInReadableId : string.Empty, 
                        ConvertIntoAlphaNumeric(cutId, digitsCount)
                    );
            }
            return readableId;
        }

        /// Gets readable identifier consisting of object prefix, server-specific prefix (i.e. WEB00), last two digits of current year, 
        /// extra specific character taken from config settings (if needed in accordance with config settings), 
        /// and long number converted into alphanumeric string. 
        /// <param name="objectPrefix">Prefix associated with type of the object, e.g. H for Human Case</param>
        /// <param name="id">Numeric system unique identifier of the object instance</param>
        /// <param name="digitsCount">Minumum number of characters of alphanumeric part of the readable identifier</param>
        /// <returns>Returns string value with readable identifier based on input parameters. If numeric id is less or equal to 0, returns empty string</returns>
        public static string GetReadableIdentifier(string objectPrefix, long id, int digitsCount = 4)
        {
            return GetReadableIdentifier(objectPrefix, id, BaseSettings.AddExtraCharInReadableId, digitsCount);
        }


        /// Gets readable identifier consisting of object prefix, server-specific prefix (i.e. WEB00), last two digits of current year, 
        /// extra specific character taken from config settings (if needed in accordance with config settings), 
        /// and long number converted into alphanumeric string. 
        /// <param name="obj">Numbering Object to identify prefix</param>
        /// <param name="id">Numeric system unique identifier of the object instance</param>
        /// <param name="digitsCount">Minumum number of characters of alphanumeric part of the readable identifier</param>
        /// <returns>Returns string value with readable identifier based on input parameters. If numeric id is less or equal to 0, returns empty string</returns>
        public static string GetReadableIdentifier(NumberingObjectEnum obj, long id, int digitsCount = 4)
        {
            var objPrefix = string.Empty;
            if (EidssSiteContext.Instance.NumberingObjectPrefixes.ContainsKey((long)obj))
            {
                objPrefix = EidssSiteContext.Instance.NumberingObjectPrefixes[(long)obj];
            }
            return GetReadableIdentifier(objPrefix, id, digitsCount);
        }


        /// <summary>
        /// Converts long number into alpha-numeric string by means of algorithm used in SQL function fnAlphaNumeric implemented for the project
        /// </summary>
        /// <param name="num">Long number to convert into alpha-numeric string</param>
        /// <param name="digitsCount">Expected length of alpha-numeric string</param>
        /// <returns>Returns string value with a result of conversion</returns>
        private static string ConvertIntoAlphaNumeric(long num, int digitsCount = 4)
        {
            var alphaNum = string.Empty;
            if (num < 1)
            {
                return alphaNum;
            }
            
            if (digitsCount < 4)
            {
                digitsCount = 4;
            }
            else if (digitsCount > 10)
            {
                digitsCount = 10;
            }

            long maxDigitalNumber = (long)(Math.Pow(10, digitsCount)) - 1;

            if (num <= maxDigitalNumber)
            {
                alphaNum = num.ToString();
            }
            else
            {
                num -= maxDigitalNumber + 1;
                long mod = 0;
                while (num > -1)
                {
                    if (num < 26)
                    {
                        mod = num % 26;
                        alphaNum = string.Format("{0}{1}", Convert.ToChar(65 /*'A'*/ + mod).ToString(), alphaNum);
                        num = -1;
                    }
                    else
                    {
                        mod = (num - 26) % 36;
                        num = (long)((num - 26) / 36);
                        alphaNum = string.Format("{0}{1}", Convert.ToChar((((mod > -1) && (mod < 10)) ? 48 /*'0'*/ : 55 /*65 ('A') - 10 */) + mod).ToString(), alphaNum);  
                    }
                }
            }
            alphaNum = alphaNum.PadLeft(digitsCount, '0');
            return alphaNum;
        }
    }
}