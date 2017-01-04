using System;
using System.Collections.Generic;
using Shockah.Affix.Utils;
using Terraria;

namespace Shockah.Affix.Content
{
	public abstract class AffixGenerator<T> where T : AffixGeneratorEnvironment
	{
		public readonly List<Dynamic<GeneratedAffix>> affixes = new List<Dynamic<GeneratedAffix>>();

		public abstract float GetTotalScore(Item item, T environment);

		public abstract List<Affix> GenerateAffixes(Item item, T environment);
	}

	public class AffixGeneratorDelegate<T> : AffixGenerator<T> where T : AffixGeneratorEnvironment
	{
		protected readonly Func<Item, T, float> getTotalScore;
		protected readonly Func<Item, T, float, List<Affix>> generateAffixes;

		public AffixGeneratorDelegate(Func<Item, T, float> getTotalScore, Func<Item, T, float, List<Affix>> generateAffixes)
		{
			this.getTotalScore = getTotalScore;
			this.generateAffixes = generateAffixes;
		}

		public override float GetTotalScore(Item item, T environment)
		{
			return getTotalScore(item, environment);
		}

		public override List<Affix> GenerateAffixes(Item item, T environment)
		{
			return generateAffixes(item, environment, GetTotalScore(item, environment));
		}
	}
}