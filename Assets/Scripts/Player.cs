using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	private Animator animator;
	public static float speed = 20f;
	public static float maxSpeed = 3;
	public static float jumpPower = 300f;
	public static GameObject lvl;
	
	public bool grounded;
	public bool canDoubleJump;
	private Rigidbody2D rigidBody2D;

	protected void Start ()
	{

		animator = GetComponent<Animator> ();
		rigidBody2D = gameObject.GetComponent<Rigidbody2D> ();

	}

	private void OnDisable ()
	{
		//when the player is disabled
	}


	void Update ()
	{
		CheckIfGameOver ();

		///////////////////////////
		/// Walk
		/// ////////////////////

		// update the variable inside animator to the variable speed in the script
		animator.SetFloat("speed", Mathf.Abs(rigidBody2D.velocity.x));
		//if the player presses a then transform position to -1,1 
		//transform means move to
		if(Input.GetAxis("Horizontal 1") > -0.1f){
			transform.localScale = new Vector3(-1, 1, 0f);
			
		}
		//if the player presses d then transform position to 1,1 
		if(Input.GetAxis("Horizontal 1") < 0.1f){
			transform.localScale = new Vector3(1, 1, 0f);
			
		}

		///////////////////////////
		/// Walk
		/// ////////////////////

		//if the player hits w
		if(Input.GetButtonDown("Vertical 1")){
			if(grounded){
				//if the player is on the ground add  upward force multiplied by jump power.
				rigidBody2D.AddForce(Vector2.up * jumpPower);
				//allow to double jump
				canDoubleJump = true;
				
			}else{
				//if can doublejum
				if(canDoubleJump){
					//dont allow to jump anymore
					canDoubleJump = false;
					//keep the same x velocity and jump
					rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 0);
					rigidBody2D.AddForce(Vector3.up * jumpPower);
					
				}
				
			}
		}

	}

	//movement
	void FixedUpdate(){
		Vector3 easeVelocity = rigidBody2D.velocity;
		easeVelocity.y = rigidBody2D.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;
		//variable h is the input horoizonal 1. adjust inpputs in ToolBar>Edit>Project Settings>Input
		float h = Input.GetAxis("Horizontal 1");
		
		if(grounded){
			rigidBody2D.velocity = easeVelocity;
			
		}
		
		//player movement on x-Axis using a/d
		rigidBody2D.AddForce((Vector2.right * speed) * h);
		
		//limmiting speed of player 
		if(rigidBody2D.velocity.x > maxSpeed){
			//if the players velocity is past max speed going right then it sets its velocity to max speed.
			rigidBody2D.velocity = new Vector2 (maxSpeed, rigidBody2D.velocity.y);
		}
		
		if(rigidBody2D.velocity.x < -maxSpeed){
			//if the players velocity is past max speed going left then it sets its velocity to max speed.
			rigidBody2D.velocity = new Vector2 (-maxSpeed, rigidBody2D.velocity.y);
		}
	}


	private void NextLevel(){
		//if the x position of the gameobject lvl is blank than load blank level
		//add level numbers in ToolBar>Build Settings>Scenes In Build>
		if (lvl.transform.position.x == 1) {
			Application.LoadLevel (1);
		} else if (lvl.transform.position.x == 2) {
			Application.LoadLevel (2);
		}else if (lvl.transform.position.x == 3) {
			Application.LoadLevel (3);
		}else if (lvl.transform.position.x == 4) {
			Application.LoadLevel (4);
		}else if (lvl.transform.position.x == 5) {
			Application.LoadLevel (5);
		}else if (lvl.transform.position.x == 6) {
			Application.LoadLevel (6);
		}else if (lvl.transform.position.x == 7) {
			Application.LoadLevel (7);
		}else if (lvl.transform.position.x == 8) {

		}
	}




	//handles all collisions of player
	private void OnTriggerStay2D (Collider2D objectPlayerCollideWith)
	{
		// if (objectPlayerCollideWith.tag == "Fruit") {
	//		playerHealth += healthPerFruit;
	//		healthText.text = "+" + healthPerFruit + " Health\n" + "Player 1 Health: " + playerHealth;
	//		objectPlayerCollideWith.gameObject.SetActive (false);
	//		SoundController.Instance.PlaySingle (fruitSound1, fruitSound2);
		//} 

	if (objectPlayerCollideWith.tag == "Door") {
//when collides with gameobject with tag Door.
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				//when is collided with door and is pressing shift it calls next level
				NextLevel();
			}
		}

	}
	//checks if game is over
	private void CheckIfGameOver ()
	{
//		if (playerHealth <= 0) {
//			if (lives > 0) {
//				if (GameController.Instance.Type == "drag"){
//					playerHealth = 100;
//				}else{
//				playerHealth = 50;
//				}
//				lives--;
//
//				livesText.text = "Lives: " + lives;
//
//			} else if(lives == 0) {
//				GameController.Instance.GameOver ();
//			}
//
//		}
	}
}
