using Terraria;
using Terraria.ModLoader;

namespace Shockah.Utils.OwnedGlobals
{
	public class OwnedGlobalProjectile : GlobalProjectile
	{
		public override bool InstancePerEntity => true;
		public Projectile owner { get; private set; }
		public Item weapon { get; private set; }

		public override GlobalProjectile NewInstance(Projectile projectile)
		{
			OwnedGlobalProjectile global = (OwnedGlobalProjectile)base.NewInstance(projectile);
			global.owner = projectile;
			global.weapon = null;
			return global;
		}

		public override void PostAI(Projectile projectile)
		{
			if (weapon == null)
				weapon = projectile.GetOwner()?.HeldItem;
		}
	}
}