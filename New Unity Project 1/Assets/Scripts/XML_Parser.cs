using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class XML_Parser  {
	
	public TextAsset xml;
	private string npc_xml = "XML/xml_NPC1";
	private string statement_xml = "XML/xml_statement1";
	private string response_xml = "XML/xml_response1";
	Response response;
	// Use this for initialization
	public XML_Parser(){}
	void Start () {
	//	LoadXML();
		//LoadResponses(2);
		//LoadStatements();
		//LoadNpcs();
	}
	public NPC[] LoadNpcs(int levelIn){

		//Get level in
		//int levelIn = 1;
		NPC npc;
		NPC[] npc_array = new NPC[2];
		int index = -1;
		bool found = false;
		
		xml = (TextAsset)Resources.Load(npc_xml);
		XmlDocument xmlDoc = new XmlDocument();
		
		xmlDoc.LoadXml(xml.text);
		//Debug.Log("REACHED");
		XmlNodeList npc_list = xmlDoc.GetElementsByTagName("Character");
		foreach(XmlNode dialog_fragment in npc_list){
			XmlNodeList xnl = dialog_fragment.ChildNodes;
			npc = new NPC();
			foreach(XmlNode xn in xnl ){
				
				//ADD NPC ID TO DATABASE
				
				if(xn.Name == "Level" && xn.InnerText != levelIn.ToString()){
					found = false;
				}
				if(xn.Name == "Level" && xn.InnerText == levelIn.ToString()){
					found = true;
					
					index++;
					//Debug.Log("Response_id : " + xn.InnerText);
					npc.SetLevel(xn.InnerText);
					
				}
					if(xn.Name == "Name" && found){
				//	Debug.Log("Symbol: " + xn.InnerText);
					npc.SetName(xn.InnerText);
				}
				if(xn.Name == "Portrait" && found){
					//Debug.Log("TEXT: " + xn.InnerText);
					npc.SetPortrait(xn.InnerText);
				}
				if(xn.Name == "Image" && found){
				//	Debug.Log("Audio File: " + xn.InnerText);
					npc.SetImage(xn.InnerText);
				}
			
				if(xn.Name == "Statement_ID" && found){
					//Debug.Log("Next Statement: " + xn.InnerText);
					npc.SetStatementId(xn.InnerText);
				}
			
				if(xn.Name == "Position" && found){
					//Debug.Log("Next Statement: " + xn.InnerText);
					npc.SetPosition(xn.InnerText);
				}		
			
			}
			
				//Add to response array
			
			if(found)
			npc.SetStatements(npc.GetName());
			npc_array[index] = npc;
			
		}
	
	  /*for(int i = 0; i < 4; i++){
			Debug.Log("Array test: " + response_array[i].getText());
		}*/
		
		//Return array
		return npc_array;
		
	}
	public Dictionary<int,Statement> LoadStatements(string nameIn){
			//Get statment id in
		//string nameIn = "Smith";
		//Debug.Log("SET STATE" + nameIn.ToString());
		Statement statement;
		Dictionary<int,Statement> statements = new Dictionary<int, Statement>();
	//	Statement[] statement_array = new Statement[10];
		int index = -1;
		bool found = false;
		
		xml = (TextAsset)Resources.Load(statement_xml);
		XmlDocument xmlDoc = new XmlDocument();
		
		xmlDoc.LoadXml(xml.text);
		//Debug.Log("REACHED");
		XmlNodeList statement_list = xmlDoc.GetElementsByTagName("Stage");
		foreach(XmlNode dialog_fragment in statement_list){
			XmlNodeList xnl = dialog_fragment.ChildNodes;
			statement = new Statement();
			foreach(XmlNode xn in xnl ){
				
				//ADD NPC ID TO DATABASE
				
				if(xn.Name == "Owner_Name" && xn.InnerText != nameIn.ToString()){
					found = false;
				}
				if(xn.Name == "Owner_Name" && xn.InnerText == nameIn.ToString()){
					found = true;
					
					index++;
					//Debug.Log("Response_id : " + xn.InnerText);
					statement.SetOwnerName(xn.InnerText);
					
				}
					if(xn.Name == "Statement_ID" && found){
				//	Debug.Log("Symbol: " + xn.InnerText);
					statement.SetStatementId(xn.InnerText);
				}
				if(xn.Name == "Text" && found){
					//Debug.Log("TEXT: " + xn.InnerText);
					statement.SetText(xn.InnerText);
				}
				if(xn.Name == "AudioFile" && found){
				//	Debug.Log("Audio File: " + xn.InnerText);
					statement.SetAudioFile(xn.InnerText);
				}
			
				if(xn.Name == "Response_ID" && found){
					//Debug.Log("Next Statement: " + xn.InnerText);
					statement.SetResponseId(xn.InnerText);
				}
				
			
			}
			
				//Add to response array
			
			if(found)
				//Before statement is saved to array, load responses
				statement.SetResponses();
				//Debug.Log("DICT BROKE " + statement.GetResponseId().ToString());
			if (statement.GetStatementId() != 0){
				statements.Add(statement.GetStatementId(), statement);}
				//statement_array[index] = statement;
			
		}
	
	  /*for(int i = 0; i < 4; i++){
			Debug.Log("Array test: " + response_array[i].getText());
		}*/
		
		//Return array
		return statements;
		
	}
	public Response[] LoadResponses(int idIn){
		//Get statment id in
		//int idIn = 1;
		Response[] response_array = new Response[4];
		response_array[0] = new Response();
		response_array[1] = new Response();
		response_array[2] = new Response();
		response_array[3] = new Response();
		int index = -1;
		bool found = false;
		
		xml = (TextAsset)Resources.Load(response_xml);
		XmlDocument xmlDoc = new XmlDocument();
		
		xmlDoc.LoadXml(xml.text);
		//Debug.Log("REACHED");
		XmlNodeList choice_list = xmlDoc.GetElementsByTagName("choices");
		foreach(XmlNode dialog_fragment in choice_list){
			XmlNodeList xnl = dialog_fragment.ChildNodes;
			response = new Response();
			foreach(XmlNode xn in xnl ){
				
				
				
				if(xn.Name == "Response_ID" && xn.InnerText != idIn.ToString()){
					found = false;
				}
				if(xn.Name == "Response_ID" && xn.InnerText == idIn.ToString()){
					found = true;
					
					index++;
					//Debug.Log("Response_id : " + xn.InnerText);
					response.SetResponseId(xn.InnerText);
					
				}
				if(xn.Name == "Text" && found){
					//Debug.Log("TEXT: " + xn.InnerText);
					response.SetText(xn.InnerText);
				}
				if(xn.Name == "AudioFile" && found){
				//	Debug.Log("Audio File: " + xn.InnerText);
					response.SetAudioFile(xn.InnerText);
				}
				if(xn.Name == "Symbol" && found){
				//	Debug.Log("Symbol: " + xn.InnerText);
					response.SetSymbol(xn.InnerText);
				}
				if(xn.Name == "NextStatement" && found){
					//Debug.Log("Next Statement: " + xn.InnerText);
					response.SetNextStatement(xn.InnerText);
				}
				
			
			}
			
				//Add to response array
			
			if(found)
			response_array[index] = response;
			
		}
	
	  /*for(int i = 0; i < 4; i++){
			Debug.Log("Array test: " + response_array[i].getText());
		}*/
		
		//Return array
		return response_array;
		
	}
	
	/*public void LoadXML(){
		
		xml = (TextAsset)Resources.Load("XML/City Life text file");
		
		XmlDocument xmlDoc = new XmlDocument();
		
		
		//xmlDoc.LoadXml((XML)Resources.Load("XML/City Life text file"));
		xmlDoc.LoadXml(xml.text);
		//Debug.Log("REACHED");
		XmlNodeList dialog_list = xmlDoc.GetElementsByTagName("NPC_Dialog");
		
		foreach(XmlNode dialog_fragment in dialog_list){
			XmlNodeList xl = dialog_fragment.ChildNodes;
			
			foreach(XmlNode xln in xl ){
				if(xln.Name == "TID"){
				//	Debug.Log("TID: " + xln.InnerText);
				}
			}
			
		}
		//Debug.Log("REACHED");
	}*/
}
