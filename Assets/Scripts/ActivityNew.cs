using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using Area730.Notifications;

public class ActivityNew : MonoBehaviour {

	public float posY=-200;
	public GameObject assignmentLine;
	public List<ActivitiesSave> events = new List<ActivitiesSave>();
	public Dictionary<ActivitiesSave,string> eventsWithFile = new Dictionary<ActivitiesSave, string> ();
	public int whereInList=0;
	public GameObject leftArrow;
	public GameObject rightArrow;
	public GameObject entryCanvas;
	public GameObject days;
	public GameObject date;
	public bool reacure=true;
	public GameObject onceBtn;
	public GameObject repeatBtn;
	public Text pageNumb;
	int whichPage=1;
	public Text errorText;
	public GameObject deletePrompt;
	public Transform canvasToLoadTo;
	bool az = false;
	int compareAz=-1;
	public bool onCal;

	[System.Serializable]
	public struct ActivitiesSave: EventsInterface  //this is the activity interface
	{
		public string name;
		public bool reacurring;
		public List<string> Days;
		public string date;
		public System.DateTime Start;
		public System.DateTime End;

		public string Name {
			set { name = value; }
			get { return name; }
		}
		public string Date {
			set { date = value; }
			get { return date; }
		}
	}

	void Start()
	{
		foreach (ActivityNew.ActivitiesSave x in RemoveCache.fullListActs.Keys) { //runs for everything that is loaded
			if (x.date != "" && System.DateTime.Parse (x.date).AddDays(1)<System.DateTime.Now) {  //deletes past events
				File.Delete (RemoveCache.fullListActs[x]);
				continue;
			}
			eventsWithFile.Add (x, RemoveCache.fullListActs[x]);
			events.Add (x);  	
		}
		events = events.OrderBy (e => e.name).ToList (); 	//initially sorts by name
		creation ();
	}
	public void sorter(int whereToSort)
	{
		rightArrow.SetActive (false);  //sets the left and right to inactive
		leftArrow.SetActive (false);
		whereInList = 0;
		switch (whereToSort) {  //runs a switch to decide which sort to use
		case 1:
			events = events.OrderBy (e => e.name).ToList ();
			break;
		case 2:
			events = events.OrderBy (e => e.Start).ToList ();  //sorts by start time
			break;
		case 3:
			events = events.OrderBy (e => //sorts by 
				e.Days[0]== "M" ? 1 :
				e.Days[0] == "Tu" ? 2 :
				e.Days[0] == "W" ? 3 :
				e.Days[0] == "Th" ? 4 :
				e.Days[0] == "F" ? 5 :
				e.Days[0] == "Sa" ? 6 :
				7).ToList ();
			break;
		}
		if (az && whereToSort == compareAz) {
			events.Reverse();
			az = false;
		} else {
			compareAz = whereToSort;
			az = true;
		}
		foreach (GameObject x in GameObject.FindGameObjectsWithTag("OverviewRow")) {
			x.transform.Find("Button").GetComponent<Button> ().onClick.RemoveAllListeners ();
			Destroy (x);
		}
		posY = -200;
		creation ();
	}
	public void creation()
	{
		whichPage = 1;
		pageNumb.text = whichPage + "/" + Mathf.Ceil ((float)((events.Count/4.0)-.1));  //sets the page number to at least one
		if (pageNumb.text == "1/0")
			pageNumb.text = "1/1";
		
		foreach (ActivitiesSave y in events) {  //creates everything
			if (whereInList < 4) {
				GameObject newRow = Instantiate (assignmentLine);
				newRow.transform.Find ("Name").GetComponent<Text> ().text = y.name;
				string tempString="";
				int skipFirst = 0;
				if (y.reacurring) {
					foreach (string x in y.Days) {
						if (skipFirst == 0) {
							tempString = x;
							skipFirst = 1;
						} else 
							tempString += ", " + x;
					}
				} else 
					tempString = y.date;
				
				newRow.transform.Find ("Days").GetComponent<Text> ().text = tempString;
				string time = y.Start.ToString("h:mm tt");
				time += " - "+y.End.ToString ("h:mm tt");
				newRow.transform.Find ("Times").GetComponent<Text> ().text = time;
				newRow.name = "assignment";
				newRow.transform.Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {delete(y);});
				newRow.transform.SetParent (canvasToLoadTo.transform);
				newRow.transform.SetSiblingIndex (1);
				newRow.transform.localPosition = new Vector2 (0, posY);
				newRow.transform.localScale = new Vector3 (1, 1, 1);
				posY -= 200;
				whereInList++;
			} else {
				rightArrow.SetActive (true);
				break;
			}
		}
	}
	public void loadMore(bool right)
	{
		whichPage = right ? whichPage + 1 : whichPage - 1;
		pageNumb.text = whichPage + "/" + Mathf.Ceil ((float)((events.Count/4.0)-.1));  //sets the page numbers
		if (pageNumb.text == "1/0")
			pageNumb.text = "1/1"; 
		
		int tempInt = whereInList;
		posY = -200;
		int trackHowManyObjs = 0;
		foreach (GameObject x in GameObject.FindGameObjectsWithTag("OverviewRow")) {
			x.transform.Find("Button").GetComponent<Button> ().onClick.RemoveAllListeners ();
			trackHowManyObjs++;
			Destroy (x);
		}
		if (!right) {
			whereInList -= trackHowManyObjs;  //corrects for anything while going left
			whereInList -= 4;
		}
		tempInt = whereInList;
		for (int i = tempInt; right ? (i < tempInt + 4 && i <= events.Count - 1) : (i >= tempInt && i<tempInt+4); i++) {				
			GameObject newRow = Instantiate (assignmentLine);
			newRow.transform.Find ("Name").GetComponent<Text> ().text = events [whereInList].name; //sets the name
			string tempString="";
			if (events [whereInList].reacurring) {
				int skipFirst = 0;
				foreach (string x in events[whereInList].Days) { //finds the days. skips the ',' for the first day
					if (skipFirst == 0) {
						tempString = x;
						skipFirst = 1;
					} else 
						tempString += ", " + x;
				}
			} else 
				tempString = events [whereInList].date;
			
			newRow.transform.Find ("Days").GetComponent<Text> ().text = tempString; //sets the days element
			string time = events[whereInList].Start.ToString("h:mm tt");
			time += " - "+events[whereInList].End.ToString ("h:mm tt");
			newRow.transform.Find ("Times").GetComponent<Text> ().text = time;
			newRow.name = "assignment";
			int forListener = whereInList;
			newRow.transform.Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {delete(events[forListener]);});  //adds the listeneres
			newRow.transform.SetParent (canvasToLoadTo.transform);
			newRow.transform.SetSiblingIndex (1);
			newRow.transform.localPosition = new Vector2 (0, posY);
			newRow.transform.localScale = new Vector3 (1, 1, 1);
			posY -= 200;
			whereInList++;
		}
		leftArrow.SetActive (whereInList>4?true:false);
		rightArrow.SetActive ((events.Count - 1 >= whereInList) ? true : false);
	}
	public void openClose(bool open)  //opens everything for the entry
	{
		entryCanvas.SetActive (open ? true : false);
		entryCanvas.transform.Find ("Name").GetComponent<InputField> ().text = "";

		for (int x = 0; x < days.transform.childCount; x++) {
			days.transform.GetChild (x).GetComponent<Toggle> ().isOn = false;
			days.transform.GetChild (x).GetComponent<Toggle> ().interactable = true;
		}
		for (int x = 0; x < date.transform.childCount; x++) 
			date.transform.GetChild (x).GetComponent<InputField> ().text="";

		reacure = true;
		days.SetActive (true);
		date.SetActive (false);
		entryCanvas.transform.Find("StartHour").GetComponent<InputField> ().text = "";
		entryCanvas.transform.Find("StartMin").GetComponent<InputField> ().text = "";
		entryCanvas.transform.Find("EndHour").GetComponent<InputField> ().text = "";
		entryCanvas.transform.Find("EndMin").GetComponent<InputField> ().text = "";
		entryCanvas.transform.Find("StartTime").GetComponent<Dropdown> ().value = 0;
		entryCanvas.transform.Find("EndTime").GetComponent<Dropdown> ().value = 0;
		errorText.text = "";
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name == "Calendar") {
			System.DateTime todays = new System.DateTime (CalendarController.curYear, CalendarController.curMonth, CalendarController.curDay);
			int whatChild = (int)todays.DayOfWeek - 1;
			if (whatChild < 0)
				whatChild = 6;
			days.transform.GetChild (whatChild).GetComponent<Toggle> ().isOn=true;
			days.transform.GetChild (whatChild).GetComponent<Toggle> ().interactable=false;
			date.transform.GetChild (0).GetComponent<InputField> ().text=""+todays.Month;
			date.transform.GetChild (1).GetComponent<InputField> ().text=""+todays.Day;
			date.transform.GetChild (2).GetComponent<InputField> ().text=""+todays.Year;
			for (int x = 0; x < date.transform.childCount; x++)
				date.transform.GetChild (x).transform.GetComponent<InputField> ().interactable = false;
		}
	}
	public void save()  //checks all conditions before saving
	{
		ActivitiesSave obj = new ActivitiesSave ();
		List<string> tempDays = new List<string> ();
		string dateString = "";

		obj.name = entryCanvas.transform.Find ("Name").GetComponent<InputField>().text;
		if (obj.name=="") {
			errorText.text = "Please enter a name";
			return;
		}

		if (reacure) {
			for (int x = 0; x < days.transform.childCount; x++) {  //checks the days that are active
				if (days.transform.GetChild (x).GetComponent<Toggle> ().isOn)
					tempDays.Add (days.transform.GetChild (x).name);
			}	
			if (tempDays.Count == 0) {
				errorText.text = "Please pick at least one day";
				return;
			}
		} else {  //otherwise it checks to make sure the date is correct
			tempDays.Add ("none");
			if(date.transform.GetChild(0).gameObject.GetComponent<InputField> ().text.StartsWith("0"))
				dateString=date.transform.GetChild(0).gameObject.GetComponent<InputField> ().text.Remove(0,1)+"/";
			else
				dateString=date.transform.GetChild(0).gameObject.GetComponent<InputField> ().text+"/";
			
			if(date.transform.GetChild(1).gameObject.GetComponent<InputField> ().text.StartsWith("0"))
				dateString+=date.transform.GetChild(1).gameObject.GetComponent<InputField> ().text.Remove(0,1)+"/";
			else
				dateString+=date.transform.GetChild(1).gameObject.GetComponent<InputField> ().text+"/";
			
			dateString+=date.transform.GetChild(2).gameObject.GetComponent<InputField> ().text;
			if (date.transform.GetChild (2).gameObject.GetComponent<InputField> ().text.Length < 4) {
				errorText.text = "Please enter a year in yyyy format";
				return;
			}
			System.DateTime tempDate;
			if (!System.DateTime.TryParse (dateString, out tempDate)) {
				errorText.text = "Please enter a correct date";
				return;
			}
		}

		obj.reacurring = reacure;
		obj.date = dateString;
		obj.Days = tempDays;
		int startH = 0;
		if (!int.TryParse (entryCanvas.transform.Find ("StartHour").GetComponent<InputField> ().text, out startH)) {  //checks through to make sure the time is right
			errorText.text = "Please enter a correct start hour";
			return;
		}
		if (startH > 12 || startH < 1) {
			errorText.text = "Please enter a correct start hour";
			return;
		}
		int startM = 0;
		if (!int.TryParse (entryCanvas.transform.Find ("StartMin").GetComponent<InputField> ().text, out startM)) {
			errorText.text = "Please enter a correct start minute";
			return;
		}
		if (startM > 60 || startM < 0) {
			errorText.text = "Please enter a correct start minute";
			return;
		}

		Dropdown timeOfDay = entryCanvas.transform.Find ("StartTime").GetComponent<Dropdown> ();
		if (startH == 12 && timeOfDay.options[timeOfDay.value].text == "AM")
			startH = 0;
		else if (startH!=12 && timeOfDay.options[timeOfDay.value].text == "PM")
			startH += 12;
		obj.Start = new System.DateTime (2001, 1, 1, startH, startM, 0);
		int endH = 0;
		if (!int.TryParse (entryCanvas.transform.Find ("EndHour").GetComponent<InputField> ().text, out endH)) {
			errorText.text = "Please enter a correct end hour";
			return;
		}
		if (endH > 12 || endH < 1) {
			errorText.text = "Please enter a correct end hour";
			return;
		}
		int endM = 0;
		if (!int.TryParse (entryCanvas.transform.Find ("EndMin").GetComponent<InputField> ().text, out endM)) {
			errorText.text = "Please enter a correct end minute";
			return;
		}
		if (endM > 60 || endM < 0) {
			errorText.text = "Please enter a correct end minute";
			return;
		}

		timeOfDay = entryCanvas.transform.Find ("EndTime").GetComponent<Dropdown> ();
		if (endH == 12 && timeOfDay.options[timeOfDay.value].text == "AM")
			endH = 0;
		else if (endH!=12 && timeOfDay.options[timeOfDay.value].text == "PM")
			endH += 12;
		obj.End = new System.DateTime (2001, 1, 1, endH, endM, 0);

		int numb = UnityEngine.Random.Range (1, 999);
		BinaryFormatter bf = new BinaryFormatter ();
		if (File.Exists (Application.persistentDataPath + "/Act/Activity" + numb + ".gd")) {
			while (File.Exists (Application.persistentDataPath + "/Act/Activity" + numb + ".gd")) 
				numb = UnityEngine.Random.Range (1, 999);
		} 
		FileStream file = File.Create (Application.persistentDataPath + "/Act/Activity" + numb + ".gd");
		bf.Serialize (file, obj);
		file.Close ();

		events.Add (obj);
		eventsWithFile.Add (obj, Application.persistentDataPath + "/Act/Activity" + numb + ".gd");
		if (onCal && !reacure) {
			print ("yes");
			System.DateTime date = System.DateTime.Parse (dateString);
			GameObject.Find ("FlatCalendar").GetComponent<FlatCalendar> ().addEvent (date.Year,date.Month,date.Day,obj);
			GameObject.Find ("FlatCalendar").GetComponent<FlatCalendar> ().refreshCalendar ();
		}
		try{			
		#if UNITY_ANDROID
			AndroidNotifications.showToast("Activity Added");
		#endif
		}
		catch(System.Exception ex) {
			Settings.errorMessage = "" + ex;
			UnityEngine.SceneManagement.SceneManager.LoadScene ("SettingsPage");
		}
		RemoveCache.fullListActs.Add (obj, Application.persistentDataPath + "/Act/Activity" + numb + ".gd");
		openClose (false);
		az = false;
		compareAz = -1;
		sorter (1);
	}
	public void typeChanger(bool oneTime)
	{
		reacure = !oneTime;
		days.SetActive (!oneTime);
		date.SetActive (oneTime);
	}
	public void delete(ActivitiesSave obj)  //sets the delete for an activity
	{
		Overview.edited = true;
		deletePrompt.SetActive (true);
		deletePrompt.transform.Find ("Delete").GetComponent<Button> ().onClick.RemoveAllListeners();
		deletePrompt.transform.Find ("Delete").GetComponent<Button> ().onClick.AddListener (() => {
			File.Delete (eventsWithFile [obj]);
			eventsWithFile.Remove (obj);
			events.Remove (obj);
			RemoveCache.fullListActs.Remove(obj);
			if(onCal)
				Overview.edited=true;
			deletePrompt.SetActive(false);
			az=false;
			compareAz=-1;
			sorter(1);
		});
		deletePrompt.transform.Find("Cancel").GetComponent<Button> ().onClick.RemoveAllListeners();
		deletePrompt.transform.Find("Cancel").GetComponent<Button> ().onClick.AddListener(()=>{deletePrompt.SetActive(false);});
	}
}