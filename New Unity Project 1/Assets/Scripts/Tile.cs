using UnityEngine;
using System.Collections;
using System;

public class Tile:IComparable<Tile>{
		
	private int id = 9;
	public Vector3 centre;
	public int x_pos;
	public int z_pos; 
	private int collison;
	private int f_score = 0;
	private int g_score = 0;
	private Tile aParent;
	public float character_offset = 0.2f;
	
	//array positon
		public Tile(){
		centre.x = 0.0f;
		centre.z = 0.0f;
		centre.y = 0.3f;
		x_pos = 0;
		z_pos = 0;
	}
	public Tile(Vector3 lower_left, Vector3 tile_size, int xpos, int zpos){
		centre.x = lower_left.x + ( tile_size.x/2);
		centre.z = lower_left.z + ( tile_size.z/2);
		centre.y = 0.3f;
		//centre.x = centre.x - centre.z; 
		//centre.z = (centre.x  + centre.z)/2;
		
		x_pos = xpos;
		z_pos = zpos;
	}
	public Tile(int xpos, int zpos, float xcord, float zcoord){
		centre.x = xcord;
		centre.z = zcoord  ;
		centre.y = 0.3f;
		//centre.x = centre.x - centre.z; 
		//centre.z = (centre.x  + centre.z)/2;
		
		x_pos = xpos;
		z_pos = zpos;
	}
	public void setCollision(int colIn){
		collison = colIn;
	}
	public int getCollision(){return collison;}
	
	
	public void setFScore(int fIn){
		
		f_score = fIn;
	}
	public int getFScore(){
		
		return f_score;
	}
	public void setGScore(int gIn){
		
		g_score = gIn;
	}
	public int getGScore(){
		
		return g_score;
	}
	public void setParent(Tile tileIn){
		
		aParent = tileIn;
	}
	public Tile getParent(){
		
		return aParent;
	}
	public int CompareTo(Tile tile){
		 return this.f_score.CompareTo(tile.f_score);
	}
}
