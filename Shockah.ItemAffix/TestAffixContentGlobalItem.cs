﻿using System;
using System.Collections.Generic;
using Shockah.Affix.Utils;
using Terraria;
using Terraria.ModLoader;
using Shockah.Affix.Content;

namespace Shockah.Affix
{
	public class TestAffixContentGlobalItem : GlobalItem
	{
		public override void OnCraft(Item item, Recipe recipe)
		{
			if (!item.CanApplyAffixes())
				return;

			Random rand = new Random();
			List<Dynamic<Affix>> affixes = new List<Dynamic<Affix>>();

			affixes.Add(OnHitBuffAffix.Fiery());
			affixes.Add(OnHitBuffAffix.Poisoned());
			affixes.Add((Func<Affix>)(() =>
			{
				Affix[] subaffixes = {
					WeaponHeldDefenseAffix.Defense3,
					WeaponHeldDefenseAffix.Defense6,
					WeaponHeldDefenseAffix.Defense9,
					WeaponHeldDefenseAffix.Defense12
				};
				return subaffixes[rand.Next(subaffixes.Length)];
			}));
			affixes.Add((Func<Affix>)(() =>
			{
				Affix[] subaffixes = {
					WeaponHeldMovementSpeedAffix.MoveSpeed5,
					WeaponHeldMovementSpeedAffix.MoveSpeed10,
					WeaponHeldMovementSpeedAffix.MoveSpeed15,
					WeaponHeldMovementSpeedAffix.MoveSpeed20
				};
				return subaffixes[rand.Next(subaffixes.Length)];
			}));

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