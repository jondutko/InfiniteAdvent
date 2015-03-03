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

	public string log;

	public enum combatPhase{
		intro,
		setup,
		playerturn,
		enemyturn
	};

	public combatPhase phase;

	public CombatDirector(){}
	public CombatDirector(Mission m){
		activeMission = m;
		phase = combatPhase.intro;
		log = m.name+" begin!\n";
	}
}
