using System.Collections.Generic;

namespace Nordeus.Build.Reporters
{
	/// <summary>
	/// Base class for reporting messages to the build environment.
	/// </summary>
	public abstract class BuildReporter
	{
		public enum MessageSeverity
		{
			Debug,
			Info,
			Warning,
			Error
		}

		/// <summary>
		/// Gets the current global build reporter. By default, it's the Unity (Debug.Log) reporter.
		/// </summary>
		public static BuildReporter Current
		{
			get
			{
				if (currentReporter == null)
				{
					currentReporter = new UnityReporter();
				}

				return currentReporter;
			}
			set
			{
				currentReporter = value;
			}
		}

		private static BuildReporter currentReporter;

		/// <summary>
		/// Minimum severity the build reporter should report.
		/// </summary>
		public MessageSeverity MinimumSeverity { get; set; }

		/// <summary>
		/// Reports a message to the build environment.
		/// </summary>
		public virtual void Log(string message, MessageSeverity severity = MessageSeverity.Info)
		{
			if (severity >= MinimumSeverity) LogInternal(message, severity);
		}

		/// <summary>
		/// Indicates a successful build to the build environment.
		/// </summary>
		public abstract void IndicateSuccessfulBuild();

		protected abstract void LogInternal(string message, MessageSeverity severity);

		/// <summary>
		/// Creates a build reporter by name. Returns null in case it cannot create one.
		/// </summary>
		public static BuildReporter CreateReporterByName(string name)
		{
			switch (name)
			{
				case "Unity":
					return new UnityReporter();
				case "TeamCity":
					return new TeamCityReporter();
				default:
					return null;
			}
		}
	}
}