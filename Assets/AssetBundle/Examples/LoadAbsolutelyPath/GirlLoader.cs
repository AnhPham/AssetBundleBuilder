using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GirlLoader : MonoBehaviour {
	
	private string url = "http://roboticsnotes.kayac.biz/roboticsnotes/uploads/assets/AssetBundleData/Android/Model/akiho_test.unity3d";
	public GUIText gText;
	GameObject girl;

	IEnumerator Start () 
	{
		yield return StartCoroutine(ABManager.Instance.DownloadAbsolutely(url));
		girl = InstantiateByName(0, new Vector3(0.25f, 0.25f, 0.25f));
		
		ABManager.Instance.FreeAsset(url);
	}
	
	GameObject InstantiateByName(int i, Vector3 scale)
	{
		GameObject go = Instantiate(ABManager.Instance.GetItem(url)) as GameObject;
		go.transform.position = Vector3.zero;
		go.transform.localScale = scale;
		return go;
	}
	
	void Update () 
	{
		if (ABManager.Instance.Downloading)
		{
			gText.text = "Download Progress: " + ABManager.Instance.GetStringProgress();
		}
		
		if (girl != null) 
		{
			girl.transform.Rotate(Vector3.up, 1f);
		}
	}
}
