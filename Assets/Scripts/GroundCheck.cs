using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

	private Player player;

	void Start(){
		//set player to the gameobject player
		player = gameObject.GetComponentInParent<Player>();

	}

	void OnTriggerEvent2D(Collider2D col){
		//if it is colliding the it is grounded
		player.grounded = true;

	}

	void OnTriggerStay2D(Collider2D col){
		//If it is colliding it is grounded
		player.grounded = true;
	
	}

	void OnTriggerExit2D(Collider2D col){
		//when it stops colliding it is not grounded
		player.grounded = false;
		
	}
}
