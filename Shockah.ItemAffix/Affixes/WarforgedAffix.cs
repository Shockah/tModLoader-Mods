using System;
using Terraria.ModLoader.IO;
using Terraria;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace Shockah.ItemAffix.Affixes
{
	public class WarforgedAffix : NamedItemAffix
	{
		public readonly float bonus;

		public static readonly Func<TagCompound, WarforgedAffix> DESERIALIZER = tag =>
		{
			return new WarforgedAffix(
				tag.GetFloat("bonus")
			);
		};

		private static string GetFormatForBonus(float bonus)
		{
			return bonus < 0.15f ? "{item}+" : "{item}++";
		}

		public WarforgedAffix(float bonus) : base("Warforged", GetFormatForBonus(bonus))
		{
			this.bonus = bonus;
		}

		public override TagCompound SerializeData()
		{
			TagCompound tag = base.SerializeData();
			tag["bonus"] = bonus;
			return tag;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), GetType().FullName, $"+{(int)Math.Round(bonus)}% stats");
			line.isModifier = true;
			line.isModifierBad = false;
			tooltips.Add(line);
		}

		public override void GetWeaponDamage(Item weapon, Player player, ref int damage)
		{
			if (item != weapon)
				return;
			damage = (int)Math.Ceiling(damage * bonus);
		}
	}
}