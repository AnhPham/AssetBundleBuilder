using UnityEngine;
using UnityEditor;
using System.Collections;

public class AssetBundleCollectionBuilder : Editor {
	
	public Object[] folders;
	
	public string collectionPath = "Collection/";

	private string webFolderPath = "Assets/AssetBundleData/Web/";
	private string androidFolderPath = "Assets/AssetBundleData/Android/";
	private string iPhoneFolderPath = "Assets/AssetBundleData/iOS/";
	
	public void Build(string path, BuildTarget buildTarget)
	{		
		System.IO.Directory.CreateDirectory(path);
		BuildPipeLine(path, collectionPath, null, folders, buildTarget);
	}
	
	private void BuildPipeLine(string path, string childPath, Object main, Object[] objs, BuildTarget buildTarget)
	{
		string fullPath = path + childPath;
		System.IO.Directory.CreateDirectory(fullPath);
		var options = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets;
		
		if (main != null)
			BuildPipeline.BuildAssetBundle(main, objs, fullPath + main.name + ".unity3d", options, buildTarget);
		else
			BuildPipeline.BuildAssetBundle(null, objs, fullPath + objs[0].name + ".unity3d", options, buildTarget);
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
