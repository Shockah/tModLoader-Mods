using System;
using System.Collections.Generic;

namespace Shockah.Utils.Rule
{
	public interface IMultipliedRule<RuleType, Input, Output> : IRule<Input, Output> where RuleType : IRule<Input, Output>
	{
		RuleType GetRule();

		Dynamic<int> GetCount();

		IMultipliedRule<RuleType, Input, Output> WithCount(Func<int> count);

		IMultipliedRule<RuleType, Input, Output> WithCount(Dynamic<int> count);
	}

	public interface IMultipliedRule<Input, Output> : IMultipliedRule<IRule<Input, Output>, Input, Output>
	{
	}

	public class MultipliedRule<RuleType, Input, Output> : IMultipliedRule<RuleType, Input, Output> where RuleType : IRule<Input, Output>
	{
		public RuleType rule;
		public Dynamic<int> count;

		public MultipliedRule(RuleType rule, Func<int> count) : this(rule, (Dynamic<int>)count)
		{
		}

		public MultipliedRule(RuleType rule, Dynamic<int> count = null)
		{
			this.rule = rule;
			this.count = count ?? 1;
		}

		public IMultipliedRule<RuleType, Input, Output> WithCount(Func<int> count)
		{
			this.count = count;
			return this;
		}

		public IMultipliedRule<RuleType, Input, Output> WithCount(Dynamic<int> count)
		{
			this.count = count;
			return this;
		}

		public virtual object Clone()
		{
			MultipliedRule<RuleType, Input, Output> clone = new MultipliedRule<RuleType, Input, Output>(rule);
			clone.count = count;
			return clone;
		}

		public virtual List<Output> GetOutput(Input input, Random random)
		{
			List<Output> outputs = new List<Output>();
			int count = this.count;
			for (int i = 0; i < count; i++)
			{
				outputs.AddRange(rule.GetOutput(input, random));
			}
			return outputs;
		}

		public RuleType GetRule()
		{
			return rule;
		}

		public Dynamic<int> GetCount()
		{
			return count;
		}
	}

	public class MultipliedRule<Input, Output> : MultipliedRule<IRule<Input, Output>, Input, Output>, IMultipliedRule<Input, Output>
	{
		public MultipliedRule(IRule<Input, Output> rule, Dynamic<int> count = null) : base(rule, count)
		{
		}
	}

	public static class MultipliedRule
	{
		public static MultipliedRule<RuleType, Input, Output> Of<RuleType, Input, Output>(RuleType rule, Dynamic<int> count = null) where RuleType : IRule<Input, Output>
		{
			return new MultipliedRule<RuleType, Input, Output>(rule, count);
		}

		public static MultipliedRule<Input, Output> Of<Input, Output>(IRule<Input, Output> rule, Dynamic<int> count = null)
		{
			return new MultipliedRule<Input, Output>(rule, count);
		}

		public static MultipliedRule<RuleType, Input, Output> Of<RuleType, Input, Output>(RuleType rule, Func<int> count) where RuleType : IRule<Input, Output>
		{
			return new MultipliedRule<RuleType, Input, Output>(rule, count);
		}

		public static MultipliedRule<Input, Output> Of<Input, Output>(IRule<Input, Output> rule, Func<int> count)
		{
			return new MultipliedRule<Input, Output>(rule, count);
		}
	}
}
