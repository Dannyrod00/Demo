using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public static GameController Instance;
	private GameObject Archer;
	public int currentLevel;
	private GameObject startScreen;
	void Awake ()
	{
		//Allows variables to be accesed throughout project without being static or being destroyed when loading levels.
		if (Instance != null && Instance != this) {
			DestroyImmediate (gameObject);
			return;
		}
		
		Instance = this;
		
		DontDestroyOnLoad (gameObject);
	}
	
	void Start ()
	{
		//DO NOT TOUCH START FUNCTION
		InitializeGame ();
	}
	
	private void InitializeGame ()
	{
		//Intialize EVERYTHING IN HERE
		startScreen= GameObject.Find ("Start Screen");
	}
	private void OnLevelWasLoaded (int levelLoaded)
	{
		//when the level is loaded add 1 to current level.
		Debug.Log (currentLevel);
		currentLevel++;
		InitializeGame ();
	}
	void Update ()
	{
		
		
	}
	public void GameOver ()
	{
		//
		//      SoundController.Instance.music.Stop ();
		//      SoundController.Instance.PlaySingle (gameOverSound);
		//
		//      leadScreen.SetActive (true);
		//
		//
		//      Archer.SetActive (false);
		//      Drag.SetActive (false);
		//      Wizard.SetActive (false);
		//      Norm.SetActive (false);
		//      NormGuy.SetActive (false);
		//      Spear.SetActive (false);
		//
		//
		//      enabled = false;
		
		
	}
	//gets called when start button is pressed
	public void StartButton(){
		startScreen.SetActive(false);
	}
}