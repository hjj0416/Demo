using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerStatus))]
public class PlayerStatusEditor : Editor {
	
	
	public override void OnInspectorGUI(){

        base.OnInspectorGUI();
        
	}
	
	
	public void ProgressBar (float val,string label) {
		Rect rect = GUILayoutUtility.GetRect (18, 18, "TextField");
		EditorGUI.ProgressBar (rect, val, label);
		EditorGUILayout.Space ();
	}
}
