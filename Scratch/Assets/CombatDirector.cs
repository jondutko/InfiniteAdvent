using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CombatDirector {
	public Hero active;
	public List<Hero> bench;
	public Creep activeEnemy;
	public List<Creep> benchEnemy;

	public Mission activeMission;

	public bool benching;

	public string log;

	public enum combatPhase{
		intro,
		setup,
		playerturn,
		enemyturn
	};

	public combatPhase phase;

	public CombatDirector(){}
	public CombatDirector(Mission m, CreepLibrary lib){
		activeMission = m;
		phase = combatPhase.intro;
		log = m.name+" begin!\n";
		benching = false;

		activeEnemy = new Creep(lib.library[m.creeps[0]]);

		log += activeEnemy.name + " appears. \n";

		benchEnemy = new List<Creep>();
		for(int i = 1; i < m.creeps.Length; i++){
			benchEnemy.Add(new Creep(lib.library[m.creeps[i]]));
		}
	}

	//catchall for using abilities
	//each class has a tag for the abilities it has
	//we do all the combat from here
	//we should also do cleanup at the end of this method
	//ideally this is just a directory method that leads
	//to a bunch of auxiliary private methods
	//ie tag 1 goes to a discrete fireball function
	public void abilityDirectory(int tag){
		Combatant a, b;
		if(phase == combatPhase.playerturn){
			a = active;
			b = activeEnemy;
		}
		else{
			a = activeEnemy;
			b = active;
		}
		switch(tag){
		case 1:
			fireball(a, b);
			break;
		case 2:
			bludgeon(a, b);
			break;
		case 3:
			backstab(a, b);
			break;
		}
		turnCleanup();
	}

	private void turnCleanup(){
		deathChecks();
		switchTurns();
	}

	//covers: checking for death conditions (hp <= 0)
	//        if dead: if all dead, end mission in vic/def
	//                 else switch
	private void deathChecks(){
		if(active.stats.hp <= 0){
			active.stats.alive = false;
			log += active.name + " has died!\n";
			if(bench.Exists(h => h.stats.alive)){
				log += "Who will take his place?\n";
				benching = true;
			}
			else{
				lossPhase();
			}
		}
		if(activeEnemy.stats.hp <= 0){
			activeEnemy.stats.alive = false;
			log += activeEnemy.name + " has died!\n";
			if(benchEnemy.Exists(c => c.stats.alive)){
				activeEnemy = benchEnemy.Find(c => c.stats.alive);
			}
			else{
				winPhase();
			}
		}
	}

	private void lossPhase(){
		log += "You have failed this mission.\n";
	}

	private void winPhase(){
		log += "You have completed this mission.\n";
	}

	private void switchTurns(){
		if(phase == combatPhase.playerturn){
			phase = combatPhase.enemyturn;
			aiTurn();
		}
		else{
			phase = combatPhase.playerturn;
		}
	}

	public void aiTurn(){
		//this will one day be more comprehensive
		//i seriously promise
		//because this is a pretty filthy shortcut
		abilityDirectory(activeEnemy.abilities[0]);
	}

	public string getAbilityName(int tag){
		switch(tag){
		case 1:
			return "Fireball";
		case 2:
			return "Bludgeon";
		case 3:
			return "Backstab";
		}
		return "noname!!";
	}

	public string getAbilityDescription(int tag){
		return "hahah not yet";
	}

	private void fireball(Combatant a, Combatant b){
		log += a.name + " casts Fireball!\n";
		int damage = Random.Range(a.stats.wd_mod - 2, a.stats.wd_mod + 1) / 2;
		b.stats.hp -= damage;
		log += b.name + " is hit for " + damage + " hp!\n";
	}
	private void bludgeon(Combatant a, Combatant b){
		log += a.name + " uses Bludgeon!\n";
		int damage = Random.Range(a.stats.st_mod - 2, a.stats.st_mod + 1) / 2;
		b.stats.hp -= damage;
		log += b.name + " is hit for " + damage + " hp!\n";
	}
	private void backstab(Combatant a, Combatant b){
		log += a.name + " uses Backstab!\n";
		int damage = Random.Range(a.stats.dx_mod - 2, a.stats.dx_mod + 1) / 2;
		b.stats.hp -= damage;
		log += b.name + " is hit for " + damage + " hp!\n";
	}
}
