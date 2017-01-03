﻿using Shockah.Affix.Utils;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix.Content
{
	public class AffixContentGlobalProjectile : GlobalProjectile
	{
		public override void PostAI(Projectile projectile)
		{
			Player player = projectile.GetOwner();
			if (player == null)
				return;

			AffixContentProjectileInfo info = projectile.GetAffixContentInfo(mod);
			if (info.weapon == null)
				info.weapon = player.HeldItem;
		}

		public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			AffixContentProjectileInfo info = projectile.GetAffixContentInfo(mod);
			if (info.weapon != null)
				info.weapon.GetAffixInfo(mod)?.OnHitNPC(info.weapon, projectile.GetOwner(), projectile, target, damage, knockback, crit);
		}
	}
}