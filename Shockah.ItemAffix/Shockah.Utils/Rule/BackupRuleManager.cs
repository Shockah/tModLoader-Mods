using System;

namespace Shockah.Utils.Rule
{
	public interface IBackupRuleManager<out RuleType, out InnerRuleType, in Input, Output> : IRuleManager<RuleType, Input, Output> where RuleType : IBackupRule<InnerRuleType, Input, Output> where InnerRuleType : IRule<Input, Output>
	{
		void Backup();

		void Restore();
	}

	public interface IBackupRuleManager<out InnerRuleType, in Input, Output> : IBackupRuleManager<IBackupRule<InnerRuleType, Input, Output>, InnerRuleType, Input, Output> where InnerRuleType : IRule<Input, Output>
	{
	}

	public interface IBackupRuleManager<Input, Output> : IBackupRuleManager<IRule<Input, Output>, Input, Output>
	{
	}

	public class BackupRuleManager<RuleType, InnerRuleType, Input, Output> : RuleManager<RuleType, Input, Output>, IBackupRuleManager<RuleType, InnerRuleType, Input, Output> where RuleType : IBackupRule<InnerRuleType, Input, Output> where InnerRuleType : IRule<Input, Output>
	{
		public BackupRuleManager(RuleType rule, Random random = null) : base(rule, random)
		{
		}

		public void Backup()
		{
			GetRule().Backup();
		}

		public void Restore()
		{
			GetRule().Restore();
		}
	}

	public class BackupRuleManager<InnerRuleType, Input, Output> : BackupRuleManager<IBackupRule<InnerRuleType, Input, Output>, InnerRuleType, Input, Output>, IBackupRuleManager<InnerRuleType, Input, Output> where InnerRuleType : IRule<Input, Output>
	{
		public BackupRuleManager(IBackupRule<InnerRuleType, Input, Output> rule, Random random = null) : base(rule, random)
		{
		}

		public BackupRuleManager(InnerRuleType rule, Random random = null) : base(BackupRule.Of<InnerRuleType, Input, Output>(rule), random)
		{
		}
	}

	public class BackupRuleManager<Input, Output> : BackupRuleManager<IRule<Input, Output>, Input, Output>, IBackupRuleManager<Input, Output>
	{
		public BackupRuleManager(IBackupRule<IRule<Input, Output>, Input, Output> rule, Random random = null) : base(rule, random)
		{
		}

		public BackupRuleManager(IRule<Input, Output> rule = null, Random random = null) : base(new BackupRule<IRule<Input, Output>, Input, Output>(rule ?? new RuleGroup<Input, Output>()), random)
		{
		}
	}
}
