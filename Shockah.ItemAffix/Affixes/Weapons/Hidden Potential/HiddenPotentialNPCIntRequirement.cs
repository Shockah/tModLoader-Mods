using System.Collections.Generic;
using System.Linq;
using Shockah.Utils;
using Terraria;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix.Content
{
	public abstract class HiddenPotentialNPCIntRequirement : HiddenPotentialIntRequirement
	{
		public readonly List<NPCMatcher> matches = new List<NPCMatcher>();
		public readonly string npcFamilyName;

		public HiddenPotentialNPCIntRequirement(int required, string npcFamilyName) : base(required)
		{
			this.npcFamilyName = npcFamilyName;
		}

		public override void SerializeData(TagCompound tag)
		{
			base.SerializeData(tag);
			tag["npcFamilyName"] = npcFamilyName;
			if (matches.Count != 0)
				tag["matches"] = matches.Select(match => TagSerializables.Serialize(match)).ToList();
		}

		public virtual bool Matches(NPC npc)
		{
			if (matches.Count == 0)
				return true;
			foreach (NPCMatcher match in matches)
			{
				if (match.Matches(npc))
					return true;
			}
			return false;
		}
	}
}
