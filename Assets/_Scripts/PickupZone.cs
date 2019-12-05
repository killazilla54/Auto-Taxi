using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupZone : MonoBehaviour {

	public GameObject zone;

	void Start(){
		zone = transform.GetChild(0).gameObject;
		DeactivateZone();
	}

	public void ActivateZone(){
		zone.SetActive(true);
	}

	public void DeactivateZone(){
		zone.SetActive(false);
	}
}
