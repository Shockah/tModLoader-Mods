using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Shockah.ItemAffix
{
	public class AffixModWorld : ModWorld
	{
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			tasks.Add(new AffixWorldGenPass(mod as AffixMod));
		}
	}
}