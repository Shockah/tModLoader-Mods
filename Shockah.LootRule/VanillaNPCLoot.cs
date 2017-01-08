using Shockah.Utils;
using Shockah.Utils.Rule;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Shockah.LootRule
{
	public static class VanillaNPCLoot
	{
		public static int[] PigronIDs = { NPCID.PigronCorruption, NPCID.PigronCrimson, NPCID.PigronHallow };
		public static int[] HallowIDs = {
			NPCID.IlluminantBat, NPCID.IlluminantSlime, NPCID.PigronHallow,
			NPCID.RainbowSlime, NPCID.BigMimicHallow, NPCID.LightMummy, NPCID.Pixie,
			NPCID.EnchantedSword, NPCID.Unicorn, NPCID.ChaosElemental, NPCID.Gastropod
		};

		public static IRule<NPC, ILootResult<NPC>> PigronBacon;
		public static IRule<NPC, ILootResult<NPC>> BlessedApple;
		public static IRule<NPC, ILootResult<NPC>> Plantera;

		private static Func<int> IExpertModifier(Dynamic<int> normal, Dynamic<int> expert)
		{
			return () => Main.expertMode ? expert : normal;
		}

		private static Func<double> DExpertModifier(Dynamic<double> normal, Dynamic<double> expert)
		{
			return () => Main.expertMode ? expert : normal;
		}

		private static void BossDowned(NPC npc, string name, int potionType)
		{
			NPCLoader.BossLoot(npc, ref name, ref potionType);
			Item.NewItem(npc.position, npc.Size, potionType, Main.rand.Inclusive(5, 15));

			int heartCount = Main.rand.Next(5) + 5;
			for (int i = 0; i < heartCount; i++)
			{
				Item.NewItem(npc.position, npc.Size, ItemID.Heart);
			}

			if (npc.type == 125 || npc.type == 126)
			{
				NPC.downedMechBoss2 = true;
				NPC.downedMechBossAny = true;
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue("Enemies.TheTwins") + " " + Lang.misc[50], 175, 75, 255, false);
				}
				else if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, Language.GetTextValue("Enemies.TheTwins") + " " + Lang.misc[50], 255, 175f, 75f, 255f, 0, 0, 0);
				}
			}
			else if (npc.type == 398)
			{
				if (Main.netMode == 0)
				{
					Main.NewText(Language.GetTextValue("Enemies.MoonLord") + " " + Lang.misc[17], 175, 75, 255, false);
				}
				else if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, Language.GetTextValue("Enemies.MoonLord") + " " + Lang.misc[17], 255, 175f, 75f, 255f, 0, 0, 0);
				}
			}
			else
			{
				if (Main.netMode == 0)
				{
					Main.NewText(name + " " + Lang.misc[17], 175, 75, 255, false);
				}
				else if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, name + " " + Lang.misc[17], 255, 175f, 75f, 255f, 0, 0, 0);
				}
			}
		}

		public static void Fill(RuleGroup<NPC, ILootResult<NPC>> ruleGroup)
		{
			ruleGroup.GetSubrules().Add(PigronBacon = ConditionalRule.Of(
				condition: npc => PigronIDs.Contains(npc.type),
				rule: new ItemLootRule<NPC>(ItemID.Bacon)
			));

			ruleGroup.GetSubrules().Add(BlessedApple = ConditionalRule.Of(
				condition: npc => HallowIDs.Contains(npc.type),
				rule: ChanceRule.Of(
					chance: DExpertModifier(1.0 / 200.0, 1.0 / 150.0),
					rule: new ItemLootRule<NPC>(ItemID.BlessedApple)
				)
			));

			ruleGroup.GetSubrules().Add(Plantera = ConditionalRule.Of(
				condition: npc => npc.type == NPCID.Plantera,
				rule: RuleGroup.Of(
					ChanceRule.Of(
						chance: 1.0 / 10.0,
						rule: new ItemLootRule<NPC>(ItemID.PlanteraTrophy)
					),
					LimitedRuleGroup.Of(
						ConditionalRule.Of(
							condition: npc => Main.expertMode,
							rule: new DelegateLootRule<NPC>(
								npc => npc.DropBossBags()
							)
						),
						RuleGroup.Of(
							ChanceRule.Of(
								chance: 1.0 / 7.0,
								rule: new ItemLootRule<NPC>(ItemID.PlanteraMask)
							),
							new ItemLootRule<NPC>(ItemID.TempleKey, IExpertModifier(1, new DynamicIntRange(2, 3))),
							ChanceRule.Of(
								chance: 1.0 / 20.0,
								rule: new ItemLootRule<NPC>(ItemID.Seedling)
							),
							ChanceRule.Of(
								chance: DExpertModifier(1 - 49.0 / 50.0, 1 - Math.Pow(49.0 / 50.0, 2)),
								rule: new ItemLootRule<NPC>(ItemID.TheAxe)
							),
							ChanceRule.Of(
								chance: DExpertModifier(1.0 / 4.0, 1.0),
								rule: new ItemLootRule<NPC>(ItemID.PygmyStaff)
							),
							ChanceRule.Of(
								chance: 1.0 / 10.0,
								rule: new ItemLootRule<NPC>(ItemID.ThornHook)
							),
							WeightedRuleGroup.Of(
								count: IExpertModifier(1, 2),
								rules: WeightRules.Of(
									WeightRule.Of(
										RuleGroup.Of(
											new ItemLootRule<NPC>(ItemID.GrenadeLauncher),
											new ItemLootRule<NPC>(ItemID.RocketI, new DynamicIntRange(50, 149))
										)
									),
									WeightRule.Of(
										weight: (item, outOf) => item > 0 || NPC.downedPlantBoss ? 1.0 : 0.0,
										rule: new ItemLootRule<NPC>(ItemID.VenusMagnum)
									),
									WeightRule.Of(
										weight: (item, outOf) => item > 0 || NPC.downedPlantBoss ? 1.0 : 0.0,
										rule: new ItemLootRule<NPC>(ItemID.NettleBurst)
									),
									WeightRule.Of(
										weight: (item, outOf) => item > 0 || NPC.downedPlantBoss ? 1.0 : 0.0,
										rule: new ItemLootRule<NPC>(ItemID.LeafBlower)
									),
									WeightRule.Of(
										weight: (item, outOf) => item > 0 || NPC.downedPlantBoss ? 1.0 : 0.0,
										rule: new ItemLootRule<NPC>(ItemID.FlowerPow)
									),
									WeightRule.Of(
										weight: (item, outOf) => item > 0 || NPC.downedPlantBoss ? 1.0 : 0.0,
										rule: new ItemLootRule<NPC>(ItemID.WaspGun)
									),
									WeightRule.Of(
										weight: (item, outOf) => item > 0 || NPC.downedPlantBoss ? 1.0 : 0.0,
										rule: new ItemLootRule<NPC>(ItemID.Seedler)
									)
								)
							)
						)
					),
					ConditionalRule.Of(
						condition: npc => !NPC.downedPlantBoss,
						rule: new DelegateLootRule<NPC>(
							npc =>
							{
								NPC.downedPlantBoss = true;
								if (Main.netMode == 0)
								{
									Main.NewText(Lang.misc[33], 50, 255, 130, false);
								}
								else if (Main.netMode == 2)
								{
									NetMessage.SendData(25, -1, -1, Lang.misc[33], 255, 50f, 255f, 130f, 0, 0, 0);
								}
							}
						)
					),
					new DelegateLootRule<NPC>(
						npc => BossDowned(npc, npc.displayName, ItemID.GreaterHealingPotion)
					)
				)
			));
		}
	}
}