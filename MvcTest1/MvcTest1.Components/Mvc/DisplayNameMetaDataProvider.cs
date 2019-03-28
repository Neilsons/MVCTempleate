using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using MvcTest1.Resource;

namespace MvcTest1.Components.Mvc
{
    public class DisplayNameMetaDataProvider:DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var MetaData = base.CreateMetadata(attributes,containerType,modelAccessor,modelType,propertyName);
            MetaData.DisplayName = containerType == null ? null : ResourceProvider.GetPropertyTitle(containerType, propertyName);
            return MetaData;
        }
    }
}
