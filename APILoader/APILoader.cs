using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using FvTech.Api;

namespace APIHelpers
{
    public class APILoader
    {
        public static List<string> LoadFromPath(string apiPath)
        {
            string apiName = string.Empty;
            var apiList = new List<string>();

            // load assembly from path
            Assembly apiAssembly = Assembly.LoadFrom(apiPath);
            apiName = apiAssembly.FullName.Substring(0, apiAssembly.FullName.IndexOf(','));

            // get the class which impliments IBusinessRules interface
            IEnumerable<Type> types = apiAssembly.GetTypes().Where(t => (typeof(IBusinessRules)).IsAssignableFrom(t));
            
            var constFields = new List<FieldInfo>();
            // loop all the types to get const fiels and retrieve their values
            foreach (Type t in types)
            {
                //var fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                //| BindingFlags.Static);
                t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                    .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType.Equals(typeof(string)))
                    .ToList()
                    .ForEach(fi => apiList.Add(apiName + "." + fi.GetValue(Activator.CreateInstance(t))));                
            }

            return apiList;
        }
    }
}
