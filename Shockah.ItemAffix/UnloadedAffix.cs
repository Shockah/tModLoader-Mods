using Shockah.Utils;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix
{
	public sealed class UnloadedAffix : Affix
	{
		public readonly TagCompound tag;

		public UnloadedAffix(TagCompound tag) : base("Unloaded Affix")
		{
			this.tag = tag;
		}

		public override TagCompound SerializeData()
		{
			return tag;
		}

		[CallOrder(double.PositiveInfinity)]
		public override string GetFormattedName(Item item, string oldName)
		{
			return base.GetFormattedName(item, oldName);
		}
	}
}