using System;
using System.Collections.Generic;

namespace Shockah.Utils.Rule
{
	public interface IConditionalRule<RuleType, in Input, Output> : IRuleGroup<RuleType, Input, Output> where RuleType : IRule<Input, Output>
	{
		RuleType GetRule();

		Func<Input, bool> GetCondition();
	}

	public interface IConditionalRule<Input, Output> : IConditionalRule<IRule<Input, Output>, Input, Output>
	{
	}

	public class ConditionalRule<RuleType, Input, Output> : RuleGroup<RuleType, Input, Output>, IConditionalRule<RuleType, Input, Output> where RuleType : IRule<Input, Output>
	{
		public RuleType rule;
		public Func<Input, bool> condition;

		public ConditionalRule(RuleType rule, Func<Input, bool> condition)
		{
			this.rule = rule;
			this.condition = condition;
		}

		public override object Clone()
		{
			return new ConditionalRule<RuleType, Input, Output>(rule, condition);
		}

		public override List<Output> GetOutput(Input input, Random random)
		{
			if (condition(input))
				return rule.GetOutput(input, random);
			else
				return new List<Output>();
		}

		public RuleType GetRule()
		{
			return rule;
		}

		public Func<Input, bool> GetCondition()
		{
			return condition;
		}
	}

	public class ConditionalRule<Input, Output> : ConditionalRule<IRule<Input, Output>, Input, Output>, IConditionalRule<Input, Output>
	{
		public ConditionalRule(IRule<Input, Output> rule, Func<Input, bool> condition) : base(rule, condition)
		{
		}
	}

	public static class ConditionalRule
	{
		public static ConditionalRule<RuleType, Input, Output> Of<RuleType, Input, Output>(RuleType rule, Func<Input, bool> condition) where RuleType : IRule<Input, Output>
		{
			return new ConditionalRule<RuleType, Input, Output>(rule, condition);
		}

		public static ConditionalRule<Input, Output> Of<Input, Output>(IRule<Input, Output> rule, Func<Input, bool> condition)
		{
			return new ConditionalRule<Input, Output>(rule, condition);
		}
	}
}