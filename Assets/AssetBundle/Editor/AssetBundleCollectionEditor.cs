using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AssetBundleCollectionBuilder))]
public class AssetBundleCollectionEditor : Editor
{
	public override void OnInspectorGUI()
	{
		AssetBundleCollectionBuilder gen = (AssetBundleCollectionBuilder)target;
		DrawDefaultInspector();

		EditorGUILayout.BeginVertical();

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Build Web")) gen.BuildWeb();
		if (GUILayout.Button("Build Android")) gen.BuildAndroid();
		if (GUILayout.Button("Build iPhone")) gen.BuildiPhone();

		EditorGUILayout.EndHorizontal();
		
		if (GUILayout.Button("Build All Platform")) gen.BuildAll();
		
		EditorGUILayout.EndVertical();
	}
}
