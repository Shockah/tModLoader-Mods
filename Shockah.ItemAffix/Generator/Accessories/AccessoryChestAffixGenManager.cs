using Shockah.ItemAffix.Content;
using Shockah.Utils;
using Shockah.Utils.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace Shockah.ItemAffix.Generator
{
	public class AccessoryChestAffixGenManager
	{
		public readonly List<ChestAffixGenerator> chestGenerators = new List<ChestAffixGenerator>();
		public readonly ChestAffixGenerator surfaceChestGenerator;
		public readonly ChestAffixGenerator undergroundChestGenerator;
		public readonly ChestAffixGenerator cavernsChestGenerator;
		public readonly ChestAffixGenerator dungeonChestGenerator;

		public AccessoryChestAffixGenManager()
		{
			ChestType[] surface = {
				ChestType.Wooden, ChestType.LivingWood, ChestType.Water
			};
			ChestType[] belowSurface = {
				ChestType.Gold, ChestType.RichMahogany, ChestType.Ivy, ChestType.Ice, ChestType.Granite, ChestType.Marble, ChestType.Water, ChestType.Mushroom
			};

			#region Surface
			chestGenerators.Add(surfaceChestGenerator = new ChestAffixGeneratorDelegate(
				env => surface.Contains(env.ChestType)
			).With(
				WeightedRuleGroup.Of(
					count: () => (int)(0.5 + Math.Sqrt(surfaceChestGenerator.GetRandom().NextDouble() * 4.5)),
					rules: WeightRules.Of(
						WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
							(input, random) => AccessoryOnHitBuffAffix.CreateFiery(0.05f + random.NextFloat() * 0.05f)), 1
						),
						WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
							(input, random) => AccessoryOnHitBuffAffix.CreatePoisoned(0.05f + random.NextFloat() * 0.05f)), 1
						)
					)
				),
				ChanceRule.Of(
					chance: 0.15f,
					rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
						(input, random) => new ShinyAffix(2.0f + random.NextFloat() * 2.0f)
					)
				)
			));
			#endregion

			#region Underground
			chestGenerators.Add(undergroundChestGenerator = new ChestAffixGeneratorDelegate(
				env => belowSurface.Contains(env.ChestType) && env.y.Between((int)Main.worldSurface, (int)Main.rockLayer)
			).With(
				WeightedRuleGroup.Of(
					count: () => (int)(1 + Math.Sqrt(undergroundChestGenerator.GetRandom().NextDouble() * 5)),
					rules: WeightRules.Of(
						WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
							(input, random) => AccessoryOnHitBuffAffix.CreateFiery(0.08f + random.NextFloat() * 0.07f)), 1
						),
						WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
							(input, random) => AccessoryOnHitBuffAffix.CreatePoisoned(0.08f + random.NextFloat() * 0.07f)), 1
						)
					)
				),
				ChanceRule.Of(
					chance: 0.15,
					rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
						(input, random) => new ShinyAffix(2.0f + random.NextFloat() * 2.0f)
					)
				)
			));
			#endregion

			#region Caverns
			chestGenerators.Add(cavernsChestGenerator = new ChestAffixGeneratorDelegate(
				env => belowSurface.Contains(env.ChestType) && env.y.Between((int)Main.rockLayer, Main.maxTilesY - 200)
			).With(
				WeightedRuleGroup.Of(
					count: () => 1 + (int)Math.Sqrt(cavernsChestGenerator.GetRandom().NextDouble() * 6),
					rules: WeightRules.Of(
						WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
							(input, random) => AccessoryOnHitBuffAffix.CreateFiery(0.10f + random.NextFloat() * 0.10f)), 1
						),
						WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
							(input, random) => AccessoryOnHitBuffAffix.CreatePoisoned(0.10f + random.NextFloat() * 0.10f)), 1
						)
					)
				),
				ChanceRule.Of(
					chance: 0.15f,
					rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
						(input, random) => new ShinyAffix(2.0f + random.NextFloat() * 2.0f)
					)
				)
			));
			#endregion

			#region Dungeon
			chestGenerators.Add(dungeonChestGenerator = new ChestAffixGeneratorDelegate(
				env => env.ChestType == ChestType.GoldLocked
			).With(
				WeightedRuleGroup.Of(
					count: () => 1 + (int)Math.Sqrt(dungeonChestGenerator.GetRandom().NextDouble() * 7),
					rules: WeightRules.Of(
						WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
							(input, random) => AccessoryOnHitBuffAffix.CreateFiery(0.15f + random.NextFloat() * 0.15f)), 1
						),
						WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
							(input, random) => AccessoryOnHitBuffAffix.CreatePoisoned(0.15f + random.NextFloat() * 0.15f)), 1
						)
					)
				),
				ChanceRule.Of(
					chance: 0.15f,
					rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
						(input, random) => new ShinyAffix(2.0f + random.NextFloat() * 2.0f)
					)
				)
			));
			#endregion
		}

		public void GenerateAndApplyAffixes(Item item, ChestAffixGenEnvironment environment)
		{
			AffixGenInfo<ChestAffixGenEnvironment> genInfo = new AffixGenInfo<ChestAffixGenEnvironment>(item, environment);
			foreach (ChestAffixGenerator generator in chestGenerators)
			{
				if (generator.MatchesEnvironment(environment))
				{
					generator.SetRandom((UnifiedRandomBridge)WorldGen.genRand);
					item.GetAffixInfo().ApplyAffixes(item, generator.GetOutput(genInfo).Select(affix => affix.Value));
					return;
				}
			}
		}
	}
}