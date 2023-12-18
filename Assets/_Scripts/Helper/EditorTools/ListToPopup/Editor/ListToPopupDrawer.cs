using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Text.RegularExpressions;

[CustomPropertyDrawer(typeof(ListToPopupAttribute))]
public class ListToPopupDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ListToPopupAttribute listToPopupAtt = attribute as ListToPopupAttribute;
        List<string> stringList = null;

        PropertyInfo listProperty = listToPopupAtt.type.GetProperty(listToPopupAtt.propertyName);
        if (listProperty != null)
        {
            stringList = listProperty.GetValue(listToPopupAtt.type) as List<string>;
        }

        if (stringList != null && stringList.Count != 0)
        {
            int selectedIndex = Mathf.Max(stringList.IndexOf(property.stringValue), 0);
            selectedIndex = EditorGUI.Popup(position, CamelCaseToSpaces.SplitCamelCase(property.name), selectedIndex, stringList.ToArray());
            property.stringValue = stringList[selectedIndex];
        }
        else EditorGUI.PropertyField(position, property, label);
        
    }
}


