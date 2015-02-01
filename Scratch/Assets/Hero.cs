using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Hero {
	public string name;
	public RPGClass profession;

	public Hero(){}
	public Hero(string n, RPGClass p){
		name = n;
		profession = p;
	}
}
