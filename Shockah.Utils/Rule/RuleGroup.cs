using System;
using System.Collections.Generic;

namespace Shockah.Utils.Rule
{
	public interface IRuleGroup<RuleType, in Input, Output> : IRule<Input, Output> where RuleType : IRule<Input, Output>
	{
		List<RuleType> GetSubrules();

		IRuleGroup<RuleType, Input, Output> With(params RuleType[] subrules);
	}

	public interface IRuleGroup<Input, Output> : IRuleGroup<IRule<Input, Output>, Input, Output>
	{
	}

	public class RuleGroup<RuleType, Input, Output> : IRuleGroup<RuleType, Input, Output> where RuleType : IRule<Input, Output>
	{
		protected readonly List<RuleType> subrules = new List<RuleType>();

		public virtual List<RuleType> GetSubrules()
		{
			return subrules;
		}

		public IRuleGroup<RuleType, Input, Output> With(params RuleType[] subrules)
		{
			this.subrules.AddRange(subrules);
			return this;
		}

		public virtual object Clone()
		{
			RuleGroup<RuleType, Input, Output> clone = new RuleGroup<RuleType, Input, Output>();
			foreach (RuleType subrule in subrules)
			{
				clone.subrules.Add((RuleType)subrule.Clone());
			}
			return clone;
		}

		public virtual List<Output> GetOutput(Input input, Random random)
		{
			List<Output> outputs = new List<Output>();
			foreach (IRule<Input, Output> rule in subrules)
			{
				outputs.AddRange(rule.GetOutput(input, random));
			}
			return outputs;
		}
	}

	public class RuleGroup<Input, Output> : RuleGroup<IRule<Input, Output>, Input, Output>, IRuleGroup<Input, Output>
	{
	}

	public static class RuleGroup
	{
		public static IRuleGroup<RuleType, Input, Output> Of<RuleType, Input, Output>(params RuleType[] rules) where RuleType : IRule<Input, Output>
		{
			return new RuleGroup<RuleType, Input, Output>().With(rules);
		}

		public static IRuleGroup<IRule<Input, Output>, Input, Output> Of<Input, Output>(params IRule<Input, Output>[] rules)
		{
			return new RuleGroup<Input, Output>().With(rules);
		}
	}
}