using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix.Content
{
	public class AffixContentProjectileInfo : ProjectileInfo
	{
		public Item weapon = null;

		public override ProjectileInfo Clone()
		{
			AffixContentProjectileInfo clone = new AffixContentProjectileInfo();
			clone.weapon = weapon;
			return clone;
		}
	}
}
