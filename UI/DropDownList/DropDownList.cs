using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ACO.UI.DropDownList
{
    public class DropDownList : MonoBehaviour {
		public DropDownListCom Com;
		[System.Serializable]
		public class Item {
			public Item(string name, string id ) {
				this.name = name;
				this.id = id;
			}
			public string name;
			public string id;
		}
		public Text text;
		public string id;
		public Button button;
		public List<Item> Items = new List<Item>();
		void Start() {
			button.onClick.AddListener(OnClick);
		}
		public event System.Action<Item> onChange;
		void OnClick() {
            if (Com == null)
            {
                Com = (DropDownListCom)FindObjectOfType(typeof(DropDownListCom));
                if (FindObjectsOfType(typeof(DropDownListCom)).Length > 1)
                {
                    Debug.LogError("You hasn't assigned any Com controller to this dropdown list. I'm trying to find one over scene, but i got strange result: i finded two or more Com controllers O_O");
                }
                if (Com == null)
                {
                    Com = ACO.UI.Glob.Get.dropdownCom;
                }
            }
			Com.Open(this);
		}
		public void SetSelected(string id ) {
			this.id = id;
			Apply();
			if (onChange != null) {
				onChange(_selected);
			}
		}
		public Item _selected;
		public Item selected {
			get {
				return _selected;
			}
			set {
				if (_selected  != value) {
					_selected = value;
					SetSelected(_selected.id);
				}
			}
		}
		void Apply() {
			_selected = Items.FirstOrDefault(i => i.id == id);
			if (_selected != null) {
				text.text = _selected.name;
			}
			else {
				Debug.LogError("Id not fonded, LOL!");
			}
		}
	}
}