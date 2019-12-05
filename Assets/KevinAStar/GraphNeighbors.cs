using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNeighbors : MonoBehaviour {

	public List<Node> neighborNodes;	

	void Start(){
		Vector2 pos = transform.position;
		Node node = GetComponent<Node>();
		node.gridX = Mathf.RoundToInt(pos.x);
		node.gridY = Mathf.RoundToInt(pos.y);
	}

	public List<Node> GetNeighborNodes(){
		return neighborNodes;
	}

	// void OnDrawGizmos(){
	// 	foreach(Node node in neighborNodes){
	// 		Color selectedColor = Color.blue;

	// 		Vector3 thisPos = transform.position;
	// 		Vector3 nodePos = node.transform.position;

	// 		float xoffset = .1f;
	// 		float yoffset = .1f;

	// 		if(nodePos.x < thisPos.x) {
	// 			xoffset *= -1;
	// 			selectedColor = Color.red;
	// 		}
	// 		else if(nodePos.y < thisPos.y){
	// 			yoffset *= -1;
	// 			selectedColor = Color.red;
	// 		}
	// 		Vector3 offset = new Vector3(thisPos.x+xoffset,thisPos.y+yoffset,0);
	// 		Debug.Log(offset);
	// 		Gizmos.color = selectedColor;
	// 		Gizmos.DrawLine(offset, nodePos);
	// 	}
	// }
}
