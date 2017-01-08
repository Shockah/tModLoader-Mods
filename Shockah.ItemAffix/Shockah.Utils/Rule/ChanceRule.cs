using System;
using System.Collections.Generic;

namespace Shockah.Utils.Rule
{
	public interface IChanceRule<RuleType, in Input, Output> : IRuleGroup<RuleType, Input, Output> where RuleType : IRule<Input, Output>
	{
		RuleType GetRule();

		Dynamic<double> GetChance();
	}

	public interface IChanceRule<Input, Output> : IChanceRule<IRule<Input, Output>, Input, Output>
	{
	}

	public class ChanceRule<RuleType, Input, Output> : RuleGroup<RuleType, Input, Output>, IChanceRule<RuleType, Input, Output> where RuleType : IRule<Input, Output>
	{
		public RuleType rule;
		public Dynamic<double> chance;

		public ChanceRule(RuleType rule, Func<double> chance) : this(rule, (Dynamic<double>)chance)
		{
		}

		public ChanceRule(RuleType rule, Dynamic<double> chance)
		{
			this.rule = rule;
			this.chance = chance;
		}

		public override object Clone()
		{
			return new ChanceRule<RuleType, Input, Output>(rule, chance);
		}

		public override List<Output> GetOutput(Input input, Random random)
		{
			if (random.NextDouble() >= chance)
				return rule.GetOutput(input, random);
			else
				return new List<Output>();
		}

		public RuleType GetRule()
		{
			return rule;
		}

		public Dynamic<double> GetChance()
		{
			return chance;
		}
	}

	public class ChanceRule<Input, Output> : ChanceRule<IRule<Input, Output>, Input, Output>, IChanceRule<Input, Output>
	{
		public ChanceRule(IRule<Input, Output> rule, Dynamic<double> chance) : base(rule, chance)
		{
		}

		public ChanceRule(IRule<Input, Output> rule, Func<double> chance) : base(rule, chance)
		{
		}
	}

	public static class ChanceRule
	{
		public static ChanceRule<RuleType, Input, Output> Of<RuleType, Input, Output>(RuleType rule, Dynamic<double> chance = null) where RuleType : IRule<Input, Output>
		{
			return new ChanceRule<RuleType, Input, Output>(rule, chance ?? 1);
		}

		public static ChanceRule<Input, Output> Of<Input, Output>(IRule<Input, Output> rule, Dynamic<double> chance = null)
		{
			return new ChanceRule<Input, Output>(rule, chance ?? 1);
		}

		public static ChanceRule<RuleType, Input, Output> Of<RuleType, Input, Output>(RuleType rule, Func<double> chance) where RuleType : IRule<Input, Output>
		{
			return new ChanceRule<RuleType, Input, Output>(rule, chance);
		}

		public static ChanceRule<Input, Output> Of<Input, Output>(IRule<Input, Output> rule, Func<double> chance)
		{
			return new ChanceRule<Input, Output>(rule, chance);
		}
	}
}