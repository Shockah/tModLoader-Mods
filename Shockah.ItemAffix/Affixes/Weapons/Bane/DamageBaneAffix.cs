using System;
using System.Collections.Generic;
using System.Linq;
using Shockah.Utils;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public class DamageBaneAffix : BaneAffix
	{
		public static readonly Func<TagCompound, DamageBaneAffix> DESERIALIZER = tag =>
		{
			DamageBaneAffix affix = new DamageBaneAffix(
				tag.GetString("name"),
				tag.GetString("format"),
				tag.GetString("npcFamilyName"),
				tag.GetFloat("damageMod")
			);
			if (tag.HasTag("matches"))
				affix.matches.AddRange(tag.GetList<TagCompound>("matches").Select(matchTag => TagSerializables.Deserialize<NPCMatcher>(matchTag)));
			return affix;
		};

		public readonly float damageMod;

		public DamageBaneAffix(string npcFamilyName, float damageMod) : this(BaneName(npcFamilyName), PrefixFormat, npcFamilyName, damageMod)
		{
		}

		public DamageBaneAffix(string name, string npcFamilyName, float damageMod) : this(name, PrefixFormat, npcFamilyName, damageMod)
		{
		}

		public DamageBaneAffix(string name, string format, string npcFamilyName, float damageMod) : base(name, format, npcFamilyName)
		{
			this.damageMod = damageMod;
		}

		public override void SerializeData(TagCompound tag)
		{
			base.SerializeData(tag);
			tag["damageMod"] = damageMod;
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), GetType().FullName, string.Format(FormatTooltip("{0:0}% damage to {family}"), damageMod * 100));
			line.isModifier = true;
			line.isModifierBad = false;
			tooltips.Add(line);
		}

		public override void ModifyHitByItem(Item item, Player player, NPC npc, ref int damage, ref float knockback, ref bool crit)
		{
			if (!Matches(npc))
				return;
			damage = (int)(damage * damageMod);
		}

		public override void ModifyHitByProjectile(Item item, Projectile projectile, Player player, NPC npc, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			ModifyHitByItem(item, player, npc, ref damage, ref knockback, ref crit);
		}
	}
}