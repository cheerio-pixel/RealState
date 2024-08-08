using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RealState.Application.Extras
{
    /// <summary>
    /// Range attribute only for decimals. Non inclusive in the min end
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class RangeDAttribute
    : ValidationAttribute
    {
        private object Min { get; set; }
        private object Max { get; set; }
        public bool MinInclusive { get; set; }

        public RangeDAttribute(object min, object max, bool minInclusive)
        {
            Min = min;
            Max = max;
            MinInclusive = minInclusive;
        }

        public RangeDAttribute(string min, string max)
        : this(min, max, false)
        {
        }
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }

            if (value is decimal decimalValue)
            {
                decimal min = decimal.Parse((string)Min, CultureInfo.GetCultureInfo("en-US"));
                decimal max = decimal.Parse((string)Max, CultureInfo.GetCultureInfo("en-US"));
                if (MinInclusive)
                {
                    return decimalValue >= min && decimalValue <= max;
                }
                else
                {
                    return decimalValue > min && decimalValue <= max;
                }
            }

            return false;
        }
    }
}