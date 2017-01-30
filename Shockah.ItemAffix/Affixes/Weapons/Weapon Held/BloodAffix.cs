using System;
using System.Collections.Generic;
using Shockah.Utils;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public class BloodAffix : WeaponHeldAffix
	{
		public readonly float damage;
		public readonly float healthLossPerSecond;

		protected float counter = 0f;

		public static readonly Func<TagCompound, BloodAffix> DESERIALIZER = tag =>
		{
			return new BloodAffix(
				tag.GetFloat("damage"),
				tag.GetFloat("healthLossPerSecond")
			);
		};

		public BloodAffix(float damage, float healthLossPerSecond) : base("Blood", PrefixFormat)
		{
			this.damage = damage;
			this.healthLossPerSecond = healthLossPerSecond;
		}

		public override void SerializeData(TagCompound tag)
		{
			base.SerializeData(tag);
			tag["damage"] = damage;
			tag["healthLossPerSecond"] = healthLossPerSecond;
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine line;

			line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), $"{GetType().FullName}.Damage", $"+{(damage * 100):F0}% damage");
			line.isModifier = true;
			line.isModifierBad = false;
			tooltips.Add(line);

			line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), $"{GetType().FullName}.Health", $"{(healthLossPerSecond * 100):F0} health loss per second");
			line.isModifier = true;
			line.isModifierBad = true;
			tooltips.Add(line);
		}

		public override void GetWeaponDamage(Item affixedItem, Item item, Player player, ref int damage)
		{
			damage = (int)(damage * (1f + this.damage));
		}

		public override void UpdateWeaponHeld(Item item, Player player)
		{
			counter += healthLossPerSecond;
			int fullLifeLost = (int)(counter / 60f);
			if (fullLifeLost != 0)
			{
				player.statLife -= fullLifeLost;
				//TODO: reset life regen
				counter -= fullLifeLost * 60;
			}
		}
	}
}