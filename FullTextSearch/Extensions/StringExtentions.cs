using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullTextSearch.Extensions
{
    public static class StringExtentions
    {
        public static string NormalizeTurkishCharactersToEnglishCharacters(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            return input
                .Replace("Ç", "C")
                .Replace("Ş", "S")
                .Replace("Ğ", "G")
                .Replace("Ü", "U")
                .Replace("Ö", "O")
                .Replace("İ", "I")
                .Replace("I", "I")
                .Replace("á", "A")
                .Replace("à", "A")
                .Replace("ä", "A")
                .Replace("â", "A")
                .Replace("é", "E")
                .Replace("è", "E")
                .Replace("ë", "E")
                .Replace("ê", "E")
                .Replace("í", "I")
                .Replace("ì", "I")
                .Replace("ï", "I")
                .Replace("î", "I")
                .Replace("ó", "O")
                .Replace("ò", "O")
                .Replace("ö", "O")
                .Replace("ô", "O")
                .Replace("ú", "U")
                .Replace("ù", "U")
                .Replace("ü", "U")
                .Replace("û", "U")
                .Replace("ñ", "N")
                .Replace("ç", "C")
                .Replace("ş", "S")
                .Replace("ğ", "G")
                .Replace("ü", "U")
                .Replace("ö", "O")
                .Replace("ı", "I")
                .Replace("í", "I")
                .Replace("ß", "S")
                .ToUpperInvariant();
        }

        
    }
}