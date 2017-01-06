using System;
using System.Collections.Generic;

namespace Shockah.ItemAffix
{
	public sealed class WeightedRandom<T>
	{
		private readonly Random random;
		private readonly List<Tuple<T, double>> entries = new List<Tuple<T, double>>();

		public int Count => entries.Count;

		private float TotalWeight
		{
			get
			{
				double total = 0;
				foreach (Tuple<T, double> tuple in entries)
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

		public void Add(T t, double weight)
		{
			entries.Add(new Tuple<T, double>(t, weight));
		}

		public T Get(bool remove = false)
		{
			float f = (float)(random.NextDouble() * TotalWeight);
			double total = 0;

			for (int i = 0; i < entries.Count; i++)
			{
				Tuple<T, double> tuple = entries[i];
				if (total + tuple.Item2 < f)
				{
					if (remove)
						entries.Remove(i);
					return tuple.Item1;
				}
				total += tuple.Item2;
			}
			return entries[entries.Count - 1].Item1;
		}
	}
}
