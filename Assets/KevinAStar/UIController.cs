using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	public ServiceOperator servController;
	public Text destText;
	public Text scoreText;
	public Text timerText;

	public GameObject riderPanel1;
	public Button btn1;
	public Text rider1Pickup;
	public Text rider1Dest;
	public Text rider1Cost;
	public GameObject riderPanel2;
	public Button btn2;
	public Text rider2Pickup;
	public Text rider2Dest;
	public Text rider2Cost;
	public GameObject riderPanel3;
	public Button btn3;
	public Text rider3Pickup;
	public Text rider3Dest;
	public Text rider3Cost;

	public Text GameOverText;
	void Start () {
		btn1.onClick.AddListener(delegate {ButtonClick(0); });
		btn2.onClick.AddListener(delegate {ButtonClick(1); });
		btn3.onClick.AddListener(delegate {ButtonClick(2); });
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "$"+servController.score;
		float timer = servController.timer;
		string minutes = Mathf.Floor(timer/60).ToString("00");
		string seconds = (timer % 60).ToString("00");
		timerText.text = minutes+":"+seconds;
	}

	public void SetDestination(string name){
		destText.text = "Dropoff at  " + name; 
	}

	public void SetPickup(string name){
		destText.text = "Pickup at " + name; 
	}

	public void ShowRiderSelection(){
		destText.text = "Choose Next Rider";
		riderPanel1.SetActive(true);
		riderPanel2.SetActive(true);
		riderPanel3.SetActive(true);
		UpdateRiderPanels();
	}

	public void HideRiderSelection(){
		riderPanel1.SetActive(false);
		riderPanel2.SetActive(false);
		riderPanel3.SetActive(false);
	}

	void UpdateRiderPanels(){
		List<Rider> riders = servController.availableRiders;

			rider1Pickup.text = "From\n" + riders[0].start.name;
			rider1Dest.text = "To\n" +riders[0].goal.name;
			if(riders[0].GetPayment() > 0){
				rider1Cost.color = Color.green;
			} else {
				rider1Cost.color = Color.red;
			}
			rider1Cost.text = "Fare: $" + riders[0].GetPayment();

			rider2Pickup.text = "From\n" + riders[1].start.name;
			rider2Dest.text = "To\n" +riders[1].goal.name;
			if(riders[1].GetPayment() > 0){
				rider2Cost.color = Color.green;
			} else {
				rider2Cost.color = Color.red;
			}
			rider2Cost.text = "Fare: $" + riders[1].GetPayment();


			rider3Pickup.text = "From\n" + riders[2].start.name;
			rider3Dest.text = "To\n" +riders[2].goal.name;
			if(riders[2].GetPayment() > 0){
				rider3Cost.color = Color.green;
			} else {
				rider3Cost.color = Color.red;
			}
			rider3Cost.text = "Fare: $" + riders[2].GetPayment();

	}

	void ButtonClick(int index){
		servController.SelectRider(index);
	}

	public void StartGameOver(){
		GameOverText.gameObject.SetActive(true);
		StartCoroutine(TimesUp());
	}

	IEnumerator TimesUp(){
		float time = 0;
		while(time < 2){
			GameOverText.transform.localScale += new Vector3(Time.deltaTime,Time.deltaTime,0);
			time += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		SceneManager.LoadScene("GameOver");
	}

}
