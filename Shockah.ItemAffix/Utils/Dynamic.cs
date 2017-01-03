using System;

namespace Shockah.Affix.Utils
{
	public abstract class Dynamic<T>
	{
		public abstract T Value
		{
			get;
		}
		
		public static implicit operator T(Dynamic<T> self)
		{
			return self.Value;
		}

		public static implicit operator Dynamic<T>(T value)
		{
			return new DynamicValue<T>(value);
		}

		public static implicit operator Dynamic<T>(Func<T> @delegate)
		{
			return new DynamicDelegate<T>(@delegate);
		}
	}

	public class DynamicValue<T> : Dynamic<T>
	{
		public readonly T value;

		public override T Value => value;

		public DynamicValue(T value)
		{
			this.value = value;
		}
	}

	public class DynamicDelegate<T> : Dynamic<T>
	{
		public readonly Func<T> @delegate;

		public override T Value => @delegate();

		public DynamicDelegate(Func<T> @delegate)
		{
			this.@delegate = @delegate;
		}
	}
}