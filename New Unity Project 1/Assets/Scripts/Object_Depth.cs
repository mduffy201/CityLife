using UnityEngine;
using System.Collections;

public class Object_Depth : MonoBehaviour {
	
	public float behind_player_vert;
	public float infront_player_vert;
	public float behind_player_hor;
	public float infront_player_hor;
	
	public bool H_Depth = false;
	public bool V_Depth = false;
	
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
		if(V_Depth){
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
		
		if(H_Depth){
			if(player.transform.position.x > transform.position.x){
				
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
}
