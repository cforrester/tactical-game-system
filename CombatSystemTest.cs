using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CombatSystem;


public class CombatSystemTest : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		GameObject combatManager = new GameObject ();
		combatManager.AddComponent<CombatManager> ();
		CombatEncounter ce = new CombatEncounter ();
		ce.testInit();
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,100,200),CombatManager.playerUnitName);
		GUI.Label (new Rect (10,30,100,20),"HP: "+CombatManager.playerUnitHP.ToString());
		GUI.Label (new Rect (10,40,100,20),"MP: "+CombatManager.playerUnitHP.ToString());
		
		GUI.Box(new Rect(300,10,100,90),CombatManager.enemyUnitName);
		GUI.Label (new Rect (300,40,100,20),"HP: "+CombatManager.enemyUnitHP.ToString());		
		
		int buttonMenuHeightOffset = 30;
		
		if (GUI.Button(new Rect(10, buttonMenuHeightOffset *2, 100, buttonMenuHeightOffset), "Fight")){
			
			
		}
		
		int combatCommandButtonPosition = 3;
		foreach (CombatAbility ability in CombatManager.playerAbilities) {
			
			if (GUI.Button(new Rect(10, buttonMenuHeightOffset*combatCommandButtonPosition, 100, buttonMenuHeightOffset), ability.AbilityName)){
				
				
				
			}
			combatCommandButtonPosition++;
			
		}
		
		
	}
	
}


namespace CombatSystem{
	
	
	
	
	public class CombatUnit{
		
		public int UnitID = 0;
		public string name = "";
		public string label = "";
		public int expLevel = 0;
		public int expPoints = 0;
		public int expForNextLevel = 0;
		public bool isActive = false;
		public bool isPlayerUnit = false;
		public int hitPoints = 0;
		public int magicPoints = 0;
		public int attackPower = 0;
		public int defensePower = 0;
		public int baseToHit = 0;
		public int baseEvade = 0;
		public int condition = 0;
		public int positionX = 0;
		public int positionY = 0;
		public int turnAvailable = 0;
		public List<CombatAbility> abilities = new List<CombatAbility>();
		public string[] commands = {"Move","Attack", "Magic", "Item", "Wait"};
		
	}
	
	
	public class CombatEncounter{
		
		public void testInit(){
			// a bunch of crap for test purposes
			// battlefield
			GameObject battlefield = GameObject.CreatePrimitive (PrimitiveType.Plane);	
			battlefield.transform.position = new Vector3 (0, 0, 0);
			battlefield.transform.localScale = new Vector3 (10, 10, 10);
			GameObject battlefieldFloor = GameObject.CreatePrimitive (PrimitiveType.Cube);
			battlefieldFloor.transform.position = new Vector3 (0, 0, 0);
			battlefieldFloor.transform.localScale = new Vector3 (10, 0.2f, 10);
			Texture2D textureGrass = new Texture2D (1, 1, TextureFormat.ARGB32, false);
			textureGrass.SetPixel(0,0,Color.green);
			textureGrass.Apply ();
			battlefieldFloor.renderer.material.mainTexture = (Texture)textureGrass;
			
			// basic battlefield lighting
			GameObject battlefieldLight = new GameObject ("light");
			battlefieldLight.AddComponent<Light> ();
			battlefieldLight.light.color = Color.white;
			battlefieldLight.transform.position = new Vector3 (0, 5, 0);
			battlefieldLight.light.type = LightType.Point;
			battlefieldLight.light.intensity = 3;
			
			// setup basic player and enemy units
			GameObject testPlayerUnit = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
			testPlayerUnit.name = "testPlayer";
			GameObject testEnemyUnit = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
			Texture2D texturePlayerUnit = new Texture2D (1, 1, TextureFormat.ARGB32, false);
			texturePlayerUnit.SetPixel(0,0,Color.blue);
			texturePlayerUnit.Apply ();
			testPlayerUnit.renderer.material.mainTexture = (Texture)texturePlayerUnit;
			
			Texture2D textureEnemyUnit = new Texture2D (1, 1, TextureFormat.ARGB32, false);
			textureEnemyUnit.SetPixel(0,0,Color.red);
			textureEnemyUnit.Apply ();
			testEnemyUnit.renderer.material.mainTexture = (Texture)textureEnemyUnit;
			testPlayerUnit.transform.position = new Vector3 (0, 0.2f, 0);
			testPlayerUnit.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
			
			testEnemyUnit.transform.position = new Vector3 (1, 0.2f, 0);
			testEnemyUnit.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
			// create logical units
			CombatUnit pawn1 = new CombatUnit();
			CombatUnit enemy1 = new CombatUnit();
			pawn1.hitPoints = 10;
			pawn1.magicPoints = 5;
			pawn1.name = "PlayerUnit1";
			pawn1.attackPower = 5;
			pawn1.defensePower = 5;
			pawn1.expLevel = 1;
			pawn1.isPlayerUnit = true;
			enemy1.hitPoints = 10;
			enemy1.name = "Enemy";
			enemy1.attackPower = 2;
			enemy1.defensePower = 3;
			
			CombatAbility ability1 = new CombatAbility (1, "Special Move 1", 3);
			CombatAbility ability2 = new CombatAbility (2, "Special Move 2", 5);
			pawn1.abilities.Add (ability1);
			pawn1.abilities.Add (ability2);
			CombatManager.playerUnitHP = pawn1.hitPoints;
			CombatManager.playerUnitMP = pawn1.magicPoints;
			CombatManager.playerUnitName = pawn1.name;
			CombatManager.playerAbilities = pawn1.abilities;
			CombatManager.enemyUnitHP = enemy1.hitPoints;
			CombatManager.enemyUnitName = enemy1.name;
			
			
			
		}
		
		
	}
	
	
	public class CombatAbility{
		public int AbilityID = 0;
		public string AbilityName = "";
		public int Power = 0;
		
		public CombatAbility(int id, string name, int power){
			AbilityID = id;
			AbilityName = name;
			Power = power;
		}
	}
	
	
	public class CombatManager : MonoBehaviour{
		
		public static int playerUnitHP = 0;
		public static int playerUnitMP = 0;
		public static string playerUnitName = "";
		public static List<CombatAbility> playerAbilities = null;
		
		public static string enemyUnitName = "";
		public static int enemyUnitHP = 0;
		
		
		
	}
	
}