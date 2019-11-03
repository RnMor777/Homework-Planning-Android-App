using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class Settings : MonoBehaviour {

	public GameObject extraInfo;
	public Text version;
	public Dropdown clearCache;
	public Dropdown notsTimes;
	public Dropdown notsDays;
	List<int> matchDays = new List<int> {5,10,15,30,60,0};
	List<int> matchTimes = new List<int> {7,8,12,15,17,0};
	List<int> matchNotsDays = new List<int> {1,2,3};
	public static string errorMessage="";
	public GameObject errorCan;

	void Start () {  //sets the value as the saved one
		int x = matchDays.FindIndex (w => PlayerPrefs.GetInt ("ClearCache",30).Equals(w));
		clearCache.value = x;
		int y = matchTimes.FindIndex (w => PlayerPrefs.GetInt ("NotsTime",7).Equals(w));
		notsTimes.value = y;
		int z = matchNotsDays.FindIndex (w => PlayerPrefs.GetInt ("NotsTime2",1).Equals(w));
		notsDays.value = z;
		version.text = "Version:\n" + Application.version;
		if (errorMessage.Length > 0)
			openErrors ();
	}
	
	public void changeCacheDate() {  //edits the field and save data
		int newDay = matchDays [clearCache.value];
		PlayerPrefs.SetInt ("ClearCache", newDay);
	}
	public void changeNotsTimes() {
		int newTime = matchTimes [notsTimes.value];
		PlayerPrefs.SetInt ("NotsTime", newTime);
		if (newTime == 0)
			notsDays.GetComponent<Dropdown> ().interactable = false;
		else
			notsDays.GetComponent<Dropdown> ().interactable = true;
	}
	public void changeNotsDays() {
		int newDay = matchNotsDays [notsDays.value];
		PlayerPrefs.SetInt ("NotsTime2", newDay);
	}
	public void openCloseInfo(bool open){
		extraInfo.SetActive (open);
	}
	public void openErrors() {
		errorCan.SetActive (true);
		errorCan.transform.Find("Text").GetComponent<Text> ().text = errorMessage;
		errorMessage = "";
	}
}
