using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ObjectFactoryWithExpressions
{
    public static class ObjectFactory
    {
        public static T CreateInstance<T>() where T : new() => New<T>.Instance();

        public static object CreateInstance<TArg1>(Type type, TArg1 arg1)
            => CreateInstance<TArg1, TypeToIgnore>(type, arg1, null);

        public static object CreateInstance<TArg1, TArg2>(Type type, TArg1 arg1, TArg2 arg2)
            => CreateInstance<TArg1, TArg2, TypeToIgnore>(type, arg1, arg2, null);

        // (TArg1 arg1, TArg2 arg2, TArg3 arg3  ) => new Type(arg1, arg2, arg3);
        public static object CreateInstance<TArg1, TArg2, TArg3>(Type type, TArg1 arg1, TArg2 arg2, TArg3 arg3)
            => ObjectFactoryCreator<TArg1, TArg2, TArg3>.CreateInstance(type, arg1, arg2, arg3);

        private class TypeToIgnore { }

        private static class ObjectFactoryCreator<TArg1, TArg2, TArg3>
        {
            private static readonly ConcurrentDictionary<Type, Func<TArg1, TArg2, TArg3, object>> objectFactoryCache =
                new ConcurrentDictionary<Type, Func<TArg1, TArg2, TArg3, object>>();
            
            private static readonly Type typeToIgnore = typeof(TypeToIgnore);

            public static object CreateInstance(Type type, TArg1 arg1, TArg2 arg2, TArg3 arg3)
            {
                Func<TArg1, TArg2, TArg3, object> objectCreatorFunc = objectFactoryCache.GetOrAdd(type, _ =>
                {
                    Type[] argumentTypes = new[]
                    {
                        typeof(TArg1),
                        typeof(TArg2),
                        typeof(TArg3),
                    };

                    Type[] constructorArgumentTypes = argumentTypes.Where(type => type != typeToIgnore).ToArray();

                    ConstructorInfo constructor = type.GetConstructor(constructorArgumentTypes);
                    if (constructor == null)
                    {
                        throw new InvalidOperationException($"{type.Name} has no constructor for the provided arguments' types: {string.Join(", ", constructorArgumentTypes.Select(type => type.Name))}");
                    }

                    //(TArg1 arg1, TArg2 arg2, TArg3 arg3)
                    ParameterExpression[] expressionParameters = argumentTypes
                        .Select((argumentType, i) => Expression.Parameter(argumentType, $"arg{i}"))
                        .ToArray();

                    ParameterExpression[] constructorExpressionParameters = expressionParameters
                        .Take(constructorArgumentTypes.Length)
                        .ToArray();

                    // new Type(arg1, arg2, arg3); or new Type(arg1, arg2, arg3); if we use overload with two parameters
                    NewExpression newExpression = Expression.New(constructor, constructorExpressionParameters);

                    //(TArg1 arg1, TArg2 arg2, TArg3 arg3) => new Type(arg1, arg2, arg3);
                    Expression<Func<TArg1, TArg2, TArg3, object>> lambdaExpression = Expression
                        .Lambda<Func<TArg1, TArg2, TArg3, object>>(newExpression, expressionParameters);

                    return objectCreatorFunc = lambdaExpression.Compile();
                });

                return objectCreatorFunc(arg1, arg2, arg3);
            }
        }
    }

}
