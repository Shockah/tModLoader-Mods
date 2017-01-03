using Shockah.Affix.Content;
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
			return item.GetModInfo<AffixItemInfo>(mod);
		}

		internal static AffixContentNPCInfo GetAffixContentInfo(this NPC npc)
		{
			return npc.GetAffixContentInfo(ModLoader.GetMod(AffixMod.ModName));
		}

		internal static AffixContentNPCInfo GetAffixContentInfo(this NPC npc, Mod mod)
		{
			return npc.GetModInfo<AffixContentNPCInfo>(mod);
		}

		internal static AffixContentProjectileInfo GetAffixContentInfo(this Projectile projectile)
		{
			return projectile.GetAffixContentInfo(ModLoader.GetMod(AffixMod.ModName));
		}

		internal static AffixContentProjectileInfo GetAffixContentInfo(this Projectile projectile, Mod mod)
		{
			return projectile.GetModInfo<AffixContentProjectileInfo>(mod);
		}

		public static void ApplyAffix(this Item item, Affix affix)
		{
			item.GetAffixInfo().ApplyAffix(item, affix);
		}

		public static void RemoveAffix(this Item item, Affix affix)
		{
			item.GetAffixInfo().RemoveAffix(item, affix);
		}

		public static IList<Affix> GetAffixes(this Item item)
		{
			return item.GetAffixInfo().affixes.AsReadOnly();
		}

		public static bool CanApplyAffixes(this Item item)
		{
			return item.damage > 0 && item.maxStack == 1;
		}
	}
}