using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.ItemAffix
{
	public static class ItemAffixExtensions
	{
		internal static AffixGlobalItem GetAffixInfo(this Item item)
		{
			return item.GetAffixInfo(ModLoader.GetMod(AffixMod.ModName));
		}

		internal static AffixGlobalItem GetAffixInfo(this Item item, Mod mod)
		{
			if (item.netID == 0 || item.stack == 0)
				return null;
			return item.GetGlobalItem<AffixGlobalItem>(mod);
		}

		public static void ApplyAffix(this Item item, Affix affix)
		{
			item.GetAffixInfo().ApplyAffix(affix);
		}

		public static void RemoveAffix(this Item item, Affix affix)
		{
			item.GetAffixInfo().RemoveAffix(affix);
		}

		public static IList<Affix> GetAffixes(this Item item)
		{
			return item.GetAffixInfo().affixes.AsReadOnly();
		}

		private static bool IsAffixableBase(this Item item)
		{
			return item.maxStack == 1;
		}

		public static bool IsAffixable(this Item item)
		{
			return item.IsAffixableWeapon() || item.IsAffixableAccessory();
		}

		public static bool IsAffixableWeapon(this Item item)
		{
			if (!item.IsAffixableBase())
				return false;
			return item.damage > 0 && (item.melee || item.ranged || item.magic || item.summon || item.thrown);
		}

		public static bool IsAffixableAccessory(this Item item)
		{
			if (!item.IsAffixableBase())
				return false;
			return item.accessory;
		}
	}
}