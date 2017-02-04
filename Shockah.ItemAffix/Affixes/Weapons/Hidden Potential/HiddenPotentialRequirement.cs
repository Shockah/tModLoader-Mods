using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public abstract class HiddenPotentialRequirement : TagSerializable
	{
		public abstract TagCompound SerializeData();

		public abstract string GetTooltip(Item item, HiddenPotentialAffix affix);

		public virtual void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit, HiddenPotentialAffix affix)
		{
		}

		public virtual void OnHitNPC(Item item, Player player, Projectile projectile, NPC target, int damage, float knockBack, bool crit, HiddenPotentialAffix affix)
		{
		}
	}
}