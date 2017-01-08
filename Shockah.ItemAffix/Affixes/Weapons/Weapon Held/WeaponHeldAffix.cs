using Terraria;

namespace Shockah.ItemAffix.Content
{
	public abstract class WeaponHeldAffix : NamedItemAffix
	{
		public WeaponHeldAffix(string name, string format = PrefixFormat) : base(name, format)
		{
		}

		public virtual void UpdateWeaponHeld(Item item, Player player)
		{
		}
	}
}