using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private Vector2 velocity;

	public float smoothTimeY;
	public float smoothTimeX;

	public GameObject player;

	public bool bounds;

	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;
	public float minY;
	public float minX;
	public float maxX;
	float posY = 4;
	float posX = 4;
	private bool hasStarted = false;

	void Start () {

	}

	void FixedUpdate(){
		if (!hasStarted) {
			//works as a start function after gamecontroller is loaded
			player = GameObject.FindGameObjectWithTag ("Player");
			hasStarted = true;
		}
		//camera boundries
		if(GameController.Instance.location == "outside"){
			if (player.transform.position.x >= minX) {
				posX = Mathf.SmoothDamp ((transform.position.x), player.transform.position.x, ref velocity.x, smoothTimeX);
			}
			if (player.transform.position.x <= maxX) {
				posX = Mathf.SmoothDamp ((transform.position.x), player.transform.position.x, ref velocity.x, smoothTimeX);
			}
			if (player.transform.position.x > maxX) {
				posX = maxX;
			} else if (player.transform.position.x < minX) {
				posX = minX;
			}
			if (player.transform.position.y >= minY) { 
				posY = Mathf.SmoothDamp ((transform.position.y), player.transform.position.y, ref velocity.y, smoothTimeY);
			} else {
				posY = minY;
			}
			transform.position = new Vector3 (posX, posY, transform.position.z);
		}else{
			posX = Mathf.SmoothDamp ((transform.position.x), player.transform.position.x, ref velocity.x, smoothTimeX);
			posY = Mathf.SmoothDamp ((transform.position.y), player.transform.position.y, ref velocity.y, smoothTimeY);
			transform.position = new Vector3 (posX, posY, transform.position.z);
		}
		


	}
}