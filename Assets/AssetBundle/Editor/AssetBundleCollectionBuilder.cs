using UnityEngine;
using UnityEditor;
using System.Collections;

public class AssetBundleCollectionBuilder : Editor {
	
	public Object mainAsset;
	public Object[] folders;
	
	public string collectionPath = "Collection/";

	private string webFolderPath = "Assets/AssetBundleData/Web/";
	private string androidFolderPath = "Assets/AssetBundleData/Android/";
	private string iPhoneFolderPath = "Assets/AssetBundleData/IPhone/";
	
	public void Build(string path, BuildTarget buildTarget)
	{		
		System.IO.Directory.CreateDirectory(path);
		BuildPipeLine(path, collectionPath, mainAsset, folders, buildTarget);
	}
	
	private void BuildPipeLine(string path, string childPath, Object main, Object[] objs, BuildTarget buildTarget)
	{
		string fullPath = path + childPath;
		System.IO.Directory.CreateDirectory(fullPath);
		var options = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets;
		BuildPipeline.BuildAssetBundle(main, objs, fullPath + main.name + ".unity3d", options, buildTarget);	
	}

	public void BuildWeb()
	{
		Build(webFolderPath, BuildTarget.WebPlayer);
	}

	public void BuildAndroid()
	{
		Build(androidFolderPath, BuildTarget.Android);
	}

	public void BuildiPhone()
	{
		Build(iPhoneFolderPath, BuildTarget.iPhone);
	}
	
	public void BuildAll()
	{
		BuildWeb();
		BuildAndroid();
		BuildiPhone();
	}
}
