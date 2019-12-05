using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSprite : MonoBehaviour {

	SpriteRenderer sr;
	public Sprite[] sprites;//direction by index: L,R,U,D

    void Start () {
		sr = GetComponent<SpriteRenderer>();
	}
	
	public void HandleSpriteMovement(Vector2 nextNodePos){
		Vector2 pos = transform.parent.position;
		Vector2 diff = pos - nextNodePos;
        //Determine the direction the sprite should be facing, by using to difference in position between
        //the car and the next node to determine the forward vector.
        //.5f offset vectors are to move the car sprite to the correct side of the road since the path graph
        //edges are right down the middle of the street.
		if(diff.x > 0){
			sr.sprite = sprites[0];
			transform.position = transform.parent.position + new Vector3(0,.5f,0);
		} else if(diff.x < 0) {
			sr.sprite = sprites[1];
			transform.position = transform.parent.position + new Vector3(0,-.5f,0);
		} else if(diff.y < 0){
			sr.sprite = sprites[2];
			transform.position = transform.parent.position + new Vector3(.5f,0,0);
		} else if(diff.y > 0){
			sr.sprite = sprites[3];
			transform.position = transform.parent.position + new Vector3(-.5f,0,0);
		}
	}
}
