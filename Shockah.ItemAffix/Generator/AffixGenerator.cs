using Shockah.ItemAffix.Utils;
using Terraria;

namespace Shockah.ItemAffix.Generator
{
	public abstract class AffixGenerator<Env> : RuleManager<AffixGenInfo<Env>, Affix> where Env : AffixGenEnvironment
	{
		protected readonly LimitedRule<AffixGenInfo<Env>, Affix> limitedRule;

		public AffixGenerator() : base(new LimitedRule<AffixGenInfo<Env>, Affix>())
		{
			limitedRule = rule as LimitedRule<AffixGenInfo<Env>, Affix>;
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