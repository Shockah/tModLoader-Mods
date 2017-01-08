using System;
using System.Collections.Generic;

namespace Shockah.Utils.Rule
{
	public interface ILimitedRuleGroup<RuleType, in Input, Output> : IRuleGroup<RuleType, Input, Output> where RuleType : IRule<Input, Output>
	{
		Dynamic<int> GetCount();

		ILimitedRuleGroup<RuleType, Input, Output> WithCount(Func<int> count);

		ILimitedRuleGroup<RuleType, Input, Output> WithCount(Dynamic<int> count);
	}

	public interface ILimitedRuleGroup<Input, Output> : ILimitedRuleGroup<IRule<Input, Output>, Input, Output>, IRuleGroup<Input, Output>
	{
	}

	public class LimitedRuleGroup<RuleType, Input, Output> : RuleGroup<RuleType, Input, Output>, ILimitedRuleGroup<RuleType, Input, Output> where RuleType : IRule<Input, Output>
	{
		public Dynamic<int> count = 1;

		public override object Clone()
		{
			LimitedRuleGroup<RuleType, Input, Output> clone = new LimitedRuleGroup<RuleType, Input, Output>();
			clone.count = count;
			foreach (RuleType subrule in subrules)
			{
				clone.subrules.Add((RuleType)subrule.Clone());
			}
			return clone;
		}

		public ILimitedRuleGroup<RuleType, Input, Output> WithCount(Func<int> count)
		{
			this.count = count;
			return this;
		}

		public ILimitedRuleGroup<RuleType, Input, Output> WithCount(Dynamic<int> count)
		{
			this.count = count;
			return this;
		}

		public override List<Output> GetOutput(Input input, Random random)
		{
			List<Output> outputs = new List<Output>();

			int countLeft = this.count;
			foreach (IRule<Input, Output> subrule in subrules)
			{
				if (countLeft <= 0)
					break;

				List<Output> subruleOutputs = subrule.GetOutput(input, random);
				if (subruleOutputs.Count != 0)
				{
					countLeft--;
					outputs.AddRange(subruleOutputs);
				}
			}
			return outputs;
		}

		public Dynamic<int> GetCount()
		{
			return count;
		}
	}

	public class LimitedRuleGroup<Input, Output> : LimitedRuleGroup<IRule<Input, Output>, Input, Output>, ILimitedRuleGroup<IRule<Input, Output>, Input, Output>
	{
	}

	public static class LimitedRuleGroup
	{
		public static ILimitedRuleGroup<RuleType, Input, Output> Of<RuleType, Input, Output>(params RuleType[] rules) where RuleType : IRule<Input, Output>
		{
			return new LimitedRuleGroup<RuleType, Input, Output>().With(rules) as LimitedRuleGroup<RuleType, Input, Output>;
		}

		public static ILimitedRuleGroup<IRule<Input, Output>, Input, Output> Of<Input, Output>(params IRule<Input, Output>[] rules)
		{
			return new LimitedRuleGroup<Input, Output>().With(rules) as LimitedRuleGroup<IRule<Input, Output>, Input, Output>;
		}

		public static ILimitedRuleGroup<RuleType, Input, Output> Of<RuleType, Input, Output>(RuleType[] rules, Dynamic<int> count = null) where RuleType : IRule<Input, Output>
		{
			return new LimitedRuleGroup<RuleType, Input, Output>().WithCount(count ?? 1).With(rules) as LimitedRuleGroup<RuleType, Input, Output>;
		}

		public static ILimitedRuleGroup<IRule<Input, Output>, Input, Output> Of<Input, Output>(IRule<Input, Output>[] rules, Dynamic<int> count = null)
		{
			return new LimitedRuleGroup<Input, Output>().WithCount(count ?? 1).With(rules) as LimitedRuleGroup<IRule<Input, Output>, Input, Output>;
		}

		public static ILimitedRuleGroup<RuleType, Input, Output> Of<RuleType, Input, Output>(RuleType[] rules, Func<int> count) where RuleType : IRule<Input, Output>
		{
			return new LimitedRuleGroup<RuleType, Input, Output>().WithCount(count).With(rules) as LimitedRuleGroup<RuleType, Input, Output>;
		}

		public static ILimitedRuleGroup<IRule<Input, Output>, Input, Output> Of<Input, Output>(IRule<Input, Output>[] rules, Func<int> count)
		{
			return new LimitedRuleGroup<Input, Output>().WithCount(count).With(rules) as LimitedRuleGroup<IRule<Input, Output>, Input, Output>;
		}
	}
}