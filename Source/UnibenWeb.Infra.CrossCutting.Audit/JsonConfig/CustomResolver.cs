using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

namespace UnibenWeb.Infra.CrossCutting.Audit.JsonConfig
{
    class CustomResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            if (((PropertyInfo)member).GetMethod.IsVirtual)
            {
                prop.ShouldSerialize = obj => false;
            }
            else
            {
                /*                if (prop.DeclaringType != typeof(PseudoContext) &&                    prop.PropertyType.IsClass &&                    prop.PropertyType != typeof(string))
                {                    prop.ShouldSerialize = obj => false;                }                                */
                if (prop.PropertyType.IsClass && (prop.PropertyType != typeof(string) && prop.PropertyType != typeof(DateTime)))
                {
                    prop.ShouldSerialize = obj => false;
                }
            }
            return prop;
        }
    }
}