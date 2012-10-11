using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarLoader : MonoBehaviour {
	
	public GUIText guiText;
	GameObject car;

	IEnumerator Start () 
	{
		yield return StartCoroutine(AssetBundleManager.Instance.Download("Model/MURCIELAGO640"));
		car = InstantiateByName("Model/MURCIELAGO640", Vector3.one);
		
		AssetBundleManager.Instance.FreeAsset("Model/MURCIELAGO640");
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
	}
}
