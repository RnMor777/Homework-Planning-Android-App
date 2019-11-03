using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitch : MonoBehaviour {

	// Use this for initialization
	bool notificationscene;
	public void button () {
		
		if (!notificationscene) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("NotificationTest");
			notificationscene = true;
		}
		if (notificationscene) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Classes View");
			notificationscene = false;
		}
	}
}
			
			
			