using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CustomEditor(typeof(ModelResizer))]
public class ModelResizerEditor : Editor
{
    private SerializedProperty _scaleFactor;
    ModelResizer _resizer;
    private void OnEnable()
    {
        _scaleFactor = serializedObject.FindProperty("ScaleFactor");
         _resizer = (ModelResizer)target;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SetResizeButton();
        serializedObject.ApplyModifiedProperties();

    }

    private void SetResizeButton()
    {
        EditorGUILayout.PropertyField(_scaleFactor);
        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.fontSize = 25;
        style.alignment = TextAnchor.MiddleCenter;
        style.fontStyle = FontStyle.Bold;

        GUILayout.BeginVertical();
        GUILayout.Space(25);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        bool iscliked = GUILayout.Button("ResizeModel", style, GUILayout.MaxHeight(100), GUILayout.MaxWidth(300));

        if (iscliked)
            _resizer.RecalculateSize();

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }

}
