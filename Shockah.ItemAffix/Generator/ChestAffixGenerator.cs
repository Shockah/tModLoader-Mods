using System;
using Terraria;

namespace Shockah.ItemAffix.Generator
{
	public abstract class ChestAffixGenerator : AffixGenerator<ChestAffixGenEnvironment>
	{
		public abstract bool MatchesEnvironment(ChestAffixGenEnvironment environment);
	}

	public class ChestAffixGeneratorDelegate : ChestAffixGenerator
	{
		public readonly Func<ChestAffixGenEnvironment, bool> @delegate;

		public ChestAffixGeneratorDelegate(Func<ChestAffixGenEnvironment, bool> @delegate)
		{
			this.@delegate = @delegate;
		}

		public override bool MatchesEnvironment(ChestAffixGenEnvironment environment)
		{
			return @delegate(environment);
		}
	}

	public class ChestAffixGenEnvironment : AffixGenEnvironment
	{
		public readonly int x;
		public readonly int y;

		public Tile Tile => Main.tile[x, y];

		public ChestType ChestType => (ChestType)(Tile.frameX / 36);

		public ChestAffixGenEnvironment(int x, int y)
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