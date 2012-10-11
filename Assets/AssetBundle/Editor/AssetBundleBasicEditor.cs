using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AssetBundleBasicBuilder))]
public class AssetBundleBasicEditor : Editor
{
	public override void OnInspectorGUI()
	{
		AssetBundleBasicBuilder gen = (AssetBundleBasicBuilder)target;
		DrawDefaultInspector();
		
		EditorGUILayout.BeginVertical();

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Build Web")) gen.BuildWeb();
		if (GUILayout.Button("Build Android")) gen.BuildAndroid();
		
		if (Application.platform == RuntimePlatform.OSXEditor)
			if (GUILayout.Button("Build iOS")) gen.BuildiPhone();

		EditorGUILayout.EndHorizontal();
		
		if (GUILayout.Button("Build All Platform")) gen.BuildAll();
		
		EditorGUILayout.EndVertical();
	}
}
