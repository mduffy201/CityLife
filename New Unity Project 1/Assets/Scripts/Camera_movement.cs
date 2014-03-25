using UnityEngine;
using System.Collections;

public class Camera_movement : MonoBehaviour {
	GameObject player;
	public float camera_distance = 11.0f;
	// Use this for initialization
	void Start () {
	player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 player_postion = new Vector3(player.transform.position.x, camera_distance, player.transform.position.z);
	this.transform.position = player_postion;
	}
}
