using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix
{
	public abstract class Affix : TagSerializable
	{
		public string name { get; private set; }
		public Item item { get; private set; }

		protected Affix(string name)
		{
			this.name = name;
		}

		public Affix Clone(Item item)
		{
			AffixMod mod = AffixMod.Instance;
			Affix affix = mod.Deserialize(mod.Serialize(this));
			affix.item = item;
			return affix;
		}

		public virtual TagCompound SerializeData()
		{
			TagCompound tag = new TagCompound();
			tag["name"] = name;
			return tag;
		}

		public virtual void OnApply()
		{
		}

		public virtual void OnRemove()
		{
		}

		public virtual string GetFormattedName(string oldName)
		{
			return oldName;
		}

		public virtual void ModifyTooltips(List<TooltipLine> tooltips)
		{
		}

		public virtual void ApplyChanges()
		{
		}

		public virtual void GetWeaponDamage(Item weapon, Player player, ref int damage)
		{
		}

		public virtual void OnHitNPC(Item weapon, Player player, NPC target, int damage, float knockBack, bool crit)
		{
		}

		public virtual void OnHitNPC(Item weapon, Player player, Projectile projectile, NPC target, int damage, float knockBack, bool crit)
		{
		}

		public virtual void UpdateEquip(Player player)
		{
		}

		public virtual void ModifyHitByItem(Player player, NPC npc, ref int damage, ref float knockback, ref bool crit)
		{
		}

		public virtual void ModifyHitByProjectile(Projectile projectile, Player player, NPC npc, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
		}
	}
}