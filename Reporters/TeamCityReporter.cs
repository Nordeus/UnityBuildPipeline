using UnityEngine;
using System.Collections;

namespace Nordeus.Build.Reporters
{
	/// <summary>
	/// Reports messages to TeamCity.
	/// </summary>
	public class TeamCityReporter : UnityReporter
	{
		protected override void LogInternal(string message, MessageSeverity severity = MessageSeverity.Info)
		{
			base.LogInternal(message, severity);

			if (severity == MessageSeverity.Error)
			{
				Debug.Log("##teamcity[message text='" + message + "'" + "status='ERROR']");
			}
		}

		public override void IndicateSuccessfulBuild()
		{
			base.IndicateSuccessfulBuild();

			// Magic string to indicate successful build.
			Debug.Log("Successful build ~0xDEADBEEF");
		}
	}
}