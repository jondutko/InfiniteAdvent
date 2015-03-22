using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Hero : Combatant{
	public RPGClass profession;
	public Hero(){}
	public Hero(string n, RPGClass p){
		name = n;
		profession = p;
		stats = new Stats(p.wd, p.st, p.dx, 10);
		abilities = p.abilities;
	}
}
