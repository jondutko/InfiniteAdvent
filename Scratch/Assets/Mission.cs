using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Mission {
	public string name;
	public int tag;
	public int[] creeps;

	public Mission(){}
	public Mission(string n, int t, int[] c){
		name = n;
		tag = t;
		creeps = c;
	}
}