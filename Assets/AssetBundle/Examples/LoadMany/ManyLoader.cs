using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManyLoader : MonoBehaviour {
	
	public GUIText gText;
	GameObject car;
	GameObject girl;
	
	string[] downloadList = {"Model/MURCIELAGO640", "Model/akiho_test"};

	IEnumerator Start () 
	{
		yield return StartCoroutine(ABManager.Instance.Download(downloadList));
		car = InstantiateByName("Model/MURCIELAGO640", Vector3.one);
		girl = InstantiateByName("Model/akiho_test", new Vector3(0.25f, 0.25f, 0.25f));
		
		ABManager.Instance.FreeAssets(downloadList);
	}
	
	GameObject InstantiateByName(string item, Vector3 scale)
	{
		GameObject go = Instantiate(ABManager.Instance.GetItem(item)) as GameObject;
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
