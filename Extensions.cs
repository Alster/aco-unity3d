using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;

namespace ACO{
	/*public static class ListExtensions{
		static public void Swap<T>(this List<T> array, T v1, T v2){
			array.SwapByIndexes(array.IndexOf(v1), array.IndexOf(v2));
		}
		static public void SwapByIndexes<T>(this List<T> array, int position1, int position2){
			T temp = array[position1];
			array[position1] = array[position2];
			array[position2] = temp;
		}
		static public void Move<T>(this List<T> array, T v1, T v2){
			array.MoveByIndexes(array.IndexOf(v1), array.IndexOf(v2));
		}
		static public void MoveByIndexes<T>(this List<T> array, int position1, int position2){
			T temp = array[position1];
			array.RemoveAt(position1);
			array.Insert(position2, temp);
		}
	}*/
	#if UNITY_EDITOR
	public static class EditorExtension{
		public static int GetWidth(this Editor Inspector){
			return (Screen.width - (20 * (EditorGUI.indentLevel + 1))) - 30;
		}
	}
	#endif
	public static class IntExtensions{
		static public int GetPercentage(this int arg, float arg2){
			return (int)(((float)arg).GetPercentage(arg2));
		}
	}
	public static class FloatExtensions{
	    public static float GetPercentage(this float arg, float arg2){
	        return (arg / 100f) * arg2;
	    }
	}
	public static class StringExtensions {
		public static T ParseToEnum<T>(this string value){
		    return (T)System.Enum.Parse(typeof(T), value, true);
		}
	}
	public class EnumDictionary<K, V, P> : DictionarySerializable<K, V> 
	where K : struct, IConvertible 
	where V : new() 
	where P : KVPSerializable<K, V>, new() 
	{
		public EnumDictionary(ref List<P> refList) {
			ApplyFrom(ref refList);
		}
		public EnumDictionary(){Init();}
		void Init(){
			if (!typeof(K).IsEnum) {
			    throw new ArgumentException("'Key type' must be an enumerated type");
			}
			FillMissing();
			Crop();
		}
		public void ApplyFrom(ref List<P> refList){
			list.Clear();
			foreach (var v in refList){
				Add(v.Key, v.Value);
			}
			Init();
		}
		public void ApplyTo(ref List<P> refList){
			refList.Clear();
			foreach (var v in list){
				refList.Add(new P{Key = v.Key, Value = v.Value});
			}
		}
		void FillMissing(){
			var arr = Enum.GetValues(typeof(K));
			bool finded = false;
			foreach (var ar in arr){
				finded = false;
				foreach (var v in list){
					if (v.Key.Equals((K)ar)){
						finded = true; break;
					}
				}
				if (!finded){
					Add((K)ar, new V());
				}
			}
		}
		void Crop(){
			var arr = Enum.GetValues(typeof(K));
			bool finded = false;
			foreach (var v in list){
				finded = false;
				foreach (var ar in arr){
					if (v.Key.Equals((K)ar)){
						finded = true;
						break;
					}
				}
				if (!finded){
					list.Remove(v);
					Crop();
					break;
				}
			}
		}
	}

	[Serializable]
	public class DictionarySerializable<K, V> : IEnumerable<KVPSerializable<K, V>> {
		[SerializeField] protected List<KVPSerializable<K, V>> list = new List<KVPSerializable<K, V>>();
		public int Count {get{return list.Count;}}
		protected void Add(K Key, V Value){
			list.Add(new KVPSerializable<K, V>{Key = Key, Value = Value});
		}
		public void Remove(K Key){
			list.Remove(Find(Key));
		}
		public V this[K Key]{
			get {return Find(Key).Value;}
			set {
				bool finded = false;
				foreach (var v in list){
					if (v.Key.Equals(Key)){v.Value = value; finded = true; break;}
				}
				if (!finded){Add(Key, value);}
			}
		}
		public KVPSerializable<K, V> Find(K Key){
			foreach (var v in list){
				if (v.Key.Equals(Key)){return v;}
			}
			throw new System.InvalidOperationException();
		}
		public IEnumerator<KVPSerializable<K, V>> GetEnumerator(){
			foreach (var v in list){
				yield return v;
			}
	    }
	    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator(){
            return this.GetEnumerator();
        }
	}

	[Serializable]
	public class KVPSerializable<K, V> {
		public K Key; public V Value;
	}
	/*[HideInInspector]
	public List<string> _listCType = new List<string>();
	[HideInInspector]
	public List<Color> _listColor = new List<Color>();
    public static Dictionary<CType, Color> _TetrisCells;
    public static Dictionary<CType, Color> TetrisCells {
        get{
            if (_TetrisCells == null){
                _TetrisCells = new Dictionary<CType, Color>();
                ColorsCollection colorsCollection = GameObject.Find("ColorsCollection").GetComponent<ColorsCollection>();
                colorsCollection.RebuildDict(
                    ref _TetrisCells, 
                    ref colorsCollection._listCType, 
                    ref colorsCollection._listColor
                    );
            }
            return _TetrisCells;
        }
    }

    [HideInInspector]
    public List<string> _InterfaceColorKeys = new List<string>();
    [HideInInspector]
    public List<Color> _InterfaceColorValues = new List<Color>();
    public static Dictionary<InterfaceColor, Color> _InterfaceColors;
    public static Dictionary<InterfaceColor, Color> InterfaceColors {
        get{
            if (_InterfaceColors == null){
                _InterfaceColors = new Dictionary<InterfaceColor, Color>();
                ColorsCollection colorsCollection = GameObject.Find("ColorsCollection").GetComponent<ColorsCollection>();
                colorsCollection.RebuildDict(
                    ref _InterfaceColors, 
                    ref colorsCollection._InterfaceColorKeys, 
                    ref colorsCollection._InterfaceColorValues
                    );
            }
            return _InterfaceColors;
        }
    }
    public void RebuildDict<T>(
        ref Dictionary<T, Color> dict, 
        ref List<string> keys, 
        ref List<Color> values
        ){
        dict.Clear();
        T tempKey;
        bool finded;
        for(int i = 0; i < keys.Count; i++) {
            finded = false;
            foreach (var value in Enum.GetValues(typeof(T))) {
                tempKey = (T)value;
                if (keys[i] == tempKey.ToString()){
                    finded = true;
                }
            }
            if (!finded){
                foreach (var item in dict){
                    if (item.Key.ToString() == keys[i]){
                        dict.Remove(item.Key);
                        break;
                    }
                }
                keys.RemoveAt(i);
                values.RemoveAt(i);
                i--;
            }
        }
        foreach (var value in Enum.GetValues(typeof(T))) {
            tempKey = (T)value;
            dict[tempKey] = Color.white;
            finded = false;
            for(int i = 0; i < keys.Count; i++) {
                if (keys[i] == tempKey.ToString()){
                    dict[tempKey] = values[i];
                    finded = true;
                }
            }
            if (!finded){
                keys.Add(tempKey.ToString());
                values.Add(Color.white);
            }
        }
    }*/
}