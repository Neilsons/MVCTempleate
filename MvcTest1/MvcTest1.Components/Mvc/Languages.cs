using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcTest1.Components.Mvc
{
    public class Languages : ILanguages
    {
        public Dictionary<string, Language> Dictionary { get; set; } 
        
        public Languages(string path)
        {
            Dictionary = new Dictionary<string, Language>();
            foreach (XElement lang in XElement.Load(path).Elements("languages"))
            {
                Language language = new Language();
                language.CultureInfo = new CultureInfo((string)lang.Attribute("culture"));
                language.IsDefault = (Boolean?)lang.Attribute("default") == true;
                language.Name = (string)lang.Attribute("name");
                language.Abbreviation = (string)lang.Attribute("abbreviation");
                Dictionary.Add(language.Abbreviation,language);
            }
            Supports = Dictionary.Select(lang=>lang.Value).ToArray();
            Default = Supports.Single(lang => lang.IsDefault);
        }
        public Language this[string Abbreviation] => throw new NotImplementedException();

        public Language Current
        {
            get
            {

                return Supports.Single(language => language.CultureInfo.Equals(CultureInfo.CurrentUICulture));
            }
            set
            {
                Thread.CurrentThread.CurrentCulture = value.CultureInfo;
                Thread.CurrentThread.CurrentUICulture = value.CultureInfo;
            }
        }

        public Language Default { get; set; }
        public Language[] Supports { get; set; }
    }
}
