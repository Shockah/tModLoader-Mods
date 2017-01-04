using System;
using System.Collections.Generic;
using Shockah.Affix.Utils;
using Terraria;

namespace Shockah.Affix.Content
{
	public class AffixWorldGenChestGenerator : AffixGenerator<AffixWorldGenChestEnvironment>
	{
		public readonly Dictionary<ChestType, Dynamic<float>> totalScores = new Dictionary<ChestType, Dynamic<float>>
		{
			{ ChestType.Wooden, (Func<float>)(() => 1.0f + (float)(Math.Pow(Main.rand.NextDouble(), 2) * 2.0f)) },
			{ ChestType.GoldLocked, (Func<float>)(() => 2.5f + (float)(Math.Pow(Main.rand.NextDouble(), 2) * 5.0f)) }
		};

		public readonly Dictionary<ChestType, Func<AffixWorldGenChestEnvironment, float>> totalScoresForEnvironment = new Dictionary<ChestType, Func<AffixWorldGenChestEnvironment, float>>
		{
			{ ChestType.Gold, environment =>
				{
					if (environment.y.Between((int)Main.worldSurface, (int)Main.rockLayer)) //underground
						return 2.0f + (float)(Math.Pow(Main.rand.NextDouble(), 2) * 3.0f);
					else if (environment.y.Between((int)Main.rockLayer, Main.maxTilesY - 200)) //caverns
						return 2.5f + (float)(Math.Pow(Main.rand.NextDouble(), 2) * 6.0f);
					return 0f;
				} }
		};

		public readonly Dictionary<ChestType, Dynamic<GeneratedAffix>> chestAffixes = new Dictionary<ChestType, Dynamic<GeneratedAffix>>();

		public override float GetTotalScore(Item item, AffixWorldGenChestEnvironment environment)
		{
			if (totalScores.ContainsKey(environment.ChestType))
				return totalScores[environment.ChestType];
			if (totalScoresForEnvironment.ContainsKey(environment.ChestType))
				return totalScoresForEnvironment[environment.ChestType](environment);
			return 0f;
		}

		public override List<Affix> GenerateAffixes(Item item, AffixWorldGenChestEnvironment environment)
		{
			throw new NotImplementedException();
		}
	}

	public class AffixWorldGenChestEnvironment : AffixGeneratorEnvironment
	{
		public readonly int x;
		public readonly int y;

		public ChestType ChestType
		{
			get
			{
				return (ChestType)(Main.tile[x, y].frameX / 18);
			}
		}

		public AffixWorldGenChestEnvironment(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}

	public enum ChestType
	{
		Wooden, Gold, GoldLocked, Shadow, ShadowLocked,
		Barrel, Trash, Ebonwood, RichMahogany, Pearlwood,
		Ivy, Ice, LivingWood, Skyware, Shadewood,
		WebCovered, Lihzahrd, Water, Jungle, Corruption,
		Crimson, Hallowed, Frozen, JungleLocked, CorruptionLocked,
		CrimsonLocked, HallowedLocked, FrozenLocked, Dynasty, Honey,
		Steampunk, PalmWood, Mushroom, BorealWood, Slime,
		GreenDungeon, GreenDungeonLocked, PinkDungeon, PinkDungeonLocked, BlueDungeon,
		BlueDungeonLocked, Bone, Cactus, Flesh, Obsidian,
		Pumpkin, Spooky, Glass, Martian, Meteorite,
		Granite, Marble,
		Count
	}
}