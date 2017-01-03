using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix
{
	public class AffixNPCInfo : NPCInfo
	{
		public readonly Dictionary<Player, List<Item>> participants = new Dictionary<Player, List<Item>>();

		public override NPCInfo Clone()
		{
			AffixNPCInfo clone = new AffixNPCInfo();
			foreach (KeyValuePair<Player, List<Item>> kvp in participants)
			{
				List<Item> items = new List<Item>();
				items.AddRange(kvp.Value);
				clone.participants[kvp.Key] = items;
			}
			return clone;
		}

		public void AddParticipant(Player participant, Item weapon)
		{
			if (!participants.ContainsKey(participant))
				participants[participant] = new List<Item>();

			List<Item> items = participants[participant];
			if (!items.Contains(weapon))
				items.Add(weapon);
		}
	}
}