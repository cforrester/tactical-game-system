using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public const int LevelMaxSizeSquare = 100;
	static private int[,] mData;
	static private Vector3 mMapCenter;
	

	// Use this for initialization
	void Start () {
		mData = new int[LevelMaxSizeSquare,LevelMaxSizeSquare];
		Read (); 
		//Debug.Log (this.ToString ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Read level based on what assets are loaded
	void Read () {
	
		CalculateCenter ();
	
		for (int i = 0; i < LevelMaxSizeSquare; i++)
		{
			System.Console.Write ("\n");
			for (int j = 0; j < LevelMaxSizeSquare; j++)
			{
				RaycastHit hit;
				bool isHit = Physics.Raycast(new Vector3( j , 10 , i ) , Vector3.down , out hit , 15.0f   );
				
				if(isHit && hit.transform.gameObject != null)
				{
					// Todo, maybe eventually a switch case of some k
					if(hit.transform.gameObject)
					{
						if(hit.transform.gameObject.name == "BOARD")
						{
							mData[j,i] = 1;
						}
					}
				}
			}
		}
		
		
	}
	
	public static Vector3 GetMapCenter()
	{
		return mMapCenter;
	}
	
	// Represent level as a string. Level will be draw according to the starting facing -z.
	//     z| 
	//  -x  | +x
	//    -z| 
	public override string ToString() {
	
		string s = "";
		for (int i = LevelMaxSizeSquare-1; i >= 0; i--)
		{
			s += "\n";
			for (int j = 0; j < LevelMaxSizeSquare; j++)
			{
				s += mData[j,i].ToString ();
			}
		}
		
		return s;
	}
	
	private void CalculateCenter(){
		GameObject board = GameObject.Find ("BOARD");
		mMapCenter = board.collider.bounds.center;
	}
}
