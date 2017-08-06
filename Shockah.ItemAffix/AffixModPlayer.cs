using Shockah.Utils;
using System;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix
{
	partial class AffixModPlayer : ModPlayer
	{
		public override TagCompound Save()
		{
			TagCompound tag = new TagCompound();
			tag["damageOverTimeEffects"] = damageOverTimeEffects;
			return tag;
		}

		public override void Load(TagCompound tag)
		{
			damageOverTimeEffects = tag.GetList<DamageOverTime>("damageOverTimeEffects");
		}

		public override void UpdateBadLifeRegen()
		{
			for (int i = 0; i < damageOverTimeEffects.Count; i++)
			{
				DamageOverTime dot = damageOverTimeEffects[i];
				dot.currentTime++;
				if (dot.currentTime > dot.totalTime)
				{
					damageOverTimeEffects.RemoveAt(i--);
					continue;
				}

				player.lifeRegen = Math.Min(player.lifeRegen, 0);
				player.lifeRegenTime = 0;

				int oldDamage = (int)(dot.damage * (dot.currentTime - 1));
				int newDamage = (int)(dot.damage * dot.currentTime);
				if (newDamage != oldDamage)
					player.lifeRegen -= 60 * (newDamage - oldDamage);
			}
		}
	}
}