using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CustomLabelAttribute : PropertyAttribute
{
    public readonly GUIContent label;
    public CustomLabelAttribute(string label)
    {
        this.label = new GUIContent(label);
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(CustomLabelAttribute))]
public class CustomLabelAttributeDrawer : PropertyDrawer
{
    // エディタ上でカスタムプロパティ描画
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        CustomLabelAttribute newLabel = attribute as CustomLabelAttribute;
        if (newLabel != null) label = newLabel.label;
        // エディタ上にプロパティを描画
        EditorGUI.PropertyField(position, property, label, true);
    }

    // エディタ上でプロパティの高さを取得
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true);
    }
}

#endif