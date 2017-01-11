using System;
using System.Collections.Generic;
using System.Linq;

namespace Shockah.Utils
{
	public static class Hooks
	{
		public static IEnumerable<R> Build<T, R>(List<T> list, Func<T, R> func) where R : class
		{
			List<Tuple<T, double>> order = new List<Tuple<T, double>>();
			foreach (T t in list)
			{
				CallOrderAttribute[] attribs = (func(t) as Delegate).Method.GetCustomAttributes(typeof(CallOrderAttribute), false) as CallOrderAttribute[];
				double f = attribs.Length == 1 && !double.IsNaN(attribs[0].order) ? attribs[0].order : CallOrderAttribute.DEFAULT;
				order.Add(new Tuple<T, double>(t, f));
			}
			return order.OrderBy(tuple => tuple.Item2).Select(tuple => func(tuple.Item1));
		}

		public static void Build<T, R>(out R[] hooks, List<T> list, Func<T, R> func) where R : class
		{
			hooks = Build(list, func).ToArray();
		}
	}

	[AttributeUsage(AttributeTargets.Method)]
	public sealed class CallOrderAttribute : Attribute
	{
		public const float DEFAULT = 0f;

		public readonly double order;

		public CallOrderAttribute(double order)
		{
			this.order = order;
		}
	}
}