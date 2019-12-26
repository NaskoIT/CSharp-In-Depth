using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionTreesPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            Battery battery = new Battery();
            Func<Battery, int> useEnergyFunc = b => b.UseEnergy(100);
            Action<Battery> rechargeBatteryFunc = b => b.Recharge(100);
            Func<Battery, bool> hasEnergyFunc = b => b.HasEnergy;
            int result = useEnergyFunc(battery);

            Expression<Func<Battery, int>> useEnergyExpression = b => b.UseEnergy(100);
            Expression<Action<Battery>> rechargeBatteryExpression = b => b.Recharge(100);
            Expression<Func<Battery, bool>> hasEnergyExpression = b => b.HasEnergy;

            ParseExpression(useEnergyExpression);
            ParseExpression(hasEnergyExpression);
            ParseExpression(rechargeBatteryExpression);

            Console.WriteLine();
            BuildExpression();
        }

        private static void BuildExpression()
        {
            Type batteryType = typeof(Battery);
            //We we compile this expression runtime
            //Expression<Func<Battery, string>> getEnergyStatusExpression = b => b.GetEnergyStatus(100, "Fully charged");

            ConstantExpression intConstantExpression = Expression.Constant(100);
            ConstantExpression stringConstantExpression = Expression.Constant("Fully charged");
            ParameterExpression parameterExpression = Expression.Parameter(batteryType, "b");
            MethodInfo methodInfo = batteryType.GetMethod(nameof(Battery.GetEnergyStatus));
            MethodCallExpression methodCallExpression = Expression.Call(parameterExpression, methodInfo, intConstantExpression, stringConstantExpression);
            Expression<Func<Battery, string>> getEnergyStatusExpression = Expression.Lambda<Func<Battery, string>>(methodCallExpression, parameterExpression);

            Func<Battery, string> getEnergyStatusFunc = getEnergyStatusExpression.Compile();
            string result = getEnergyStatusFunc(new Battery());
            Console.WriteLine("New expression built runtime gives result: " + result);

            ////If we do not know the type of the arguemnts compile time
            //LambdaExpression getEnergyStatusExpression = Expression.Lambda(methodCallExpression, parameterExpression);
            //Delegate getEnergyStatusDelegate = getEnergyStatusExpression.Compile();
            //var batteryInstance = Activator.CreateInstance(batteryType);
            //string result = (string)getEnergyStatusDelegate.DynamicInvoke(batteryInstance);
            //Console.WriteLine($"New expression built runtime and do not know the arguments' types compile time gives result: " + result);
        }

        private static void ParseExpression(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Lambda)
            {
                Console.Write("Lambda: ");
                LambdaExpression lambdaExpression = (LambdaExpression)expression;
                Expression lambdaExpressionBody = lambdaExpression.Body;
                Console.WriteLine(string.Join(", ", lambdaExpression.Parameters));
                ParseExpression(lambdaExpressionBody);
            }
            else if (expression.NodeType == ExpressionType.Call)
            {
                Console.Write("Method: ");
                MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
                Console.WriteLine(methodCallExpression.Method.Name);

                foreach (var argument in methodCallExpression.Arguments)
                {
                    ParseExpression(argument);
                }
            }
            else if (expression.NodeType == ExpressionType.MemberAccess)
            {
                Console.Write("Property: ");
                MemberExpression memberExpression = (MemberExpression)expression;
                Console.WriteLine(memberExpression.Member.Name);
            }
            else if (expression.NodeType == ExpressionType.Constant)
            {
                Console.Write("Constant: ");
                ConstantExpression constantExpression = (ConstantExpression)expression;
                Console.WriteLine(constantExpression.Value);
            }
        }
    }
}
