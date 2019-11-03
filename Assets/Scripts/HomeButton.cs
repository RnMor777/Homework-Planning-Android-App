using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeButton : MonoBehaviour {

	public void homeBtn() //goes home
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("HomeNew");
	}
	public void Update() 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
			UnityEngine.SceneManagement.SceneManager.LoadScene ("HomeNew");
	}
}
