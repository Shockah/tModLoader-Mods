using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix
{
	public static class Extensions
	{
		internal static AffixItemInfo GetAffixInfo(this Item item)
		{
			return item.GetAffixInfo(ModLoader.GetMod(AffixMod.ModName));
		}

		internal static AffixItemInfo GetAffixInfo(this Item item, Mod mod)
		{
			if (item.netID == 0 || item.stack == 0)
				return null;
			return item.GetModInfo<AffixItemInfo>(mod);
		}

		internal static AffixNPCInfo GetAffixContentInfo(this NPC npc)
		{
			return npc.GetAffixInfo(ModLoader.GetMod(AffixMod.ModName));
		}

		internal static AffixNPCInfo GetAffixInfo(this NPC npc, Mod mod)
		{
			return npc.GetModInfo<AffixNPCInfo>(mod);
		}

		internal static AffixProjectileInfo GetAffixInfo(this Projectile projectile)
		{
			return projectile.GetAffixInfo(ModLoader.GetMod(AffixMod.ModName));
		}

		internal static AffixProjectileInfo GetAffixInfo(this Projectile projectile, Mod mod)
		{
			return projectile.GetModInfo<AffixProjectileInfo>(mod);
		}

		public static void ApplyAffix(this Item item, Affix affix)
		{
			item.GetAffixInfo()?.ApplyAffix(item, affix);
		}

		public static void RemoveAffix(this Item item, Affix affix)
		{
			item.GetAffixInfo()?.RemoveAffix(item, affix);
		}

		public static IList<Affix> GetAffixes(this Item item)
		{
			AffixItemInfo info = item.GetAffixInfo();
			if (info == null)
				return new List<Affix>().AsReadOnly();
			return info.affixes.AsReadOnly();
		}

		public static bool CanApplyAffixes(this Item item)
		{
			return item.damage > 0 && item.maxStack == 1;
		}
	}
}