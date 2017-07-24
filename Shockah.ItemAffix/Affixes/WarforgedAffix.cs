using System;
using Terraria.ModLoader.IO;
using Terraria;
using System.Collections.Generic;
using Terraria.ModLoader;
using Shockah.Utils;

namespace Shockah.ItemAffix.Affixes
{
	public class WarforgedAffix : NamedItemAffix
	{
		public readonly float bonus;

		public static readonly Func<TagCompound, WarforgedAffix> DESERIALIZER = tag =>
		{
			return new WarforgedAffix((AffixRarity)tag.GetInt("rarity"), tag.GetFloat("bonus"));
		};

		static readonly IDictionary<AffixRarity, Func<Random, float>> bonusValues = new Dictionary<AffixRarity, Func<Random, float>>
		{
			[AffixRarity.Common] = rand => CalculateBonus(rand, 0.03f, 0.02f),
			[AffixRarity.Uncommon] = rand => CalculateBonus(rand, 0.05f, 0.05f),
			[AffixRarity.Rare] = rand => CalculateBonus(rand, 0.10f, 0.05f),
			[AffixRarity.Exotic] = rand => CalculateBonus(rand, 0.15f, 0.10f),
			[AffixRarity.Mythic] = rand => CalculateBonus(rand, 0.25f, 0.10f),
			[AffixRarity.Legendary] = rand => CalculateBonus(rand, 0.35f, 0.15f),
			[AffixRarity.Ascended] = rand => CalculateBonus(rand, 0.50f, 0.20f)
		};

		public static WarforgedAffix CreateForRarity(AffixRarity rarity, Random rand)
		{
			return new WarforgedAffix(rarity, bonusValues[rarity](rand));
		}

		static float CalculateBonus(Random rand, float @base, float extra)
		{
			return @base + rand.NextFloat() * extra;
		}

		public WarforgedAffix(AffixRarity rarity, float bonus) : base("Warforged", rarity, "{item}+")
		{
			this.bonus = bonus;
		}

		public override TagCompound SerializeData()
		{
			TagCompound tag = base.SerializeData();
			tag["rarity"] = (int)rarity;
			tag["bonus"] = bonus;
			return tag;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), GetType().FullName, $"+{(int)Math.Round(bonus)}% stats");
			line.isModifier = true;
			line.isModifierBad = false;
			line.overrideColor = rarity.GetTooltipColor();
			tooltips.Add(line);
		}

		public override void GetWeaponDamage(Item weapon, Player player, ref int damage)
		{
			if (item != weapon)
				return;
			damage = (int)Math.Ceiling(damage * (1f + bonus));
		}
	}
}