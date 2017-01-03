using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix
{
	public static class Extensions
	{
		public static void ApplyAffix(this Item item, Affix affix)
		{
			AffixItemInfo info = item.GetModInfo<AffixItemInfo>(ModLoader.GetMod("Shockah.Affix"));
			info.ApplyAffix(item, affix);
		}

		public static void RemoveAffix(this Item item, Affix affix)
		{
			AffixItemInfo info = item.GetModInfo<AffixItemInfo>(ModLoader.GetMod("Shockah.Affix"));
			info.RemoveAffix(item, affix);
		}

		public static IList<Affix> GetAffixes(this Item item)
		{
			AffixItemInfo info = item.GetModInfo<AffixItemInfo>(ModLoader.GetMod("Shockah.Affix"));
			return info.affixes.AsReadOnly();
		}

		public static bool CanApplyAffixes(this Item item)
		{
			return item.damage > 0 && item.maxStack == 1;
		}
	}
}