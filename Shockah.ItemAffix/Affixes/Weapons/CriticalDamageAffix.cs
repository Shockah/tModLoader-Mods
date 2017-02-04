using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public class CriticalDamageAffix : NamedItemAffix
	{
		public readonly float criticalDamage;

		public static readonly Func<TagCompound, CriticalDamageAffix> DESERIALIZER = tag =>
		{
			return new CriticalDamageAffix(
				tag.GetFloat("criticalDamage")
			);
		};

		public CriticalDamageAffix(float criticalDamage) : base("Bandit", SuffixOfTheFormat)
		{
			this.criticalDamage = criticalDamage;
		}

		public override TagCompound SerializeData()
		{
			TagCompound tag = base.SerializeData();
			tag["criticalDamage"] = criticalDamage;
			return tag;
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), GetType().FullName, $"+{(criticalDamage * 100):F0}% critical strike damage");
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