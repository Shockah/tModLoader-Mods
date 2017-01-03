using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix.Content
{
	public class AffixContentModPlayer : ModPlayer
	{
		public override void PostUpdateEquips()
		{
			Item heldItem = player.HeldItem;
			if (heldItem == null)
				return;

			foreach (Affix affix in heldItem.GetAffixes())
			{
				WeaponHeldAffix weaponHeldAffix = affix as WeaponHeldAffix;
				if (weaponHeldAffix != null)
					weaponHeldAffix.UpdateWeaponHeld(heldItem, player);
			}
		}
	}
}