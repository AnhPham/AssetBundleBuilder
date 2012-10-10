using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// Editor Utility tool.
/// author koki ibukuro
/// </summary>
public class CacheTools : ScriptableObject {
		
	/// <summary>
	/// Delete all player preferences.
	/// </summary>
	[MenuItem ("CacheTools/Delete/Delete PlayerPref")]
	public static void DeletePlayerPref ()
	{
		PlayerPrefs.DeleteAll ();
		Debug.LogWarning ("[EDITOR LOG] deleted all player prefs");
	}
	
	[MenuItem ("CacheTools/Delete/Delete Cache")]
	public static void DeleteCache ()
	{
		if (Caching.CleanCache ()) {
			Debug.LogWarning ("[EDITOR LOG] clean all caches");
		} else {
			Debug.LogWarning ("[EDITOR LOG] cache was in use");
		}
	}
}
