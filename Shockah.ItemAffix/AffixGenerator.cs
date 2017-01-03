using Shockah.Affix.Utils;
using System;
using System.Collections.Generic;
using Terraria;

namespace Shockah.Affix.Content
{
	public class AffixGenerator
	{
		public readonly List<AffixGeneratorEntry> entries = new List<AffixGeneratorEntry>();

		public void Generate(Item item, AffixGeneratorSource source)
		{

		}
	}

	public class AffixGeneratorSource
	{
		protected readonly Dynamic<float> value;

		public AffixGeneratorSource(float value)
		{
			this.value = value;
		}

		public virtual float GetValue(Item item)
		{
			return value;
		}
	}

	public class AffixGeneratorEntry
	{
		public readonly float weight;
		public readonly Func<Affix> @delegate;

		public AffixGeneratorEntry(float weight, Func<Affix> @delegate)
		{
			this.weight = weight;
			this.@delegate = @delegate;
		}
	}
}
