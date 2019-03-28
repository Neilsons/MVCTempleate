using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTest1.Components.Mvc
{
    public class Language
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public bool IsDefault { get; set; }
        public CultureInfo CultureInfo { get; set; }
    }
}
