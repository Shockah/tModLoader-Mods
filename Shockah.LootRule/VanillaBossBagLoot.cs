using Shockah.Utils.Rule;
using Terraria;
using Terraria.ID;

namespace Shockah.LootRule
{
	public static class VanillaBossBagLoot
	{
		public static IRule<BossBag, ILootResult<Player>> Plantera;

		public static void Fill(RuleGroup<BossBag, ILootResult<Player>> ruleGroup)
		{
			ruleGroup.GetSubrules().Add(Plantera = ConditionalRule.Of(
				condition: bag => bag.item.type == ItemID.PlanteraBossBag,
				rule: new ItemLootRule<Player>(ItemID.Bacon)
			));
		}
	}

	public class BossBag
	{
		public readonly Player player;
		public readonly Item item;

		public BossBag(Player player, Item item)
		{
			this.player = player;
			this.item = item;
		}
	}
}