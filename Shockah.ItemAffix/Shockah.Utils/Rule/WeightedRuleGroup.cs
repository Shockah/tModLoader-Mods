using System;
using System.Collections.Generic;

namespace Shockah.Utils.Rule
{
	public interface IWeightedRuleGroup<RuleType, in Input, Output> : IRuleGroup<RuleType, Input, Output> where RuleType : IWeightRule<Input, Output>
	{
		bool ReturnUnique();

		Dynamic<int> GetCount();

		IWeightedRuleGroup<RuleType, Input, Output> WithUnique(bool unique);

		IWeightedRuleGroup<RuleType, Input, Output> WithCount(Func<int> count);

		IWeightedRuleGroup<RuleType, Input, Output> WithCount(Dynamic<int> count);
	}

	public interface IWeightedRuleGroup<Input, Output> : IWeightedRuleGroup<IWeightRule<Input, Output>, Input, Output>
	{
	}

	public class WeightedRuleGroup<RuleType, Input, Output> : RuleGroup<RuleType, Input, Output>, IWeightedRuleGroup<RuleType, Input, Output> where RuleType : IWeightRule<Input, Output>
	{
		public bool unique = true;
		public Dynamic<int> count = 1;

		public override object Clone()
		{
			WeightedRuleGroup<RuleType, Input, Output> clone = new WeightedRuleGroup<RuleType, Input, Output>();
			clone.unique = unique;
			clone.count = count;
			foreach (RuleType subrule in subrules)
			{
				clone.subrules.Add((RuleType)subrule.Clone());
			}
			return clone;
		}

		public IWeightedRuleGroup<RuleType, Input, Output> WithUnique(bool unique)
		{
			this.unique = unique;
			return this;
		}

		public IWeightedRuleGroup<RuleType, Input, Output> WithCount(Func<int> count)
		{
			this.count = count;
			return this;
		}

		public IWeightedRuleGroup<RuleType, Input, Output> WithCount(Dynamic<int> count)
		{
			this.count = count;
			return this;
		}

		public override List<Output> GetOutput(Input input, Random random)
		{
			List<RuleType> subrules = new List<RuleType>(this.subrules);

			int count = this.count;
			if (count <= 0)
			{
				return new List<Output>();
			}
			else
			{
				List<Output> outputs = new List<Output>();
				for (int i = 0; i < count; i++)
				{
					if (subrules.Count == 0)
						break;

					WeightedRandom<RuleType> weightedRandom = new WeightedRandom<RuleType>(random);
					foreach (RuleType weightRule in subrules)
					{
						double weight = weightRule.GetWeight(i, count);
						if (weight > 0)
							weightedRandom.Add(weightRule, weight);
					}

					if (weightedRandom.Count == 0)
						continue;

					RuleType subrule = weightedRandom.Get();
					outputs.AddRange(subrule.GetOutput(input, random));
					if (unique)
						subrules.Remove(subrule);
				}
				return outputs;
			}
		}

		public bool ReturnUnique()
		{
			return unique;
		}

		public Dynamic<int> GetCount()
		{
			return count;
		}
	}

	public class WeightedRuleGroup<Input, Output> : WeightedRuleGroup<IWeightRule<Input, Output>, Input, Output>, IWeightedRuleGroup<Input, Output>
	{
	}

	public static class WeightedRuleGroup
	{
		public static IWeightedRuleGroup<RuleType, Input, Output> Of<RuleType, Input, Output>(params RuleType[] rules) where RuleType : IWeightRule<Input, Output>
		{
			return new WeightedRuleGroup<RuleType, Input, Output>().With(rules) as WeightedRuleGroup<RuleType, Input, Output>;
		}

		public static IWeightedRuleGroup<IWeightRule<Input, Output>, Input, Output> Of<Input, Output>(params IWeightRule<Input, Output>[] rules)
		{
			return new WeightedRuleGroup<Input, Output>().With(rules) as WeightedRuleGroup<IWeightRule<Input, Output>, Input, Output>;
		}

		public static IWeightedRuleGroup<RuleType, Input, Output> Of<RuleType, Input, Output>(RuleType[] rules, Dynamic<int> count = null, bool unique = true) where RuleType : IWeightRule<Input, Output>
		{
			return new WeightedRuleGroup<RuleType, Input, Output>().WithUnique(unique).WithCount(count ?? 1).With(rules) as WeightedRuleGroup<RuleType, Input, Output>;
		}

		public static IWeightedRuleGroup<IWeightRule<Input, Output>, Input, Output> Of<Input, Output>(IWeightRule<Input, Output>[] rules, Dynamic<int> count = null, bool unique = true)
		{
			return new WeightedRuleGroup<Input, Output>().WithUnique(unique).WithCount(count ?? 1).With(rules) as WeightedRuleGroup<IWeightRule<Input, Output>, Input, Output>;
		}

		public static IWeightedRuleGroup<RuleType, Input, Output> Of<RuleType, Input, Output>(RuleType[] rules, Func<int> count, bool unique = true) where RuleType : IWeightRule<Input, Output>
		{
			return new WeightedRuleGroup<RuleType, Input, Output>().WithUnique(unique).WithCount(count).With(rules) as WeightedRuleGroup<RuleType, Input, Output>;
		}

		public static IWeightedRuleGroup<IWeightRule<Input, Output>, Input, Output> Of<Input, Output>(IWeightRule<Input, Output>[] rules, Func<int> count, bool unique = true)
		{
			return new WeightedRuleGroup<Input, Output>().WithUnique(unique).WithCount(count).With(rules) as WeightedRuleGroup<IWeightRule<Input, Output>, Input, Output>;
		}
	}
}