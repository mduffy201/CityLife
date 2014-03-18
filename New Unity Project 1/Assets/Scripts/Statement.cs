using UnityEngine;
using System.Collections;

public class Statement {
	
	private string owner_name;
	private int statement_id;
	private string text;
	private string audio_file;
	private int response_id;
	private Response[] choices;
	XML_Parser xml_p;

	public Statement(){
		xml_p = new XML_Parser();
		owner_name = "";
		statement_id = 0;
		text = "";
		audio_file = "";
		response_id = 0;
		choices = new Response[4];
	}
	public void SetResponses(){
		//Debug.Log("BROKE");
		choices = xml_p.LoadResponses(statement_id);
	}
	public Response[] GetResponses(){
		return choices;
	}
	public void SetOwnerName(string ownerNameIn){
		owner_name = ownerNameIn;
	}
	public void SetStatementId(string statementIdIn){
		//Debug.Log(statementIdIn.ToString());
		statement_id = int.Parse(statementIdIn);
	}
	public void SetText(string textIn){
		text = textIn;
	}
	public void SetAudioFile(string audioIn){
		audio_file = audioIn;
	}
	public void SetResponseId(string responseIn){
		response_id = int.Parse(responseIn);
	}
	
	public string GetOwnerName(){
		return owner_name;
	}
	public int GetStatementId(){
		return statement_id;
	}
	public string GetText(){
		return text;
	}
	public string GetAudioFile(){
		return audio_file;
	}
	public int GetResponseId(){
		return response_id;
	}
}
