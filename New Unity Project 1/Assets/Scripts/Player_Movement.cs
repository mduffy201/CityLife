using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player_Movement : MonoBehaviour
{
	
	
	
	public Vector3 move_to;
	private float moveSpeed = 3f;
	private float gridSize = 1f;
	public Vector2 input;
	private bool isMoving = false;
	public float character_offset = 0.0f;
	
	GameObject ground;
	Grid levelGrid;
	public int xMoves;
	public int zMoves;
	Tile current_tile;
	Tile destination_tile;
	
	void Start ()
	{
		ground = GameObject.FindGameObjectWithTag ("Ground"); //Find plane representing the ground
		levelGrid = (Grid)ground.GetComponent (typeof(Grid)); //Get the levelGrid
		transform.position = levelGrid.iso_array [19, 19].centre; //Set starting position
		current_tile = levelGrid.iso_array [19, 19]; //Get the current tile
		
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
	
		//astarPath();
		//	Debug.Log("=========================================NEW MOVE!!!================================");
			//Debug.Log ("Current Tile: " + "[" + current_tile.x_pos + "," + current_tile.z_pos +"] " + "C " + current_tile.centre.ToString());
			//Cast ray from camera to mouse pointer
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			
			//If ray hits plane object
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				
				move_to = new Vector3 (hit.point.x, 0, hit.point.z);
				
				//Create temp tile
				Tile tempTile = new Tile ();
				int i = 0;
				
				//Check each tile in array
				foreach (Tile aTile in levelGrid.iso_array) {
				//foreach (Tile aTile in levelGrid.tile_array) {
	
					//Calculate distances
					float distance = Vector3.Distance (move_to, aTile.centre);
					float tempDistance = Vector3.Distance (tempTile.centre, move_to);
					
					i++;
					
					//if movex closer to aTile centre than tempTile AND movez closer to aTile centre than tempTile
					//then save aTile as tempTile
					if (distance < tempDistance) {
						/*Debug.Log ("Tile " + i + " " + aTile.x_pos.ToString () + 
						" " + aTile.z_pos.ToString () + " Distance from click " + distance.ToString () + 
							"Temp distance : " + tempDistance.ToString ());*/
						
						tempTile = aTile;			
					}		
				}
				//Check closest tile
				Debug.Log("Closest Tile: " + "[" + tempTile.x_pos + "," + tempTile.z_pos + "]");
				destination_tile = tempTile;
				
				//Calculate distance to target
				//xMoves = destination_tile.x_pos - current_tile.x_pos;
				//zMoves = destination_tile.z_pos - current_tile.z_pos;	
				//Debug.Log("MOVES: x = " + xMoves.ToString() + " z = " + zMoves.ToString());
			}
			int a = 0;

			
			if (current_tile != destination_tile) {
				a++;
				//Debug.Log(a.ToString());
				//FindPath
				//Send path to move
				//List<Vector3> path = FindPath (xMoves, zMoves);
				StartCoroutine (move (transform, astarPath(destination_tile)));
				
			}	
			current_tile = levelGrid.getTile(current_tile.x_pos + xMoves, current_tile.z_pos + zMoves);
		}
	}
	
	//PATH FIND METHOD With mouse input
	/*private List<Vector3> FindPath (int x_move, int z_move)
	{
		//Debug.Log("MOVES IN PATH LOOP: x = " + x_move.ToString() + " z = " + z_move.ToString());
		List<Vector3> path = new List<Vector3> ();

		int moves_to_complete = Mathf.Abs(x_move) + Mathf.Abs(z_move);
		Tile pathTile = current_tile;
		int xm = 0;
		int zm = 0;
		
	//ARRAY FINE
		
		//Add path to vector list
		for (int i = 0; i < moves_to_complete; i++) {
			if (x_move > 0) {
				xm++;
				pathTile = moveRight (pathTile);
				
				//Debug.Log("path Tile: " + pathTile.x_pos + "," + pathTile.z_pos + " Centre: " + pathTile.centre.ToString());
				path.Add (pathTile.centre);

				
				x_move--;
			} else if (z_move > 0) {
				zm++;
				pathTile = moveUp (pathTile);
				path.Add (pathTile.centre);
				z_move--;
			}
			else if (x_move < 0) {
				xm++;
				pathTile = moveLeft (pathTile);
				//Debug.Log("path Tile: " + pathTile.x_pos + "," + pathTile.z_pos + " Centre: " + pathTile.centre.ToString());
				path.Add (pathTile.centre);
				x_move++;
			} else if (z_move < 0) {
				zm++;
				pathTile = moveDown (pathTile);
				path.Add (pathTile.centre);
				z_move++;
			}
		}
		return path;
	}*/
	
	//FROM c# GridMove.cs (http://wiki.unity3d.com/index.php?title=GridMove)
	public IEnumerator move (Transform transform, List<Vector3> path)
	{
		Vector3 startPosition;
		Vector3 endPosition;
		//Start moving
		isMoving = true;							
		startPosition = current_tile.centre;			//Current tile position
		float t = 0;

		//for each vector3 in list move to centre
		foreach (Vector3 v in path) {
			
			//Debug.Log("MOVE LOOP_> " + v.ToString() );
			t = 0;
			startPosition = transform.position;
			endPosition = v;  
			
			while (transform.position != endPosition) {                                 
				t += Time.deltaTime * (moveSpeed / gridSize);
				transform.position = Vector3.Lerp (startPosition, endPosition, t);

				yield return null;
			}
		}
			
		isMoving = false;
		yield return 0;
	}
	
	/*Tile moveUp (Tile pathTile)
	{
		//current_tile.z_pos++;
		//pathTile.z_pos++;
		pathTile = levelGrid.getTile (pathTile.x_pos, pathTile.z_pos + 1);
		return pathTile;
	}

	Tile moveDown (Tile pathTile)
	{
		//current_tile.z_pos--;
		//pathTile.z_pos--;
		pathTile = levelGrid.getTile (pathTile.x_pos, pathTile.z_pos - 1);
		return pathTile;
	}

	Tile moveLeft (Tile pathTile)
	{
		//Debug.Log("PT" + pathTile.x_pos.ToString());
		pathTile = levelGrid.getTile (pathTile.x_pos - 1, pathTile.z_pos);
//Debug.Log("PT RT " + pathTile.centre.ToString());
		return pathTile;
	}

	Tile moveRight (Tile pathTile)
	{
	
		//Debug.Log("PATH TILE AT MR: " + pathTile.x_pos.ToString() + ", " + pathTile.z_pos.ToString());
		//Debug.Log("PATH TILE AT MR: " + pathTile.x_pos.ToString() + ", " + pathTile.z_pos.ToString());
		pathTile = levelGrid.getTile (pathTile.x_pos + 1, pathTile.z_pos);
	
		return pathTile;
	}*/
	
	public List<Vector3> astarPath(Tile endIn){
		
		List<Vector3> path = new List<Vector3>();;
		
		//Tile start= new Tile();
		Tile start = current_tile; //start tile in
		
		//Tile end = new Tile();
		Tile end = endIn;  //levelGrid.getTile(10,10); //target tile in
		
		Tile current_check_tile = new Tile();  
		
		List<Tile> open_list = new List<Tile>();
		List<Tile> closed_list = new List<Tile>();
		
		
		int g_score = 0; 				// Cost from start along best known path.
        int h_score; 					//heuristic score
        int f_score; 					// Estimated total cost from start to goal through y.
		 
		start.setGScore(g_score);
		h_score = Mathf.Abs(end.x_pos - start.x_pos) + Mathf.Abs(end.z_pos - start.z_pos); 
		start.setFScore(g_score + h_score);
		
		Debug.Log("START TILE SCORE: G: " + start.getGScore().ToString() + ", F: " + start.getFScore().ToString());
		
		int loop_counter = 0;
		
		open_list.Add(start); 					//Add start tile to open list
		
		//while open list is not empty
		while(open_list.Count > 0){
			
			Debug.Log("Loop counter: " + loop_counter.ToString());
			loop_counter++;
			
			if(loop_counter == 30)
				break;
			
			current_check_tile = getBestTile(open_list);			 //Get tile with lowest f value	
			//Debug.Log("BEST TILE: " + current_check_tile.x_pos.ToString() + "," + current_check_tile.z_pos.ToString());
			
			//if the current_tile in the target
			if(current_check_tile.centre == end.centre){
				 Debug.Log("Goal reached");
				closed_list.Add(current_check_tile);
           		path = ConstructPath(closed_list);		//Calculate the path
          		  break;							//exit while loop
			}
			
			
			open_list.Remove(current_check_tile);		//remove current_tile from open list
			closed_list.Add(current_check_tile);		//add current_tile to closed list
			
			Debug.Log("CURRENT TILE: " + current_check_tile.x_pos.ToString() + "," + current_check_tile.z_pos.ToString());
			//get tiles connected to current_tile
			
			List<Tile> neighbors = getNeighbours(current_check_tile);
			foreach(Tile nt in neighbors){
			//Debug.Log("NEIGHBOR TILE SCORE: G: " + nt.getGScore().ToString() + ", F: " + nt.getFScore().ToString());
			
			}
			foreach(Tile nt in neighbors){
				Debug.Log("NEIBOUR X: " + nt.x_pos.ToString() + " Z: " + nt.z_pos.ToString());
				
			
				
				if(closed_list.Contains(nt) || nt.getCollision() == 1)		//if tile is on closed list, move to next tile
					continue;
				
				
				//G score of current tile
				g_score = current_check_tile.getGScore() + 1;
				//heuristic sore of current neighbor tile
				h_score = Mathf.Abs(end.x_pos - nt.x_pos) + Mathf.Abs(end.z_pos - nt.z_pos); 
				//total movement score of current neighbor tile
				f_score = g_score + h_score;
				
				
			
				
				//if tile not already on open list OR f score of current n tile 
				if(!open_list.Contains(nt) ||  f_score < nt.getFScore()){
					//Set scores for neighbour tiles
					nt.setParent(current_check_tile);
					nt.setGScore(g_score);
              		nt.setFScore(f_score);
                	
					//If current neighbour tile is not on open list, add
                    if(!open_list.Contains(nt)){
						
                    	open_list.Add(nt);
                	}
				}
			}
		}
		return path;
	}
	//private List<Vector3> FindPath(){}
	public Tile getBestTile(List<Tile> open_list){
		
		foreach(Tile t in open_list){
		Debug.Log("OPENLIST TILE SCORE:" + " [" + t.x_pos.ToString() + ","+ t.z_pos.ToString() + "]" + " G: " + t.getGScore().ToString() + ", F: " + t.getFScore().ToString());
		}
		//List<Tile> sortedList =open_list.Sort( 
		//open_list.Sort();
		Tile temp = new Tile();
		//temp.setFScore(1000);
		//Debug.Log(temp.getFScore());
		//temp = open_list.
		foreach(Tile t in open_list){
			Debug.Log("Get best tile from open list -> Tile: " + t.x_pos.ToString() +"," + t.z_pos.ToString() + " f score: " + t.getFScore().ToString());
			
			if(t.getFScore() <= temp.getFScore() || temp.getFScore() == 0 ){
				// 
				
				temp = t;
			}
			
		}
		Debug.Log("RETURNED BEST: " + temp.x_pos.ToString() + "," + temp.z_pos.ToString());
	//return levelGrid.getTile(8,8);
		return temp;
	}
	public List<Tile> getNeighbours(Tile tile){
		//get tile
		List<Tile> neighbours = new List<Tile>();
		
		int xpos = tile.x_pos;
		int zpos = tile.z_pos;
		
		neighbours.Add(levelGrid.getTile( (xpos + 1), zpos));
		neighbours.Add(levelGrid.getTile( (xpos - 1), zpos));
		neighbours.Add(levelGrid.getTile(xpos, (zpos +1) ));
		neighbours.Add(levelGrid.getTile(xpos , (zpos - 1)));
		
		return neighbours;
		
	}
	public List<Vector3> ConstructPath(List<Tile> closed_list){
		List<Vector3> path = new List<Vector3> ();
		
		foreach(Tile t in closed_list){
			Debug.Log("PATH X: " + t.x_pos.ToString() + " Z: " + t.z_pos.ToString());
			path.Add(t.centre);
		}
		
		return path;
		
	}
	
	
}
