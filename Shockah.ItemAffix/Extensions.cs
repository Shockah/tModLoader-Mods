using System;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix
{
	public static class Extensions
	{
		public static void ApplyAffix(this Item item, string internalName)
		{
			(ModLoader.GetMod("Shockah.Affix") as AffixMod).ApplyAffix(item, internalName);
		}

		public static void ApplyAffix(this Item item, AffixFactory factory)
		{
			(ModLoader.GetMod("Shockah.Affix") as AffixMod).ApplyAffix(item, factory);
		}

		public static void ApplyAffix<T>(this Item item, string internalName, Action<T> createDelegate) where T : Affix
		{
			(ModLoader.GetMod("Shockah.Affix") as AffixMod).ApplyAffix(item, internalName, createDelegate);
		}

		public static void ApplyAffix<T>(this Item item, AffixFactory factory, Action<T> createDelegate) where T : Affix
		{
			(ModLoader.GetMod("Shockah.Affix") as AffixMod).ApplyAffix(item, factory, createDelegate);
		}

		public static void ApplyAffix(this Item item, Affix affix)
		{
			(ModLoader.GetMod("Shockah.Affix") as AffixMod).ApplyAffix(item, affix);
		}

		public static void RemoveAffix(this Item item, Affix affix)
		{
			(ModLoader.GetMod("Shockah.Affix") as AffixMod).RemoveAffix(item, affix);
		}

		public static bool CanApplyAffixes(this Item item)
		{
			return (ModLoader.GetMod("Shockah.Affix") as AffixMod).CanApplyAffixes(item);
		}
	}
}