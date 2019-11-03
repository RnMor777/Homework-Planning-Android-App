using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class CalendarController : MonoBehaviour {

	public static int curDay=System.DateTime.Now.Day;
	public static int curMonth=System.DateTime.Now.Month; //these are set from listeners in the flatcalendar script
	public static int curYear=System.DateTime.Now.Year;
	public GameObject canvasEntry;
	public GameObject dateField;
	public FlatCalendar flatCal;
	public Overview overForCal;
	public ActivityNew actScript;
	public ImagesNew imageScript;
	public Entry entryScript;
	public CalendarSwitcher switcherScript;
	public Text dayText;
	System.DateTime forDate;
	public GameObject hwSort;
	public GameObject actSort;
	public GameObject canvasEvents;
	public GameObject overviewX;
	public GameObject canvasFilter;
	public GameObject toggleInstan;
	public GameObject whatViewTog;
	public GameObject classesTog;
	public GameObject typeTog;
	public GameObject prioTog;
	bool[] isOn = new bool[3]{false,false,false};
	Dictionary<string,bool> whatChecked = new Dictionary<string,bool> () { //this is used inorder to have the filter apply
		{"Homework", true},
		{"Test", true},
		{"Quiz", true},
		{"Reading", true},
		{"Presentation", true},
		{"Lab", true},
		{"Book Work", true},
		{"Study Guide", true},
		{"High", true},
		{"Medium", true},
		{"Low", true} 
	};
	List<string> myClasses = new List<string> ();
	public GameObject contentView;
	public GameObject addTypeBtn;

	public void addSomething(bool open) {  //opens the add box, either acts or hws
		addTypeBtn.SetActive (open);
	}
	public void AddEvent() {   //this sets up the entry canvas that the entry script controls
		canvasEntry.SetActive (true);
		entryScript.imageAmounts.SetActive (false);
		imageScript.images.Clear ();
		canvasEntry.transform.Find ("Name").GetComponent<InputField> ().text = "";
		canvasEntry.transform.Find ("Class").GetComponent<InputField> ().text = "";
		dateField.transform.GetChild(0).gameObject.GetComponent<InputField> ().text = ""+curMonth;
		dateField.transform.GetChild(0).gameObject.GetComponent<InputField> ().interactable = false;
		dateField.transform.GetChild(1).gameObject.GetComponent<InputField> ().text = ""+curDay;
		dateField.transform.GetChild(1).gameObject.GetComponent<InputField> ().interactable = false;
		dateField.transform.GetChild(2).gameObject.GetComponent<InputField> ().text = ""+curYear;
		dateField.transform.GetChild(2).gameObject.GetComponent<InputField> ().interactable = false;
		canvasEntry.transform.Find ("Priority").GetComponent<Dropdown> ().value = 0;
		canvasEntry.transform.Find ("Type").GetComponent<Dropdown> ().value = 0;
		canvasEntry.transform.Find ("EnterAll").GetComponent<Text> ().text = "";
	}

	public void eventsOnDay()  //this starts the adding of events to the overview canvas for that day
	{
		canvasEvents.SetActive (true);
		foreach (GameObject y in GameObject.FindGameObjectsWithTag("OverviewRow")) {
			y.transform.Find("Button").GetComponent<Button> ().onClick.RemoveAllListeners ();
			Destroy (y);
		}

		actScript.deletePrompt.SetActive (false);
		actSort.SetActive (false);
		hwSort.SetActive (true);
		forDate = new System.DateTime(curYear,curMonth,curDay);
		dayText.text = forDate.ToString ("MMMM") + " " + curDay + ", " + curYear;
		foreach (CalendarSwitcher.EventObj x in RemoveCache.fullListAssigns.Keys) {
			if (x.dueDate.Equals (curMonth + "/" + curDay + "/" + curYear)) {
				overForCal.files.Add (x, RemoveCache.fullListAssigns[x]);
				overForCal.events.Add (x);
			}
		}
		overForCal.events = overForCal.events.OrderBy (e => System.DateTime.Parse(e.dueDate)).ToList ();
		overForCal.creation ();
	}
	public void closeOverview()  //closes the overview and then repopulates everything if something was edited
	{
		foreach (GameObject x in GameObject.FindGameObjectsWithTag("OverviewRow")) {
			x.transform.Find("Button").GetComponent<Button> ().onClick.RemoveAllListeners ();
			Destroy (x);
		}
		overForCal.posY = -200;
		overForCal.events.Clear ();
		overForCal.files.Clear ();
		overForCal.whereInList = 0;
		actScript.posY = -200;
		actScript.events.Clear ();
		actScript.eventsWithFile.Clear ();
		actScript.whereInList = 0;
		overForCal.closeExtendView ();
		canvasEvents.SetActive (false);
		actScript.entryCanvas.SetActive (false);

		if (Overview.edited) {
			flatCal.removeAllCalendarEvents ();
			switcherScript.repopulateEvents (new System.DateTime(curYear,curMonth,curDay));
		}
		Overview.edited = false;
	}
	public void closeEntry()  //this is the cancel for the entry
	{
		canvasEntry.SetActive (false);
	}
	public void showActs()  //this starts loading the activities on the overview page
	{
		overForCal.posY = -200;
		overForCal.events.Clear ();
		overForCal.files.Clear ();
		overForCal.whereInList = 0;
		actScript.posY = -200;
		actScript.events.Clear ();
		actScript.eventsWithFile.Clear ();
		actScript.whereInList = 0;
		actSort.SetActive (true);
		hwSort.SetActive (false);
		actScript.deletePrompt.SetActive (false);
		foreach (GameObject x in GameObject.FindGameObjectsWithTag ("OverviewRow")) {
			x.transform.Find("Button").GetComponent<Button> ().onClick.RemoveAllListeners ();
			Destroy (x);
		}
		foreach (ActivityNew.ActivitiesSave x in RemoveCache.fullListActs.Keys) {
			if (x.reacurring) {
				foreach (string y in x.Days) {
					if (forDate.DayOfWeek.ToString ().StartsWith (y)) {
						actScript.events.Add (x);
						actScript.eventsWithFile.Add (x,RemoveCache.fullListActs[x]);
						break;
					}						
				}
			} else {
				if (x.date.Equals(curMonth + "/" + curDay + "/" + curYear)) {
					actScript.events.Add (x);
					actScript.eventsWithFile.Add (x, RemoveCache.fullListActs[x]);
				}					
			}
		}
		actScript.events = actScript.events.OrderBy (e => e.Name).ToList ();
		actScript.creation ();
	}
	public void showHw()  //this does the homework adding instead of the activities
	{
		overForCal.posY = -200;
		overForCal.events.Clear ();
		overForCal.files.Clear ();
		overForCal.whereInList = 0;
		actScript.posY = -200;
		actScript.events.Clear ();
		actScript.eventsWithFile.Clear ();
		actScript.whereInList = 0;
		actSort.SetActive (false);
		hwSort.SetActive (true);
		actScript.deletePrompt.SetActive (false);
		foreach(GameObject x in GameObject.FindGameObjectsWithTag ("OverviewRow")){
			x.transform.Find("Button").GetComponent<Button> ().onClick.RemoveAllListeners ();
			Destroy (x);
		}
		foreach (CalendarSwitcher.EventObj x in RemoveCache.fullListAssigns.Keys) {
			if (x.dueDate.Equals (curMonth + "/" + curDay + "/" + curYear)) {
				overForCal.files.Add (x, RemoveCache.fullListAssigns[x]);
				overForCal.events.Add (x);
			}
		}
		overForCal.events = overForCal.events.OrderBy (e => System.DateTime.Parse(e.dueDate)).ToList ();
		overForCal.creation ();
	}
	public void openFilter() {  //sets the filter active and populates it with all of the classes
		canvasFilter.SetActive (true);
		if (File.Exists (Application.persistentDataPath + "/Classes/Classes.gd")) {
			FileStream classesFile = File.Open (Application.persistentDataPath + "/Classes/Classes.gd", FileMode.Open);
			BinaryFormatter bf = new BinaryFormatter ();
			Saveclasses.Classes classesObj = (Saveclasses.Classes)bf.Deserialize (classesFile);
			foreach (string y in classesObj.perm) {
				if(!myClasses.Contains(y))
					myClasses.Add (y);
				if(!whatChecked.ContainsKey(y))
					whatChecked.Add (y, true);
			}
			classesFile.Close ();
		}
	}
	public void closeFilter() {  //closes the filter and checks through the conditions changed and only adds the ones that are checked to the calendar
		FlatCalendar flatCalendar = flatCal.GetComponent<FlatCalendar>();
		flatCalendar.removeAllCalendarEvents ();
		canvasFilter.SetActive (false);
		int whatChild = 0; //used to decide what toggle view is being used
		for (int i = 0; i < whatViewTog.transform.childCount; i++)
			if (whatViewTog.transform.GetChild (i).GetComponent<Toggle> ().isOn) {
				whatChild = i;
				break;
			}		
		//whatChild 0 means only hw, whatChild 1 is only acts, and whatchild 2 is both
		if (whatChild == 0 || whatChild == 2) {
			foreach (CalendarSwitcher.EventObj x in RemoveCache.fullListAssigns.Keys) {
				if (whatChecked.ContainsKey (x.className)) {
					if (whatChecked [x.className] && whatChecked [x.priority] && whatChecked [x.type]) {
						System.DateTime tempDate = System.DateTime.Parse (x.dueDate);
						flatCalendar.addEvent (tempDate.Year, tempDate.Month, tempDate.Day, x);
					}
				} else if(whatChecked [x.priority] && whatChecked [x.type]) {
					System.DateTime tempDate = System.DateTime.Parse (x.dueDate);
					flatCalendar.addEvent (tempDate.Year, tempDate.Month, tempDate.Day, x);
				}
			}
		}
		if (whatChild == 1 || whatChild == 2) {
			foreach (ActivityNew.ActivitiesSave x in RemoveCache.fullListActs.Keys) {
				if (x.reacurring) {
					continue;
				}
				System.DateTime tempDate = System.DateTime.Parse (x.date);
				flatCalendar.addEvent (tempDate.Year, tempDate.Month, tempDate.Day, x);
			}
		}
		flatCalendar.refreshCalendar ();
	}
	public void expandOptions(GameObject x) {  //this opens up the fuleds and dropdowns
		float posY=-100;
		int whichGameObject = 0;
		List<string> boxNames = new List<string> ();
		x.transform.Find ("Expand").transform.localScale = new Vector3 (1, 1, 1);
		for (int a = 0; a < x.transform.parent.childCount; a++)
			if (x.transform.parent.GetChild (a).name.StartsWith ("O")) {
				x.transform.parent.GetChild (a).transform.Find ("Tog").GetComponent<Toggle> ().onValueChanged.RemoveAllListeners ();
				Destroy (x.transform.parent.GetChild (a).gameObject);
			}
		switch (x.name) {
		case "ClassesOption":
			boxNames = myClasses;
			whichGameObject = 0;
			break;
		case "TypeOption":
			boxNames = new List<string> (){ "Homework", "Test", "Quiz", "Reading", "Presentation", "Lab", "Book Work" };
			whichGameObject = 1;
			break;
		case "PrioOption":
			boxNames = new List<string> (){ "High", "Medium", "Low" };
			whichGameObject = 2;
			break;
		}
		if (!isOn [whichGameObject]) {
			foreach(string a in boxNames) {
				GameObject z = Instantiate (toggleInstan);
				z.name = "Option" + a;
				z.transform.SetParent (x.transform.parent);
				z.transform.localPosition = new Vector2 (0, posY);
				z.transform.localScale = new Vector3 (1, 1, 1);
				z.transform.Find ("Tog/Label").transform.GetComponent<Text> ().text = a;
				z.transform.Find ("Tog").GetComponent<Toggle> ().isOn = whatChecked [a];
				//this switches everything so when it changes it updates it in the array
				z.transform.Find ("Tog").GetComponent<Toggle> ().onValueChanged.AddListener (delegate {
					whatChecked[a]=z.transform.Find ("Tog").GetComponent<Toggle> ().isOn;
				});
				posY -= 100;
			}
			x.transform.Find ("Expand").transform.localScale = new Vector3 (-1, 1, 1);
			isOn [whichGameObject] = true;
		} else
			isOn [whichGameObject] = false;
		
		float tempPos = classesTog.transform.localPosition.y;  //this just readjusts everything
		if (isOn [0])
			tempPos -= 100 * (classesTog.transform.childCount - 1) + 125;
		else
			tempPos -= 125;
		typeTog.transform.localPosition = new Vector2 (500, tempPos);
		if (isOn [1])
			tempPos -= 100 * (typeTog.transform.childCount - 1) + 125;
		else
			tempPos -= 125;
		prioTog.transform.localPosition = new Vector2 (500, tempPos);
		if (isOn [2])
			tempPos -= 375;

		RectTransform rt = contentView.GetComponent<RectTransform>();
		rt.localPosition = new Vector2 (0, 0);
		rt.sizeDelta = new Vector2 (rt.sizeDelta.x, Mathf.Abs(tempPos+750));
	}
}
