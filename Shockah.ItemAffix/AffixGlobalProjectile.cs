using Shockah.Utils;
using Shockah.Utils.OwnedGlobals;
using Terraria;

namespace Shockah.ItemAffix
{
	class AffixGlobalProjectile : OwnedGlobalProjectile
	{
		public override void OnHitNPC(Projectile _, NPC target, int damage, float knockback, bool crit)
		{
			if (weapon == null)
				return;
			Player player = owner.GetOwner();
			if (player == null)
				return;

			weapon.GetAffixInfo().HookOnHitNPC(weapon, player, owner, target, damage, knockback, crit);
			foreach (Item equippedItem in player.armor)
			{
				equippedItem.GetAffixInfo().HookOnHitNPC(weapon, player, owner, target, damage, knockback, crit);
			}
		}
	}
}