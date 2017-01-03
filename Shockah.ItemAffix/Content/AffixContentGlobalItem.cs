using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix.Content
{
	public class AffixContentGlobalItem : GlobalItem
	{
		public override void OnCraft(Item item, Recipe recipe)
		{
			if (!item.CanApplyAffixes())
				return;

			Random rand = new Random();
			List<AffixFactory> affixes = new List<AffixFactory>();
			affixes.Add(OnHitBuffAffix.Fiery);
			affixes.Add(OnHitBuffAffix.Poisoned);
			foreach (AffixFactory affix in affixes)
			{
				int r = rand.Next() % 3;
				switch (r)
				{
					case 0:
						item.ApplyAffix(affix);
						break;
					case 1:
						item.ApplyAffix(HiddenPotentialAffix.Self, (HiddenPotentialAffix hiddenPotentialAffix) =>
						{
							hiddenPotentialAffix.hiddenAffix = affix.Create(item, null);
							hiddenPotentialAffix.required = 5 + rand.Next() % 11;
							hiddenPotentialAffix.progressType = HiddenPotentialProgressType.Hits;
							hiddenPotentialAffix.requirementFormat = "Hits: {progress}/{required}";
						});
						break;
				}
			}
		}
	}
}