using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour {

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
		if(GameController.Instance.startFollow == true && GameController.Instance.toFollow == true){
			posX = Mathf.SmoothDamp ((transform.position.x), player.transform.position.x, ref velocity.x, smoothTimeX);
			posY = Mathf.SmoothDamp ((transform.position.y), player.transform.position.y, ref velocity.y, smoothTimeY);
			transform.position = new Vector3 (posX, posY, transform.position.z);
		}
		


	}
}