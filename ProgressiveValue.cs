using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

namespace ACO{
[Serializable]
public class ProgressiveValue {
	public float start = 1f, progress = 1f, dynamic = 1f;

	public delegate float PostAct(float arg);
	PostAct postAct = None;
	public static float None(float arg){return arg;}
	public static float Floor(float arg){return Mathf.Floor(arg);}
	public static float Round(float arg){return Mathf.Round(arg);}
	public static float Ceil(float arg){return Mathf.Ceil(arg);}
	public enum PostActType : byte {None, Floor, Round, Ceil}
	[SerializeField] PostActType applyedPostActType;
	bool initialized = false;
	public void Apply(PostActType pat){
		applyedPostActType = pat;
		switch (pat){
			case PostActType.None: postAct = None; break;
			case PostActType.Floor: postAct = Floor; break;
			case PostActType.Round: postAct = Round; break;
			case PostActType.Ceil: postAct = Ceil; break;
		}
	}
	public float Get(int position){
		if (!initialized){
			initialized = true;
			Apply(applyedPostActType);
		}
		float res = start;
		float currentScale = progress;
		for (int i = 0; i < position; i++){
			currentScale *= dynamic;
			res += start * currentScale;
		}
		return postAct(res);
	}
	public PostActType GetPostActType(){
		return applyedPostActType;
	}
	#if UNITY_EDITOR
	public class DrawEntity {
		public string name = "???";
		public Color color = Color.clear;
		public ProgressiveValue val;
	}
	public static void Draw(Editor Inspector, int iterations, params DrawEntity[] list){
		Color defColor = GUI.color;

		GUIContent guiContentBase = new GUIContent("?Значення", "Стартове Значення (кількість очок, яку даєм гравцю)(незмінне для першої ітерації), далі видозмінюється згідно з наступними параметрами");
		GUIContent guiContentStartScaler = new GUIContent("?Скаляр", "Стартовий Скаляр. З кожною новою ітерацією до Стартового Значення прибавляється Стартове Значення, помножене на Стартовий Скаляр. \nЯкщо вибрати нуль, то Стартове Значення мінятися не буде. \nЯкщо одиницю, то воно буде подвоюватись");
		GUIContent guiContentScaler = new GUIContent("?Динаміка", "Динаміка. З кожною новою ітерацією Стартовий Скаляр множиться на Динаміку, виходить що чим вона більша, тим сильніше росте Значення, а коли Динаміка нульова, то і приросту не буде");
		GUIContent guiContentPostAct = new GUIContent("?Округлення", "Метод округлення вихідного значення\nNone: без округлення результат видається 'як є'\nFloor: найближче ціле число, яке меньше за вхідне\nRound: найближче ціле число\nCeil: найближче ціле число, яке більше за вхідне");
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(GUIContent.none, GUILayout.Width(Inspector.GetWidth().GetPercentage(20f)));
		EditorGUILayout.LabelField(guiContentBase, GUILayout.Width(Inspector.GetWidth().GetPercentage(20f)));
		EditorGUILayout.LabelField(guiContentStartScaler, GUILayout.Width(Inspector.GetWidth().GetPercentage(20f)));
		EditorGUILayout.LabelField(guiContentScaler, GUILayout.Width(Inspector.GetWidth().GetPercentage(20f)));
		EditorGUILayout.LabelField(guiContentPostAct, GUILayout.Width(Inspector.GetWidth().GetPercentage(20f)));
		EditorGUILayout.EndHorizontal();

		foreach (var v in list){
			if (v == null){
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("No DrawEntity setted");
				EditorGUILayout.EndHorizontal();
				continue;
			}
			GUI.color = v.color == Color.clear ? defColor : v.color;
			if (v.val == null){
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("No ProgressiveValue setted");
				EditorGUILayout.EndHorizontal();
				continue;
			}
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(v.name, GUILayout.Width(Inspector.GetWidth().GetPercentage(20f)));
			v.val.start = EditorGUILayout.FloatField(GUIContent.none, v.val.start, GUILayout.Width(Inspector.GetWidth().GetPercentage(20f)));
			v.val.progress = EditorGUILayout.FloatField(GUIContent.none, v.val.progress, GUILayout.Width(Inspector.GetWidth().GetPercentage(20f)));
			v.val.dynamic = EditorGUILayout.FloatField(GUIContent.none, v.val.dynamic, GUILayout.Width(Inspector.GetWidth().GetPercentage(20f)));
			v.val.Apply((PostActType)EditorGUILayout.EnumPopup(GUIContent.none, v.val.GetPostActType(), GUILayout.Width(Inspector.GetWidth().GetPercentage(20f))));
			EditorGUILayout.EndHorizontal();
		}

		GUI.color = defColor;
		GUI.enabled = false;
		for (int iter = 0; iter < iterations; iter++){
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField((iter + 1).ToString(), GUILayout.Width(Inspector.GetWidth().GetPercentage(10f)));
			foreach (var v in list){
				if (v == null || v.val == null){continue;}
				GUI.color = v.color == Color.clear ? defColor : v.color;
				EditorGUILayout.FloatField(GUIContent.none, v.val.Get(iter), GUILayout.Width(Inspector.GetWidth().GetPercentage(90f / list.Length)));
			}
			EditorGUILayout.EndHorizontal();
		}
		GUI.enabled = true;

		GUI.color = defColor;
	}
	#endif
}
[Serializable]
public class ProgressiveValuePrizes {
	public ProgressiveValue details, oil, diamonds;
}
}