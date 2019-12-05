using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverCtrl : MonoBehaviour {

	public Text scoreText;

	void Start(){
		float score = ScoreHolder.instance.score;
		// score = Mathf.Round(score * 100)/100; 
		scoreText.text = "Today's Take: $" + score;
	}

	public void PlayAgain(){
		SceneManager.LoadScene("Taxi");
	}

	public void MainMenu(){
		SceneManager.LoadScene("Start Menu");
	}

    public void QuitApp() {
        Application.Quit();
    }
}
