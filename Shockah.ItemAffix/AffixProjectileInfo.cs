﻿using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix
{
	public class AffixProjectileInfo : ProjectileInfo
	{
		public Item weapon = null;

		public override ProjectileInfo Clone()
		{
			AffixProjectileInfo clone = new AffixProjectileInfo();
			clone.weapon = weapon;
			return clone;
		}
	}
}