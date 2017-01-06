using Shockah.ItemAffix.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix
{
	public sealed class UnloadedAffix : Affix
	{
		public readonly string typeName;
		public readonly TagCompound tag;

		public UnloadedAffix(string typeName, TagCompound tag) : base("Unloaded Affix")
		{
			this.typeName = typeName;
			this.tag = tag;
		}

		public override void SerializeData(TagCompound tag)
		{
			foreach (KeyValuePair<string, object> pair in this.tag)
			{
				tag[pair.Key] = pair.Value;
			}
		}

		[CallOrder(double.PositiveInfinity)]
		public override string GetFormattedName(Item item, string oldName)
		{
			return base.GetFormattedName(item, oldName);
		}
	}
}