using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HardwareType { STORAGE1, STORAGE2, PROCESSOR1, PROCESSOR2, GRAPHICS1, GRAPHICS2, RAM1, RAM2}

[System.Serializable]
public class Hardware {

	public HardwareType type;
	public int dataProd;
	public int processProd;
	public int graphicProd;
	public int evolveProd;
	public int stabilityProd;


	public static Hardware CreateHardwareRandom() {
		HardwareType type = (HardwareType)Random.Range(0, 8);
		return CreateHardware(type);
	}

	public static Hardware CreateHardware(HardwareType type) {
		switch (type) {
		case HardwareType.STORAGE1:
			return new Hardware(){
				type = type,
				dataProd = 80,
				processProd = 30,
				graphicProd = 40,
				evolveProd = 20,
				stabilityProd = 0
			};
		
		case HardwareType.STORAGE2:
			return new Hardware(){
				type = type,
				dataProd = 60,
				processProd = 20,
				graphicProd = 60,
				evolveProd = 30,
				stabilityProd = 1
			};
		
		case HardwareType.PROCESSOR1:
			return new Hardware(){
				type = type,
				dataProd = 30,
				processProd = 80,
				graphicProd = 40,
				evolveProd = 20,
				stabilityProd = 0
			};
		
		case HardwareType.PROCESSOR2:
			return new Hardware(){
				type = type,
				dataProd = 10,
				processProd = 100,
				graphicProd = 30,
				evolveProd = 10,
				stabilityProd = -3
			};
		
		case HardwareType.GRAPHICS1:
			return new Hardware(){
				type = type,
				dataProd = 50,
				processProd = 10,
				graphicProd = 60,
				evolveProd = 10,
				stabilityProd = -1
			};
		
		case HardwareType.GRAPHICS2:
			return new Hardware(){
				type = type,
				dataProd = 20,
				processProd = 50,
				graphicProd = 120,
				evolveProd = 0,
				stabilityProd = 2
			};
		
		case HardwareType.RAM1:
			return new Hardware(){
				type = type,
				dataProd = 40,
				processProd = 10,
				graphicProd = 30,
				evolveProd = 100,
				stabilityProd = 0
			};
		
		case HardwareType.RAM2:
			return new Hardware(){
				type = type,
				dataProd = 20,
				processProd = 30,
				graphicProd = 80,
				evolveProd = 50,
				stabilityProd = 0
			};
		
		default:
			return null;
		}
		
	}

}
