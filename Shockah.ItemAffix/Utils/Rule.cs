using System;
using System.Collections.Generic;

namespace Shockah.ItemAffix.Utils
{
	public abstract class Rule<Input, Output>
	{
		public abstract List<Output> GetOutput(Input input, Random random);
	}

	public abstract class RuleGroup<RuleType, Input, Output> : Rule<Input, Output> where RuleType : Rule<Input, Output>
	{
		public readonly List<Rule<Input, Output>> subrules = new List<Rule<Input, Output>>();

		public RuleGroup<RuleType, Input, Output> With(params Rule<Input, Output>[] subrules)
		{
			this.subrules.AddRange(subrules);
			return this;
		}
	}

	public class AllRule<Input, Output> : RuleGroup<Rule<Input, Output>, Input, Output>
	{
		public override List<Output> GetOutput(Input input, Random random)
		{
			List<Output> outputs = new List<Output>();
			foreach (Rule<Input, Output> rule in subrules)
			{
				outputs.AddRange(rule.GetOutput(input, random));
			}
			return outputs;
		}
	}

	public class LimitedRule<Input, Output> : RuleGroup<Rule<Input, Output>, Input, Output>
	{
		public Dynamic<int> count = int.MaxValue;

		public LimitedRule<Input, Output> WithCount(Dynamic<int> count)
		{
			this.count = count;
			return this;
		}

		public override List<Output> GetOutput(Input input, Random random)
		{
			List<Output> outputs = new List<Output>();

			int countLeft = this.count;
			foreach (Rule<Input, Output> subrule in subrules)
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
	}

	public class WeightedRule<Input, Output> : RuleGroup<WeightRule<Input, Output>, Input, Output>
	{
		public Dynamic<int> count = 1;

		public WeightedRule<Input, Output> WithCount(Dynamic<int> count)
		{
			this.count = count;
			return this;
		}

		public override List<Output> GetOutput(Input input, Random random)
		{
			WeightedRandom<Rule<Input, Output>> weightedRandom = new WeightedRandom<Rule<Input, Output>>(random);
			foreach (WeightRule<Input, Output> weightRule in subrules)
			{
				weightedRandom.Add(weightRule.rule, weightRule.weight);
			}

			int count = this.count;
			if (count <= 0)
			{
				return new List<Output>();
			}
			else if (count == 1)
			{
				if (weightedRandom.Count == 0)
					return new List<Output>();
				return weightedRandom.Get().GetOutput(input, random);
			}
			else
			{
				List<Output> outputs = new List<Output>();
				for (int i = 0; i < count; i++)
				{
					if (weightedRandom.Count == 0)
						break;
					outputs.AddRange(weightedRandom.Get().GetOutput(input, random));
				}
				return outputs;
			}
		}
	}

	public class WeightRule<Input, Output> : Rule<Input, Output>
	{
		public readonly Rule<Input, Output> rule;
		public readonly double weight;

		public static WeightRule<TInput, TOutput> Of<TInput, TOutput>(Rule<TInput, TOutput> rule, double weight = 1)
		{
			return new WeightRule<TInput, TOutput>(rule, weight);
		}

		public WeightRule(Rule<Input, Output> rule, double weight = 1)
		{
			this.rule = rule;
			this.weight = weight;
		}

		public override List<Output> GetOutput(Input input, Random random)
		{
			return rule.GetOutput(input, random);
		}
	}

	public class ChanceRule<Input, Output> : Rule<Input, Output>
	{
		public readonly Rule<Input, Output> rule;
		public readonly double chance;

		public static ChanceRule<TInput, TOutput> Of<TInput, TOutput>(Rule<TInput, TOutput> rule, double chance)
		{
			return new ChanceRule<TInput, TOutput>(rule, chance);
		}

		public ChanceRule(Rule<Input, Output> rule, double chance)
		{
			this.rule = rule;
			this.chance = chance;
		}

		public override List<Output> GetOutput(Input input, Random random)
		{
			if (random.NextDouble() >= chance)
				return rule.GetOutput(input, random);
			else
				return new List<Output>();
		}
	}

	public class ConditionalRule<Input, Output> : Rule<Input, Output>
	{
		public readonly Rule<Input, Output> rule;
		public readonly Func<Input, bool> condition;

		public static ConditionalRule<TInput, TOutput> Of<TInput, TOutput>(Rule<TInput, TOutput> rule, Func<TInput, bool> condition)
		{
			return new ConditionalRule<TInput, TOutput>(rule, condition);
		}

		public ConditionalRule(Rule<Input, Output> rule, Func<Input, bool> condition)
		{
			this.rule = rule;
			this.condition = condition;
		}

		public override List<Output> GetOutput(Input input, Random random)
		{
			if (condition(input))
				return rule.GetOutput(input, random);
			else
				return new List<Output>();
		}
	}
}