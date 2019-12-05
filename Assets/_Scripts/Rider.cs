using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rider : MonoBehaviour {

	public Node start; //where it gets picked up
	public Node goal; // any valid destination that != start
	public float costToPickup;


	public float GetPayment(){
		float fare = Vector3.Distance(start.transform.position,goal.transform.position);
		fare -= costToPickup;
		return Mathf.Round(fare * 100)/100;
	}

	public void SetPickupCost(float cost){
		costToPickup = cost/4;
	}
}
