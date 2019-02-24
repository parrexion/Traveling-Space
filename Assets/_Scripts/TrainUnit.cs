using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainUnit {

	public UnitType type;
	public int cost;


	public int ProduceAmount(int prod) {
		return Mathf.Max(1, cost / prod);
	}

}
