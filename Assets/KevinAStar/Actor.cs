using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

	// Use this for initialization
	private Coroutine coroutine;

	public CarSprite carSprite;

	Node lastDestination;

	//Proposed solution, Have 1 exterior method, it calls the pickup coroutine and yeilds til done,
	//Then it calls the deliver coroutine and yeilds til done.

	IEnumerator FullTrip(Stack<Node> path){
		//pickup
		yield return StartCoroutine(MoveToPosition(path));
		Debug.Log("Done with Full trip");
	}

	public void PickUpRider(Stack<Node> path){
		FullTrip(path);

	}

	public void MoveOnPath (Stack<Node> path){
		if(coroutine != null){
			StopMoveOnPath();
		}
		coroutine = StartCoroutine(MoveToPosition(path));
	}

	public void StopMoveOnPath(){
		StopCoroutine(coroutine);
		coroutine = null;
	}

	IEnumerator MoveToPosition(Stack<Node> path){
	Node lastNode = path.Pop();
	
		while(path.Count > 0){
			Node next = path.Pop();
			carSprite.HandleSpriteMovement(next.transform.position);

			Vector3 a = new Vector3(lastNode.gridX,lastNode.gridY,0);
			Vector3 b = new Vector3(next.gridX,next.gridY,0);
			float step = (1 / (a - b).magnitude) * Time.fixedDeltaTime * 5f;
			float t = 0;
			while (t <= 1.0f) {
				t += step; // Goes from 0 to 1, incrementing by step each time
				// transform.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
				transform.position = Vector3.MoveTowards(a,b,3);
				yield return new WaitForFixedUpdate();
			}
			transform.position = b;
			lastNode = next;
		}
		lastDestination = lastNode;

		
	}
}
