using Shockah.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public class AccessoryOnHitBuffAffix : NamedItemAffix
	{
		public readonly int buffType;
		public readonly Dynamic<int> buffTime;
		public readonly float chance;
		public readonly string tooltip;

		public static readonly TagDeserializer<AccessoryOnHitBuffAffix> DESERIALIZER = new TagDeserializer<AccessoryOnHitBuffAffix>(tag =>
		{
			return new AccessoryOnHitBuffAffix(
				tag.GetString("name"),
				tag.GetInt("buffType"),
				TagSerializables.Deserialize<Dynamic<int>>(tag["buffTime"] as TagCompound),
				tag.GetFloat("chance"),
				tag.GetString("tooltip")
			);
		});

		public static AccessoryOnHitBuffAffix CreateFiery(float chance = 0.25f)
		{
			return new AccessoryOnHitBuffAffix("Fiery", BuffID.OnFire, 60 * 5, chance, "{0:0}% chance to put enemies on fire");
		}

		public static AccessoryOnHitBuffAffix CreatePoisoned(float chance = 0.25f)
		{
			return new AccessoryOnHitBuffAffix("Poisoned", BuffID.Poisoned, 60 * 5, chance, "{0:0}% chance to poison enemies");
		}

		public AccessoryOnHitBuffAffix(string name, int buffType, Dynamic<int> buffTime, float chance, string tooltip) : this(name, true, buffType, buffTime, chance, tooltip)
		{
		}

		public AccessoryOnHitBuffAffix(string name, bool prefixedName, int buffType, Dynamic<int> buffTime, float chance, string tooltip) : base(name, prefixedName)
		{
			this.buffType = buffType;
			this.buffTime = buffTime;
			this.chance = chance;
			this.tooltip = tooltip;
		}

		public AccessoryOnHitBuffAffix(string name, string format, int buffType, Dynamic<int> buffTime, float chance, string tooltip) : base(name, format)
		{
			this.buffType = buffType;
			this.buffTime = buffTime;
			this.chance = chance;
			this.tooltip = tooltip;
		}

		public override void SerializeData(TagCompound tag)
		{
			base.SerializeData(tag);
			tag["buffType"] = buffType;
			tag["buffTime"] = TagSerializables.Serialize(buffTime);
			tag["chance"] = chance;
			tag["tooltip"] = tooltip;
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), GetType().FullName, string.Format(tooltip, (int)(chance * 100f)));
			line.isModifier = true;
			line.isModifierBad = false;
			tooltips.Add(line);
		}

		public override void OnHitNPC(Item affixedItem, Item item, Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.rand.NextFloat() >= chance)
				target.AddBuff(buffType, buffTime);
		}

		public override void OnHitNPC(Item affixedItem, Item item, Player player, Projectile projectile, NPC target, int damage, float knockBack, bool crit)
		{
			OnHitNPC(affixedItem, item, player, target, damage, knockBack, crit);
		}
	}
}