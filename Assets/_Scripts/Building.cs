using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildType { BASE, GRANARY, MINE, LIBRARY, MARKET, TOURIST, FARMS }

public class Building {
   
	public BuildType type;
	public int foodProd;
	public int indProd;
	public int goldProd;
	public int infProd;

	public int stability;
	public int cost;
	public int upkeep;
	
	
	public static Building CreateBuilding(BuildType type) {
		Building b = new Building();
		b.type = type;
		switch (type) {
		case BuildType.BASE:
			b.foodProd = 200;
			b.indProd = 200;
			b.upkeep = 50;
			return b;
		case BuildType.GRANARY:
			b.foodProd = 100;
			b.cost = 1500;
			b.upkeep = 20;
			return b;
		case BuildType.MINE:
			b.indProd = 100;
			b.cost = 2000;
			b.upkeep = 10;
			return b;
		case BuildType.MARKET:
			b.goldProd = 150;
			b.cost = 1200;
			return b;
		case BuildType.LIBRARY:
			b.infProd = 75;
			b.cost = 2500;
			b.upkeep = 25;
			return b;
		case BuildType.TOURIST:
			b.goldProd = 100;
			b.infProd = 50;
			b.cost = 2200;
			b.upkeep = 0;
			return b;
		case BuildType.FARMS:
			b.foodProd = 200;
			b.cost = 2500;
			b.upkeep = 25;
			return b;
		default:
			return null;
		}
	}
}
