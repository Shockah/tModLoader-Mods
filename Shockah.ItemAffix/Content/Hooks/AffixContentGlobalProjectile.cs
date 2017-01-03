using Shockah.Affix.Utils;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix.Content
{
	public class AffixContentGlobalProjectile : GlobalProjectile
	{
		public override void PostAI(Projectile projectile)
		{
			Player player = projectile.GetOwner();
			if (player != null)
			{
				AffixContentProjectileInfo info = projectile.GetModInfo<AffixContentProjectileInfo>(mod);
				if (info.weapon == null)
					info.weapon = player.HeldItem;
			}
		}
	}
}