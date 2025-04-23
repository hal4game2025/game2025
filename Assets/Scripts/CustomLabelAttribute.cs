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
    // �G�f�B�^��ŃJ�X�^���v���p�e�B�`��
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        CustomLabelAttribute newLabel = attribute as CustomLabelAttribute;
        if (newLabel != null) label = newLabel.label;
        // �G�f�B�^��Ƀv���p�e�B��`��
        EditorGUI.PropertyField(position, property, label, true);
    }

    // �G�f�B�^��Ńv���p�e�B�̍������擾
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true);
    }
}

#endif