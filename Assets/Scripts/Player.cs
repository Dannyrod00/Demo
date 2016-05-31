using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	private Animator animator;
	public static float speed = 200f;
	public static float maxSpeed = 3;
	public static float jumpPower = 300f;
	public static GameObject lvl;
	public static Text enter;
	private GameObject tutorial;
	private Text tuttext;
	private GameObject daline;
	private GameObject messenger;
	private GameObject nme;
	private GameObject village;
	private string datext;
	private bool tackle = false;
	private int points = 0;
	public AudioClip Knock;
	public AudioClip Scream;
	public Camera camra1;
	public Camera camra2;
	private int run = 0;
	public bool grounded;
	public bool canDoubleJump;
	private Rigidbody2D rigidBody2D;
	
	protected void Start ()
	{
		
		animator = GetComponent<Animator> ();
		rigidBody2D = gameObject.GetComponent<Rigidbody2D> ();
		
		daline= GameObject.Find ("Panel");
		enter= GameObject.Find ("Enter").GetComponent<Text> ();
		tuttext= GameObject.Find ("TutText").GetComponent<Text> ();
		messenger = GameObject.Find ("Messenger");
		nme = GameObject.Find ("Enemy");
		daline.SetActive (false);
		village = GameObject.Find ("village1");
		tutorial = GameObject.Find ("Tutorial");
		
		
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
		
		if (Input.GetButtonDown ("Jump")) {
			//daline.SetActive (true);
			//camra1.rect = new Rect(.5f,0,.5f,1);
			//camra2.rect = new Rect(0,0,.5f,1);
		}
		if (Input.GetKeyDown (KeyCode.Return)) {
			
			enter.text = "";
		}
		if (GameController.Instance.tut == true) {
			
			if (Input.GetKeyDown (KeyCode.Return)) {
				
				tutorial.SetActive(false);
			}
			
			if(GameController.Instance.location == "inside" && run == 0){
				
				moreDialog();
			}
			if(GameController.Instance.location == "outside" && run == 7){
				moreDialog();
			}
			if(GameController.Instance.location == "inside" && run == 12){
				messenger.SetActive(false);
				moreDialog();
			}
		}
	}
	void moreDialog(){
		//speeek(time to display, text)
		
		if (run == 0){
			village.SetActive(false);
			speeek(2,"Joe: Where did you get that armour.");
		}else if(run == 1){
			speeek(3,"Joe: It looks amazing.");
		}else if(run == 2){
			speeek(1,"You: My dad gave it...");
		}else if(run == 3){
			GameController.Instance.startFollow = true;
			
			
		}else if(run==4){
			
			Debug.Log ("herro");
			village.SetActive(true);
			transform.position = new Vector3 (183, -78, transform.position.z);
			GameController.Instance.toFollow = false;
			speeek(5,"Judge: You have been scentanced to death by fire pit.");
		}else if(run == 5){
			daline.SetActive(true);
			speeek(2,"You: *Screams*");
			SoundController.Instance.PlaySingle (Scream);
		}
		else if(run == 6){
			speeek(3,"");
			Invoke ("nock",1);
			transform.position = new Vector3 (30, -175, transform.position.z);
		}
		else if(run == 7){
			daline.SetActive(false);
			speeek(3,"Conscious: Wonder who's at the door.");
		}
		else if(run == 8){
			
		}
		else if (run == 9){
			speeek(9,"Messenger: As a Messenger of the Monarch I am hear to deliver you the news of your sons tragic death from combat.");
		}else if(run == 10){
			speeek(3,"You: Where is his gear..");
		}else if(run == 11){
			speeek(4,"Messenger: Sadly we have lost it in combat.");
		}else if(run == 12){
			speeek(4,"Conscious: I need to go inside to gather my thoughts.");
		}else if(run == 13){
			
		}else if(run == 14){
			speeek(3,"Conscious: Shit. Where is that armour.");
		}
		else if(run == 15){
			speeek(5,"Conscious: My family worked on it for centuries.");
		}
		else if(run == 16){
			speeek(5,"Conscious: I need to go to the bar.");
		}
		run++;
		Debug.Log (run);
		Debug.Log (GameController.Instance.location);
	}
	
	void speeek (int tim,string wrd){
		tutorial.SetActive(true);
		Invoke ("moreDialog",tim);
		tuttext.text = wrd;
		
	}
	
	
	void nock(){
		
		SoundController.Instance.PlaySingle (Knock);
	}
	//movement
	void FixedUpdate(){
		Vector3 easeVelocity = rigidBody2D.velocity;
		easeVelocity.y = rigidBody2D.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;
		//variable h is the input horoizonal 1. adjust inpputs in ToolBar>Edit>Project Settings>Input
		float h = Input.GetAxis("Horizontal 1");
		float v = Input.GetAxis("Vertical 1");
		
		
		
		//player movement on x-Axis using a/d
		rigidBody2D.AddForce((Vector2.right * speed) * h);
		rigidBody2D.AddForce((Vector2.up * speed) * v);
		if (h == 0) {
			rigidBody2D.velocity = new Vector2 (0, rigidBody2D.velocity.y);
		}
		if (v == 0) {
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, 0);
		}
		
		//limmiting speed of player 
		if(rigidBody2D.velocity.x > maxSpeed){
			//if the players velocity is past max speed going right then it sets its velocity to max speed.
			rigidBody2D.velocity = new Vector2 (maxSpeed, rigidBody2D.velocity.y);
			
		}
		
		if(rigidBody2D.velocity.x < -maxSpeed){
			//if the players velocity is past max speed going left then it sets its velocity to max speed.
			rigidBody2D.velocity = new Vector2 (-maxSpeed, rigidBody2D.velocity.y);
			
		}
		
		if(rigidBody2D.velocity.y > maxSpeed){
			//if the players velocity is past max speed going right then it sets its velocity to max speed.
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, maxSpeed);
			
		}
		
		if(rigidBody2D.velocity.y < -maxSpeed){
			//if the players velocity is past max speed going left then it sets its velocity to max speed.
			rigidBody2D.velocity = new Vector2 (rigidBody2D.velocity.x, -maxSpeed);
			
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
		//      playerHealth += healthPerFruit;
		//      healthText.text = "+" + healthPerFruit + " Health\n" + "Player 1 Health: " + playerHealth;
		//      objectPlayerCollideWith.gameObject.SetActive (false);
		//      SoundController.Instance.PlaySingle (fruitSound1, fruitSound2);
		//} 
		Debug.Log (objectPlayerCollideWith.tag);
		if (objectPlayerCollideWith.tag == "Door") {
			Debug.Log ("idk2");
			//when collides with gameobject with tag Door.
			enter.text = "Press [Enter]";
			if (Input.GetKeyDown (KeyCode.Return)) {
				transform.position = new Vector3 (8, -178, transform.position.z);
				Debug.Log ("Open");
				GameController.Instance.location = "inside";
			}
		} 
		
		if (objectPlayerCollideWith.tag == "Ladder") {
			Debug.Log("idk2");
			//when collides with gameobject with tag Door.
			enter.text = "Press [Enter]";
			if (Input.GetKeyDown (KeyCode.Return)) {
				transform.position = new Vector3 (7, -112, transform.position.z);
				Debug.Log ("Open");
				GameController.Instance.location = "outside";
			}
		} if (objectPlayerCollideWith.tag == "bathroom") {
			Debug.Log("idk2");
			//when collides with gameobject with tag Door.
			enter.text = "Press [Enter]";
			if (Input.GetKeyDown (KeyCode.Return) && GameController.Instance.location == "insideinside") {
				transform.position = new Vector3 (90, -137, transform.position.z);
				Debug.Log ("Open");
				GameController.Instance.location = "inside";
			}
			else if (Input.GetKeyDown (KeyCode.Return) && GameController.Instance.location == "inside") {
				transform.position = new Vector3 (168, -183, transform.position.z);
				Debug.Log ("Open");
				GameController.Instance.location = "insideinside";
			}
		}if (objectPlayerCollideWith.tag == "bar") {
			Debug.Log("idk2");
			//when collides with gameobject with tag Door.
			enter.text = "Press [Enter]";
			if (Input.GetKeyDown (KeyCode.Return) && GameController.Instance.location == "outside") {
				transform.position = new Vector3 (105,-155, transform.position.z);
				Debug.Log ("Open");
				GameController.Instance.location = "inside";
			}
			else if (Input.GetKeyDown (KeyCode.Return) && GameController.Instance.location == "inside") {
				transform.position = new Vector3 (-66, -107, transform.position.z);
				Debug.Log ("Open");
				GameController.Instance.location = "outside";
			}
		}if (objectPlayerCollideWith.tag == "train") {
			Debug.Log("idk2");
			//when collides with gameobject with tag Door.
			enter.text = "Press [Enter]";
			if (Input.GetKeyDown (KeyCode.Return) && GameController.Instance.location == "inside") {
				transform.position = new Vector3 (233,-163, transform.position.z);
				Debug.Log ("Open");
				GameController.Instance.location = "insideinside";
			}
			else if (Input.GetKeyDown (KeyCode.Return) && GameController.Instance.location == "insideinside") {
				transform.position = new Vector3 (120, -137, transform.position.z);
				Debug.Log ("Open");
				GameController.Instance.location = "inside";
			}
		}
		if (objectPlayerCollideWith.tag == "Enemy") {
			
			if(!tackle){
				speeek(1,"General: Thats Him!");
				Debug.Log("dashit");
				tackle = true;
			}
		}
		if(objectPlayerCollideWith.tag == "trainin"){
			enter.text = points.ToString();
			if (Input.GetKeyDown (KeyCode.J)) {
				points++;
			}
		}
		
	}
	
	
	private void OnTriggerExit2D (Collider2D objectPlayerNotCollideWith)
	{
		if (objectPlayerNotCollideWith.tag == "bathroom") {
			enter.text = "";
		}
		if (objectPlayerNotCollideWith.tag == "bar") {
			enter.text = "";
		}
		if (objectPlayerNotCollideWith.tag == "Ladder") {
			enter.text = "";
		}
		if (objectPlayerNotCollideWith.tag == "Door"){
			enter.text = "";
		}
		if (objectPlayerNotCollideWith.tag == "train"){
			enter.text = "";
		}
		if(objectPlayerNotCollideWith.tag == "trainin"){
			enter.text = "";
		}
	}
	
	//checks if game is over
	private void CheckIfGameOver ()
	{
		//      if (playerHealth <= 0) {
		//          if (lives > 0) {
		//              if (GameController.Instance.Type == "drag"){
		//                  playerHealth = 100;
		//              }else{
		//              playerHealth = 50;
		//              }
		//              lives--;
		//
		//              livesText.text = "Lives: " + lives;
		//
		//          } else if(lives == 0) {
		//              GameController.Instance.GameOver ();
		//          }
		//
		//      }
	}
}
