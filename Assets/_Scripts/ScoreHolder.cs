using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHolder : MonoBehaviour {
	public static ScoreHolder instance;

	public float score;

    void Awake () {
		if(instance == null){
			DontDestroyOnLoad(gameObject);
			instance = this;
		} else if(instance != this) {
			Destroy(gameObject);
		}
	}
	
}
