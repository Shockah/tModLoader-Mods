using Shockah.Utils;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public abstract class NPCMatcher : TagSerializable
	{
		public readonly string lang;

		public NPCMatcher(string lang)
		{
			this.lang = lang;
		}

		public virtual void SerializeData(TagCompound tag)
		{
			tag["lang"] = lang;
		}

		public abstract bool Matches(NPC npc);
	}
}