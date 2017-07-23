namespace Shockah.ItemAffix
{
	public abstract class NamedItemAffix : Affix
	{
		public const string PrefixFormat = "{affix} {item}";
		public const string SuffixFormat = "{item} {affix}";
		public const string SuffixOfFormat = "{item} of {affix}";
		public const string SuffixOfTheFormat = "{item} of the {affix}";

		public readonly string format;

		public NamedItemAffix(string name, string format = PrefixFormat) : base(name)
		{
			this.format = format;
		}

		public override string GetFormattedName(string oldName)
		{
			string format = this.format;
			if (item.Name.Contains(format.Replace("{item}", "").Replace("{affix}", name)))
				format = "{item}";
			format = format.Replace("{affix}", name);
			format = format.Replace("{item}", oldName);
			return format;
		}
	}
}