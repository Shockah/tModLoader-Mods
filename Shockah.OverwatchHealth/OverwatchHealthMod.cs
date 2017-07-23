using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Shockah.OverwatchHealth
{
	class OverwatchHealthMod : Mod
	{
		public OverwatchHealthMod()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}

		public override void ModifyInterfaceLayers(List<MethodSequenceListItem> layers)
		{
			base.ModifyInterfaceLayers(layers);
		}
	}
}