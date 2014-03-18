using UnityEngine;
using System.Collections;

public class Object_Depth : MonoBehaviour {
	
	GameObject player;
	private Vector3 depth_position;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(player == null){
			Debug.Log("PLAYER NOT FOUND");
		}
		if(player.transform.position.z > transform.position.z){
			
			depth_position = new Vector3(transform.position.x, 0.4f , transform.position.z);
			transform.position = depth_position;
		}
		else
		{
			depth_position = new Vector3(transform.position.x, 0.2f , transform.position.z);
			transform.position = depth_position;
		}
	}
}
