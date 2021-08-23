using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Infra.Utils
{
    public static class StringUtils
    {
        public static string RemoveAcentuacao(this string value)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = value.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }
    }
}
