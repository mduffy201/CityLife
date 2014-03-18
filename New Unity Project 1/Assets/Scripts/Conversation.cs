using UnityEngine;
using System.Collections;

public class Conversation : MonoBehaviour {
	
	GameObject conversation_component;
	GameObject npc_p; 
	//MeshRenderer p_mrender;
	Renderer p_render;
	Material portrait;
	Renderer[] c_renderer;
	bool display;
	Level_Logic level_logic;
	GameObject camera; 
	//Level_Logic ll = camera.GetComponent(typeof(Level_Logic),"Level_Logic");
	private GUIText txtStatement;
	private GUIText txtRes1;
	private GUIText txtRes2;
	private GUIText txtRes3;
	private GUIText txtRes4;
	
	public string statement = "H";
	public string response1 = "N";
	public string response2 = "I";
	public string response3 = "O";
	public string response4 = "I";
	
	// Use this for initialization
	void Start () {
		
		camera = GameObject.Find("Camera");
		display = false;
		conversation_component = (GameObject)GameObject.Find("Conversation_Component");
		level_logic = (Level_Logic)camera.GetComponent("Level_Logic");
		
	//Debug.Log("CONVERSATION CHECK " + level_logic.GetCurrentStatementText());
		
		if(conversation_component!= null){
			//Debug.Log("conversation component found");
			c_renderer = conversation_component.GetComponentsInChildren<Renderer>();
			
			GUIText[] txtBoxes = conversation_component.GetComponentsInChildren<GUIText>();
			foreach(GUIText txtBox in txtBoxes){
				//Debug.Log("BOXES: " + txtBox.name.ToString());
				if(txtBox.name.ToString() == "NPC_Speach"){
					txtStatement = txtBox;
				}
				if(txtBox.name.ToString() == "Choice_01"){
					txtRes1 = txtBox;
				}
				if(txtBox.name.ToString() == "Choice_02"){
					txtRes2 = txtBox;
				}
				if(txtBox.name.ToString() == "Choice_03"){
					txtRes3 = txtBox;
				}
				if(txtBox.name.ToString() == "Choice_04"){
					txtRes4 = txtBox;
				}
			}
			//txtStatement = conversation_component.GetComponent("NPC_Speach").GetComponent<GUIText>();
			
			//txtRes1 = conversation_component.GetComponentInChildren<GUIText>();
		}
		
		
		npc_p = (GameObject) GameObject.Find("NPC_Portrait");
		//p_mrender = (MeshRenderer) npc_p.GetComponent(typeof(MeshRenderer));
		p_render = (Renderer)npc_p.GetComponent(typeof(Renderer));
		portrait = (Material)Resources.Load("NPC_Portraits/Materials/horse");
		p_render.material = null;
	}
	
	public void initConversation(){
		statement = level_logic.GetCurrentStatementText();
	 	response1 = level_logic.GetResponse01();
	 	response2 = level_logic.GetResponse02();
	 	response3 = level_logic.GetResponse03();
	 	response4 = level_logic.GetResponse04();
		portrait = (Material)Resources.Load("NPC_Portraits/Materials/" + level_logic.GetCurrentPortrait());
		
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)){
			//Debug.Log("CONVERSATION INIT");	
			
			if(p_render.enabled == false)
			{
				p_render.enabled = true;
			}
			else{
				p_render.enabled = false;
			}
		}
		if(Input.GetKeyDown(KeyCode.A)){
			//Debug.Log("IMAGE LOAD");
			p_render.material = portrait;
		}
		if(Input.GetKeyDown(KeyCode.B)){
				
			if(display){
				display = false;
			}
			else{
				display = true;
			}
				
			//Debug.Log("TURN ALL OFF");
				
			if(display){
				foreach(Renderer r in c_renderer){
					
					r.enabled=true;
				}
			}
			else{
				foreach(Renderer r in c_renderer){
					
					r.enabled=false;
				}	
			}
		}
		if(Input.GetKeyDown(KeyCode.Q)){
			initConversation();
			p_render.material = portrait;
			txtStatement.text = statement;
			txtRes1.text = response1;
			txtRes2.text = response2;
			txtRes3.text = response3;
			txtRes4.text = response4;
		}
			if(Input.GetKeyDown(KeyCode.E)){
			//Debug.Log("CONVERSATION CHECK " + level_logic.GetCurrentStatementText());
		}
		if (Input.GetMouseButtonDown (0)){
			Debug.Log("MOUSE CLICK");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			
			//If ray hits plane object
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				
				Debug.Log("HIT: " + hit.collider.name.ToString());
				level_logic.SetNewStatement(hit.collider.name.ToString());
			}
			
			
		}
	}
}

