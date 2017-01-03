using Shockah.Affix.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace Shockah.Affix
{
	public class AffixMod : Mod
	{
		public readonly Dictionary<string, AffixFactory> affixFactories = new Dictionary<string, AffixFactory>();

		public override string Name => "Shockah.Affix";

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
			Register(OnHitBuffAffix.Fiery);
			Register(OnHitBuffAffix.Poisoned);
			Register(HiddenPotentialAffix.Self);
		}

		public void Register(AffixFactory factory)
		{
			if (affixFactories.ContainsKey(factory.internalName))
				throw new Exception(string.Format("AffixFactory '{0}' already registered.", factory.internalName));
			affixFactories[factory.internalName] = factory;
		}

		public void Unregister(AffixFactory factory)
		{
			affixFactories.Remove(factory.internalName);
		}

		public AffixFactory GetAffixFactory(string internalName)
		{
			AffixFactory factory;
			return affixFactories.TryGetValue(internalName, out factory) ? factory : null;
		}

		public void ApplyAffix(Item item, string internalName)
		{
			AffixItemInfo info = item.GetModInfo<AffixItemInfo>(this);
			info.ApplyAffix(item, internalName);
		}

		public void ApplyAffix(Item item, AffixFactory factory)
		{
			AffixItemInfo info = item.GetModInfo<AffixItemInfo>(this);
			info.ApplyAffix(item, factory);
		}

		public void ApplyAffix<T>(Item item, string internalName, Action<T> createDelegate) where T : Affix
		{
			AffixItemInfo info = item.GetModInfo<AffixItemInfo>(this);
			info.ApplyAffix(item, internalName, createDelegate);
		}

		public void ApplyAffix<T>(Item item, AffixFactory factory, Action<T> createDelegate) where T : Affix
		{
			AffixItemInfo info = item.GetModInfo<AffixItemInfo>(this);
			info.ApplyAffix(item, factory, createDelegate);
		}

		public void ApplyAffix(Item item, Affix affix)
		{
			AffixItemInfo info = item.GetModInfo<AffixItemInfo>(this);
			info.ApplyAffix(item, affix);
		}

		public void RemoveAffix(Item item, Affix affix)
		{
			AffixItemInfo info = item.GetModInfo<AffixItemInfo>(this);
			info.RemoveAffix(item, affix);
		}

		public bool CanApplyAffixes(Item item)
		{
			return item.damage > 0 && item.maxStack == 1;
		}
	}
}