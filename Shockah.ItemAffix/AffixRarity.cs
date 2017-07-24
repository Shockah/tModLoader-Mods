using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Shockah.ItemAffix
{
	public enum AffixRarity
	{
		Common, Uncommon, Rare, Exotic, Mythic, Legendary, Ascended
	}

	public static class AffixRarities
	{
		public static readonly IDictionary<AffixRarity, float> minimumRandomFloat = new Dictionary<AffixRarity, float>
		{
			[AffixRarity.Common] = 0.00f,
			[AffixRarity.Uncommon] = 0.30f,
			[AffixRarity.Rare] = 0.55f,
			[AffixRarity.Exotic] = 0.75f,
			[AffixRarity.Mythic] = 0.90f,
			[AffixRarity.Legendary] = 0.96f,
			[AffixRarity.Ascended] = 0.99f
		};

		public static readonly IDictionary<AffixRarity, Color> tooltipColors = new Dictionary<AffixRarity, Color>
		{
			[AffixRarity.Common] = new Color(150, 150, 255),
			[AffixRarity.Uncommon] = new Color(150, 255, 150),
			[AffixRarity.Rare] = new Color(255, 200, 150),
			[AffixRarity.Exotic] = new Color(255, 150, 150),
			[AffixRarity.Mythic] = new Color(255, 150, 255),
			[AffixRarity.Legendary] = new Color(210, 160, 255),
			[AffixRarity.Ascended] = new Color(150, 255, 10)
		};

		public static float GetMinimumRandomFloat(this AffixRarity rarity)
		{
			return minimumRandomFloat[rarity];
		}

		public static Color GetTooltipColor(this AffixRarity rarity)
		{
			return tooltipColors[rarity];
		}

		public static AffixRarity GetRarityForRandomFloat(float f)
		{
			foreach (AffixRarity rarity in Enum.GetValues(typeof(AffixRarity)).Cast<AffixRarity>().Reverse())
			{
				if (f >= rarity.GetMinimumRandomFloat())
					return rarity;
			}
			return AffixRarity.Common;
		}
	}
}