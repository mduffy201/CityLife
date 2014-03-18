using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
	
	public string name = "LEVEL";
	public bool grid_on = true;
	public Vector3 start_point;
	public Vector3 end_point;
	public GameObject grid_end;
	public const int horizontal_tiles = 31;	//Number of tiles on x axis
	public const int vertical_tiles = 31;	//Number of tiles on z axis
	public int no_of_tiles;					//Total number of tiles in grid
	public Vector3 tile_size;				//Size on an individual tile
	
//	private Mesh mesh;						//Mesh of ground plane object
	//public Bounds ground;					//Bound data of plane object
	
	//public int[,] tiles;					//Array for tile data (Collision)
	public Tile[,] tile_array;               //Array of tile objects
	public Tile[,] iso_array;
	public int[,] collision_array;
	private Vector3 lower_left;				//Calculate centre
	private Dictionary<string,Tile> tile_dict;
	public GameObject center_point;
	public GameObject center_point_blocked;
	//public string tile_data = "Tile: "+ aTile.x_pos.ToString() + ", " + aTile.z_pos.ToString() +"C: " + aTile.centre.ToString() ;
	
	void Update(){
		
		
	}
	
	// Use this for initialization
	void Start () {
		
		
		center_point = (GameObject)Resources.Load("Prefabs/CenterPoint");
		center_point_blocked = (GameObject)Resources.Load("Prefabs/CentrePointBlocked");
		grid_end = GameObject.Find("grid_end");							//Find marker for grid size
		start_point = new Vector3(0.0f,0.0f,0.0f);						//Set grid start at origin
		end_point = grid_end.transform.position;						//Set grid end to marker
		
		no_of_tiles = horizontal_tiles*vertical_tiles;					//Calculate the total number of tiles
		tile_array = new Tile[horizontal_tiles, vertical_tiles];
		iso_array = new Tile[horizontal_tiles, vertical_tiles];//Create array of tiles
		
		
		
		CalculateTileSize();											//Calculate size of individual tile
		
		
				collision_array = new int[horizontal_tiles, vertical_tiles] {
		 	{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1},
			{1, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
			};
		
		//Crate tile array
		//loop horzontal
		for(int i = 0; i < horizontal_tiles; i++){
			//loop vertical
			for(int j = 0; j < vertical_tiles; j++){
				
				
				
				lower_left.x = tile_size.x * i;
				lower_left.z = tile_size.z * j;
				
				//Debug.Log(i.ToString() + ", " + j.ToString() + " = " + lower_left.x.ToString() + ", " + lower_left.z.ToString());
				
				tile_array[i,j] = new Tile(lower_left, tile_size, i, j);
			}
		}
		/*tile_dict = new Dictionary<string, Tile>();
		//foreach(Tile aTile in tile_array){
		foreach(Tile aTile in iso_array){
			Debug.Log(aTile.x_pos.ToString() + aTile.z_pos.ToString());
			tile_dict.Add( aTile.x_pos.ToString() + aTile.z_pos.ToString(),aTile);
		}*/
		
		for(int i = 0; i < horizontal_tiles; i++){
			for(int j = 0; j < vertical_tiles; j++){
				//Debug.Log("Tile [" + i + "," + j + "] " + "Centre: (" + tile_array[i,j].centre.x.ToString() + "," + tile_array[i,j].centre.z.ToString() + ")");
				
				iso_array[i,j] = new Tile(i,j,tile_array[i,j].centre.x - tile_array[i,j].centre.z ,(tile_array[i,j].centre.x + tile_array[i,j].centre.z)/2);
				iso_array[i,j].setCollision(collision_array[i,j]);
				
				if(iso_array[i,j].getCollision() == 0){
					Instantiate(center_point, new Vector3(iso_array[i,j].centre.x , 0.0f,iso_array[i,j].centre.z), Quaternion.identity);
				}
				else{
					Instantiate(center_point_blocked, new Vector3(iso_array[i,j].centre.x , 0.0f,iso_array[i,j].centre.z), Quaternion.identity);
				}
			}
		}
		
		
		
		tile_dict = new Dictionary<string, Tile>();
		//foreach(Tile aTile in tile_array){
		foreach(Tile aTile in iso_array){
			//Debug.Log(aTile.x_pos.ToString() + aTile.z_pos.ToString());
			tile_dict.Add( aTile.x_pos.ToString()+ "," + aTile.z_pos.ToString(),aTile);
		}
		

		
		
		
		//Retrive bound data for attached plane object
		//mesh = GetComponent<MeshFilter>().mesh;
		//ground = mesh.bounds;
	}
	
	// Update is called once per frame
	/*void OnDrawGizmos () {
	
		if (grid_on){
			Gizmos.color = Color.white;
			Gizmos.DrawLine(new Vector3(0.0f,0.6f,0.0f), new Vector3(8.0f,0.6f,5.0f));
			
		}
		
	}*/
	
	//Calculate the size of an individual tile
	void CalculateTileSize(){
	//	Debug.Log("Calulate Tile Size " + end_point.x.ToString());
		//tile_size.x = (end_point.x - start_point.x)/horizontal_tiles;
		//tile_size.z = (end_point.z - start_point.z)/vertical_tiles;
		tile_size.x  = 1f;
		tile_size.z = 1f;
	}
	
	public Tile getTile(int x, int z){
		
		Tile temp = null;
		
		//Debug.Log("Find TILE at pos:  " + x.ToString() + ", " +z.ToString());
		 if(tile_dict.TryGetValue( x.ToString() + "," + z.ToString(), out temp)){
			return temp;
		}
		else{
			
		return null;	
		}
		
	/*	foreach(Tile aTile in tile_array){
		
		//	Debug.Log("Search Tile: "+ aTile.x_pos.ToString() + ", " + aTile.z_pos.ToString() +"C: " + aTile.centre.ToString());
			
			if(x == aTile.x_pos && z == aTile.z_pos ){
				
				//Debug.Log("TILE FOUND  " + "Tile: "+ aTile.x_pos.ToString() + ", " + aTile.z_pos.ToString() +"C: " + aTile.centre.ToString() );
				temp = aTile;
				//Debug.Log("RETURNED TILE P: " + temp.x_pos.ToString() + temp.z_pos.ToString() + "C " + temp.centre.ToString() );
				return temp;
			
			}
		
		}
		
		return null;*/
	}
	public Tile getTile(Transform transform){
		
		Tile temp = new Tile();
		//Debug.Log("GET TILE: " + x.ToString() + ", " +z.ToString());
		foreach(Tile aTile in tile_array){
			if(transform.position == aTile.centre ){
				//Debug.Log("TILE FOUND");
				temp = aTile;
				return temp;
			}
		}
		
		return null;
	}
	/*public Tile getTile(int xpos, int zpos){
		
		Tile temp = new Tile();
		//Debug.Log("GET TILE: " + x.ToString() + ", " +z.ToString());
		foreach(Tile aTile in tile_array){
			if(xpos == aTile.x_pos && zpos == aTile.z_pos ){
				//Debug.Log("TILE FOUND");
				temp = aTile;
				return temp;
			}
		}
		
		return null;
	}*/
}
