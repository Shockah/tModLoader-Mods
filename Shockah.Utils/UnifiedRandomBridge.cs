using System;
using Terraria.Utilities;

namespace Shockah.Utils
{
	public sealed class UnifiedRandomBridge : Random
	{
		public readonly UnifiedRandom random;

		public UnifiedRandomBridge(UnifiedRandom random)
		{
			this.random = random;
		}

		public override int Next()
		{
			return random.Next();
		}

		public override int Next(int maxValue)
		{
			return random.Next(maxValue);
		}

		public override int Next(int minValue, int maxValue)
		{
			return random.Next(minValue, maxValue);
		}

		public override void NextBytes(byte[] buffer)
		{
			random.NextBytes(buffer);
		}

		public override double NextDouble()
		{
			return random.NextDouble();
		}

		public static implicit operator UnifiedRandomBridge(UnifiedRandom random)
		{
			return new UnifiedRandomBridge(random);
		}
	}
}
