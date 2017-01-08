using System;
using System.Collections.Generic;

namespace Shockah.Utils.Rule
{
	public sealed class EmptyRule<Input, Output> : IRule<Input, Output>
	{
		public object Clone()
		{
			return new EmptyRule<Input, Output>();
		}

		public List<Output> GetOutput(Input input, Random random)
		{
			return new List<Output>();
		}
	}
}
