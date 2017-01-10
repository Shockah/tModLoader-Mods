using Shockah.Utils;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public abstract class NPCMatcher : TagSerializable
	{
		public abstract void SerializeData(TagCompound tag);

		public abstract bool Matches(NPC npc);
	}
}