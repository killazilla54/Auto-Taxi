using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Node start;
	public Node goal;
	AstarPathfinding astar;
	IGrid graph;
	// public Actor car;
	public PlayerController controller;
	// Use this for initialization
	void Start () {
		astar = GetComponent<AstarPathfinding>();
		graph = GetComponent<GraphGrid>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.I)){
			Stack<Node> path = astar.FindPath(graph,start,goal);

			Debug.Log("Stack size: " + path.Count);
			// car.MoveOnPath(path);
			controller.SetDestination(path);
			graph.Clear();
		}
	}
}
