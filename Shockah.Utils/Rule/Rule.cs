using System;
using System.Collections.Generic;

namespace Shockah.Utils.Rule
{
	public interface IRule<in Input, Output> : ICloneable
	{
		List<Output> GetOutput(Input input, Random random);
	}

	public class RuleDelegate<Input, Output> : IRule<Input, Output>
	{
		public Func<Input, Random, List<Output>> @delegate;

		public RuleDelegate(Func<Input, Random, Output> @delegate) : this((input, random) => new List<Output> { @delegate(input, random) })
		{
		}

		public RuleDelegate(Func<Input, Random, List<Output>> @delegate)
		{
			this.@delegate = @delegate;
		}

		public virtual object Clone()
		{
			return new RuleDelegate<Input, Output>(@delegate);
		}

		public virtual List<Output> GetOutput(Input input, Random random)
		{
			return @delegate(input, random);
		}
	}
}
