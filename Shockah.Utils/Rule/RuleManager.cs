using System;
using System.Collections.Generic;
using Terraria;

namespace Shockah.Utils.Rule
{
	public interface IRuleManager<out RuleType, in Input, Output> where RuleType : IRule<Input, Output>
	{
		Random GetRandom();

		RuleType GetRule();

		List<Output> GetOutput(Input input);
	}

	public interface IRuleManager<Input, Output> : IRuleManager<IRule<Input, Output>, Input, Output>
	{
	}

	public class RuleManager<RuleType, Input, Output> : IRuleManager<RuleType, Input, Output> where RuleType : IRule<Input, Output>
	{
		protected readonly Random random;
		public readonly RuleType rule;

		public RuleManager(RuleType rule, Random random = null)
		{
			this.rule = rule;
			this.random = random ?? (UnifiedRandomBridge)Main.rand;
		}

		public virtual Random GetRandom()
		{
			return random;
		}

		public virtual RuleType GetRule()
		{
			return rule;
		}

		public virtual List<Output> GetOutput(Input input)
		{
			return rule.GetOutput(input, GetRandom());
		}
	}

	public class RuleManager<Input, Output> : RuleManager<IRule<Input, Output>, Input, Output>
	{
		public RuleManager(IRule<Input, Output> rule = null, Random random = null) : base(rule ?? new RuleGroup<Input, Output>(), random)
		{
		}
	}
}