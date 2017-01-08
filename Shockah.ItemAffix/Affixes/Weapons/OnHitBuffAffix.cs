using Shockah.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public class OnHitBuffAffix : NamedItemAffix
	{
		public readonly int buffType;
		public readonly Dynamic<int> buffTime;
		public readonly float chance;
		public readonly string tooltip;
		public readonly string tooltip100;

		public static readonly TagDeserializer<OnHitBuffAffix> DESERIALIZER = new TagDeserializer<OnHitBuffAffix>(tag =>
		{
			return new OnHitBuffAffix(
				tag.GetString("name"),
				tag.GetInt("buffType"),
				TagSerializables.Deserialize<Dynamic<int>>(tag["buffTime"] as TagCompound),
				tag.GetFloat("chance"),
				tag.GetString("tooltip"),
				tag.GetString("tooltip100")
			);
		});

		public static OnHitBuffAffix CreateFiery(float chance = 1f)
		{
			return new OnHitBuffAffix("Fiery", BuffID.OnFire, 60 * 5, chance, "{0:0}% chance to set enemies ablaze", "Sets enemies ablaze");
		}

		public static OnHitBuffAffix CreatePoisoned(float chance = 1f)
		{
			return new OnHitBuffAffix("Poisoned", BuffID.Poisoned, 60 * 5, chance, "{0:0}% chance to poison enemies", "Poisons enemies");
		}

		public OnHitBuffAffix(string name, int buffType, Dynamic<int> buffTime, float chance, string tooltip, string tooltip100) : this(name, PrefixFormat, buffType, buffTime, chance, tooltip, tooltip100)
		{
		}

		public OnHitBuffAffix(string name, string format, int buffType, Dynamic<int> buffTime, float chance, string tooltip, string tooltip100) : base(name, format)
		{
			this.buffType = buffType;
			this.buffTime = buffTime;
			this.chance = chance;
			this.tooltip = tooltip;
			this.tooltip100 = tooltip100;
		}

		public override void SerializeData(TagCompound tag)
		{
			base.SerializeData(tag);
			tag["buffType"] = buffType;
			tag["buffTime"] = TagSerializables.Serialize(buffTime);
			tag["chance"] = chance;
			tag["tooltip"] = tooltip;
			tag["tooltip100"] = tooltip100;
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(ModLoader.GetMod(AffixMod.ModName), GetType().FullName, chance >= 1f ? tooltip100 : string.Format(tooltip, (int)(chance * 100f)));
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