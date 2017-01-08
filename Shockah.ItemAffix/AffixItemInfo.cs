using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Shockah.Utils;

namespace Shockah.ItemAffix
{
	public class AffixItemInfo : ItemInfo
	{
		private delegate void ActionNNNRRR<T1, T2, T3, T4, T5, T6>(T1 t1, T2 t2, T3 t3, ref T4 t4, ref T5 t5, ref T6 t6);
		private delegate void ActionNNNNRRRR<T1, T2, T3, T4, T5, T6, T7, T8>(T1 t1, T2 t2, T3 t3, T4 t4, ref T5 t5, ref T6 t6, ref T7 t7, ref T8 t8);

		public readonly List<Affix> affixes = new List<Affix>();

		private Func<Item, string, string>[] hooksGetFormattedName;
		private Action<Item, List<TooltipLine>>[] hooksModifyTooltips;
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

		public void ApplyAffix(Item item, Affix affix)
		{
			affixes.Add(affix);
			SetupHooks();
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
		}

		public void RemoveAffixes(Item item, IEnumerable<Affix> affixes)
		{
			foreach (Affix affix in affixes)
			{
				this.affixes.Remove(affix);
			}
			SetupHooks();
		}

		private IEnumerable<R> BuildHooks<R>(Func<Affix, R> func) where R : class
		{
			return Hooks.Build<Affix, R>(affixes, func);
		}

		private void SetupHooks()
		{
			hooksGetFormattedName = BuildHooks<Func<Item, string, string>>(o => o.GetFormattedName).ToArray();
			hooksModifyTooltips = BuildHooks<Action<Item, List<TooltipLine>>>(o => o.ModifyTooltips).ToArray();
			hooksOnHitNPC = BuildHooks<Action<Item, Item, Player, NPC, int, float, bool>>(o => o.OnHitNPC).ToArray();
			hooksOnHitNPC2 = BuildHooks<Action<Item, Item, Player, Projectile, NPC, int, float, bool>>(o => o.OnHitNPC).ToArray();
			hooksUpdateEquip = BuildHooks<Action<Item, Player>>(o => o.UpdateEquip).ToArray();
			hooksModifyHitByItem = BuildHooks<ActionNNNRRR<Item, Player, NPC, int, float, bool>>(o => o.ModifyHitByItem).ToArray();
			hooksModifyHitByProjectile = BuildHooks<ActionNNNNRRRR<Item, Projectile, Player, NPC, int, float, bool, int>>(o => o.ModifyHitByProjectile).ToArray();
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