using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stats{
	//base values and modifiers
	public int wd, wd_mod, st, st_mod, dx, dx_mod, maxhp, maxhp_mod, hp;
	public bool alive;

	public Stats(){}
	public Stats(int w, int s, int d, int mh){
		wd = w;
		wd_mod = w;
		st = s;
		st_mod = s;
		dx = d;
		dx_mod = d;
		maxhp = mh;
		maxhp_mod = mh;
		hp = mh;
		alive = true;
	}
}