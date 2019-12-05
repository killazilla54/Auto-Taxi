using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public CarSprite sprite;

	Stack<Node> path = new Stack<Node>();
	Node currentNode;
	public Node prevNode;
	float driveTime=0;

	public bool arrived = true;
	void Start(){
		GameObject start = GameObject.Find("StartNode");
		transform.position = start.transform.position;
		prevNode = start.GetComponent<Node>();
	}
	void Update(){
		MoveToDest();
	}

	public void SetDestination(Stack<Node> stack){
		path = stack;
		currentNode = path.Pop();
		arrived = false;
	}
	public void MoveToDest(){
		if(!arrived){
			Vector3 a = new Vector3(prevNode.gridX,prevNode.gridY,0);
			Vector3 b = new Vector3(currentNode.gridX,currentNode.gridY,0);
			float step = (1 / (a - b).magnitude) * Time.fixedDeltaTime * 5f;
			// if (driveTime <= 1.0f) {
			if(Vector3.Distance(transform.position,b)>.01f){
				driveTime += step; // Goes from 0 to 1, incrementing by step each time
				transform.position = Vector3.MoveTowards(transform.position,b,Time.deltaTime*5);
			} else {
				if(path.Count > 0){
					driveTime = 0;
					transform.position = b;	
					prevNode = currentNode;
					currentNode = path.Pop();
					sprite.HandleSpriteMovement(currentNode.transform.position);
				} else {
					arrived = true;
					Debug.Log("ARRIVED");
					prevNode = currentNode;
				}
			}

		}
	}

}
