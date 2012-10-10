using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManyLoader : MonoBehaviour {
	
	public string testUrl = "http://roboticsnotes.kayac.biz/roboticsnotes/uploads/assets/AssetBundleData/";
	public string url = "http://noitamina.tv/roboticsnotes/uploads/assets/AssetBundleData/";
	
	public GUIText guiText;
	GameObject car;
	GameObject girl;
	
	string[] downloadList = {"Model/MURCIELAGO640", "Model/akiho_test"};

	IEnumerator Start () 
	{
		SetPath();
		yield return StartCoroutine(AssetBundleManager.Instance.Download(downloadList));
		car = InstantiateIndex("Model/MURCIELAGO640", Vector3.one);
		girl = InstantiateIndex("Model/akiho_test", new Vector3(0.25f, 0.25f, 0.25f));
		
		AssetBundleManager.Instance.FreeAssets(downloadList);
	}
	
	void SetPath()
	{
		AssetBundleManager.Instance.androidTestPath = testUrl + "Android/";
		AssetBundleManager.Instance.androidPath = url + "Android/";
		
		AssetBundleManager.Instance.iOSTestPath = testUrl + "IPhone/";
		AssetBundleManager.Instance.iOSPath = url + "IPhone/";
	}
	
	GameObject InstantiateIndex(string item, Vector3 scale)
	{
		GameObject go = Instantiate(AssetBundleManager.Instance.GetItem(item)) as GameObject;
		go.transform.position = Vector3.zero;
		go.transform.localScale = scale;
		return go;
	}
	
	void Update () 
	{
		if (AssetBundleManager.Instance.Downloading)
		{
			guiText.text = "Download Progress: " + AssetBundleManager.Instance.GetStringProgress();
		}
		
		if (car != null) 
		{
			car.transform.Rotate(Vector3.up, 1f);
		}
		
		if (girl != null) 
		{
			girl.transform.Rotate(Vector3.up, 1f);
		}
	}
}
