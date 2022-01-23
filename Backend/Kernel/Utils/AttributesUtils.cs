using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Backend.Kernel.Utils
{
    public static class AttributesUtils
    {
        public static IEnumerable<Type> GetAllTypesWithAttribute(Type attributeType)
        {
            return from type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                from Attribute attribute in type.GetCustomAttributes(attributeType, true)
                select type;
        }

        /// <summary>
        /// Retrieve all methods with a given attribute from all types loaded in the current AppDomain.
        /// If classAttributeType is provided, the search will only look in classes having a corresponding attribute. Use classAttributeType
        /// to speed up the search by pruning most of the types.
        /// </summary>
        public static IEnumerable<MethodInfo> GetAllMethodsWithAttribute(Type methodAttributeType, Type classAttributeType = null)
        {
            return from method in AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes()
                        .Where(t => classAttributeType == null || t.GetCustomAttributes(classAttributeType, true).Any())
                        .SelectMany(t => t.GetMethods()))
                from Attribute attribute in method.GetCustomAttributes(methodAttributeType, true)
                select method;
        }
    }
}