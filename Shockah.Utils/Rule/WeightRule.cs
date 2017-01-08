using System;
using System.Collections.Generic;

namespace Shockah.Utils.Rule
{
	public interface IWeightRule<out RuleType, in Input, Output> : IRule<Input, Output> where RuleType : IRule<Input, Output>
	{
		RuleType GetRule();

		Func<int, int, double> GetWeight();

		double GetWeight(int item, int outOf);
	}

	public interface IWeightRule<Input, Output> : IWeightRule<IRule<Input, Output>, Input, Output>
	{
	}

	public class WeightRule<RuleType, Input, Output> : IWeightRule<RuleType, Input, Output> where RuleType : IRule<Input, Output>
	{
		public RuleType rule;
		public Func<int, int, double> weight;

		public WeightRule(RuleType rule, double weight = 1) : this(rule, (item, outOf) => weight)
		{
		}

		public WeightRule(RuleType rule, Func<double> weight) : this(rule, (item, outOf) => weight())
		{
		}

		public WeightRule(RuleType rule, Func<int, int, double> weight)
		{
			this.rule = rule;
			this.weight = weight;
		}

		public virtual object Clone()
		{
			return new WeightRule<RuleType, Input, Output>(rule, weight);
		}

		public virtual List<Output> GetOutput(Input input, Random random)
		{
			return rule.GetOutput(input, random);
		}

		public virtual RuleType GetRule()
		{
			return rule;
		}

		public virtual Func<int, int, double> GetWeight()
		{
			return weight;
		}

		public virtual double GetWeight(int item, int outOf)
		{
			return weight(item, outOf);
		}
	}

	public class WeightRule<Input, Output> : WeightRule<IRule<Input, Output>, Input, Output>, IWeightRule<Input, Output>
	{
		public WeightRule(IRule<Input, Output> rule, Func<int, int, double> weight) : base(rule, weight)
		{
		}

		public WeightRule(IRule<Input, Output> rule, Func<double> weight) : base(rule, weight)
		{
		}

		public WeightRule(IRule<Input, Output> rule, double weight = 1) : base(rule, weight)
		{
		}
	}

	public static class WeightRule
	{
		public static WeightRule<RuleType, Input, Output> Of<RuleType, Input, Output>(RuleType rule, Func<int, int, double> weight) where RuleType : IRule<Input, Output>
		{
			return new WeightRule<RuleType, Input, Output>(rule, weight);
		}

		public static WeightRule<Input, Output> Of<Input, Output>(IRule<Input, Output> rule, Func<int, int, double> weight)
		{
			return new WeightRule<Input, Output>(rule, weight);
		}

		public static WeightRule<RuleType, Input, Output> Of<RuleType, Input, Output>(RuleType rule, Func<double> weight = null) where RuleType : IRule<Input, Output>
		{
			return new WeightRule<RuleType, Input, Output>(rule, weight);
		}

		public static WeightRule<Input, Output> Of<Input, Output>(IRule<Input, Output> rule, Func<double> weight)
		{
			return new WeightRule<Input, Output>(rule, weight);
		}

		public static WeightRule<RuleType, Input, Output> Of<RuleType, Input, Output>(RuleType rule, double weight = 1) where RuleType : IRule<Input, Output>
		{
			return new WeightRule<RuleType, Input, Output>(rule, weight);
		}

		public static WeightRule<Input, Output> Of<Input, Output>(IRule<Input, Output> rule, double weight = 1)
		{
			return new WeightRule<Input, Output>(rule, weight);
		}
	}

	public static class WeightRules
	{
		public static WeightRule<RuleType, Input, Output>[] Of<RuleType, Input, Output>(params WeightRule<RuleType, Input, Output>[] rules) where RuleType : IRule<Input, Output>
		{
			return rules;
		}

		public static WeightRule<Input, Output>[] Of<Input, Output>(params WeightRule<Input, Output>[] rules)
		{
			return rules;
		}
	}
}