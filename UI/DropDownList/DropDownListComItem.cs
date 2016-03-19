using UnityEngine;
using UnityEngine.UI;

namespace ACO.UI
{
    public class DropDownListComItem : MonoBehaviour {
		public Text text;
		public string id;
		public Button button;
		public event System.Action<string> onSelect;
        public void Apply(DropDownList.DropDownList.Item item ) {
			text.text = item.name;
			id = item.id;
			button.onClick.AddListener(OnClick);
		}
		void OnClick() {
			if (onSelect != null) {
				onSelect(id);
			}
		}
	}
}