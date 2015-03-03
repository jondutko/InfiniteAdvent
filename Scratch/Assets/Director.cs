using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Director : MonoBehaviour {

	public GUISkin skinny;

	public Texture blank;

	public CombatDirector currentCombat;

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
		case page.combat:
			combatPage();
			break;
		}
	}

	//PAGE MANAGEMENT AND DISPLAY FUNCTIONS

	void transition(page p){
		state = p;
	}

	void startPage(){
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
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.BeginGroup(new Rect(490, 5, 200, 480)); 
		GUI.Label (new Rect(10, 10, 150, 25), "Missions");
		for(int j = 0; j < missions.library.Count; j++){
			if(GUI.Button(new Rect(10, 40, 150, 25), missions.library[j].name)){
				primeCurrentMission(missions.library[j]);
				transition(page.combat);
			}
		}
		GUI.EndGroup();
	}

	void combatPage(){
		switch(currentCombat.phase){
		case CombatDirector.combatPhase.intro:
			GUI.Label(new Rect(10, 200, 690, 25), currentCombat.activeMission.name);
			if(GUI.Button (new Rect(325, 230, 50, 25), "Go!")){
				currentCombat.phase = CombatDirector.combatPhase.setup;
			}
			break;
		case CombatDirector.combatPhase.setup:
			GUI.Label (new Rect(10, 10, 690, 25), "Who will first enter the fray?");
			GUI.skin.button.alignment = TextAnchor.MiddleCenter;
			for(int i = 0 ; i < squad.Count; i++){
				if(GUI.Button(new Rect(10 + 10*(i) + 100*(i), 50, 100, 100), squad[i].name + "\n" + squad[i].profession.title)){
					currentCombat.active = squad[i];
					currentCombat.phase = CombatDirector.combatPhase.playerturn;
					currentCombat.log += squad[i].name + " enters the fray!\n";
				}
			}
			break;
		case CombatDirector.combatPhase.playerturn:
			combatLayout();
			break;
		case CombatDirector.combatPhase.enemyturn:
			combatLayout();
			break;
		}
	}

	void combatLayout(){
		//Combat Log
		GUI.Box(new Rect(555, 5, 140, 490), " ");
		GUI.Label(new Rect(560, 10, 130, 25), "Combat Log");

		GUI.skin.label.alignment = TextAnchor.LowerLeft;
		GUI.skin.label.fontSize = 10;
		GUI.Label(new Rect(560, 40, 130, 450), currentCombat.log);

		//Title Box
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.skin.label.fontSize = 15;
		GUI.Box(new Rect(5, 5, 540, 30), " ");
		GUI.Label(new Rect(5, 5, 540, 30), currentCombat.activeMission.name);

		//Player Tab
		GUI.Box(new Rect(5, 40, 540, 250), " ");

		//Creep Tab
		GUI.Box(new Rect(5, 300, 540, 190), " ");
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
	void primeCurrentMission(Mission m){
		currentCombat = new CombatDirector(m);
	}

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
