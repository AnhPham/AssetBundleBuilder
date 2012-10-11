using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManyLoader : MonoBehaviour {
	
	public GUIText guiText;
	GameObject car;
	GameObject girl;
	
	string[] downloadList = {"Model/MURCIELAGO640", "Model/akiho_test"};

	IEnumerator Start () 
	{
		yield return StartCoroutine(AssetBundleManager.Instance.Download(downloadList));
		car = InstantiateByName("Model/MURCIELAGO640", Vector3.one);
		girl = InstantiateByName("Model/akiho_test", new Vector3(0.25f, 0.25f, 0.25f));
		
		AssetBundleManager.Instance.FreeAssets(downloadList);
	}
	
	GameObject InstantiateByName(string item, Vector3 scale)
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
