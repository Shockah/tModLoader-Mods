using System;
using System.Collections.Generic;

namespace Shockah.ItemAffix.Utils
{
	public class RuleManager<Input, Output>
	{
		public readonly Random random;
		public readonly Rule<Input, Output> rule;

		public RuleManager(Rule<Input, Output> rule = null, Random random = null)
		{
			this.rule = rule ?? new AllRule<Input, Output>();
			this.random = random ?? new Random();
		}

		public virtual List<Output> GetOutput(Input input)
		{
			return rule.GetOutput(input, random);
		}
	}
}