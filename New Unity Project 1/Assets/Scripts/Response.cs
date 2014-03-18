using UnityEngine;
using System.Collections;

public class Response {
	
	private int response_id;
	private string text;
	private string audio_file;
	private string symbol;
	private int next_statement;
	
	public Response(){
		response_id = 0;
		text = "";
		audio_file = "";
		symbol = "";
		next_statement = 0;
	}
	public int GetResponseId(){
		return response_id;
	}
	public void SetResponseId(string idIn){

		response_id = int.Parse(idIn);
		
	}
	public string GetText(){
		return text;
	}
	public void SetText(string textIn){
		text = textIn;
	}
	public string GetAudioFile(){
		return audio_file;
	}
	public void SetAudioFile(string audioFileIn){
		audio_file = audioFileIn;
	}
	public string GetSymbol(){
		return symbol;
	}
	public void SetSymbol(string symbolIn){
		symbol = symbolIn;
	}
	public int GetNextStatement(){
		return next_statement;
	}
	public void SetNextStatement(string nextStatementIn){
		next_statement = int.Parse(nextStatementIn);
	}
	


}
