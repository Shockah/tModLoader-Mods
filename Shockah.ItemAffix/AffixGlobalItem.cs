using Shockah.Utils.OwnedGlobals;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.ItemAffix
{
	partial class AffixGlobalItem : OwnedGlobalItem
	{
		public override GlobalItem NewInstance(Item item)
		{
			AffixGlobalItem clone = (AffixGlobalItem)base.NewInstance(item);
			clone.affixes = new List<Affix>();
			clone.affixes.AddRange(affixes);
			clone.SetupHooks();
			return clone;
		}

		public override void ModifyTooltips(Item _, List<TooltipLine> tooltips)
		{
			TooltipLine nameLine = tooltips.Find(line => line.Name == "ItemName");
			if (nameLine != null)
				nameLine.text = HookGetFormattedName(nameLine.text);

			HookModifyTooltips(tooltips);
		}

		public override void SetDefaults(Item _)
		{
			HookApplyChanges();
		}

		public override void PostReforge(Item _)
		{
			HookApplyChanges();
		}

		public override void OnHitNPC(Item _, Player player, NPC target, int damage, float knockBack, bool crit)
		{
			HookOnHitNPC(owner, player, target, damage, knockBack, crit);
			foreach (Item equippedItem in player.armor)
			{
				equippedItem.GetAffixInfo().HookOnHitNPC(owner, player, target, damage, knockBack, crit);
			}
		}
	}
}