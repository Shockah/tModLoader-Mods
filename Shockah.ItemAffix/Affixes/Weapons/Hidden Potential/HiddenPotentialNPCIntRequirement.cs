using System.Collections.Generic;
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

		public override TagCompound SerializeData()
		{
			TagCompound tag = base.SerializeData();
			tag["npcFamilyName"] = npcFamilyName;
			if (matches.Count != 0)
				tag["matches"] = matches;
			return tag;
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
