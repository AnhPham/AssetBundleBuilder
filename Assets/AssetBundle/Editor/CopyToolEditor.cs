using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CopyTool))]
public class CopyToolEditor : Editor
{
	public override void OnInspectorGUI()
	{
		CopyTool gen = (CopyTool)target;
		DrawDefaultInspector();
		
		if (GUILayout.Button("Copy")) gen.Copy();
	}
}
