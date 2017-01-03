using Terraria;

namespace Shockah.Affix.Content
{
	public abstract class WeaponHeldAffix : NamedItemAffix
	{
		public WeaponHeldAffix(string name, bool prefixedName = true) : base(name, prefixedName)
		{
		}

		public WeaponHeldAffix(string name, string format) : base(name, format)
		{
		}

		public virtual void UpdateWeaponHeld(Item item, Player player)
		{
		}
	}
}