using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public float MovementSpeed = 20.0f;
	public float ZoomSpeed = 5.0f;
	public float RotationSpeed = 2000.0f;
	public GameObject debugSphere = null;
	
	
	private GameObject oldChar = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		float transX = Input.GetAxis("Horizontal")*MovementSpeed*Time.deltaTime;
		float transZ = Input.GetAxis("Vertical")*MovementSpeed*Time.deltaTime;
		float transZLocal = Input.GetAxis ("Mouse ScrollWheel")*ZoomSpeed*Time.deltaTime*500.0f;
		
		// Poll our mouse to see if we need to rotate - Not finished
		// Todo - need to spin around map's center
		float rotY = 0.0f;
		if (Input.GetMouseButton(2) )
		{ 
			rotY += Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime;	
			this.transform.RotateAround (  Level.GetMapCenter (), new Vector3(0,1,0) , rotY );
		}
		
		// Translate our x,y position
		Vector3 f = this.transform.forward; f.y = 0.0f; // Null out our Y so we don't translate at an angle
		this.transform.position += f * transZ;
		this.transform.position += this.transform.right * transX;
		
		// Zoom in or out along our z
		this.transform.Translate (new Vector3(0.0f, 0.0f, transZLocal ) , Space.Self );
		
		HandleMouseSelect ();
	
	}
	
	void HandleMouseSelect()
	{	
	
		RaycastHit hitz;
		if(Physics.Raycast(this.camera.ScreenPointToRay(Input.mousePosition), out hitz))
		{
			int gridx = (int)Mathf.Round(hitz.point.x);
			int gridz = (int)Mathf.Round(hitz.point.z);
			
			GameObject highlight = GameObject.Find("HighlightProjector");
			highlight.transform.position = new Vector3( gridx , 8 , gridz );
		}
		
		if(Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			if(Physics.Raycast(this.camera.ScreenPointToRay(Input.mousePosition), out hit))
			{
			
				if( hit.transform.gameObject )
				{
					if( hit.transform.gameObject.name == "SCENERY" )
					{
						return;
					}
				}
				int gridx = (int)Mathf.Round(hit.point.x);
				int gridz = (int)Mathf.Round(hit.point.z);
				Destroy (oldChar);
				
				// We need to find out what the center height is
				RaycastHit heightHit;
				Physics.Raycast(new Vector3( gridx , 10 , gridz ) , Vector3.down , out heightHit );
				
				oldChar = (GameObject)Instantiate ( debugSphere , new Vector3( gridx , heightHit.point.y+0.47f , gridz ) , Quaternion.identity );
				oldChar.transform.Rotate (new Vector3(-90, 180, 0 ) ); // Orient our character properly
				
			}
			else
			{
				// MISS
			}
		}
		
	}
}
