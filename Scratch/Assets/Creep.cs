using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Creep {
	public string name;
	public int hp, maxhp;
	public int tag;
	public Creep(){}
	public Creep(string n, int h, int i){
		name = n;
		hp = h;
		maxhp = h;
		tag = i;
	};
}