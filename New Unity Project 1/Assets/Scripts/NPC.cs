using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC  {
	
	//connect to xmlparser
	XML_Parser xml_p;
	//load statements
	//List of Statements
	private string name;
	private string portrait;
	private string	Image;
	private int Statement_ID;
	private int Level;
	private int Position;
	Dictionary<int,Statement> dialog_tree;
	
	public NPC(){
		 xml_p = new XML_Parser();
		name = "";
		portrait = "";
		Image = "";
		Statement_ID = 0;
	 	Level = 0;
	 	Position = 0;
	}
	public void SetStatements(string nameIn){
		//Debug.Log("SET STATE" + nameIn.ToString());
		dialog_tree = xml_p.LoadStatements(nameIn);
	
	}
	public Dictionary<int,Statement> GetStatements(){
		
		return	dialog_tree;
	
	}
	public Statement GetStatement(int idIn){
		if(dialog_tree.ContainsKey(idIn)){
			return dialog_tree[idIn];
		}
		else{
			return null;
		}
		
	}
	public void  SetName(string nameIn){
		name = nameIn;
	}
	public void  SetPortrait(string portraitIn){
		portrait = portraitIn;
	}
	public void  SetImage(string imageIn){
		Image = imageIn;
	}
	public void  SetStatementId(string statmentIdIn){
		Statement_ID = int.Parse(statmentIdIn);
	}
	public void  SetLevel(string levelIn){
		Level = int.Parse(levelIn);
	}
	public void  SetPosition(string positionIn){
		Position = int.Parse(positionIn);
	}
	
	public string  GetName(){
		return name;
	}
	public string  GetPortrait(){
		return portrait;
	}
	public string  GetImage(){
		return Image;
	}
	public int  GetStatementId(){
		return Statement_ID;
	}
	public int  GetLevel(){
		return Level;
	}
	public int  GetPosition(){
		return Position;
	}
	
	

}
