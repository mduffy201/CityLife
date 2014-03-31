using UnityEngine;
using System.Collections;

public class Conversation : MonoBehaviour {
	
	GameObject conversation_component;
	GameObject npc_p; 
	//MeshRenderer p_mrender;
	Renderer p_render;
	Material portrait;
	//Renderer[] c_renderer;
	
	public bool display;
	Level_Logic level_logic;
	GameObject camera; 
	//Level_Logic ll = camera.GetComponent(typeof(Level_Logic),"Level_Logic");
/*	private GUIText txtStatement;
	private GUIText txtRes1;
	private GUIText txtRes2;
	private GUIText txtRes3;
	private GUIText txtRes4;*/
	
	
	private TextMesh tmStatement;
	private TextMesh tmRes1;
	private TextMesh tmRes2;
	private TextMesh tmRes3;
	private TextMesh tmRes4;
	
	public string statement = "S";
	/*public Response res1;
	public string response1 = "1";
	public string response2 = "2";
	public string response3 = "3";
	public string response4 = "4";*/
	
	public Response response1;
	public Response response2;
	public Response response3;
	public Response response4;
	
	public bool isConvo = false;
	// Use this for initialization
	void Start () {
		
		camera = GameObject.Find("Camera");
		display = false;
		conversation_component = (GameObject)GameObject.Find("Conversation_Component");
		level_logic = (Level_Logic)camera.GetComponent("Level_Logic");
		
	//Debug.Log("CONVERSATION CHECK " + level_logic.GetCurrentStatementText());
		
		if(conversation_component!= null){
			//Debug.Log("conversation component found");
		//	c_renderer = conversation_component.GetComponentsInChildren<Renderer>();
		
			
			TextMesh[] tmBoxes = conversation_component.GetComponentsInChildren<TextMesh>();
			
			foreach(TextMesh tmBox in tmBoxes){
				if(tmBox.name == "Statement"){
					tmStatement = tmBox;
				
				}
				if(tmBox.name == "Response01"){
					tmRes1 = tmBox;
					
				}
				if(tmBox.name == "Response02"){
					tmRes2 = tmBox;
					
				}
				if(tmBox.name == "Response03"){
					tmRes3 = tmBox;
					
				}
				if(tmBox.name == "Response04"){
					tmRes4 = tmBox;
					
				}
			}
		}
		
		
		npc_p = (GameObject) GameObject.Find("NPC_Portrait");
		//p_mrender = (MeshRenderer) npc_p.GetComponent(typeof(MeshRenderer));
		p_render = (Renderer)npc_p.GetComponent(typeof(Renderer));
		portrait = (Material)Resources.Load("NPC_Portraits/Materials/horse");
		p_render.material = null;
	}
	
	public void initConversation(string npc_name){
		isConvo = true;
		level_logic.SetCurrentNPC(npc_name);
		statement = level_logic.GetCurrentStatementText();
		
		//res1 = level_logic.GetResponse01();
	 	
		response1 = level_logic.GetResponse01();
	 	response2 = level_logic.GetResponse02();
	 	response3 = level_logic.GetResponse03();
	 	response4 = level_logic.GetResponse04();
		portrait = (Material)Resources.Load("NPC_Portraits/Materials/" + level_logic.GetCurrentPortrait());
		
		p_render.material = portrait;
		tmStatement.text = statement;
		
		//tmRes1.text = res1.GetText();
		tmRes1.text = response1.GetText();
		tmRes2.text = response2.GetText();
		tmRes3.text = response3.GetText();
		tmRes4.text = response4.GetText();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown (0)){
			Debug.Log("MOUSE CLICK");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			
			//If ray hits plane object
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				
				/*if(hit.collider.tag == "NPC_CONVO"){
					Debug.Log("CONVO INIT " + hit.collider.name);
				}*/
				
				
				//Debug.Log("HIT: " + hit.collider.name.ToString());
				if(hit.collider.name.ToString() == "Choice_01"){
					
					Debug.Log("OPTION 1 SELECTED");
					level_logic.SetCurrentStatment(response1.GetNextStatement());
					refreshConvo();
					
					//level_logic.SetCurrentStatment(4);
				}
				if(hit.collider.name.ToString() == "Choice_02"){
					
					Debug.Log("OPTION 2 SELECTED");
					level_logic.SetCurrentStatment(response2.GetNextStatement());
					refreshConvo();
				}
				if(hit.collider.name.ToString() == "Choice_03"){
					
					Debug.Log("OPTION 3 SELECTED");
					level_logic.SetCurrentStatment(response3.GetNextStatement());
					refreshConvo();
				}
				if(hit.collider.name.ToString() == "Choice_04"){
					
					Debug.Log("OPTION 4 SELECTED");
					level_logic.SetCurrentStatment(response4.GetNextStatement());
					refreshConvo();
				}
				
				//level_logic.SetNewStatement(hit.collider.name.ToString());
			}
		}
	}
	public void refreshConvo(){
		statement = level_logic.GetCurrentStatementText();
	 	response1 = level_logic.GetResponse01();
	 	response2 = level_logic.GetResponse02();
	 	response3 = level_logic.GetResponse03();
	 	response4 = level_logic.GetResponse04();
		
		tmStatement.text = statement;
		tmRes1.text = response1.GetText();
		tmRes2.text = response2.GetText();
		tmRes3.text = response3.GetText();
		tmRes4.text = response4.GetText();
	}
	/*		
 * REFRESH
 * if(Input.GetKeyDown(KeyCode.Q)){
			initConversation();
			p_render.material = portrait;
			tmStatement.text = statement;
			tmRes1.text = response1;
			tmRes2.text = response2;
			tmRes3.text = response3;
			tmRes4.text = response4;
		}*/
		
		/*if(display){
			//conversation_component.active = true;
			foreach(Renderer r in c_renderer){		
				r.enabled=true;
				}
		
			}
			else{
			//conversation_component.active = false;
				foreach(Renderer r in c_renderer){
					
					r.enabled=false;
				
				}
				
			}*/
		
		
		
		/*if (Input.GetMouseButtonDown (1)){
			//Debug.Log("CONVERSATION INIT");	
			
			if(p_render.enabled == false)
			{
				p_render.enabled = true;
			}
			else{
				p_render.enabled = false;
			}
		}*/
		/*if(Input.GetKeyDown(KeyCode.A)){
			//Debug.Log("IMAGE LOAD");
			p_render.material = portrait;
		}*/
		/*if(Input.GetKeyDown(KeyCode.B)){
				
			if(display){
				display = false;
			}
			else{
				display = true;
			}
				
			//Debug.Log("TURN ALL OFF");
				
		
		}*/
			
	
		/*if(Input.GetKeyDown(KeyCode.E)){
			//Debug.Log("CONVERSATION CHECK " + level_logic.GetCurrentStatementText());
		}*/
	/*public void intiConvo(){
		initConversation();
			p_render.material = portrait;
			tmStatement.text = statement;
			tmRes1.text = response1;
			tmRes2.text = response2;
			tmRes3.text = response3;
			tmRes4.text = response4;
		
	}*/
}

