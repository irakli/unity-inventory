﻿using Gamelogic.Extensions.Internal;
using UnityEditor;
using UnityEngine;

namespace Gamelogic.Extensions.Editor
{
	/// <summary>
	///     A property drawer for the MinMaxFloat class.
	/// </summary>
	[Version(1, 2), CustomPropertyDrawer(typeof(MinMaxFloat))]
	public class MinMaxFloatPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			SerializedProperty minProp = property.FindPropertyRelative("min");
			SerializedProperty maxProp = property.FindPropertyRelative("max");

			var minValue = minProp.floatValue;
			var maxValue = maxProp.floatValue;

			EditorGUI.MinMaxSlider(position, ref minValue, ref maxValue, 0, 1);

			if (GUI.changed)
			{
				minProp.floatValue = minValue;
				maxProp.floatValue = maxValue;
			}

			EditorGUI.EndProperty();
		}
	}
}