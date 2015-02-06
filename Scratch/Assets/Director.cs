using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Director : MonoBehaviour {

	public GUISkin skinny;

	enum page {start, spawn, combat};
	page state;
	int squadSize;
	List<Hero> squad;
	List<RPGClass> unlockedClasses;
	List<string> namesDirectory;

	MissionLibrary missions;

	void Start () {
		initializeNames();
		initializeClasses();
		initializeSquadSettings();
		initializeMissions();
		spawnNewSquad();

		transition(page.start);
	}

	void Update () {}

	void OnGUI(){
		GUI.skin = skinny;
		switch(state){
		case page.start:
			startPage();
			break;
		case page.spawn:
			spawnPage();
			break;
		}
	}

	//PAGE MANAGEMENT AND DISPLAY FUNCTIONS

	void transition(page p){
		state = p;
	}

	void startPage(){
		Debug.Log(GUI.skin.ToString());
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.Label(new Rect(100, 200, 500, 25), "Infinite Advent");
		if(GUI.Button(new Rect(330, 227, 40, 26), ">")){
			transition (page.spawn);
		}
	}

	void spawnPage(){
		GUI.skin.label.alignment = TextAnchor.MiddleLeft;
		GUI.BeginGroup(new Rect(10, 10, 200, 480));
		for(int i = 0; i < squad.Count; i++){
			GUI.Label(new Rect(5, 5 + (i*50), 190, 25), squad[i].name + ", "+squad[i].profession.title);
		}
		GUI.EndGroup();
		GUI.BeginGroup(new Rect(490, 5, 200, 480)); 
		GUI.Label (new Rect(10, 10, 95, 25), "Missions");
		GUI.EndGroup();
	}

	//END PAGE MANAGEMENT AND DISPLAY FUNCTIONS

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

	void initializeMissions(){
		missions = new MissionLibrary();
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
			squad.Add(new Hero(hname, hclass));
		}
	}
	//END OF GAME LOOP FUNCTIONS
}
