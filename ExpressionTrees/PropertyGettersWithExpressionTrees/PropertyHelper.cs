using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace PropertyGettersWithExpressionTrees
{
    public class PropertyHelper
    {
        private static readonly Type typeOfObject = typeof(object);
        private static readonly ConcurrentDictionary<Type, List<PropertyHelper>> cache = new ConcurrentDictionary<Type, List<PropertyHelper>>();

        public string Name { get; set; }

        public object Value { get; set; }

        public static List<PropertyHelper> GetProperties(object insatnce)
        {
            Type instanceType = insatnce.GetType();
            return cache.GetOrAdd(instanceType, _ =>
            {
                List<PropertyHelper> properties = new List<PropertyHelper>();

                foreach (PropertyInfo property in instanceType.GetProperties())
                {
                    // object obj
                    ParameterExpression parameter = Expression.Parameter(typeOfObject, "obj");
                    // (T)obj
                    UnaryExpression convertedParameter = Expression.Convert(parameter, instanceType);
                    // ((T)obj).Property
                    MemberExpression body = Expression.MakeMemberAccess(convertedParameter, property);
                    // (object)((T)obj).Property
                    UnaryExpression convertedProperty = Expression.Convert(body, typeOfObject);
                    // object obj => (object)((T)obj).Property
                    Expression<Func<object, object>> lambdaExpression = Expression.Lambda<Func<object, object>>(convertedProperty, parameter);
                    
                    Func<object, object> propetyGetterFunc = lambdaExpression.Compile();

                    properties.Add(new PropertyHelper
                    {
                        Name = property.Name,
                        Value = propetyGetterFunc.Invoke(insatnce)
                    });
                }

                return properties;
            });
        }
    }
}
