using Microsoft.Xna.Framework;
using Terraria.ModLoader;

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

	public static class AffixRarityExtensions
	{
		public static void SetAffixRarityColor(this TooltipLine tooltip, AffixRarity rarity)
		{
			switch (rarity)
			{
				case AffixRarity.Common:
					tooltip.overrideColor = new Color(150, 150, 255);
					break;
				case AffixRarity.Uncommon:
					tooltip.overrideColor = new Color(150, 255, 150);
					break;
				case AffixRarity.Rare:
					tooltip.overrideColor = new Color(255, 200, 150);
					break;
				case AffixRarity.Exotic:
					tooltip.overrideColor = new Color(255, 150, 150);
					break;
				case AffixRarity.Mythic:
					tooltip.overrideColor = new Color(255, 150, 255);
					break;
				case AffixRarity.Legendary:
					tooltip.overrideColor = new Color(210, 160, 255);
					break;
				case AffixRarity.Ascended:
					tooltip.overrideColor = new Color(150, 255, 10);
					break;
			}
		}
	}
}