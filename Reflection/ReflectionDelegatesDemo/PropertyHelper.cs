using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace ReflectionDelegatesDemo
{
    public class PropertyHelper
    {
        private static readonly IDictionary<string, Delegate> cache = new ConcurrentDictionary<string, Delegate>();
        private static readonly MethodInfo CallInnerDelegateMethod =
            typeof(PropertyHelper).GetMethod(nameof(CallInnerDelegate), BindingFlags.NonPublic | BindingFlags.Static);

        public static Func<object, TResult> MakeFastPropertyGetter<TResult>(PropertyInfo property)
        {
            Type declaringClassType = property.DeclaringType;
            string key = $"{property.Name}:{declaringClassType.Name}";

            //If the delegate has been created, we get it from the cache
            if (cache.ContainsKey(key))
            {
                return (Func<object, TResult>)cache[key];
            }

            MethodInfo getMethod = property.GetMethod;
            Type typeOfResult = typeof(TResult);

            //The slowest operation is creating the delegate
            //Func<ControlllerType, TResult>
            Type getMethodDelegateType = typeof(Func<,>).MakeGenericType(declaringClassType, typeOfResult);
            Delegate getMethodDelegate = getMethod.CreateDelegate(getMethodDelegateType);

            //CallInnerDelegate<ControllerType, TResult>
            MethodInfo callInnerDelegateGenericType = CallInnerDelegateMethod.MakeGenericMethod(declaringClassType, typeOfResult);
            var result = (Func<object, TResult>)callInnerDelegateGenericType.Invoke(null, new[] { getMethodDelegate });
            
            //So we create the delegate only once and cache it
            cache.Add(key, result);
            return result;
        }

        private static Func<object, TResult> CallInnerDelegate<TClass, TResult>(Func<TClass, TResult> innerDelegate)
        {
            return (object instance) => innerDelegate((TClass)instance);
        }
    }
}
