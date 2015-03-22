using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPGClass{
	public string title;
	public int[] abilities;
	public int key;
	public int wd, st, dx;
	public RPGClass(){}
	public RPGClass(string t, int k, int w, int s, int d, int[] a){
		title = t; 
		key = k;
		wd = w;
		st = s;
		dx = d;
		abilities = a;
	}
}