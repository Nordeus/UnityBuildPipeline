using UnityEditor;

namespace Nordeus.Build
{
	/// <summary>
	/// Sets up the appropriate player settings to properly represent the game version and build number for different platforms and versions of Unity.
	/// </summary>
	public static class BundleVersionResolver
	{
#if !UNITY_5
		private const BuildTarget IosTarget = BuildTarget.iPhone;
#else
		private const BuildTarget IosTarget = BuildTarget.iOS;
#endif

		/// <summary>
		/// Pretty version of the game, for example 0.123f
		/// </summary>
		public static string PrettyVersion { get; set; }

		/// <summary>
		/// Number of the build. Corresponds to bundle version code for Android and build number for iOS.
		/// </summary>
		public static int? BuildNumber { get; set; }

		/// <summary>
		/// Setups player settings with the specified pretty version and build number.
		/// </summary>
		/// <param name="target"></param>
		public static void Setup(BuildTarget target)
		{
			if (target == BuildTarget.Android)
			{
				SetupAndroid();
			}
			else if (target == IosTarget)
			{
				SetupIos();
			}
		}

#if !UNITY_5_2
		private static void SetupIos()
		{
			if (BuildNumber != null)
			{
				PlayerSettings.bundleVersion = BuildNumber.Value.ToString();
			}
		}
#else
		private static void SetupIos()
		{
			if (PrettyVersion != null)
			{
				PlayerSettings.bundleVersion = PrettyVersion; 
			}

			if (BuildNumber != null)
			{
				PlayerSettings.iOS.buildNumber = BuildNumber.Value.ToString();
			}
		}
#endif

		private static void SetupAndroid()
		{
			if (PrettyVersion != null)
			{
				PlayerSettings.bundleVersion = PrettyVersion;
			}

			if (BuildNumber != null)
			{
				PlayerSettings.Android.bundleVersionCode = BuildNumber.Value;
			}
		}
	}
}