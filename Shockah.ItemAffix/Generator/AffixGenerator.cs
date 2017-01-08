using Shockah.Utils;
using Shockah.Utils.Rule;
using Terraria;

namespace Shockah.ItemAffix.Generator
{
	public abstract class AffixGenerator<Env> : RuleManager<IWeightedRuleGroup<AffixGenInfo<Env>, Dynamic<Affix>>, AffixGenInfo<Env>, Dynamic<Affix>> where Env : AffixGenEnvironment
	{
		public AffixGenerator() : base(new WeightedRuleGroup<AffixGenInfo<Env>, Dynamic<Affix>>())
		{
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