using Shockah.ItemAffix.Content;
using Shockah.Utils;
using Shockah.Utils.Rule;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace Shockah.ItemAffix.Generator
{
	public class ChestAffixGenManager
	{
		public readonly List<ChestAffixGenerator> chestGenerators = new List<ChestAffixGenerator>();
		public readonly ChestAffixGenerator surfaceChestGenerator;
		public readonly ChestAffixGenerator undergroundChestGenerator;
		public readonly ChestAffixGenerator cavernsChestGenerator;
		public readonly ChestAffixGenerator dungeonChestGenerator;

		public ChestAffixGenManager()
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
					count: () => (int)(0.5 + Math.Pow(surfaceChestGenerator.GetRandom().NextDouble(), 2) * 2.0),
					rules: WeightRules.Of(
						WeightRule.Of(
							weight: 1.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => OnHitBuffAffix.CreateFiery(0.3f + random.NextFloat() * 0.2f)
							)
						),
						WeightRule.Of(
							weight: 1.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => OnHitBuffAffix.CreatePoisoned(0.3f + random.NextFloat() * 0.2f)
							)
						),
						WeightRule.Of(
							weight: 5.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => new WeaponHeldDefenseAffix(random.Inclusive(1, 6))
							)
						),
						WeightRule.Of(
							weight: 5.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => new WeaponHeldMovementSpeedAffix(0.03f + random.NextFloat() * 0.07f)
							)
						),
						WeightRule.Of(
							weight: 3.5,
							rule: WeightedRuleGroup.Of(
								WeightRule.Of(
									new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
										(input, random) => new DamageBaneAffix("Slimes", 1.3f + random.NextFloat() * 0.4f).WithMatches(
											new RegexNameNPCMatcher(@"\bSlime\b")
										).AsHiddenPotentialWithKillRequirement(30 + random.Inclusive(0, 70))
									)
								),
								WeightRule.Of(
									new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
										(input, random) => new DamageBaneAffix("Zombies", 1.3f + random.NextFloat() * 0.4f).WithMatches(
											new RegexNameNPCMatcher(@"\bZombie\b"),
											new RegexNameNPCMatcher(@"\bEye\b")
										).AsHiddenPotentialWithKillRequirement(30 + random.Inclusive(0, 70))
									)
								)
							)
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

			#region Underground
			chestGenerators.Add(undergroundChestGenerator = new ChestAffixGeneratorDelegate(
				env => belowSurface.Contains(env.ChestType) && env.y.Between((int)Main.worldSurface, (int)Main.rockLayer)
			).With(
				WeightedRuleGroup.Of(
					count: () => (int)(0.75 + Math.Pow(undergroundChestGenerator.GetRandom().NextDouble(), 2) * 2.75),
					rules: WeightRules.Of(
						WeightRule.Of(
							weight: 1.2,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => OnHitBuffAffix.CreateFiery(0.4f + random.NextFloat() * 0.2f)
							)
						),
						WeightRule.Of(
							weight: 1.2,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => OnHitBuffAffix.CreatePoisoned(0.4f + random.NextFloat() * 0.2f)
							)
						),
						WeightRule.Of(
							weight: 5.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => new WeaponHeldDefenseAffix(random.Inclusive(2, 9))
							)
						),
						WeightRule.Of(
							weight: 5.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => new WeaponHeldMovementSpeedAffix(0.05f + random.NextFloat() * 0.10f)
							)
						),
						WeightRule.Of(
							weight: 7.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => new CriticalDamageAffix(0.4f + random.NextFloat() * 0.2f)
							)
						),
						WeightRule.Of(
							weight: 5.0,
							rule: WeightedRuleGroup.Of(
								WeightRule.Of(
									new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
										(input, random) => new DamageBaneAffix("Zombies", 1.3f + random.NextFloat() * 0.4f).WithMatches(
											new RegexNameNPCMatcher(@"\bZombie\b"),
											new RegexNameNPCMatcher(@"\bEye\b")
										).AsHiddenPotentialWithKillRequirement(30 + random.Inclusive(0, 70))
									)
								),
								WeightRule.Of(
									new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
										(input, random) => new DamageBaneAffix("Silverplated", "Undead", 1.3f + random.NextFloat() * 0.4f).WithMatches(
											new RegexNameNPCMatcher(@"\bZombie\b"),
											new RegexNameNPCMatcher(@"\bEye\b"),
											new RegexNameNPCMatcher(@"\bSkeleton\b")
										).AsHiddenPotentialWithKillRequirement(30 + random.Inclusive(0, 70))
									)
								)
							)
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

			#region Caverns
			chestGenerators.Add(cavernsChestGenerator = new ChestAffixGeneratorDelegate(
				env => belowSurface.Contains(env.ChestType) && env.y.Between((int)Main.rockLayer, Main.maxTilesY - 200)
			).With(
				WeightedRuleGroup.Of(
					count: () => (int)(1 + Math.Pow(cavernsChestGenerator.GetRandom().NextDouble(), 2) * 2.5),
					rules: WeightRules.Of(
						WeightRule.Of(
							weight: 2.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => OnHitBuffAffix.CreateFiery(0.5f + random.NextFloat() * 0.25f)
							)
						),
						WeightRule.Of(
							weight: 2.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => OnHitBuffAffix.CreatePoisoned(0.5f + random.NextFloat() * 0.25f)
							)
						),
						WeightRule.Of(
							weight: 5.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => new WeaponHeldDefenseAffix(random.Inclusive(4, 12))
							)
						),
						WeightRule.Of(
							weight: 5.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => new WeaponHeldMovementSpeedAffix(0.07f + random.NextFloat() * 0.13f)
							)
						),
						WeightRule.Of(
							weight: 7.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => new CriticalDamageAffix(0.5f + random.NextFloat() * 0.2f)
							)
						),
						WeightRule.Of(
							weight: 5.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => new DamageBaneAffix("Silverplated", "Undead", 1.3f + random.NextFloat() * 0.4f).WithMatches(
									new RegexNameNPCMatcher(@"\bZombie\b"),
									new RegexNameNPCMatcher(@"\bEye\b"),
									new RegexNameNPCMatcher(@"\bSkeleton\b")
								).AsHiddenPotentialWithKillRequirement(30 + random.Inclusive(0, 70))
							)
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
					count: () => (int)(1 + Math.Pow(dungeonChestGenerator.GetRandom().NextDouble(), 2) * 3),
					rules: WeightRules.Of(
						WeightRule.Of(
							weight: 1.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => OnHitBuffAffix.CreateFiery()
							)
						),
						WeightRule.Of(
							weight: 1.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => OnHitBuffAffix.CreatePoisoned()
							)
						),
						WeightRule.Of(
							weight: 1.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => new WeaponHeldDefenseAffix(random.Inclusive(6, 12))
							)
						),
						WeightRule.Of(
							weight: 1.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => new WeaponHeldMovementSpeedAffix(0.10f + random.NextFloat() * 0.10f)
							)
						),
						WeightRule.Of(
							weight: 1.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => new CriticalDamageAffix(0.5f + random.NextFloat() * 0.25f)
							)
						),
						WeightRule.Of(
							weight: 2.0,
							rule: new RuleDelegate<AffixGenInfo<ChestAffixGenEnvironment>, Dynamic<Affix>>(
								(input, random) => new DamageBaneAffix("Silverplated", "Undead", 1.3f + random.NextFloat() * 0.4f).WithMatches(
									new RegexNameNPCMatcher(@"\bZombie\b"),
									new RegexNameNPCMatcher(@"\bEye\b"),
									new RegexNameNPCMatcher(@"\bSkeleton\b")
								).AsHiddenPotentialWithKillRequirement(30 + random.Inclusive(0, 70))
							)
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
				}
			}
		}
	}
}