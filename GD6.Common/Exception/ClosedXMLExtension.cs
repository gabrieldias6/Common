using ClosedXML.Excel;
using System;

namespace GD6.Common
{
    public static class ClosedXMLExtension
    {
        public static string GetString(this IXLRangeRow linha, Enum enums)
        {
            var row = linha.Cell(enums.GetHashCode());
            if (row != null && row.Value != null)
                return row.Value.ToString().Trim();

            return null;
        }
        public static string GetStringTamanho(this IXLRangeRow linha, Enum enums, int tamanho)
        {
            var value = linha.GetString(enums);

            if (!string.IsNullOrEmpty(value))
            {
                if (value.Length > tamanho)
                    return value.Substring(0, tamanho);
                return value;
            }

            return null;
        }
        public static int? GetInt(this IXLRangeRow linha, Enum enums)
        {
            var value = linha.GetString(enums);

            if (!string.IsNullOrEmpty(value))
            {
                if (int.TryParse(value, out int valueInt))
                    return valueInt;
            }

            return null;
        }
        public static decimal? GetDecimal(this IXLRangeRow linha, Enum enums)
        {
            var value = linha.GetString(enums);

            if (!string.IsNullOrEmpty(value))
            {
                if (Decimal.TryParse(value, out decimal valueDecimal))
                    return valueDecimal;
            }

            return null;
        }
        public static DateTime? GetDateTime(this IXLRangeRow linha, Enum enums)
        {
            var value = linha.GetString(enums);

            if (!string.IsNullOrEmpty(value))
            {
                if (DateTime.TryParse(value, out DateTime valueConvert))
                    return valueConvert;
            }

            return null;
        }
        public static bool GetBoolean(this IXLRangeRow linha, Enum enums)
        {
            var value = linha.GetString(enums);

            if (!string.IsNullOrEmpty(value))
            {
                if (Boolean.TryParse(value, out bool valueConvert))
                    return valueConvert;
            }

            return false;
        }
    }
}
