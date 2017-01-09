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
			ChestAffixGenerator chestGenerator;

			ChestType[] belowSurface = {
				ChestType.Gold, ChestType.RichMahogany, ChestType.Ivy, ChestType.Ice, ChestType.Granite, ChestType.Marble, ChestType.Water, ChestType.Mushroom
			};

			#region Surface
			chestGenerators.Add(chestGenerator = surfaceChestGenerator = new ChestAffixGeneratorDelegate(
				env => new ChestType[] { ChestType.Wooden, ChestType.LivingWood, ChestType.Water }.Contains(env.ChestType) && env.y <= Main.worldSurface
			));
			chestGenerator.rule.WithCount(() => (int)(0.5 + Math.Sqrt(Main.rand.NextDouble() * 4.5)));
			chestGenerator.rule.With(
				WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
					(input, random) => AccessoryOnHitBuffAffix.CreateFiery(0.05f + random.NextFloat() * 0.05f)), 1
				),
				WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
					(input, random) => AccessoryOnHitBuffAffix.CreatePoisoned(0.05f + random.NextFloat() * 0.05f)), 1
				)
			);
			#endregion

			#region Underground
			chestGenerators.Add(chestGenerator = undergroundChestGenerator = new ChestAffixGeneratorDelegate(
				env => belowSurface.Contains(env.ChestType) && env.y.Between((int)Main.worldSurface, (int)Main.rockLayer)
			));
			chestGenerator.rule.WithCount(() => 1 + (int)Math.Sqrt(Main.rand.NextDouble() * 5));
			chestGenerator.rule.With(
				WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
					(input, random) => AccessoryOnHitBuffAffix.CreateFiery(0.08f + random.NextFloat() * 0.07f)), 1
				),
				WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
					(input, random) => AccessoryOnHitBuffAffix.CreatePoisoned(0.08f + random.NextFloat() * 0.07f)), 1
				)
			);
			#endregion

			#region Caverns
			chestGenerators.Add(chestGenerator = cavernsChestGenerator = new ChestAffixGeneratorDelegate(
				env => belowSurface.Contains(env.ChestType) && env.y.Between((int)Main.rockLayer, Main.maxTilesY - 200)
			));
			chestGenerator.rule.WithCount(() => 1 + (int)Math.Sqrt(Main.rand.NextDouble() * 6));
			chestGenerator.rule.With(
				WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
					(input, random) => AccessoryOnHitBuffAffix.CreateFiery(0.10f + random.NextFloat() * 0.10f)), 1
				),
				WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
					(input, random) => AccessoryOnHitBuffAffix.CreatePoisoned(0.10f + random.NextFloat() * 0.10f)), 1
				)
			);
			#endregion

			#region Dungeon
			chestGenerators.Add(chestGenerator = dungeonChestGenerator = new ChestAffixGeneratorDelegate(
				env => env.ChestType == ChestType.GoldLocked
			));
			chestGenerator.rule.WithCount(() => 1 + (int)Math.Sqrt(Main.rand.NextDouble() * 7));
			chestGenerator.rule.With(
				WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
					(input, random) => AccessoryOnHitBuffAffix.CreateFiery(0.15f + random.NextFloat() * 0.15f)), 1
				),
				WeightRule.Of(new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
					(input, random) => AccessoryOnHitBuffAffix.CreatePoisoned(0.15f + random.NextFloat() * 0.15f)), 1
				)
			);
			#endregion
		}

		public void GenerateAndApplyAffixes(Item item, ChestAffixGenEnvironment environment)
		{
			AffixGenInfo<ChestAffixGenEnvironment> genInfo = new AffixGenInfo<ChestAffixGenEnvironment>(item, environment);
			foreach (ChestAffixGenerator generator in chestGenerators)
			{
				if (generator.MatchesEnvironment(environment))
				{
					item.GetAffixInfo().ApplyAffixes(item, generator.GetOutput(genInfo).Select(affix => affix.Value));
					return;
				}
			}
		}
	}
}