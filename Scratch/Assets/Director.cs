using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Director : MonoBehaviour {

	int squadSize;
	List<Hero> squad;
	List<RPGClass> unlockedClasses;
	List<string> namesDirectory;
	void Start () {
		initializeNames();
		initializeClasses();
		initializeSquadSettings();
	}

	void Update () {}

	void OnGUI(){
		GUI.BeginGroup(new Rect(15, 15, 150, 500));
		GUI.Label(new Rect(0, 0, 150, 25), "Hello world!");
		GUI.EndGroup();
	}

	//INITIALIZATION FUNCTIONS

	void initializeNames(){
		namesDirectory = new List<string>();
		StreamReader n = new StreamReader("Assets/names.txt");
		string l;
		do{
			l = n.ReadLine();
			if(l!=null)
				namesDirectory.Add(l);
		}while(l!=null);
	}

	void initializeClasses(){
		unlockedClasses = new List<RPGClass>();
		unlockedClasses.Add(new RPGClass("Wizard"));
		unlockedClasses.Add(new RPGClass("Knight"));
		unlockedClasses.Add(new RPGClass("Rogue"));
	}

	void initializeSquadSettings(){
		squadSize = 2;
	}

	//END OF INITIALIZATION FUNCTIONS

	//GAME LOOP FUNCTIONS
	void spawnNewSquad(){
		squad = new List<Hero>();
		for(int i = 0; i < squadSize; i++){
			string hname = namesDirectory[Random.Range(0, namesDirectory.Count)];
			RPGClass hclass = unlockedClasses[Random.Range(0, unlockedClasses.Count)];
			Debug.Log(hname+","+hclass.title);
			squad.Add(new Hero());
		}
	}
	//END OF GAME LOOP FUNCTIONS
}
