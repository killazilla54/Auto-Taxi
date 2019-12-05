using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServiceOperator : MonoBehaviour {
	public int maxRiders;
	public List<Node> destinations;

	//Pathfinding
	public Node start;
	public Node goal;
	AstarPathfinding astar;
	IGrid graph;

	public PlayerController controller;

	public List<Rider> availableRiders = new List<Rider>();
	public Rider selectedRider;
	public string state ="start";

	public Node initStartNode;
	public Node initEndNode;
	public float score;

	public UIController ui;

	public SoundManager sounds;

	public float timer; 

	void Start () {
		astar = GetComponent<AstarPathfinding>();
		graph = GetComponent<GraphGrid>();

		Node start = initStartNode;
		Node finish = initEndNode;
		Stack<Node> path = astar.FindPath(graph, start,finish);
		controller.SetDestination(path);
		state = "start";
	}
	
	void Update () {
		DetermineState();
		timer -= Time.deltaTime;
		if(timer <= 0){
			ScoreHolder.instance.score = score;
			ui.StartGameOver();
		}
	}

	private void DetermineState(){
		switch (state){
			case "start":
				if(controller.arrived){
					state = "chooseNext";
				}
			break;
			case "chooseNext":
				if(availableRiders.Count == 0){
					GetAvailableRiders();
				} 
			break;
			case "processSelect":		
					Stack<Node> path1 = astar.FindPath(graph,controller.prevNode,selectedRider.start);
					ui.SetPickup(selectedRider.start.name);
					controller.SetDestination(path1);
					float pickupCost = Vector2.Distance(controller.transform.position,selectedRider.start.transform.position);
					selectedRider.start.GetComponent<PickupZone>().ActivateZone();
					graph.Clear();
					state = "moveToPickup";
			break;
			case "moveToPickup":
				if(controller.arrived){
					state = "pickup";
					sounds.EnterCar();
				}
			break;
			case "pickup":
				Stack<Node> path = astar.FindPath(graph,selectedRider.start,selectedRider.goal);
				selectedRider.start.GetComponent<PickupZone>().DeactivateZone();
				selectedRider.goal.GetComponent<PickupZone>().ActivateZone();
				ui.SetDestination(selectedRider.goal.name);
				controller.SetDestination(path);
				state = "movingToDest";
				graph.Clear();
			break;
			case "movingToDest":
				if(controller.arrived){
					state = "chooseNext";
					selectedRider.goal.GetComponent<PickupZone>().DeactivateZone();
					score += selectedRider.GetPayment();
					sounds.ChaChing();
				}
			break;
		}
	}

	public void GetAvailableRiders(){
		availableRiders = new List<Rider>();
		List<int> usedStartIndexes = new List<int>();
		for(int i = 0; i < maxRiders; i++){
			Rider rider = new Rider();
			
			int index = UniqueRandomInt(0,destinations.Count, usedStartIndexes);
			usedStartIndexes.Add(index);
			rider.start = destinations[index];

			int goalIndex = UniqueRandomInt(0, destinations.Count, index); // Goal is not start
			rider.goal = destinations[goalIndex];
								
			float pickupCost = Vector2.Distance(controller.transform.position,rider.start.transform.position);		
			rider.SetPickupCost(pickupCost);
			availableRiders.Add(rider);
		}
		ui.ShowRiderSelection();
	}

	public void SelectRider(int index){
		selectedRider = availableRiders[index];
		availableRiders.Clear();
		ui.HideRiderSelection();
		state="processSelect";
	}

	private int UniqueRandomInt(int min, int max, List<int> usedInts){
		int num = Random.Range(min,max);
		while(usedInts.Contains(num)){
			num = Random.Range(min,max);
		}
		return num;
	}
	private int UniqueRandomInt(int min, int max, int usedInt){
		int num = Random.Range(min,max);
		while(usedInt == num){
			num = Random.Range(min,max);
		}
		return num;
	}


}
