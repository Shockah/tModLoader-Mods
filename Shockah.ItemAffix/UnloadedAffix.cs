using Shockah.Affix.Utils;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.Affix
{
	public class UnloadedAffix : Affix
	{
		public readonly TagCompound tag;

		public UnloadedAffix(AffixFactory factory, TagCompound tag) : base(factory, "Unloaded Affix")
		{
			this.tag = tag;
		}

		[CallOrder(double.PositiveInfinity)]
		public override string GetFormattedName(Item item, string oldName)
		{
			return base.GetFormattedName(item, oldName);
		}
	}

	public class UnloadedAffixFactory : AffixFactory
	{
		public UnloadedAffixFactory(string internalName) : base(internalName)
		{
		}

		public override Affix Create(Item item, TagCompound tag)
		{
			return new UnloadedAffix(this, tag);
		}

		public override TagCompound Store(Item item, Affix affix)
		{
			return (affix as UnloadedAffix).tag;
		}
	}
}