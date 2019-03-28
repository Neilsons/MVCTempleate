using MvcTest1.Components.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

[assembly:WebActivatorEx.PostApplicationStartMethod(typeof(MvcTest1.App_Start.Mvc.ExtentionActivator),"Start")]
namespace MvcTest1.App_Start.Mvc
{
    public static  class ExtentionActivator
    {
        public  static void Start()
        {
            ModelMetadataProviders.Current = new DisplayNameMetaDataProvider();
        }
    }
}