using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleManager : MonoBehaviour {
	
	public static bool UseAssetBundleCache = true;
	
	public bool isUseTestServer = true;
	public int assetVersion = 1;
	
	private string editorPath = string.Empty;
	
	public string domain;
	public string domainDev;
	
	Dictionary<string, AssetBundle> assetBundleList;
	static AssetBundleManager instance = null;
	AssetBundle assetBundle;
	WWW downloadWWW;	
	string url = string.Empty;
	int countLoaded = 0;
	int totalLoad = 1;
	bool downloading = false;

	void Awake()
	{		
		Init();
		instance = this;
	}

	protected void Init()
	{
		editorPath = "file://" + Application.dataPath + "/AssetBundleData/Android/";
		//Debug.Log(editorPath);
		assetBundleList = new Dictionary<string, AssetBundle>();
	}

	public static AssetBundleManager Instance
	{
		get 
		{ 
			if (instance == null) {
				GameObject go = new GameObject("AssetBundleManager");
				DontDestroyOnLoad(go);
				AssetBundleManager abm = go.AddComponent<AssetBundleManager>();
				instance = abm;
			}
			return instance; 
		}
	}
	
	public bool Downloading
	{
		get { return downloading; }
		set { this.downloading = value; countLoaded = 0; }
	}
	
	void SetTotal(int total)
	{
		totalLoad = total;
	}
	
	public float GetProgress()
	{
		float p = (float)countLoaded / totalLoad + downloadWWW.progress / totalLoad;
		return p;
	}
	
	public string GetStringProgress()
	{
		float p = GetProgress();
		if (p >= 1) return "100%";
		return p.ToString("P");
	}
	
	public Object GetItem(string item)
	{
		return assetBundleList[item].mainAsset;
	}
	
	public Object GetItemChild(string item, string name)
	{
		return assetBundleList[item].Load(name);
	}
	
	public IEnumerator Download(List<string> stringList)
	{
		Downloading = true;
		countLoaded = 0;
		SetTotal(stringList.Count);
		
		foreach (string str in stringList)
		{
			LoadItemsBegin(str);
			yield return this.downloadWWW;
			LoadItemsEnd(str);
		}
		
		Downloading = false;
	}
	
	public IEnumerator Download(string[] stringList)
	{
		Downloading = true;
		countLoaded = 0;
		SetTotal(stringList.Length);
		
		foreach (string str in stringList)
		{
			LoadItemsBegin(str);
			yield return this.downloadWWW;
			LoadItemsEnd(str);
		}
		
		Downloading = false;
	}
	
	public IEnumerator Download(string str)
	{
		Downloading = true;
		countLoaded = 0;
		SetTotal(1);
		
		LoadItemsBegin(str);
		yield return this.downloadWWW;
		LoadItemsEnd(str);
		
		Downloading = false;
	}
	
	public IEnumerator DownloadAbsolutely(string str)
	{
		Downloading = true;		
		countLoaded = 0;
		SetTotal(1);
		
		if (UseAssetBundleCache)
			downloadWWW = WWW.LoadFromCacheOrDownload(str, assetVersion);
		else
			downloadWWW = new WWW(str);
		
		yield return this.downloadWWW;
		LoadItemsEnd(str);
		
		Downloading = false;
	}
	
	public void FreeAllAssets()
	{
		foreach (KeyValuePair<string, AssetBundle> obj in assetBundleList)
			FreeAsset(obj.Key);	
	}
	
	public void FreeAsset(string item)
	{
		assetBundleList[item].Unload(false);
		assetBundleList.Remove(item);
		Debug.Log("[Asset Bundle]Free asset: " + item);
	}
	
	public void FreeAssets(List<string> items)
	{
		foreach (string item in items)
			FreeAsset(item);
	}
	
	public void FreeAssets(string[] items)
	{
		foreach (string item in items)
			FreeAsset(item);
	}
	
	private void LoadItemsBegin(string str)
	{
		#if UNITY_EDITOR
		url = editorPath + str + ".unity3d";
		
		#elif UNITY_IPHONE
		url = isUseTestServer ? domainDev + "iOS/" + str + ".unity3d" : domain + "iOS/" + str + ".unity3d";

		#elif UNITY_ANDROID
		url = isUseTestServer ? domainDev + "Android/" + str + ".unity3d" : domain + "Android/" + str + ".unity3d";
		
		#elif UNITY_WEBPLAYER
		url = isUseTestServer ? domainDev + "Web/" + str + ".unity3d" : domain + "Web/" + str + ".unity3d";
		#endif
		
		Debug.Log("[Asset Bundle]Full Path: " + url);
		if (UseAssetBundleCache)
			downloadWWW = WWW.LoadFromCacheOrDownload(url, assetVersion);
		else
			downloadWWW = new WWW(url);
	}
	
	private void LoadItemsEnd(string str)
	{
		if (downloadWWW.error != null)
		{
		    Debug.Log (downloadWWW.error);
		    return;
		}
		
		assetBundle = downloadWWW.assetBundle;

		if (assetBundle != null)
		{
			assetBundleList.Add(str, assetBundle);
			Debug.Log("[Asset Bundle]List Added: " + str);
			countLoaded++;
		}
		else
			Debug.Log("Couldnt load resource" + str);
	}

	private void OnDestroy()
	{
		Debug.Log("[Asset Bundle]Auto free all assets on destroy");
		FreeAllAssets();
	}
}
