using FastExpressionCompiler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace FastExpressionCompilerTest
{
    class Program
    {
        private const int OperationsCount = 1000;

        static void Main(string[] args)
        {
            var text = "Use expression trees";
            Expression<Func<Cat, string>> sayMewExpression = cat => cat.SayMew(text);
            List<string> list = new List<string>();

            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < OperationsCount; i++)
            {
                MethodCallExpression body = sayMewExpression.Body as MethodCallExpression;
                Expression argument = body.Arguments[0];

                // () => (object)test;
                UnaryExpression converted = Expression.Convert(argument, typeof(object));
                Expression<Func<object>> lambda = Expression.Lambda<Func<object>>(converted);
                Func<object> func = lambda.Compile();

                string value = func() as string;
                list.Add(value);
            }

            Console.WriteLine($"{stopwatch.Elapsed} - Extracting using normal expression compiler");

            list = new List<string>();
            stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < OperationsCount; i++)
            {
                MethodCallExpression body = sayMewExpression.Body as MethodCallExpression;
                Expression argument = body.Arguments[0];

                // () => (object)test;
                UnaryExpression converted = Expression.Convert(argument, typeof(object));
                Expression<Func<object>> lambda = Expression.Lambda<Func<object>>(converted);
                Func<object> func = lambda.CompileFast();

                string value = func() as string;
                list.Add(value);
            }

            Console.WriteLine($"{stopwatch.Elapsed} - Extracting using fast expression compiler");

            list = new List<string>();
            stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < OperationsCount; i++)
            {
                MethodCallExpression body = sayMewExpression.Body as MethodCallExpression;
                Expression argument = body.Arguments[0];

                MemberExpression arguemntMemeber = argument as MemberExpression;
                ConstantExpression closureClassExpression = arguemntMemeber.Expression as ConstantExpression;
                object closureClass = closureClassExpression.Value;

                FieldInfo clasureClassFieldInfo = arguemntMemeber.Member as FieldInfo;
                string value = (string)clasureClassFieldInfo.GetValue(closureClass);
                list.Add(value);
            }

            Console.WriteLine($"{stopwatch.Elapsed} - Extracting with reflection");

            list = new List<string>();
            stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < OperationsCount; i++)
            {
                string value = NormalExpression.BuildExpression();
                list.Add(value);
            }

            Console.WriteLine($"{stopwatch.Elapsed} - Building with normal expression");

            list = new List<string>();
            stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < OperationsCount; i++)
            {
                string value = FastExpression.BuildExpression();
                list.Add(value);
            }

            Console.WriteLine($"{stopwatch.Elapsed} - Building with normal expression and using fast compile");
        }
    }
}
