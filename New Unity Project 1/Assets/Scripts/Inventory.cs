using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	
	//public int MAX_ITEMS = 10;
	public int current_items;
	public List<string> inventory; // = new string[MAX_ITEMS];
	
	// Use this for initialization
	void Start () {
		
		Debug.Log("Inventory Init");
		inventory = new List<string>();
		current_items = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}
	void OnTriggerEnter(Collider collider){
		Debug.Log("WALLET HIT");
		Debug.Log(collider.name);
		RemoveObjectFromScene(collider.name);
		AddObjectToInventory(collider.name);
	}
	public void RemoveObjectFromScene(string name){
		GameObject item = GameObject.Find(name);
		item.active = false;
	}
	public void AddObjectToInventory(string name){
		inventory.Add(name);
		//inventory[current_items] = name;
		current_items++;
	}
	public void RemoveObjectFromInventory(string name){
		inventory.Remove(name);

	}
	public bool checkItem(string item){
		if(inventory.Contains(item)){
			return true;
		}
		else{
			return false;
		}
	}
}
