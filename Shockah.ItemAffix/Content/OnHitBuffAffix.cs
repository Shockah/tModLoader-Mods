using Shockah.Affix.Utils;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.Affix.Content
{
	public class OnHitBuffAffix : NamedItemAffix
	{
		public readonly int buffType;
		public readonly Dynamic<int> buffTime;
		public readonly string tooltip;

		public static readonly TagDeserializer<OnHitBuffAffix> DESERIALIZER = new TagDeserializer<OnHitBuffAffix>(tag =>
		{
			return new OnHitBuffAffix(
				tag.GetString("name"),
				tag.GetInt("buffType"),
				TagSerializables.Deserialize<Dynamic<int>>(tag["buffTime"] as TagCompound),
				tag.GetString("tooltip")
			);
		});

		public static OnHitBuffAffix CreateFiery()
		{
			return new OnHitBuffAffix("Fiery", BuffID.OnFire, 60 * 5, "Puts enemies on fire");
		}

		public static OnHitBuffAffix CreatePoisoned()
		{
			return new OnHitBuffAffix("Poisoned", BuffID.Poisoned, 60 * 5, "Poisons enemies");
		}

		public OnHitBuffAffix(string name, int buffType, Dynamic<int> buffTime, string tooltip) : this(name, true, buffType, buffTime, tooltip)
		{
		}

		public OnHitBuffAffix(string name, bool prefixedName, int buffType, Dynamic<int> buffTime, string tooltip) : base(name, prefixedName)
		{
			this.buffType = buffType;
			this.buffTime = buffTime;
			this.tooltip = tooltip;
		}

		public OnHitBuffAffix(string name, string format, int buffType, Dynamic<int> buffTime, string tooltip) : base(name, format)
		{
			this.buffType = buffType;
			this.buffTime = buffTime;
			this.tooltip = tooltip;
		}

		public override void SerializeData(TagCompound tag)
		{
			base.SerializeData(tag);
			tag["buffType"] = buffType;
			tag["buffTime"] = TagSerializables.Serialize(buffTime);
			tag["tooltip"] = tooltip;
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), GetType().FullName, tooltip);
			line.isModifier = true;
			line.isModifierBad = false;
			tooltips.Add(line);
		}

		public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
		{
			target.AddBuff(buffType, buffTime);
		}

		public override void OnHitNPC(Item item, Player player, Projectile projectile, NPC target, int damage, float knockBack, bool crit)
		{
			OnHitNPC(item, player, target, damage, knockBack, crit);
		}
	}
}