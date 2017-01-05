using System;
using System.Collections.Generic;

namespace Shockah.Affix
{
	public sealed class WeightedRandom<T>
	{
		private readonly Random random;
		private readonly List<Tuple<T, float>> entries = new List<Tuple<T, float>>();

		private float TotalWeight
		{
			get
			{
				double total = 0.0; //double for precision
				foreach (Tuple<T, float> tuple in entries)
				{
					total += tuple.Item2;
				}
				return (float)total;
			}
		}

		public WeightedRandom(Random random = null)
		{
			this.random = random ?? new Random();
		}

		public void Add(T t, float weight)
		{
			entries.Add(new Tuple<T, float>(t, weight));
		}

		public T Get()
		{
			float f = (float)(random.NextDouble() * TotalWeight);
			double total = 0.0; //double for precision
			foreach (Tuple<T, float> tuple in entries)
			{
				if (total + tuple.Item2 < f)
					return tuple.Item1;
				total += tuple.Item2;
			}
			return entries[entries.Count - 1].Item1;
		}
	}
}
