using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Web;
using SimpleInjector.Advanced;

namespace C_18_01_Capstone.Web.Infrastructure.SimpleInjector
{
    public class ImportPropertySelectionBehavior : IPropertySelectionBehavior
    {
        public bool SelectProperty(Type implementationType, 
            PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributes
                (typeof(ImportAttribute)).Any();
        }
    }
}