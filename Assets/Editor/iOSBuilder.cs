using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class iOSBuilder : MonoBehaviour {

	[MenuItem("Build/Build iOS")]
	public static void BuildiOS() {
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
		string dir = Application.dataPath + "/Resources/Scenes/";
		buildPlayerOptions.scenes = new[] { dir + "IntroScene.unity", dir + "MainScene.unity", dir + "GoodEndScene.unity" };
		buildPlayerOptions.target = BuildTarget.iOS;
		buildPlayerOptions.options = BuildOptions.None;
		buildPlayerOptions.locationPathName = CheckDir() + "iOS Build";
		BuildPipeline.BuildPlayer(buildPlayerOptions);
	}

	[MenuItem("Build/Build Android")]
	public static void BuildAndroid() {
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
		string dir = Application.dataPath + "/Resources/Scenes/";
		buildPlayerOptions.scenes = new[] { dir + "IntroScene.unity", dir + "MainScene.unity", dir + "GoodEndScene.unity" };
		buildPlayerOptions.locationPathName = "AndroidBuild.apk";
		buildPlayerOptions.target = BuildTarget.Android;
		buildPlayerOptions.options = BuildOptions.None;
		buildPlayerOptions.locationPathName = CheckDir() + "Android.apk";
		BuildPipeline.BuildPlayer(buildPlayerOptions);
	}

	static string CheckDir() {
		string dir = Application.dataPath + "/Build/";
		if (Directory.Exists(dir)) {
			return dir;
		} else {
			Directory.CreateDirectory(dir);
			return dir;
		}
	}
}