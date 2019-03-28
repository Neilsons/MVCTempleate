using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MvcTest1.Resource
{
   public static  class ResourceProvider
    {
        private static Dictionary<string, ResourceManager> ViewTitles { get; set; }
        private static Dictionary<string, ResourceManager> Resources { get; set; }
        static ResourceProvider()
        {
            Resources = new Dictionary<string, ResourceManager>();
            ViewTitles = new Dictionary<string, ResourceManager>();
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach(var type in types)
            {
                PropertyInfo Property = type.GetProperty("ResourceManager", BindingFlags.Public | BindingFlags.Static);
                if(Property!=null)
                {
                    ResourceManager manager = Property.GetValue(null) as ResourceManager;
                    manager.IgnoreCase = true;
                    if(type.FullName.StartsWith("MvcTest1.Resource.Views"))
                    {
                        ViewTitles.Add(type.Namespace.Split('.').Last(),manager);
                    }
                    else
                    {
                        Resources.Add(type.FullName,manager);
                    }
                }
            }
        }
        public static string GetPropertyTitle(Type view,string property)
        {
            return GetPropertyTitle(view.Name,property??null);
        }
        private static string GetPropertyTitle(string view,string property)
        {
            string title = GetViewTitles(view,property);
            if (title != null)
                return title;
            string[] Propertys = SplitCamelCase(property);
            for (Int32 skipped = 0; skipped < Propertys.Length; skipped++)
            {
                for (Int32 viewSize = 1; viewSize < Propertys.Length - skipped; viewSize++)
                {
                    String joinedView = String.Concat(Propertys.Skip(skipped).Take(viewSize)) + "View";
                    String joinedProperty = String.Concat(Propertys.Skip(viewSize + skipped));

                    title = GetViewTitles(joinedView, joinedProperty);
                    if (title != null)
                        return title;
                }
            }
            return null;
        }
        private static string GetResource(string type,string key)
        {
            return Resources.ContainsKey(type) ? Resources[type].GetString(key) : null;
        }
        private static string GetViewTitles(string type,string key)
        {
            return ViewTitles.ContainsKey(type) ? ViewTitles[type].GetString(key) : null;
        }
        private static string[] SplitCamelCase(string value)
        {
            return Regex.Split(value, "(?<!^)(?=[A-Z])");
        }
    }
}
