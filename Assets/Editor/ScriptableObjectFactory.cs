﻿using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
///     Helper class for instantiating ScriptableObjects.
/// </summary>
public class ScriptableObjectFactory
{
	[MenuItem("Project/Create/Item")]
	[MenuItem("Assets/Create/Item")]
	public static void Create()
	{
		Assembly assembly = GetAssembly();

		// Get all classes derived from ScriptableObject
		var allScriptableObjects = (from t in assembly.GetTypes()
			where t.IsSubclassOf(typeof(ScriptableObject)) && !t.IsAbstract
			select t).ToArray();

		// Show the selection window.
		var window = EditorWindow.GetWindow<ScriptableObjectWindow>(true, "Create a new Item", true);
		window.ShowPopup();

		window.Types = allScriptableObjects;
	}

	/// <summary>
	///     Returns the assembly that contains the script code for this project (currently hard coded)
	/// </summary>
	private static Assembly GetAssembly()
	{
		return Assembly.Load(new AssemblyName("Assembly-CSharp"));
	}
}