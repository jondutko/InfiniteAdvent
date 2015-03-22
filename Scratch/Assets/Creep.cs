using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Creep : Combatant{
	public int hp, maxhp;
	public int tag;
	public Creep(){}
	public Creep(string n, int h, int i, int wd, int st, int dx, int[] a){
		name = n;
		tag = i;
		stats = new Stats(wd, st, dx, h);
		abilities = a;
	}
	public Creep(Creep c){
		name = c.name;
		tag = c.tag;
		stats = c.stats;
		abilities = c.abilities;
	}
}