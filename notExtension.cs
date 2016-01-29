using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*public abstract class SiblingListHook<T> : MonoBehaviour {
	T cl;
	int lastSiblingIndex = 0;
	public bool SH_childrensChanged = true;
	protected void Start(){
		lastSiblingIndex = transform.GetSiblingIndex();
		Transform tr = transform;
		while (cl == null){
			cl = tr.gameObject.GetComponent<T>();
			if (tr.parent != null){tr = tr.parent;}else {break;}
		}
		if (cl != null){
			cl.SH_childrensChanged = true;
		}
	}
	protected void OnTransformChildrenChanged(){SH_childrensChanged = true;}
	protected void Update(ref List<T> sh_entities) {
		if (cl != null && transform.GetSiblingIndex() != lastSiblingIndex){
			lastSiblingIndex = transform.GetSiblingIndex();
			cl.SH_RebuildEntities(sh_entities);
		}
		if (SH_childrensChanged){
			SH_childrensChanged = false;
			SH_RebuildEntities(sh_entities);
		}
	}
	public void SH_RebuildEntities(ref List<T> sh_entities) {
		if (sh_entities != null){
			sh_entities.Clear();
			SiblingListHook ef = null;
			foreach (Transform tr in transform){
				if (tr.gameObject.activeInHierarchy){
					ef = tr.gameObject.GetComponent<SiblingListHook>();
					if (ef != null){
						sh_entities.Add(ef);
					}
				}
			}
			sh_entities.Sort((L, R) => L.transform.GetSiblingIndex() > R.transform.GetSiblingIndex() ? 1 : -1);
		}
	}
}*/

namespace ACO{
	public class Utility {
		public static List<T> GetComponentsSortedToList<T>(Transform obj) where T : MonoBehaviour{
			List<T> res = new List<T>(); T ef;
			foreach (Transform tr in obj){
				if (tr.gameObject.activeInHierarchy){
					ef = tr.gameObject.GetComponent<T>();
					if (ef != null){res.Add(ef);}
				}
			}
			res.Sort((L, R) => L.transform.GetSiblingIndex() > R.transform.GetSiblingIndex() ? 1 : -1);
			return res;
		}
		public static T FindParent<T>(Transform obj) where T : MonoBehaviour{
			T res = null;
			Transform tr = obj;
			while (res == null){
				res = tr.gameObject.GetComponent<T>();
				if (tr.parent != null){tr = tr.parent;}else {break;}
			}
			return res;
		}
		public static void FixListLength<T>(ref List<T> list, int count) where T : new() {
			while (list.Count < count){
				list.Add(new T());
			}
			while (list.Count > count){list.RemoveAt(list.Count - 1);}
		}
		/*public static int GetMedian(int[] Value){
			if (Value.Length == 2){
				return (int)(((float)Value[0] + (float)Value[1]) / 2f);
			}
	        decimal Median = 0;
	        int size = Value.Length;
	        int mid = size / 2;
	        Median = (size % 2 != 0) ? (decimal)Value[mid] : ((decimal)Value[mid] + (decimal)Value[mid + 1]) / 2;
	        return Convert.ToInt32(Math.Round(Median));
	    }*/
	    public static decimal GetMedian(int[] temp)
	    {
	        // Create a copy of the input, and sort the copy
	        //int[] temp = source.ToArray();    
	        Array.Sort(temp);

	        int count = temp.Length;
	        if (count == 0)
	        {
	            return 0;
	        }
	        else if (count % 2 == 0)
	        {
	            // count is even, average two middle elements
	            int a = temp[count / 2 - 1];
	            int b = temp[count / 2];
	            return (a + b) / 2m;
	        }
	        else
	        {
	            // count is odd, return the middle element
	            return temp[count / 2];
	        }
	    }
	    public static void ShakeList<T>(ref List<T> list){
	    	System.Random RND = new System.Random();
            for (int i = 0; i < list.Count; i++)
            {
                T tmp = list[0];
                list.RemoveAt(0);
                list.Insert(RND.Next(list.Count), tmp);
            }
	    }
	}
}