using Shockah.Utils;
using System;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using Terraria;

namespace Shockah.ItemAffix.Affixes
{
	public class SharpAffix : NamedItemAffix
	{
		public readonly float chance;
		public readonly float percent;
		public readonly float seconds;

		public static readonly Func<TagCompound, SharpAffix> DESERIALIZER = tag =>
		{
			return new SharpAffix(
				(AffixRarity)tag.GetInt("rarity"),
				tag.GetFloat("chance"),
				tag.GetFloat("percent"),
				tag.GetFloat("seconds")
			);
		};

		static readonly IDictionary<AffixRarity, Func<Random, float>> percentValues = new Dictionary<AffixRarity, Func<Random, float>>
		{
			[AffixRarity.Common] = rand => rand.BaseWithBonus(0.03f, 0.02f),
			[AffixRarity.Uncommon] = rand => rand.BaseWithBonus(0.05f, 0.05f),
			[AffixRarity.Rare] = rand => rand.BaseWithBonus(0.10f, 0.05f),
			[AffixRarity.Exotic] = rand => rand.BaseWithBonus(0.15f, 0.10f),
			[AffixRarity.Mythic] = rand => rand.BaseWithBonus(0.25f, 0.10f),
			[AffixRarity.Legendary] = rand => rand.BaseWithBonus(0.35f, 0.15f),
			[AffixRarity.Ascended] = rand => rand.BaseWithBonus(0.50f, 0.20f)
		};

		public static SharpAffix CreateForRarity(AffixRarity rarity, Random rand)
		{
			return new SharpAffix(
				rarity,
				1f,
				percentValues[rarity](rand),
				rand.BaseWithBonus(2.0f, 8.0f)
			);
		}

		public SharpAffix(AffixRarity rarity, float chance, float percent, float seconds) : base("Sharp", rarity, PrefixFormat)
		{
			this.chance = chance;
			this.percent = percent;
			this.seconds = seconds;
		}

		public override TagCompound SerializeData()
		{
			TagCompound tag = base.SerializeData();
			tag["rarity"] = (int)rarity;
			tag["chance"] = chance;
			tag["percent"] = percent;
			tag["seconds"] = seconds;
			return tag;
		}

		public override void OnHitNPC(Item weapon, Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.rand.NextFloat() >= chance)
				player.ApplyDamageOverTimeEffect(new DamageOverTime(damage * percent, (int)(seconds * 60)));
		}

		public override void OnHitNPC(Item weapon, Player player, Projectile projectile, NPC target, int damage, float knockBack, bool crit)
		{
			OnHitNPC(weapon, player, target, damage, knockBack, crit);
		}
	}
}
