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
		library.Add(0, new Creep("Goblin Grunt", 6, 0, 2, 4, 5, new int[]{3}));
		library.Add(1, new Creep("Goblin Shriek", 7, 1, 5, 3, 4, new int[]{1}));
		library.Add(2, new Creep("Goblin Thunk", 9, 2, 2, 7, 3, new int[]{2}));
	}
}