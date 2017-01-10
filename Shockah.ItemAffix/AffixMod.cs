using Shockah.ItemAffix.Generator;
using Shockah.Utils;
using System;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Shockah.ItemAffix
{
	public class AffixMod : Mod
	{
		public const string ModName = "Shockah.ItemAffix";

		public override string Name => ModName;

		public ChestAffixGenManager ChestGenManager
		{
			get;
			private set;
		}

		public AccessoryChestAffixGenManager AccessoryChestGenManager
		{
			get;
			private set;
		}

		public AffixMod()
		{
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
			};
		}

		public override void Load()
		{
			ChestGenManager = new ChestAffixGenManager();
			AccessoryChestGenManager = new AccessoryChestAffixGenManager();
		}

		// WIP item post-SetDefaults hack
		/*public override void ModifyInterfaceLayers(List<MethodSequenceListItem> layers)
		{
			int layerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Sign Tile Bubble"));

			MethodSequenceListItem theLayer = new MethodSequenceListItem(string.Format("{0} Item Values Hack", ModName), () =>
			{
				if (Main.playerInventory)
				{
				}
				else
				{
				}
				return true;
			});

			if (layerIndex == 0)
				layers.Add(theLayer);
			else
				layers.Insert(layerIndex, theLayer);
		}*/

		public Affix Deserialize(TagCompound tag)
		{
			try
			{
				return TagSerializables.Deserialize<Affix>(tag);
			}
			catch (TypeUnloadedException)
			{
				TagCompound dataTag = tag.HasTag("data") ? tag.GetCompound("data") : null;
				return new UnloadedAffix(tag.GetString("type"), dataTag);
			}
		}

		public TagCompound Serialize(Affix affix)
		{
			UnloadedAffix unloadedAffix = affix as UnloadedAffix;
			if (unloadedAffix == null)
			{
				return TagSerializables.Serialize(affix);
			}
			else
			{
				TagCompound tag = new TagCompound();
				tag["type"] = unloadedAffix.typeName;
				if (unloadedAffix.tag != null)
					tag["data"] = unloadedAffix.tag;
				return tag;
			}
		}
	}
}