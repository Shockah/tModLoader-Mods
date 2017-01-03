﻿using System.Collections.Generic;
using Shockah.Affix.Utils;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix.Content
{
	public class AffixContentGlobalNPC : GlobalNPC
	{
		public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
		{
			AffixContentNPCInfo info = npc.GetModInfo<AffixContentNPCInfo>(mod);
			info.AddParticipant(player, item);
		}

		public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
		{
			AffixContentProjectileInfo projectileInfo = projectile.GetModInfo<AffixContentProjectileInfo>(mod);
			if (projectileInfo.weapon == null)
				return;

			AffixContentNPCInfo info = npc.GetModInfo<AffixContentNPCInfo>(mod);
			info.AddParticipant(projectile.GetOwner(), projectileInfo.weapon);
		}

		public override void NPCLoot(NPC npc)
		{
			AffixContentNPCInfo info = npc.GetModInfo<AffixContentNPCInfo>(mod);
			foreach (KeyValuePair<Player, List<Item>> kvp in info.participants)
			{
				foreach (Item weapon in kvp.Value)
				{
					//list may change due to Hidden Potential affixes, but in that case we exit the loop anyway
					foreach (Affix affix in weapon.GetAffixes())
					{
						HiddenPotentialAffix hiddenPotentialAffix = affix as HiddenPotentialAffix;
						if (hiddenPotentialAffix == null)
							continue;

						HiddenPotentialKillRequirement killRequirement = hiddenPotentialAffix.requirement as HiddenPotentialKillRequirement;
						if (killRequirement != null)
						{
							killRequirement.OnNPCKilled(npc, kvp.Key, weapon, hiddenPotentialAffix);
							goto AfterWeaponLoop; //only progress one weapon if hit with multiple
						}
					}
				}
			AfterWeaponLoop:;
			}
		}
	}
}