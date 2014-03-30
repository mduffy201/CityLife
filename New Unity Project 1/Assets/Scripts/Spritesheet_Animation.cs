using UnityEngine;
using System.Collections;

public class Spritesheet_Animation : MonoBehaviour
{
	public Texture walkRight;
	public Texture walkLeft;
	//public Texture jumpRight;
	//public Texture jumpLeft;
	private int rows;
	private int columns;
	public int walkColumns = 5;
	public int walkRows = 6;
//	public int jumpColumns = 5;
//	public int jumpRows = 4;
	public float jumpFPS = 10.0f;
	public float FPS;
	private int index = 0;
	private bool left;
	private bool right ;
	private bool run = false;
	//private bool jump = false;
//	private bool fall = false;
	
	public void still ()
	{
		renderer.material.SetTextureOffset ("_MainTex", new Vector2 (1f, 1f));
		renderer.material.SetTextureScale ("_MainTex", new Vector2 (1f / walkColumns, 1f / walkRows));
		if (left) {
			this.renderer.material.mainTexture = walkLeft;
		} else {
			this.renderer.material.mainTexture = walkRight;
		}
	}


	//=========================================RUN
	public void StartRunRight ()
	{
		//if (!jump) {
			columns = walkColumns;
			rows = walkRows;
			left = false;
			right = true;
			//jump = false;
		
			this.renderer.material.mainTexture = walkRight;
			run = true;
			index = 0;
			StartCoroutine (AnimateRun ());	
		//}
	}

	public void StartRunLeft ()
	{
		//if (!jump) {
			columns = walkColumns;
			rows = walkRows;
			right = false;
			left = true;
			//jump = false;
		
			this.renderer.material.mainTexture = walkLeft;
			run = true;
			index = rows * columns;
			StartCoroutine (AnimateRun ());	
		//}
	}

	public void StopRun ()
	{
		run = false;
	}

	private IEnumerator AnimateRun ()
	{
		while (run) {
			
			
			Vector2 size;
			if (left) {
				size = new Vector2 (1f / columns, 1f / rows);
			} else {
				size = new Vector2 (1f / columns, 1f / rows);
			}
			renderer.material.SetTextureScale ("_MainTex", size);
			
			if (index >= rows * columns) {
				index = 0;
			}
				
			float uIndex = index % columns;
			float vIndex = index / columns;
			int columnStart = 0;
			int rowStart = 0;
			
			float offsetX = (uIndex + columnStart) * size.x;
			float offsetY = (1.0f - size.y) - (vIndex + rowStart) * size.y;
					
			//split into x and y indexes
			if (right || left) {
				index++;
				Vector2 offset = new Vector2 (offsetX, offsetY);
	
				renderer.material.SetTextureOffset ("_MainTex", offset);
			} 

			yield return new WaitForSeconds(1f / jumpFPS);
		}			
	}		
}
