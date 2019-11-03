using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Area730.Notifications;

public class PageChanger : MonoBehaviour {

	public GameObject popup;

	void Start()
	{
		if (!System.IO.Directory.Exists (Application.persistentDataPath + "/Act")) 
			System.IO.Directory.CreateDirectory (Application.persistentDataPath + "/Act");
		
		if (!System.IO.Directory.Exists (Application.persistentDataPath + "/Images")) //creates the folder
			System.IO.Directory.CreateDirectory (Application.persistentDataPath + "/Images");
		
		if (!System.IO.Directory.Exists (Application.persistentDataPath + "/Finished"))   //creates the folder
			System.IO.Directory.CreateDirectory (Application.persistentDataPath + "/Finished");
		
		if (!System.IO.Directory.Exists (Application.persistentDataPath + "/Finished/Images")) //creates the folder
			System.IO.Directory.CreateDirectory (Application.persistentDataPath + "/Finished/Images");

		if (!System.IO.Directory.Exists (Application.persistentDataPath + "/Classes")) //creates the folder
			System.IO.Directory.CreateDirectory (Application.persistentDataPath + "/Classes");
		
		if (Entry.fromEntry) {
			#if UNITY_EDITOR
				popup.GetComponent<RawImage> ().color = new Color (1F, 1F, 1F, 1F);  //sets up a popup in unity editor only
				popup.transform.Find ("Text").GetComponent<Text> ().color = new Color (0F, 0F, 0F, 1F);
				popup.SetActive (true);
				StartCoroutine (alphaChange ());
			#endif
			Entry.fromEntry = false;
		}
	}

	public void change(int x)	{  //loads all the scenes
		switch (x) {
		case 1:
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Calendar");
			break;
		case 2:
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Overview");
			break;
		case 3:
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Enter");
			break;
		case 4:
			UnityEngine.SceneManagement.SceneManager.LoadScene ("LateNew");
			break;
		case 5:
			UnityEngine.SceneManagement.SceneManager.LoadScene ("ActivitiesNew");
			break;
		case 6:
			UnityEngine.SceneManagement.SceneManager.LoadScene ("EstimatedTime");
			break;
		case 7:
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Classes View");
			break;
		case 8:
			UnityEngine.SceneManagement.SceneManager.LoadScene ("SettingsPage");
			break;
		}
	}
	IEnumerator alphaChange()  //shows the popup on unity editor only 
	{
		while (popup.GetComponent<RawImage> ().color.a > 0) {
			yield return new WaitForSeconds (.1F);
			float temp = popup.GetComponent<RawImage> ().color.a; 
			popup.GetComponent<RawImage> ().color = new Color (1F, 1F, 1F, (float)(temp - .05));
			popup.transform.Find ("Text").GetComponent<Text> ().color = new Color (0F, 0F, 0F, (float)(temp - .05));
		}
		popup.SetActive (false);
	}
	public void Update() //quits when back arrow pressed
	{
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}
}
