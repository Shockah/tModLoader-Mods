using Shockah.ItemAffix.Generator;
using Terraria;
using Terraria.World.Generation;

namespace Shockah.ItemAffix
{
	public sealed class AffixWorldGenPass : GenPass
	{
		private readonly AffixMod mod;

		public AffixWorldGenPass(AffixMod mod) : base(AffixMod.ModName, 1)
		{
			this.mod = mod;
		}

		public override void Apply(GenerationProgress progress)
		{
			progress.Message = "Applying affixes...";
			foreach (Chest chest in Main.chest)
			{
				if (chest == null || chest.x <= 0 || chest.y <= 0)
					continue;
				foreach (Item item in chest.item)
				{
					if (!item.IsAir)
					{
						if (item.IsAffixableWeapon())
						{
							mod.ChestGenManager.GenerateAndApplyAffixes(item, new ChestAffixGenEnvironment(chest.x, chest.y));
						}
						else if (item.IsAffixableAccessory())
						{
							mod.AccessoryChestGenManager.GenerateAndApplyAffixes(item, new ChestAffixGenEnvironment(chest.x, chest.y));
						}
					}
				}
			}
		}
	}
}