using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix.Content
{
	public class TestAffixContentGlobalItem : GlobalItem
	{
		public override void OnCraft(Item item, Recipe recipe)
		{
			if (!item.CanApplyAffixes())
				return;

			Random rand = new Random();
			List<Affix> affixes = new List<Affix>();

			affixes.Add(OnHitBuffAffix.Fiery());
			affixes.Add(OnHitBuffAffix.Poisoned());

			foreach (Affix affix in affixes)
			{
				int r = rand.Next() % 3;
				switch (r)
				{
					case 0:
						item.ApplyAffix(affix);
						break;
					case 1:
						item.ApplyAffix(new HiddenPotentialAffix(
							affix,
							new HiddenPotentialKillRequirement(2 + rand.Next() % 4)
						));
						break;
				}
			}
		}
	}
}