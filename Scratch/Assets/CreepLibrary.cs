using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CreepLibrary {
	public Dictionary<int, Creep> library;

	public CreepLibrary(){
		library = new Dictionary<int, Creep>();
		populateLibrary();
	}

	public void populateLibrary(){
		getGoblins();
	}

	public void getGoblins(){
		library.Add(0, new Creep("Goblin Grunt", 6, 0));
		library.Add(1, new Creep("Goblin Shriek", 7, 1));
		library.Add(2, new Creep("Goblin Thunk", 9, 2));
	}
}