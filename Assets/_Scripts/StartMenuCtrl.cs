using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuCtrl : MonoBehaviour {

	public void OnPlayBtn() {
		SceneManager.LoadScene("Taxi");
	}

	public void ToCredits() {
		SceneManager.LoadScene("Credits");
	}

    public void QuitApp() {
        Application.Quit();
    }
}
