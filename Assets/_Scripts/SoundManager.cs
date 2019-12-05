using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource music;
	public AudioSource sfx;
	public AudioClip cash;
	public AudioClip carDoor;

	void Start(){
		music.Play();
	}

	public void ChaChing(){
		sfx.PlayOneShot(cash);
	}

	public void EnterCar(){
		sfx.PlayOneShot(carDoor);
	}
}
