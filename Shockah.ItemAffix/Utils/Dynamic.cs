﻿using System;
using Terraria.ModLoader.IO;

namespace Shockah.Affix.Utils
{
	public abstract class Dynamic<T> : TagSerializable
	{
		public abstract T Value
		{
			get;
		}

		public virtual void SerializeData(TagCompound tag)
		{
			throw new NotImplementedException();
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
		public static readonly TagDeserializer<DynamicValue<T>> DESERIALIZER = new TagDeserializer<DynamicValue<T>>(tag =>
		{
			return new DynamicValue<T>((T)tag["value"]);
		});

		public readonly T value;

		public override T Value => value;

		public DynamicValue(T value)
		{
			this.value = value;
		}

		public override void SerializeData(TagCompound tag)
		{
			tag["value"] = value;
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

	public class DynamicIntRange : Dynamic<int>
	{
		public static readonly TagDeserializer<DynamicIntRange> DESERIALIZER = new TagDeserializer<DynamicIntRange>(tag => {
			return new Tuple<int, int>(tag.GetInt("a"), tag.GetInt("b"));
		});

		public readonly Tuple<int, int> range;
		public readonly Random random;

		public override int Value => random.Next(Math.Min(range.Item1, range.Item2), Math.Max(range.Item1, range.Item2) + 1);

		public DynamicIntRange(Tuple<int, int> range, Random random = null)
		{
			this.range = range;
			this.random = random ?? new Random();
		}

		public static implicit operator DynamicIntRange(Tuple<int, int> range)
		{
			return new DynamicIntRange(range);
		}

		public override void SerializeData(TagCompound tag)
		{
			tag["a"] = range.Item1;
			tag["b"] = range.Item2;
		}
	}
}