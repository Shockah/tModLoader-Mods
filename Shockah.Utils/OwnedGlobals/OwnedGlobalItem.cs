using Terraria;
using Terraria.ModLoader;

namespace Shockah.Utils.OwnedGlobals
{
	public class OwnedGlobalItem : GlobalItem
	{
		public override bool InstancePerEntity => true;
		public Item owner { get; private set; }

		public override GlobalItem NewInstance(Item item)
		{
			OwnedGlobalItem global = (OwnedGlobalItem)base.NewInstance(item);
			global.owner = item;
			return global;
		}
	}
}