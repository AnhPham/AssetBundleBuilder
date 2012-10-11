using UnityEngine;
using UnityEditor;
using System.Collections;

public class AssetBundleBasicBuilder : Editor {
	
	public Object objectToBuild;
	
	public string assetBundlePath = "Single/";

	private string webFolderPath = "Assets/AssetBundleData/Web/";
	private string androidFolderPath = "Assets/AssetBundleData/Android/";
	private string iPhoneFolderPath = "Assets/AssetBundleData/iOS/";
	
	public void Build(string path, BuildTarget buildTarget)
	{		
		if (objectToBuild == null) {
			Debug.LogError("Object is Empy");
			return;
		}
		
		if (assetBundlePath == string.Empty) {
			Debug.LogError("Path is Empy");
			return;
		}
		
		System.IO.Directory.CreateDirectory(path);
		BuildPipeLine(path, assetBundlePath, objectToBuild, buildTarget);
	}
	
	private void BuildPipeLine(string path, string childPath, Object obj, BuildTarget buildTarget)
	{
		string fullPath = path + childPath;
		System.IO.Directory.CreateDirectory(fullPath);
		var options = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets;
		BuildPipeline.BuildAssetBundle(obj, null, fullPath + obj.name + ".unity3d", options, buildTarget);	
	}

	public void BuildWeb()
	{
		Build(webFolderPath, BuildTarget.WebPlayer);
		AssetDatabase.Refresh();
	}

	public void BuildAndroid()
	{
		Build(androidFolderPath, BuildTarget.Android);
		AssetDatabase.Refresh();
	}

	public void BuildiPhone()
	{
		Build(iPhoneFolderPath, BuildTarget.iPhone);
		AssetDatabase.Refresh();
	}
	
	public void BuildAll()
	{
		BuildWeb();
		BuildAndroid();
		
		if (Application.platform == RuntimePlatform.OSXEditor)
			BuildiPhone();
	}
}
