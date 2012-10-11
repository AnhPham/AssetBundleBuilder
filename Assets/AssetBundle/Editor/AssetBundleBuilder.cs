using UnityEngine;
using UnityEditor;
using System.Collections;

public class AssetBundleBuilder : Editor {
	
	public TextAsset[] textDataObjects;
	public Object[] modelObjects;
	public AudioClip[] soundObjects;
	public Texture2D[] imageObjects;
	
	public string dataPath = "Data/";
	public string modelPath = "Model/";
	public string soundPath = "Sound/";
	public string imagePath = "Image/";

	private string webFolderPath = "Assets/AssetBundleData/Web/";
	private string androidFolderPath = "Assets/AssetBundleData/Android/";
	private string iPhoneFolderPath = "Assets/AssetBundleData/iOS/";
	
	public void Build(string path, BuildTarget buildTarget)
	{		
		System.IO.Directory.CreateDirectory(path);
		BuildPipeLine(path, dataPath, textDataObjects, buildTarget);
		BuildPipeLine(path, modelPath, modelObjects, buildTarget);
		BuildPipeLine(path, soundPath, soundObjects, buildTarget);
		BuildPipeLine(path, imagePath, imageObjects, buildTarget);
	}
	
	private void BuildPipeLine(string path, string childPath, Object[] objs, BuildTarget buildTarget)
	{
		string fullPath = path + childPath;
		System.IO.Directory.CreateDirectory(fullPath);
		var options = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets;
		foreach (Object obj in objs)
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
