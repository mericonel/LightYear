using UnityEngine;
using System.Collections;
using UnityEditor;

namespace ACR.TimeOfDayFree
{

	public class TimeOfDayEditor : Editor
	{

		protected Color WhiteColor{get{return Color.white;}}
		protected Color EnableButtonColor{get{return new Color(.75f, .75f, .75f, 1f);}}


		protected void HorizontalSeparator(Color color, int height)
		{
			GUI.color = color;
			GUILayout.Box("", new GUILayoutOption[] {GUILayout.ExpandWidth(true), GUILayout.Height(height)});
			GUI.color = Color.white;
		}


		protected void TexTitle(string text)
		{

			GUIStyle texStyle = new GUIStyle(EditorStyles.label); 
			texStyle.fontStyle = FontStyle.Bold;
			texStyle.fontSize = 12;

			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Label(text, texStyle);
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
			GUI.backgroundColor =  Color.white;
		}

		protected void Tex(string text, int fontSize)
		{

			GUIStyle texStyle = new GUIStyle(EditorStyles.label); 
			texStyle.fontStyle = FontStyle.Bold;
			texStyle.fontSize = fontSize;
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label(text, texStyle);
			EditorGUILayout.EndHorizontal();
			GUI.backgroundColor =  Color.white;
		}



		protected void ColorField(SerializedProperty color, string name, int width)
		{
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField (name, GUILayout.MinWidth(20));
			EditorGUILayout.PropertyField(color, new GUIContent(""), GUILayout.MaxWidth(width),GUILayout.MinWidth(width/2));
			EditorGUILayout.EndHorizontal ();
		}

		protected void CurveField(string name,  SerializedProperty curve, Color color, Rect rect, int width)
		{
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField (name,GUILayout.MinWidth(20));
			curve.animationCurveValue = EditorGUILayout.CurveField ("", curve.animationCurveValue, color , rect, GUILayout.MaxWidth(width), GUILayout.MinWidth(width/2));
			EditorGUILayout.EndHorizontal ();
		}



		protected enum GUIColorType
		{
			Background,
			Color
		}

		protected GUIColorType ColorType;


		protected void ToggleColor(bool toggle, Color enable, Color disable, GUIColorType colorType) 
		{
			switch(ColorType)
			{
				case GUIColorType.Background :GUI.backgroundColor = toggle ? enable : disable;break;
				case GUIColorType.Color :GUI.color = toggle ? enable : disable;break;
			}
		}


		protected void ToggleButton(SerializedProperty toggle, string name)
		{

			ToggleColor(toggle.boolValue, EnableButtonColor, WhiteColor, GUIColorType.Background);

			if (GUILayout.Button (name, EditorStyles.miniButton, GUILayout.MaxWidth(20), GUILayout.MaxHeight(16)))
				toggle.boolValue = !toggle.boolValue;

			GUI.backgroundColor = Color.white;

		}

	}
}
