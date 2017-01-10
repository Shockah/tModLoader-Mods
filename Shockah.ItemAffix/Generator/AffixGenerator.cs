using Shockah.Utils;
using Shockah.Utils.Rule;
using System;
using Terraria;

namespace Shockah.ItemAffix.Generator
{
	public abstract class AffixGenerator<Env> : RuleManager<IRuleGroup<AffixGenInfo<Env>, Dynamic<Affix>>, AffixGenInfo<Env>, Dynamic<Affix>> where Env : AffixGenEnvironment
	{
		private new Random random;

		public AffixGenerator() : base(new RuleGroup<AffixGenInfo<Env>, Dynamic<Affix>>())
		{
			random = base.random;
		}

		public void SetRandom(Random random)
		{
			this.random = random;
		}

		public override Random GetRandom()
		{
			return random;
		}

		public AffixGenerator<Env> With(params IRule<AffixGenInfo<Env>, Dynamic<Affix>>[] rules)
		{
			rule.With(rules);
			return this;
		}
	}

	public sealed class AffixGenInfo<Env> where Env : AffixGenEnvironment
	{
		public readonly Item item;
		public readonly Env environment;

		public AffixGenInfo(Item item, Env environment)
		{
			this.item = item;
			this.environment = environment;
		}
	}

	public abstract class AffixGenEnvironment
	{
	}
}