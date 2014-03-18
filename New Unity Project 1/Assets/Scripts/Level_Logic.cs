using UnityEngine;
using System.Collections;

public class Level_Logic : MonoBehaviour {
	
	private XML_Parser xm_p;
	private NPC current_npc;
	private int current_level = 1;
	private string current_npc_name = "Smith";
	private Statement current_statement;
	private Response[] currrent_responses;
	private int no_of_npcs = 2;
	private NPC[] level_npc;
	
	private Response response01;
	private Response response02;
	private Response response03;
	private Response response04;
	
	
	
	// Use this for initialization
	void Start () {
		xm_p = new XML_Parser();
		
		
		level_npc = new NPC[no_of_npcs];
	
		level_npc = xm_p.LoadNpcs(current_level); 	
		
		/*foreach( NPC npc in level_npc){
			Debug.Log("NPC LOOP " + npc.GetName());
		}*/
		SetCurrentNPC();
		SetCurrentStatment();
		SetCurrentResponses();
	}
	public void SetCurrentNPC(){
		foreach( NPC npc in level_npc){
			if(npc.GetName() == current_npc_name){
				current_npc = npc;
			} 
		}
		
		//Debug.Log("CURRENT NPC: " + current_npc.GetName());
		
	}
	public void SetCurrentStatment(){
		current_statement = current_npc.GetStatement(1);
		//Debug.Log("CURRENT STATEMENT: " + current_statement.GetText());
		
	}
	public void SetCurrentStatment(int statementIdIn){
		current_statement = current_npc.GetStatement(statementIdIn);
		//Debug.Log("CURRENT STATEMENT: " + current_statement.GetText());
		
	}
	public void SetCurrentResponses(){
		
		currrent_responses = current_statement.GetResponses();
		
		response01 = currrent_responses[0];
		response02 = currrent_responses[1];
		response03 = currrent_responses[2];
		response04 = currrent_responses[3];
		
		foreach(Response r in currrent_responses){
			//Debug.Log("CURRENT RESPONSES: " + r.GetText());
		}
		
	}
	public string GetCurrentStatementText(){
		return current_statement.GetText();
	}
	public string GetCurrentPortrait(){
		return current_npc.GetPortrait();
	}
	public string GetResponse01(){
		return response01.GetText();
	}
	public string GetResponse02(){
		return response02.GetText();
	}
	public string GetResponse03(){
		return response03.GetText();
	}
	public string GetResponse04(){
		return response04.GetText();
	}
	public void SetNewStatement(string responseIn){
		string resNameIn = "Choice_01";
		Response pickedResponse = new Response();
		if (resNameIn == "Choice_01"){
			pickedResponse = response01;
		}
		int newStatementId = pickedResponse.GetNextStatement();
		SetCurrentStatment(newStatementId);
		SetCurrentResponses();
		
	}
}