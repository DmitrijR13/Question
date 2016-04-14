using System;
using System.Text.RegularExpressions;

namespace Sobits.Story.Logic.Extensions
{
    /// <summary>
    /// Extensions for String class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Normalizes string to use in URLs (remove dangerous chars).
        /// </summary>
        /// <param name="self">The self.</param>
        /// <returns></returns>
        /// <remarks>
        /// See FUNCTION [dbo].[RemoveDangerousSymbols]
        /// in TalentBrew code
        /// </remarks>
        public static String NormalizeForUrl(this String self)
        {
            // replace some chars with spaces
            var re = new Regex(@"[\<\>\-:;|,_%]");
            var tmp = re.Replace(self, " ");
            
            // remove some chars
            re = new Regex(@"[\*\@\$\^\?\""\']");
            tmp = re.Replace(tmp, "");
            
            // remove duplicate spaces
            re = new Regex(@"\s+");
            tmp = re.Replace(tmp, " ");

            // change spaces to underscore
            return tmp.Replace(' ','_');
        }
    }
}