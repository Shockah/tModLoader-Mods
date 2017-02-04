using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public abstract class NPCMatcher : TagSerializable
	{
		public abstract TagCompound SerializeData();

		public abstract bool Matches(NPC npc);
	}
}