using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class CopyTool : Editor {
	
	public string sourcePath = string.Empty;
	public string targetPath = string.Empty;
	
	public void Copy()
	{		
		//Now Create all of the directories
		foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", 
		    SearchOption.AllDirectories))
		    Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
		
		//Copy all the files
		foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", 
		    SearchOption.AllDirectories))
		    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
		
		Debug.Log("Copied all");
	}
}
