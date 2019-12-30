using FastExpressionCompiler;
using System;
using System.Linq.Expressions;

namespace FastExpressionCompilerTest
{
    public class FastExpression
    {
        private static readonly Type catClassType = typeof(Cat);
        public static string BuildExpression()
        {
            //cat => cat.SayMew("test");
            ParameterExpression parameter = Expression.Parameter(catClassType, "cat");
            ConstantExpression methodParameter = Expression.Constant("test");
            MethodCallExpression body = Expression.Call(parameter, catClassType.GetMethod(nameof(Cat.SayMew)), methodParameter);
            Expression<Func<Cat, string>> lambdaExpression = Expression.Lambda<Func<Cat, string>>(body, parameter);

            Func<Cat, string> func = lambdaExpression.CompileFast();
            return func(new Cat());
        }
    }
}
