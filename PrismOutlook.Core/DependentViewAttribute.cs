using System;

namespace PrismOutlook.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependentViewAttribute : Attribute
    {
        public string Region { get; set; }

        public Type Type { get; set; }

        public DependentViewAttribute(string region, Type ribbonType)
        {
            if (region == null)
                throw new ArgumentNullException(nameof(region));

            if (ribbonType == null)
                throw new ArgumentNullException(nameof(ribbonType));

            Region = region;
            Type = ribbonType;
        }
    }
}
