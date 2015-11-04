using System;
using System.Collections.Generic;
using Nordeus.Build.Reporters;
using UnityEditor;

namespace Nordeus.Build
{
	public static class CommandLineBuild
	{
		#region Constants

		private const string PublishPathCommand = "-out";
		private const string BuildNumberCommand = "-buildNumber";
		private const string BuildVersionCommand = "-buildVersion";
		private const string BuildReporterCommand = "-reporter";
		private const string BuildTargetCommand = "-target";
		private const string AndroidTextureCompressionCommand = "-textureCompression";

		private const char CommandStartCharacter = '-';

		#endregion

		#region Methods

		/// <summary>
		/// Performs the command line build by using the passed command line arguments.
		/// </summary>
		private static void Build()
		{
			string publishPath, buildNumber, buildVersion, buildReporter, buildTarget, androidTextureCompression;

			Dictionary<string, string> commandToValueDictionary = GetCommandLineArguments();

			// Extract our arguments from dictionary
			commandToValueDictionary.TryGetValue(PublishPathCommand, out publishPath);
			commandToValueDictionary.TryGetValue(BuildNumberCommand, out buildNumber);
			commandToValueDictionary.TryGetValue(BuildVersionCommand, out buildVersion);
			commandToValueDictionary.TryGetValue(BuildReporterCommand, out buildReporter);
			commandToValueDictionary.TryGetValue(BuildTargetCommand, out buildTarget);
			commandToValueDictionary.TryGetValue(AndroidTextureCompressionCommand, out androidTextureCompression);


			if (!string.IsNullOrEmpty(buildReporter)) BuildReporter.Current = BuildReporter.CreateReporterByName(buildReporter);

			try
			{
				if (!string.IsNullOrEmpty(buildNumber))
				{
					BundleVersionResolver.BuildNumber = int.Parse(buildNumber);
				}
				if (!string.IsNullOrEmpty(buildVersion))
				{
					BundleVersionResolver.PrettyVersion = buildVersion;
				}

				if (string.IsNullOrEmpty(buildTarget))
				{
					BuildReporter.Current.Log("No target was specified for this build.", BuildReporter.MessageSeverity.Error);
				}
				else
				{
					BuildTarget parsedBuildTarget = (BuildTarget)Enum.Parse(typeof(BuildTarget), buildTarget);
					MobileTextureSubtarget? parsedTextureSubtarget = null;
					if (!string.IsNullOrEmpty(androidTextureCompression)) parsedTextureSubtarget = (MobileTextureSubtarget)Enum.Parse(typeof(MobileTextureSubtarget), androidTextureCompression);

					BundleVersionResolver.Setup(parsedBuildTarget);

					Builder.Build(parsedBuildTarget, publishPath, parsedTextureSubtarget);
				}
			}
			catch (Exception e)
			{
				BuildReporter.Current.Log(e.Message, BuildReporter.MessageSeverity.Error);
			}
		}


		/// <summary>
		/// Gets all the command line arguments relevant to the build process. All commands that don't have a value after them have their value at string.Empty.
		/// </summary>
		private static Dictionary<string, string> GetCommandLineArguments()
		{
			Dictionary<string, string> commandToValueDictionary = new Dictionary<string, string>();

			string[] args = System.Environment.GetCommandLineArgs();

			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].StartsWith(CommandStartCharacter.ToString()))
				{
					string command = args[i];
					string value = string.Empty;

					if (i < args.Length - 1 && !args[i + 1].StartsWith(CommandStartCharacter.ToString()))
					{
						value = args[i + 1];
						i++;
					}

					if (!commandToValueDictionary.ContainsKey(command))
					{
						commandToValueDictionary.Add(command, value);
					}
					else
					{
						BuildReporter.Current.Log("Duplicate command line argument " + command, BuildReporter.MessageSeverity.Warning);
					}
				}
			}

			return commandToValueDictionary;
		}

		#endregion
	}
}