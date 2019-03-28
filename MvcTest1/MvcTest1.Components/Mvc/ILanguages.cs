using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTest1.Components.Mvc
{
    public interface ILanguages
    {
        Language Current { get; set; }
        Language Default { get; set; }
        Language[] Supports { get; set; }
        Language this[string Abbreviation] { get; }
    }
}
