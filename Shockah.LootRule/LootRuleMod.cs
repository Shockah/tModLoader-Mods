using Shockah.Utils.Rule;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.LootRule
{
	public class LootRuleMod : Mod
	{
		public const string ModName = "Shockah.LootRule";

		public override string Name => ModName;

		public BackupRuleManager<RuleGroup<NPC, ILootResult<NPC>>, NPC, ILootResult<NPC>> npcLootRuleManager;
		public BackupRuleManager<RuleGroup<BossBag, ILootResult<BossBag>>, BossBag, ILootResult<BossBag>> bossBagLootRuleManager;

		public LootRuleMod()
		{
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
			};
		}

		public override void Load()
		{
			npcLootRuleManager = new BackupRuleManager<RuleGroup<NPC, ILootResult<NPC>>, NPC, ILootResult<NPC>>(new RuleGroup<NPC, ILootResult<NPC>>());
			bossBagLootRuleManager = new BackupRuleManager<RuleGroup<BossBag, ILootResult<BossBag>>, BossBag, ILootResult<BossBag>>(new RuleGroup<BossBag, ILootResult<BossBag>>());
			VanillaNPCLoot.Fill(npcLootRuleManager.GetRule().GetRule());
			VanillaBossBagLoot.Fill(bossBagLootRuleManager.GetRule().GetRule());
		}
	}
}