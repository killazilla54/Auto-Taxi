﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCam : MonoBehaviour {

	public int xBound;
	public int yBound;
	// // Use this for initialization
	// void Start () {
	// 	Camera cam = GetComponent<Camera>();
	// 	float size;
	// 	size = ( Screen.currentResolution.height/(15*2));
	// 	cam.orthographicSize = size;
	// }
	
	// // Update is called once per frame
	// void Update () {
		// if((playerY+(camHeight/2)) < (currentBounds.center.y + currentBounds.extents.y) && 
		// 	(playerY-(camHeight/2)) > (currentBounds.center.y - currentBounds.extents.y)){
		// 	newCamY = player.transform.position.y;
		// }
		// if((playerX+(camWidth/2)) < (currentBounds.center.x + currentBounds.extents.x) && 
		// 	(playerX-(camWidth/2)) > (currentBounds.center.x - currentBounds.extents.x)){
		// 	newCamX = player.transform.position.x;
		// }
	// }
	/**
	 * The target size of the view port.
	 */
	public Vector2 targetViewportSizeInPixels = new Vector2(480.0f, 320.0f);
	/**
	 * Snap movement of the camera to pixels.
	 */
	public bool lockToPixels = true;
	/**
	 * The number of target pixels in every Unity unit.
	 */
	public float pixelsPerUnit = 32.0f;
	/**
	 * A game object that the camera will follow the x and y position of.
	 */
	public GameObject followTarget;
	
	private Camera _camera;
	private int _currentScreenWidth = 0;
	private int _currentScreenHeight = 0;
	
	private float _pixelLockedPPU = 32.0f;
	private Vector2 _winSize;
	
	protected void Start(){
		_camera = this.GetComponent<Camera>();
		if(!_camera){
			Debug.LogWarning("No camera for pixel perfect cam to use");
		}else{
			_camera.orthographic = true;
			ResizeCamToTargetSize();
		}
	}
	
	public void ResizeCamToTargetSize(){
		if(_currentScreenWidth != Screen.width || _currentScreenHeight != Screen.height){
			// check our target size here to see how much we want to scale this camera
			float percentageX = Screen.width/targetViewportSizeInPixels.x;
			float percentageY = Screen.height/targetViewportSizeInPixels.y;
			float targetSize = 0.0f;
			if(percentageX > percentageY){
				targetSize = percentageY;
			}else{
				targetSize = percentageX;
			}
			int floored = Mathf.FloorToInt(targetSize);
			if(floored < 1){
				floored = 1;
			}
			// now we have our percentage let's make the viewport scale to that
			float camSize = ((Screen.height/2)/floored)/pixelsPerUnit;
			_camera.orthographicSize = camSize;
			_pixelLockedPPU = floored * pixelsPerUnit;
		}
		_winSize = new Vector2(Screen.width, Screen.height);
	}
	
	public void Update(){
		if(_winSize.x != Screen.width || _winSize.y != Screen.height){
			ResizeCamToTargetSize();
		}
		if(_camera && followTarget){

			float screenAspect = (float) Screen.width / (float) Screen.height;
			float camHalfHeight = _camera.orthographicSize;
			float camHalfWidth = screenAspect * camHalfHeight;

			Vector2 newPosition = new Vector2(followTarget.transform.position.x, followTarget.transform.position.y);
			float nextX = Mathf.Round(_pixelLockedPPU * newPosition.x);
			Debug.Log("ScreenW: " + camHalfWidth +" - nextX/_pixelLockedPPU: " + nextX/_pixelLockedPPU);

			if(nextX/_pixelLockedPPU < camHalfWidth ||nextX/_pixelLockedPPU > (102 -camHalfWidth) ){
				nextX = transform.position.x * _pixelLockedPPU;
			}
			float nextY = Mathf.Round(_pixelLockedPPU * newPosition.y);
			Debug.Log("ScreenH: " + camHalfHeight +" - nextY/_pixelLockedPPU: " + nextY/_pixelLockedPPU);

			if(nextY/_pixelLockedPPU < camHalfHeight ||nextY/_pixelLockedPPU > (100 -camHalfHeight) ){
				nextY = transform.position.y * _pixelLockedPPU;
			}

			
			
			_camera.transform.position = new Vector3(nextX/_pixelLockedPPU, nextY/_pixelLockedPPU, _camera.transform.position.z);
		}
	}

}
