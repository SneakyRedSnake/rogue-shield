using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 	Represents an inventory.
/// </summary>
public class Inventory : MonoBehaviour {

	[SerializeField]int maxSize;										//the max size of the inventory;
	public ItemInventory inventoryList = new ItemInventory();				//the inventory is a list of Item
	public GameObject[] inventory { get { return inventoryList.inventory; } }


	/// <summary>
	/// 	We instantiate all the items in the inventory at the start
	/// 	(they are prefab so we need to instantiate them in order to
	/// 	use them)
	/// </summary>
	void Start(){
		for(int i = 0; i < this.CurrentSize(); i++){
			inventory[i] = (GameObject)Instantiate(inventory[i]);
			inventory[i].SetActive(false);
		}
	}

	/// <summary>
	/// 	The current size of the inventory (the number of item in it)
	/// </summary>
	/// <returns>The size of the inventory.</returns>
	public int CurrentSize(){
		for(int i = 0; i < inventory.Length; i++){
			if(!inventory[i]){
				return i;
			}
		}
		return inventory.Length;
	}

	/// <summary>
	/// 	Add the specified item to the inventory
	/// 	If the inventory is full, we throw aways
	/// 	the first item
	/// </summary>
	/// <param name="item">The item we want to add.</param>
	public void Add(GameObject item){
		int size = this.CurrentSize ();
		if (size >= maxSize) {

			inventory[0].transform.position = this.gameObject.transform.position;
			inventory[0].SetActive(true);
			inventory[0].rigidbody2D.AddForce(new Vector2(10,300));
			this.RemoveAt(0);
		}
		Debug.Log ("In the inventory");
		inventory[size%maxSize] = item;
	}

	/// <summary>
	/// 	Use the item at the specified position.
	/// </summary>
	/// <param name="position">Position of the object we want to use</param>
	public void Use(int position){
		Debug.Log (inventory.Length);
		if (position < this.CurrentSize() && position >= 0) {
			GameObject item = inventory[position];
			Item itemComponent = item.GetComponent<Item>();
			if(itemComponent){
				if(itemComponent.isDestroyedAfterUse()){
					this.RemoveAt(position);
				}
				itemComponent.Use ();
			}else{
				Debug.Log("ItemComponent doesn't exist");
			}
		}
	}

	private void RemoveAt(int position){
		int i;
		for (i = position; i<inventory.Length-1; i++) {
			inventory[i] = inventory[i+1];
		}
		inventory [i] = null;
	}

	/// <summary>
	/// 	Gets a string representing the content of the inventory.
	/// </summary>
	/// <returns>The inventory content as a String.</returns>
	public string GetInventoryContent(){
		string inventoryContent = "";
		int size = this.CurrentSize ();
		for(int i = 0; i < size; i++){
			inventoryContent += inventory[i].GetComponent<Item>().GetItemName()+"\n";
		}
		return inventoryContent;
	}



	//This is our custom inventory class with the item
	[System.Serializable]
	public class ItemInventory{
		public GameObject[] inventory = new GameObject[0];
	}
}

#if UNITY_EDITOR
[CustomPropertyDrawer (typeof(Inventory.ItemInventory))]
public class InventoryDrawer : PropertyDrawer
{
	float lineHeight = 18;
	float spacing = 4;
	
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty (position, label, property);
		
		float x = position.x;
		float y = position.y;
		float inspectorWidth = position.width;
		
		// Draw label
		
		
		// Don't make child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		
		var items = property.FindPropertyRelative ("inventory");
		string[] titles = new string[] { "Inventory", "", "", "" };
		string[] props = new string[] { "item", "^", "v", "-" };
		float[] widths = new float[] { .7f, .1f, .1f, .1f };
		float lineHeight = 18;
		bool changedLength = false;
		if (items.arraySize > 0)
		{
			
			for (int i=-1; i<items.arraySize; ++i) {
				
				var item = items.GetArrayElementAtIndex (i);
				
				float rowX = x;
				for (int n=0; n<props.Length; ++n)
				{
					float w = widths[n] * inspectorWidth;
					
					// Calculate rects
					Rect rect = new Rect (rowX, y, w, lineHeight);
					rowX += w;
					
					if (i == -1)
					{
						EditorGUI.LabelField(rect, titles[n]);
					} else {
						if (n==0)
						{
							item.objectReferenceValue = EditorGUI.ObjectField(rect, item.objectReferenceValue, typeof(GameObject), true);						
						} else {
							if (GUI.Button (rect, props[n]))
							{
								switch (props[n])
								{
								case "-":
									items.DeleteArrayElementAtIndex(i);
									changedLength = true;
									break;
								case "v":
									if (i > 0) items.MoveArrayElement(i,i+1);
									break;
								case "^":
									if (i < items.arraySize-1) items.MoveArrayElement(i,i-1); 
									break;
								}
								
							}
						}
					}
				}
				
				y += lineHeight + spacing;
				if (changedLength)
				{
					break;
				}
			}
			
		}
		// add button
		var addButtonRect = new Rect ((x + position.width) - widths[widths.Length-1]*inspectorWidth, y, widths[widths.Length-1]*inspectorWidth, lineHeight);
		if (GUI.Button (addButtonRect, "+")) {
			items.InsertArrayElementAtIndex(items.arraySize);
		}
		
		y += lineHeight + spacing;
		
		// Set indent back to what it was
		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty ();
	}
	
	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		SerializedProperty items = property.FindPropertyRelative ("inventory");
		float lineAndSpace = lineHeight + spacing;

		return 40 + (items.arraySize * lineAndSpace) + lineAndSpace;
	}
}
#endif
