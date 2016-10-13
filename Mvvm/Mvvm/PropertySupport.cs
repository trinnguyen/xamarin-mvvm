using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Mvvm
{
	public static class PropertySupport
	{
		public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
		{
			if (propertyExpression == null)
				throw new ArgumentNullException(nameof(propertyExpression));

			return ExtractPropertyNameFromLambda(propertyExpression);
		}

		static string ExtractPropertyNameFromLambda(LambdaExpression expression)
		{
			if (expression == null)
				throw new ArgumentNullException(nameof(expression));

			var memberExpression = expression.Body as MemberExpression;
			if (memberExpression == null)
				throw new ArgumentNullException(nameof(expression));

			var property = memberExpression.Member as PropertyInfo;
			if (property == null)
				throw new ArgumentNullException(nameof(expression));

			var getMethod = property.GetMethod;
			if (getMethod.IsStatic)
				throw new ArgumentException(nameof(expression));

			return memberExpression.Member.Name;
		}
	}
}