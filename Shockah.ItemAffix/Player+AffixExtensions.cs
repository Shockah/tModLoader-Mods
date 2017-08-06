using Shockah.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.ItemAffix
{
	public static class PlayerAffixExtensions
	{
		internal static AffixModPlayer GetAffixInfo(this Player player)
		{
			return player.GetAffixInfo(ModLoader.GetMod(AffixMod.ModName));
		}

		internal static AffixModPlayer GetAffixInfo(this Player player, Mod mod)
		{
			return player.GetModPlayer<AffixModPlayer>(mod);
		}

		public static void ApplyDamageOverTimeEffect(this Player player, DamageOverTime dot)
		{
			player.GetAffixInfo().damageOverTimeEffects.Add(dot);
		}

		public static void RemoveDamageOverTimeEffect(this Player player, DamageOverTime dot)
		{
			player.GetAffixInfo().damageOverTimeEffects.Remove(dot);
		}

		public static IList<DamageOverTime> GetDamageOverTimeEffects(this Player player)
		{
			return new ReadOnlyCollection<DamageOverTime>(player.GetAffixInfo().damageOverTimeEffects);
		}
	}
}
