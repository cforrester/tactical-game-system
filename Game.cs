using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	private static Game instance;
	private static GameObject container;
	
	public static Game GetInstance()
	{
		if(!instance)
		{
			container = new GameObject();
			container.name = "Game";
			instance = container.AddComponent (typeof(Game)) as Game;
		}
		return instance;
		
	}
}