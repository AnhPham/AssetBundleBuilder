using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GirlLoader : MonoBehaviour {
	
	private string url = "http://roboticsnotes.kayac.biz/roboticsnotes/uploads/assets/AssetBundleData/Android/Model/akiho_test.unity3d";
	public GUIText guiText;
	GameObject girl;

	IEnumerator Start () 
	{
		yield return StartCoroutine(AssetBundleManager.Instance.DownloadAbsolutely(url));
		girl = InstantiateIndex(0, new Vector3(0.25f, 0.25f, 0.25f));
		
		AssetBundleManager.Instance.FreeAsset(url);
	}
	
	GameObject InstantiateIndex(int i, Vector3 scale)
	{
		GameObject go = Instantiate(AssetBundleManager.Instance.GetItem(url)) as GameObject;
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
		
		if (girl != null) 
		{
			girl.transform.Rotate(Vector3.up, 1f);
		}
	}
}
