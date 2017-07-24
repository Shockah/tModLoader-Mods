using System;

namespace Shockah.ItemAffix
{
	public enum AffixRarity
	{
		Common,
		Uncommon,
		Rare,
		Exotic,
		Mythic,
		Legendary,
		Ascended
	}

	public static class AffixRarities
	{
		public static AffixRarity GetRarityForRandomFloat(float f)
		{
			if (f >= 0.99f)
				return AffixRarity.Ascended;
			if (f >= 0.96f)
				return AffixRarity.Legendary;
			if (f >= 0.90f)
				return AffixRarity.Mythic;
			if (f >= 0.75f)
				return AffixRarity.Exotic;
			if (f >= 0.55f)
				return AffixRarity.Rare;
			if (f >= 0.30f)
				return AffixRarity.Uncommon;
			return AffixRarity.Common;
		}
	}
}