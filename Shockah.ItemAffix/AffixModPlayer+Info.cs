using Shockah.Utils;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace Shockah.ItemAffix
{
	partial class AffixModPlayer : ModPlayer
	{
		public IList<DamageOverTime> damageOverTimeEffects = new List<DamageOverTime>();
	}
}
