using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Shockah.Utils;
using Shockah.ItemAffix.Content;

namespace Shockah.ItemAffix
{
	public class AffixGlobalNPC : GlobalNPC
	{
		public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
		{
			npc.GetAffixInfo(mod).AddParticipant(player, item);
		}

		public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
		{
			AffixProjectileInfo projectileInfo = projectile.GetAffixInfo(mod);
			if (projectileInfo.weapon != null)
				npc.GetAffixInfo(mod).AddParticipant(projectile.GetOwner(), projectileInfo.weapon);
		}

		public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
		{
			item.GetAffixInfo()?.ModifyHitByItem(item, player, npc, ref damage, ref knockback, ref crit);
		}

		public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Item weapon = projectile.GetAffixInfo().weapon;
			weapon?.GetAffixInfo()?.ModifyHitByProjectile(weapon, projectile, projectile.GetOwner(), npc, ref damage, ref knockback, ref crit, ref hitDirection);
		}

		public override void NPCLoot(NPC npc)
		{
			foreach (KeyValuePair<Player, List<Item>> kvp in npc.GetAffixInfo(mod).participants)
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