using Shockah.Utils.OwnedGlobals;
using System.Collections.Generic;

namespace Shockah.ItemAffix
{
	partial class AffixGlobalItem : OwnedGlobalItem
	{
		public List<Affix> affixes { get; private set; } = new List<Affix>();

		public void ApplyChanges()
		{
			HookApplyChanges();
		}

		public void ApplyAffix(Affix affix)
		{
			affix = affix.Clone(owner);
			affixes.Add(affix);
			SetupHooks();
			affix.OnApply();
		}

		public void ApplyAffixes(IEnumerable<Affix> affixes)
		{
			this.affixes.AddRange(affixes);
			SetupHooks();
		}

		public void RemoveAffix(Affix affix)
		{
			affixes.Remove(affix);
			SetupHooks();
			affix.OnRemove();
		}

		public void RemoveAffixes(IEnumerable<Affix> affixes)
		{
			foreach (Affix affix in affixes)
			{
				this.affixes.Remove(affix);
			}
			SetupHooks();
			foreach (Affix affix in affixes)
			{
				affix.OnRemove();
			}
		}
	}
}