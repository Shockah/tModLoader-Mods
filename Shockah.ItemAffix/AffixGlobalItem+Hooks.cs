using Shockah.Utils;
using Shockah.Utils.OwnedGlobals;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.ItemAffix
{
	partial class AffixGlobalItem : OwnedGlobalItem
	{
		private delegate void ActionNNR<T1, T2, T3>(T1 t1, T2 t2, ref T3 t3);
		private delegate void ActionNNRRR<T1, T2, T3, T4, T5>(T1 t1, T2 t2, ref T3 t3, ref T4 t4, ref T5 t5);
		private delegate void ActionNNNRRRR<T1, T2, T3, T4, T5, T6, T7>(T1 t1, T2 t2, T3 t3, ref T4 t4, ref T5 t5, ref T6 t6, ref T7 t7);

		private Func<string, string>[] hooksGetFormattedName;
		private Action<List<TooltipLine>>[] hooksModifyTooltips;
		private Action[] hooksApplyChanges;
		private ActionNNR<Item, Player, int>[] hooksGetWeaponDamage;
		private Action<Item, Player, NPC, int, float, bool>[] hooksOnHitNPC;
		private Action<Item, Player, Projectile, NPC, int, float, bool>[] hooksOnHitNPC2;
		private Action<Player>[] hooksUpdateEquip;
		private ActionNNRRR<Player, NPC, int, float, bool>[] hooksModifyHitByItem;
		private ActionNNNRRRR<Projectile, Player, NPC, int, float, bool, int>[] hooksModifyHitByProjectile;

		private void SetupHooks()
		{
			BuildHooks(out hooksGetFormattedName, o => o.GetFormattedName);
			BuildHooks(out hooksModifyTooltips, o => o.ModifyTooltips);
			BuildHooks(out hooksApplyChanges, o => o.ApplyChanges);
			BuildHooks(out hooksGetWeaponDamage, o => o.GetWeaponDamage);
			BuildHooks(out hooksOnHitNPC, o => o.OnHitNPC);
			BuildHooks(out hooksOnHitNPC2, o => o.OnHitNPC);
			BuildHooks(out hooksUpdateEquip, o => o.UpdateEquip);
			BuildHooks(out hooksModifyHitByItem, o => o.ModifyHitByItem);
			BuildHooks(out hooksModifyHitByProjectile, o => o.ModifyHitByProjectile);
		}

		private void BuildHooks<R>(out R[] hooks, Func<Affix, R> func) where R : class
		{
			Hooks.Build(out hooks, affixes, func);
		}

		internal string HookGetFormattedName(string oldName)
		{
			string name = oldName;
			foreach (var method in hooksGetFormattedName)
			{
				name = method(name);
			}
			return name;
		}

		internal void HookModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach (var method in hooksModifyTooltips)
			{
				method(tooltips);
			}
		}

		internal void HookApplyChanges()
		{
			foreach (var method in hooksApplyChanges)
			{
				method();
			}
		}

		internal void HookGetWeaponDamage(Item weapon, Player player, ref int damage)
		{
			foreach (var method in hooksGetWeaponDamage)
			{
				method(weapon, player, ref damage);
			}
		}

		internal void HookOnHitNPC(Item weapon, Player player, NPC target, int damage, float knockback, bool crit)
		{
			foreach (var method in hooksOnHitNPC)
			{
				method(weapon, player, target, damage, knockback, crit);
			}
		}

		internal void HookOnHitNPC(Item weapon, Player player, Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			foreach (var method in hooksOnHitNPC2)
			{
				method(weapon, player, projectile, target, damage, knockback, crit);
			}
		}

		internal void HookUpdateEquip(Player player)
		{
			foreach (var method in hooksUpdateEquip)
			{
				method(player);
			}
		}

		internal void HookModifyHitByItem(Player player, NPC npc, ref int damage, ref float knockback, ref bool crit)
		{
			foreach (var method in hooksModifyHitByItem)
			{
				method(player, npc, ref damage, ref knockback, ref crit);
			}
		}

		internal void HookModifyHitByProjectile(Projectile projectile, Player player, NPC npc, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			foreach (var method in hooksModifyHitByProjectile)
			{
				method(projectile, player, npc, ref damage, ref knockback, ref crit, ref hitDirection);
			}
		}
	}
}