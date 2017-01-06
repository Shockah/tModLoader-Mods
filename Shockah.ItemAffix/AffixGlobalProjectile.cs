using Shockah.ItemAffix.Utils;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.ItemAffix
{
	public class AffixGlobalProjectile : GlobalProjectile
	{
		public override void PostAI(Projectile projectile)
		{
			Player player = projectile.GetOwner();
			if (player == null)
				return;

			AffixProjectileInfo info = projectile.GetAffixInfo(mod);
			if (info.weapon == null)
				info.weapon = player.HeldItem;
		}

		public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			AffixProjectileInfo info = projectile.GetAffixInfo(mod);
			if (info.weapon != null)
				info.weapon.GetAffixInfo(mod)?.OnHitNPC(info.weapon, projectile.GetOwner(), projectile, target, damage, knockback, crit);
		}
	}
}