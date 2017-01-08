﻿using Shockah.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public class CriticalDamageAffix : NamedItemAffix
	{
		public readonly float criticalDamage;

		public static readonly TagDeserializer<CriticalDamageAffix> DESERIALIZER = new TagDeserializer<CriticalDamageAffix>(tag =>
		{
			return new CriticalDamageAffix(
				tag.GetFloat("criticalDamage")
			);
		});

		public CriticalDamageAffix(float criticalDamage) : base("Bandit", SuffixOfTheFormat)
		{
			this.criticalDamage = criticalDamage;
		}

		public override void SerializeData(TagCompound tag)
		{
			base.SerializeData(tag);
			tag["criticalDamage"] = criticalDamage;
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), GetType().FullName, string.Format("+{0:0}% critical strike damage", criticalDamage * 100));
			line.isModifier = true;
			line.isModifierBad = false;
			tooltips.Add(line);
		}

		public override void ModifyHitByItem(Item item, Player player, NPC npc, ref int damage, ref float knockback, ref bool crit)
		{
			if (crit)
				damage = (int)(damage * 1 + criticalDamage * 0.5f); // 0.5 because it will be doubled by the game due to crit later on
		}

		public override void ModifyHitByProjectile(Item item, Projectile projectile, Player player, NPC npc, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			ModifyHitByItem(item, player, npc, ref damage, ref knockback, ref crit);
		}
	}
}