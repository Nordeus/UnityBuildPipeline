# Unity Build Pipeline

This is a simplified version of the build pipeline that we use at Nordeus. It is capable of building Android and iOS and reporting build progress back to TeamCity. When building, Unity should be started with the following parameters:
```sh
unity.exe -projectPath "pathToYourProject" -batchMode -quit -executeMethod Nordeus.Build.CommandLineBuild.Build <aditional build parameters>
```

Aditional build parameters can be:
  - -out - Output path for the built project
  - -target - Platform for the build, it can be iOS or Android
  - -textureCompression - Texture compression subtarget for Android (ETC1, ATC, ETC2...)
  - -buildNumber - Integer value for the number of the build. It is copied to Android's bundle version code and iOS's build number (Unity 5.2+ only)
  - -buildVersion - Pretty build version string (eg. 1.2.3f). It is copied to bundle version on both Android and iOS.
  - -reporter - Build report to use. By default UnityReporter is used which reprots all emssages to Unity's console. TeamCityReporter also reports errors as service messages to TeamCity.
