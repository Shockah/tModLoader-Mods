using Shockah.Affix.Utils;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.Affix
{
	public class UnloadedAffix : Affix
	{
		public readonly TagCompound tag;

		public UnloadedAffix(TagCompound tag) : base("Unloaded Affix")
		{
			this.tag = tag;
		}

		[CallOrder(double.PositiveInfinity)]
		public override string GetFormattedName(Item item, string oldName)
		{
			return base.GetFormattedName(item, oldName);
		}
	}
}