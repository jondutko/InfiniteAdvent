using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MissionLibrary {
	public Dictionary<int, Mission> library;
	public MissionLibrary(){
		library = new Dictionary<int, Mission>();
		populateLibrary();
	}
	public void populateLibrary(){
		library.Add(0, new Mission("Goblin Mayhem", 0, new int[] {0, 1, 2}));
	}
}