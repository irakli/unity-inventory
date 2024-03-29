using Gamelogic.Extensions.Internal;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Gamelogic.Extensions.Editor
{
	/// <summary>
	///     A property drawer for type InspectorList.
	/// </summary>
	[Version(2, 5), CustomPropertyDrawer(typeof(InspectorList), true)]
	public class InspectorListPropertyDrawer : PropertyDrawer
	{
		private ReorderableList reorderableList;
		private float lastHeight;

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			//	property.isExpanded = true;
			//	return EditorGUI.GetPropertyHeight(property, label, true) + 200;

			SerializedProperty list = property.FindPropertyRelative("values");

			if (list == null)
				return 0;

			InitList(list, property);

			if (reorderableList != null)
				return reorderableList.GetHeight();

			return lastHeight;

			//return EditorGUIUtility.singleLineHeight;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty list = property.FindPropertyRelative("values");

			if (list == null)
				return;

			var indentLevel = EditorGUI.indentLevel;

			InitList(list, property);

			if (list.arraySize > 0)
				reorderableList.elementHeight = EditorGUI.GetPropertyHeight(list.GetArrayElementAtIndex(0));

			if (position.height <= 0)
				return;

			lastHeight = reorderableList.GetHeight();

			reorderableList.DoList(position);

			EditorGUI.indentLevel = indentLevel;
		}

		public void InitList(SerializedProperty list, SerializedProperty property)
		{
			if (reorderableList == null)
				reorderableList = new ReorderableList(property.serializedObject, list, true, true, true, true)
				{
					drawElementCallback =
						(rect, index, isActive, isFocused) =>
						{
							SerializedProperty element = list.GetArrayElementAtIndex(index);
							SerializedProperty labelProperty = element;
							var potentialProperty = (SerializedProperty) null;
							var maxCheck = 0;

							while (labelProperty.Next(true) && maxCheck++ < 3)
								if (labelProperty.propertyType == SerializedPropertyType.String)
									if (labelProperty.name == "name" || potentialProperty == null)
									{
										potentialProperty = labelProperty;
										break;
									}

							GUIContent itemLabel = potentialProperty == null
								? new GUIContent("Element: " + index)
								: new GUIContent(labelProperty.stringValue);

							EditorGUI.PropertyField(rect, list.GetArrayElementAtIndex(index), itemLabel, true);
						},

					drawHeaderCallback =
						rect =>
						{
							EditorGUI.indentLevel++;
							EditorGUI.LabelField(rect, property.displayName);
						},


#if UNITY_5
					elementHeightCallback = index => EditorGUI.GetPropertyHeight(list.GetArrayElementAtIndex(index), null, true)
#endif
				};
		}
	}
}