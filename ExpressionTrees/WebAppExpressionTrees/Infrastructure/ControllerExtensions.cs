using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace WebAppExpressionTrees.Infrastructure
{
    public static class ControllerExtensions
    {
        public static IActionResult RedirectTo<TController>(this Controller controller, Expression<Action<TController>> redirectExpression)
        {
            if(redirectExpression.Body.NodeType != ExpressionType.Call)
            {
                throw new InvalidOperationException($"The provided expression is not a valid method: {redirectExpression.Body}");
            }

            MethodCallExpression methodCallExpression = (MethodCallExpression)redirectExpression.Body;
            string actionName = GetActionName(methodCallExpression.Method);
            string controllerName = typeof(TController).Name.Replace(nameof(Controller), string.Empty);
            RouteValueDictionary routeValues = ExtractRouteValues(methodCallExpression);

            return controller.RedirectToAction(actionName, controllerName, routeValues); 
        }

        private static string GetActionName(MethodInfo methodInfo)
        {
            string methodName = methodInfo.Name;
            //If we have [ActionName("SomeActionName")] attribute on some method in some controller
            string actionName = methodInfo
                .GetCustomAttributes(true)
                .OfType<ActionNameAttribute>()
                .FirstOrDefault()?.Name;

            return actionName ?? methodName;
        }

        private static RouteValueDictionary ExtractRouteValues(MethodCallExpression expression)
        {
            string[] routeValueNames = expression.Method.GetParameters().Select(p => p.Name).ToArray();
            object[] routeValues = expression.Arguments
                .Select(argument =>
                {
                    if(argument is ConstantExpression constantExpression)
                    {
                        return constantExpression.Value;
                    }
                   
                    if(argument.NodeType == ExpressionType.MemberAccess)
                    {
                        MemberExpression memberExpression = (MemberExpression)argument;
                        if(memberExpression.Member is FieldInfo)
                        {
                            ConstantExpression constantAccessExpression = (ConstantExpression)memberExpression.Expression;
                            if(constantAccessExpression != null)
                            {
                                string innerMemberName = memberExpression.Member.Name;
                                var compiledLambdaScopeField = constantAccessExpression.Value.GetType().GetField(innerMemberName);
                                return compiledLambdaScopeField.GetValue(constantAccessExpression.Value);
                            }
                        }
                    }

                    //() => (object)argument;
                    UnaryExpression convertExpression = Expression.Convert(argument, typeof(object));
                    Expression<Func<object>> funcExpression = Expression.Lambda<Func<object>>(convertExpression);
                    Func<object> func = funcExpression.Compile();
                    object result = func.Invoke();
                    return result;
                })
                .ToArray();

            RouteValueDictionary routeValueDictionaty = new RouteValueDictionary();

            for (int i = 0; i < routeValueNames.Length; i++)
            {
                routeValueDictionaty.Add(routeValueNames[i], routeValues[i]);
            }

            return routeValueDictionaty;
        }
    }
}
