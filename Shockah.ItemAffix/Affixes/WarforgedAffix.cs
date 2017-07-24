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

		public static WarforgedAffix CreateForRarity(AffixRarity rarity, Random rand)
		{
			float bonus = 0f;
			switch (rarity)
			{
				case AffixRarity.Common:
					bonus = 0.03f + rand.NextFloat() * 0.02f;
					break;
				case AffixRarity.Uncommon:
					bonus = 0.05f + rand.NextFloat() * 0.05f;
					break;
				case AffixRarity.Rare:
					bonus = 0.10f + rand.NextFloat() * 0.05f;
					break;
				case AffixRarity.Exotic:
					bonus = 0.15f + rand.NextFloat() * 0.10f;
					break;
				case AffixRarity.Mythic:
					bonus = 0.25f + rand.NextFloat() * 0.10f;
					break;
				case AffixRarity.Legendary:
					bonus = 0.35f + rand.NextFloat() * 0.15f;
					break;
				case AffixRarity.Ascended:
					bonus = 0.50f + rand.NextFloat() * 0.20f;
					break;
			}
			return new WarforgedAffix(rarity, bonus);
		}

		public static readonly Func<TagCompound, WarforgedAffix> DESERIALIZER = tag =>
		{
			return new WarforgedAffix(
				(AffixRarity)tag.GetInt("rarity"),
				tag.GetFloat("bonus")
			);
		};

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
			line.SetAffixRarityColor(rarity);
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