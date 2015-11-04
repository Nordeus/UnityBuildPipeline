using System;
using Nordeus.Build;
using Nordeus.Build.Reporters;
using UnityEngine;
using System.Collections;

namespace Nordeus.Build.Reporters
{
	/// <summary>
	/// Reports messages to Unity console.
	/// </summary>
	public class UnityReporter : BuildReporter
	{
		public override void IndicateSuccessfulBuild() {}

		protected override void LogInternal(string message, MessageSeverity severity = MessageSeverity.Info)
		{
			switch (severity)
			{
				case MessageSeverity.Debug:
					Debug.Log(message);
					break;
				case MessageSeverity.Info:
					Debug.Log(message);
					break;
				case MessageSeverity.Warning:
					Debug.LogWarning(message);
					break;
				case MessageSeverity.Error:
					Debug.LogError(message);
					break;
				default:
					throw new ArgumentOutOfRangeException("Severity out of bounds");
			}
		}
	}
}