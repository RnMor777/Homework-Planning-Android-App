using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Area730.Notifications;

public class Entry : MonoBehaviour {

	public GameObject enterAllFields;
	public GameObject nameField;
	public GameObject classField;
	public GameObject dateField;
	public GameObject priority;
	public GameObject typeOfAssign;
	List<GameObject> textFields = new List<GameObject>(); //gets a list of all text fields on entry
	List<string> entryText = new List<string>(); //the final values of entered stuff
	public List<GameObject> projectFields = new List<GameObject>();
	List<string> projectTexts = new List<string>();
	List<Texture2D> images = new List<Texture2D> ();
	public GameObject projectCanvas;
	public GameObject projectFieldPrefab;
	public GameObject addProjectButton;
	public GameObject projectAmounts;
	public GameObject imageAmounts;
	public GameObject classesDropDown;
	public GameObject backDropDown;
	public GameObject secondDates;

	int yPosProject=-150;
	bool incorrect=false;
	int linkFinal;
	public int amountOfProj;
	public GameObject control;

	public static bool fromEntry=false;
	public bool onCalendar;

	void Start() //this resets everything and loads the classes into the dropdown
	{
		enterAllFields.GetComponent<Text> ().text = "";
		textFields.Add (nameField);
		textFields.Add (classField);
		if (File.Exists (Application.persistentDataPath + "/Classes/Classes.gd")) {
			FileStream classesFile = File.Open (Application.persistentDataPath + "/Classes/Classes.gd", FileMode.Open);
			BinaryFormatter bf = new BinaryFormatter ();
			Saveclasses.Classes classesObj = (Saveclasses.Classes)bf.Deserialize (classesFile);
			foreach(string x in classesObj.perm)
				classesDropDown.GetComponent<Dropdown> ().options.Add (new Dropdown.OptionData(x));
			classesFile.Close ();
		}
		classesDropDown.GetComponent<Dropdown> ().options.Add (new Dropdown.OptionData("Other"));
		int dropDownCount = classesDropDown.GetComponent<Dropdown> ().options.Count;  //this changes the size and sets a max length
		if (dropDownCount >= 2) {
			classesDropDown.transform.Find ("Label").GetComponent<Text> ().text = classesDropDown.GetComponent<Dropdown> ().options [0].text;
			classesDropDown.transform.Find ("Template").GetComponent<RectTransform> ().sizeDelta = new Vector2 (1, 
				dropDownCount <= 5 ? 125 * dropDownCount : 625);
		} else
			classesDropDown.SetActive (false);
		fromEntry = false;
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name == "Enter") {  //this sets the dropdown for days
			for (int i = 1; i <= 31; i++)
				secondDates.transform.Find ("Day").GetComponent<Dropdown> ().options.Add (new Dropdown.OptionData ("" + i));
			for (int i = System.DateTime.Today.Year; i < System.DateTime.Today.Year + 2; i++)
				secondDates.transform.Find ("Year").GetComponent<Dropdown> ().options.Add (new Dropdown.OptionData ("" + i));
		}
	}
		
	//when the sumbit button is clicked, run this function
	public void Submiting()
	{	 
		incorrect = false;
		foreach (GameObject x in textFields) {			
			if (x.GetComponent<InputField> ().text == "") { //makes sure there is something in these fields, otherwise do this
				if (x.name == "Class" && classesDropDown.activeSelf)
					continue;
				enterAllFields.GetComponent<Text> ().text = "Please Enter All Fields";
				x.transform.Find ("star").GetComponent<Text> ().text = "*";
				incorrect = true; //makes the code know that there is an incorrect field
				return;
			}		
			else
				x.transform.Find ("star").GetComponent<Text> ().text = "";				
		}	
		string parseTest="";
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name == "Enter") {
			for (int i = 0; i < secondDates.transform.childCount; i++) {
				Dropdown field = secondDates.transform.GetChild (i).GetComponent<Dropdown> ();
				if (field.value == 0) {
					enterAllFields.GetComponent<Text> ().text = "Please Enter A Correct Date";
					incorrect = true;
					return;
				} else
					parseTest += field.options [field.value].text + "/";
			}
		} else {
			for (int i = 0; i < dateField.transform.childCount; i++) {
				InputField field = dateField.transform.GetChild (i).GetComponent<InputField> ();
				parseTest += field.text + "/";
			}
		}
		parseTest = parseTest.Remove (parseTest.Length - 1);
		System.DateTime tempDate = System.DateTime.Parse(parseTest);

		CalendarSwitcher.EventObj newObj = new CalendarSwitcher.EventObj ();
		newObj.name = nameField.GetComponent<InputField> ().text;
		Dropdown tempDrop = classesDropDown.GetComponent<Dropdown> ();
		if (classesDropDown.activeSelf)
			newObj.className = tempDrop.options [tempDrop.value].text;
		else
			newObj.className = classField.GetComponent<InputField> ().text;

		newObj.Date = parseTest;
		newObj.priority = priority.transform.Find ("Label").GetComponent<Text> ().text;  //sets values
		newObj.type = typeOfAssign.transform.Find ("Label").GetComponent<Text> ().text;
		if (newObj.type == "Project" && projectTexts.Count != 0) {
			newObj.project = projectTexts;
			newObj.isProj = true;
		} else {
			newObj.project = new List<string> ();
			newObj.isProj = false;
		}
		images = control.GetComponent<ImagesNew> ().images;
		newObj.link = images.Count != 0 ? control.GetComponent<ImagesNew> ().genLink () : 0;  //creates a link to images
		if (images.Count != 0)
			control.GetComponent<ImagesNew> ().addLetter ();
			
		newObj.expectedTime = 0;

		int numb = UnityEngine.Random.Range (1, 999);  //generates the name
		BinaryFormatter bf = new BinaryFormatter ();
		if (File.Exists (Application.persistentDataPath + "/Assignment" + numb + ".gd")) {
			while (File.Exists (Application.persistentDataPath + "/Assignment" + numb + ".gd")) {
				numb = UnityEngine.Random.Range (1, 999);
			}
		} 
		FileStream file = File.Create (Application.persistentDataPath + "/Assignment" + numb + ".gd");  //creates assignment
		bf.Serialize (file, newObj);
		file.Close ();
		try{	
			#if UNITY_ANDROID //notifications
			AndroidNotifications.showToast ("Homework Added"); //shows android toast

			if (PlayerPrefs.GetInt ("NotsTime2", 1) != 0 && System.DateTime.Now<tempDate.AddDays(-PlayerPrefs.GetInt("NotsTime2",1))) {
			tempDate=tempDate.AddDays(-PlayerPrefs.GetInt("NotsTime2",1));
			tempDate=tempDate.AddHours(PlayerPrefs.GetInt("NotsTime",7));
			Area730.Notifications.DataHolder forAList = Area730.Notifications.DataHolder.CreateInstance<DataHolder>();
			int id = forAList.notifications.Count+1;
			string body = newObj.name + " for "+newObj.className + " is due in "+PlayerPrefs.GetInt("NotsTime2",1)+ " Day(s)!";
			NotificationBuilder builder = new NotificationBuilder (id, "HWhen", body);
			builder
			.setTicker ("HWhen")
			.setDelay (tempDate.Subtract (System.DateTime.Now))
			.setDefaults (NotificationBuilder.DEFAULT_ALL)
			.setSmallIcon("ic_stat_hourglass_empty")
			.setSmallIcon ("AppLogo")
			.setAlertOnlyOnce (true)
			.setAutoCancel (true)
			.setRepeating (false);

			AndroidNotifications.scheduleNotification (builder.build ());
			}
			#endif
		}
		catch(System.Exception ex) {
			Settings.errorMessage = "" + ex;
			UnityEngine.SceneManagement.SceneManager.LoadScene ("SettingsPage");
		}

		if (!onCalendar) {  //based on active scene
			fromEntry = true;
			UnityEngine.SceneManagement.SceneManager.LoadScene ("HomeNew");
		} else {
			this.transform.GetComponentInParent<CalendarController> ().canvasEntry.SetActive (false);
			GameObject.Find ("FlatCalendar").GetComponent<FlatCalendar> ().addEvent (tempDate.Year, tempDate.Month, tempDate.Day, newObj);
			GameObject.Find ("FlatCalendar").GetComponent<FlatCalendar> ().refreshCalendar ();
		}
		RemoveCache.fullListAssigns.Add (newObj, new FileInfo (Application.persistentDataPath + "/Assignment" + numb + ".gd"));
	}
	public void openProjectsPage() //opens the projects page.
	{
		if(typeOfAssign.GetComponent<Dropdown>().value==7) //project choice
		{
			projectCanvas.SetActive (true);
			projectFields.Add (GameObject.Find ("ProjectSlotEntry"));
		}
	}
	public void addProjectSlots()  //creates new project fields
	{
		if (projectFields.Count <= amountOfProj) {
			GameObject newSlot = Instantiate (projectFieldPrefab);
			newSlot.transform.SetParent (projectCanvas.transform);
			newSlot.transform.localPosition = new Vector2 (0, yPosProject);
			newSlot.transform.localScale = new Vector3 (1, 1, 1);
			newSlot.name = "newSlot";
			int tempInt = yPosProject;
			newSlot.transform.Find ("Remove").GetComponent<Button> ().onClick.AddListener (()=>{removeProjectSlots(tempInt);});
			projectFields.Add (newSlot);
			yPosProject -= 150;
			if (projectFields.Count == amountOfProj) {
				addProjectButton.SetActive (false);
			}
		}
	}
	public void removeProjectSlots(int x)  //removes fields
	{
		Destroy (projectFields [Mathf.Abs(x / 150)]);
		for (int y = Mathf.Abs (x / 150)+1; y <= projectFields.Count - 1; y++) {
			float tempY = projectFields [y].transform.localPosition.y;
			projectFields [y].transform.localPosition = new Vector2 (0, tempY + 150);
			projectFields [y].transform.Find("Remove").GetComponent<Button> ().onClick.RemoveAllListeners ();
			projectFields [y].transform.Find("Remove").GetComponent<Button> ().onClick.AddListener (() => {removeProjectSlots((int)tempY+150);});
		}
		projectFields.RemoveAt (Mathf.Abs (x / 150));
		yPosProject += 150;
		if (projectFields.Count != amountOfProj)
			addProjectButton.SetActive (true);
	}
	public void closeProjectsPage()  //closes everything on projects
	{
		projectTexts.Clear ();
		foreach (GameObject x in projectFields) { //gets the values
			if (x.transform.Find ("Type").GetComponent<InputField> ().text != "" && x.transform.Find ("TypeDate").GetComponent<InputField> ().text != "") {
				projectTexts.Add (x.transform.Find ("Type").GetComponent<InputField> ().text);
				projectTexts.Add (x.transform.Find ("TypeDate").GetComponent<InputField> ().text);
			}
		}
		foreach (GameObject y in projectFields) { //destroys all the fields
			if (y.name != "ProjectSlotEntry")
				Destroy (y);
		}
		if (projectTexts.Count != 0) {
			projectAmounts.SetActive (true);
			projectAmounts.GetComponent<Text> ().text = "[" + projectTexts.Count / 2 + "]";
		} else
			typeOfAssign.GetComponent<Dropdown> ().value = 0;
		projectFields.Clear ();
		yPosProject = -150;
		projectCanvas.SetActive (false);
	}
	public void reopenProjects()  //when you reopen your saved projects
	{
		projectCanvas.SetActive (true);
		projectFields.Add (GameObject.Find ("ProjectSlotEntry"));
		for (int x = 2; x < projectTexts.Count; x += 2) {
			GameObject newSlot = Instantiate (projectFieldPrefab);
			newSlot.transform.SetParent (projectCanvas.transform);
			newSlot.name = "newSlot";
			newSlot.transform.localPosition = new Vector2 (0, yPosProject);
			newSlot.transform.localScale = new Vector3 (1, 1, 1);
			newSlot.transform.Find ("Type").GetComponent<InputField> ().text = projectTexts [x];
			newSlot.transform.Find ("TypeDate").GetComponent<InputField> ().text = projectTexts [x + 1];
			yPosProject -= 150;
			projectFields.Add (newSlot);
		}
		addProjectButton.SetActive (projectFields.Count < amountOfProj ? true : false);
	}
	public void starReset(GameObject x) //runs where input field changes. Removes the star
	{
		x.transform.Find("star").GetComponent<Text>().text="";
	}
	public void dropClassChanged()
	{
		Dropdown temp = classesDropDown.GetComponent<Dropdown> ();
		if (temp.options [temp.value].text == "Other") {
			backDropDown.SetActive (true);
			backDropDown.GetComponent<Button> ().onClick.AddListener (() => {
				backDropDown.SetActive (false);
				backDropDown.GetComponent<Button> ().onClick.RemoveAllListeners();
				temp.value=0;
				classesDropDown.SetActive (true);
			});
			Destroy(classesDropDown.transform.Find("Dropdown List").gameObject);
			classesDropDown.SetActive (false);
		}
	}
	public void monthYearChange(){  //this checks to make sure that the day is within the month and changes the max day 
		Dropdown months = secondDates.transform.Find ("Month").GetComponent<Dropdown> ();
		Dropdown years = secondDates.transform.Find ("Year").GetComponent<Dropdown> ();
		Dropdown days = secondDates.transform.Find ("Day").GetComponent<Dropdown>();
		if (months.value != 0 && years.value != 0) {
			int maxDays = System.DateTime.DaysInMonth (int.Parse(years.options [years.value].text), int.Parse(months.options [months.value].text));
			if (days.value > maxDays)
				days.value = 0;
			print (maxDays);
			if (days.options.Count > maxDays) {				
				for (int i = days.options.Count-1; i > maxDays; i--)
					days.options.RemoveAt (i);
			} else if (days.options.Count < maxDays) {
				for (int i = maxDays; i > days.options.Count; i--)
					days.options.Add (new Dropdown.OptionData ("" + i));
			}					
		}
	}
}
