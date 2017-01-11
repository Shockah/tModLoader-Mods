using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Shockah.Utils;

namespace Shockah.ItemAffix
{
	public class AffixItemInfo : ItemInfo
	{
		private delegate void ActionNNNR<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, ref T4 t4);
		private delegate void ActionNNNRRR<T1, T2, T3, T4, T5, T6>(T1 t1, T2 t2, T3 t3, ref T4 t4, ref T5 t5, ref T6 t6);
		private delegate void ActionNNNNRRRR<T1, T2, T3, T4, T5, T6, T7, T8>(T1 t1, T2 t2, T3 t3, T4 t4, ref T5 t5, ref T6 t6, ref T7 t7, ref T8 t8);

		public readonly List<Affix> affixes = new List<Affix>();

		private Func<Item, string, string>[] hooksGetFormattedName;
		private Action<Item, List<TooltipLine>>[] hooksModifyTooltips;
		private Action<Item>[] hooksApplyChanges;
		private ActionNNNR<Item, Item, Player, int>[] hooksGetWeaponDamage;
		private Action<Item, Item, Player, NPC, int, float, bool>[] hooksOnHitNPC;
		private Action<Item, Item, Player, Projectile, NPC, int, float, bool>[] hooksOnHitNPC2;
		private Action<Item, Player>[] hooksUpdateEquip;
		private ActionNNNRRR<Item, Player, NPC, int, float, bool>[] hooksModifyHitByItem;
		private ActionNNNNRRRR<Item, Projectile, Player, NPC, int, float, bool, int>[] hooksModifyHitByProjectile;

		public override ItemInfo Clone()
		{
			AffixItemInfo clone = new AffixItemInfo();
			clone.affixes.AddRange(affixes);
			clone.SetupHooks();
			return clone;
		}

		public void ApplyChanges(Item item)
		{
			//item.name = GetFormattedName(item, item.name);
			foreach (var method in hooksApplyChanges)
			{
				method(item);
			}
		}

		public void ApplyAffix(Item item, Affix affix)
		{
			affixes.Add(affix);
			SetupHooks();
			affix.OnApply(item);
		}

		public void ApplyAffixes(Item item, IEnumerable<Affix> affixes)
		{
			this.affixes.AddRange(affixes);
			SetupHooks();
		}

		public void RemoveAffix(Item item, Affix affix)
		{
			affixes.Remove(affix);
			SetupHooks();
			affix.OnRemove(item);
		}

		public void RemoveAffixes(Item item, IEnumerable<Affix> affixes)
		{
			foreach (Affix affix in affixes)
			{
				this.affixes.Remove(affix);
			}
			SetupHooks();
			foreach (Affix affix in affixes)
			{
				affix.OnRemove(item);
			}
		}

		private void BuildHooks<R>(out R[] hooks, Func<Affix, R> func) where R : class
		{
			Hooks.Build(out hooks, affixes, func);
		}

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

		public string GetFormattedName(Item item, string oldName)
		{
			string name = oldName;
			foreach (var method in hooksGetFormattedName)
			{
				name = method(item, name);
			}
			return name;
		}

		public void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			foreach (var method in hooksModifyTooltips)
			{
				method(item, tooltips);
			}
		}

		public void GetWeaponDamage(Item affixedItem, Item item, Player player, ref int damage)
		{
			foreach (var method in hooksGetWeaponDamage)
			{
				method(affixedItem, item, player, ref damage);
			}
		}

		public void OnHitNPC(Item affixedItem, Item item, Player player, NPC target, int damage, float knockBack, bool crit)
		{
			foreach (var method in hooksOnHitNPC)
			{
				method(affixedItem, item, player, target, damage, knockBack, crit);
			}
		}

		public void OnHitNPC(Item affixedItem, Item item, Player player, Projectile projectile, NPC target, int damage, float knockBack, bool crit)
		{
			foreach (var method in hooksOnHitNPC2)
			{
				method(affixedItem, item, player, projectile, target, damage, knockBack, crit);
			}
		}

		public void UpdateEquip(Item item, Player player)
		{
			foreach (var method in hooksUpdateEquip)
			{
				method(item, player);
			}
		}

		public void ModifyHitByItem(Item item, Player player, NPC npc, ref int damage, ref float knockback, ref bool crit)
		{
			foreach (var method in hooksModifyHitByItem)
			{
				method(item, player, npc, ref damage, ref knockback, ref crit);
			}
		}

		public void ModifyHitByProjectile(Item item, Projectile projectile, Player player, NPC npc, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			foreach (var method in hooksModifyHitByProjectile)
			{
				method(item, projectile, player, npc, ref damage, ref knockback, ref crit, ref hitDirection);
			}
		}
	}
}