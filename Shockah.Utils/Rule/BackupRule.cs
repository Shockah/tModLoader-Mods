using System;
using System.Collections.Generic;

namespace Shockah.Utils.Rule
{
	public interface IBackupRule<out RuleType, in Input, Output> : IRule<Input, Output> where RuleType : IRule<Input, Output>
	{
		RuleType GetRule();

		void Backup();

		void Restore();
	}

	public interface IBackupRule<in Input, Output> : IBackupRule<IRule<Input, Output>, Input, Output>
	{
	}

	public class BackupRule<RuleType, Input, Output> : IBackupRule<RuleType, Input, Output> where RuleType : IRule<Input, Output>
	{
		public RuleType rule;
		private RuleType backupRule;

		public BackupRule(RuleType rule)
		{
			this.rule = rule;
		}

		public virtual object Clone()
		{
			BackupRule<RuleType, Input, Output> clone = new BackupRule<RuleType, Input, Output>(rule);
			clone.backupRule = backupRule;
			return clone;
		}

		public virtual List<Output> GetOutput(Input input, Random random)
		{
			return rule.GetOutput(input, random);
		}

		public virtual RuleType GetRule()
		{
			return rule;
		}

		public virtual void Backup()
		{
			backupRule = (RuleType)rule.Clone();
		}

		public virtual void Restore()
		{
			rule = (RuleType)backupRule.Clone();
		}
	}

	public class BackupRule<Input, Output> : BackupRule<IRule<Input, Output>, Input, Output>, IBackupRule<Input, Output>
	{
		public BackupRule(IRule<Input, Output> rule) : base(rule)
		{
		}
	}

	public static class BackupRule
	{
		public static BackupRule<RuleType, Input, Output> Of<RuleType, Input, Output>(RuleType rule) where RuleType : IRule<Input, Output>
		{
			return new BackupRule<RuleType, Input, Output>(rule);
		}

		public static BackupRule<Input, Output> Of<Input, Output>(IRule<Input, Output> rule)
		{
			return new BackupRule<Input, Output>(rule);
		}
	}
}