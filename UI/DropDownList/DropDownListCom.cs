using UnityEngine;
namespace ACO.UI
{
    public class DropDownListCom : MonoBehaviour {
		public WindowBase window;
		public GameObject listPlace;
		public DropDownListComItem itemPrefab;
		DropDownList.DropDownList currentList = null;
		public void Open(DropDownList.DropDownList list ) {
			if (currentList != null) {
				Debug.LogError("list is already opened, lol!");
				return;
			}
			currentList = list;
			Clear();
			foreach (var item in list.Items) {
				DropDownListComItem comItem = Instantiate(itemPrefab) as DropDownListComItem;
				comItem.transform.SetParent(listPlace.transform, false);
				comItem.Apply(item);
				comItem.onSelect -= OnItemSelect; comItem.onSelect += OnItemSelect;
			}
			window.Open();
			window.OnClose.AddListener(OnClose);
		}
		void OnItemSelect(string id ) {
			currentList.SetSelected(id);
			Close();
		}
		public void Close() {
			window.Close();
		}
		void Clear() {
			foreach (Transform tr in listPlace.transform) {
				Destroy(tr.gameObject);
			}
		}
		public void OnClose() {
			currentList = null;
			Clear();
		}
	}
}