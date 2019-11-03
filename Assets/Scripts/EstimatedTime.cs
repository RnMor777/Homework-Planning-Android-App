using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class EstimatedTime : MonoBehaviour {

	public float posY=-150;
	public GameObject assignmentLine;
	public GameObject estimatedTimeLine;
	public List<CalendarSwitcher.EventObj> events = new List<CalendarSwitcher.EventObj>();
	public Dictionary<CalendarSwitcher.EventObj,string> files = new Dictionary<CalendarSwitcher.EventObj,string> ();
	public int whereInList=0;
	public Overview overScript;
	public Transform holderCanForAssign;
	public Dropdown viewDropdown;
	public Text pageNumb;
	List<string> classes = new List<string>();
	List<string> types = new List<string>{"Homework", "Test", "Quiz", "Reading", "Presentation", "Lab", "Book Work"};
	List<string> prios = new List<string>{"High", "Medium", "Low"};

	void Start()
	{
		typeToView (0);
	}
	public void onChangeViewer()
	{
		typeToView (viewDropdown.value);
	}
	public void typeToView(int i) //this switches what you are viewing
	{
		pageNumb.text = "1/1";
		files.Clear ();
		events.Clear();
		overScript.files.Clear();
		overScript.events.Clear();
		overScript.whereInList=0;
		posY = -150;
		foreach (GameObject oldRows in GameObject.FindGameObjectsWithTag("OverviewRow"))
			Destroy (oldRows);
		
		DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath+"/Finished");
		FileInfo[] fileInfo = info.GetFiles();
		BinaryFormatter bf = new BinaryFormatter ();

		foreach (FileInfo x in fileInfo) {  //loads all of the assignments
			FileStream file2 = File.Open (x.FullName, FileMode.Open);
			if (x.Name.StartsWith ("Log")) {
				file2.Close ();
				continue;
			}
			CalendarSwitcher.EventObj obj = (CalendarSwitcher.EventObj)bf.Deserialize (file2);
			switch (i) {
			case 0:
				files.Add (obj, obj.className);
				break;
			case 1: 	
				files.Add (obj, obj.type);
				break; 
			case 2:
				files.Add (obj, obj.priority);
				break;
			case 3:
				files.Add (obj, x.FullName);
				events.Add (obj);
				overScript.files.Add (obj, x);
				overScript.events.Add (obj);
				break;
			}
			file2.Close ();
		} 
		creation (i);
	}
	public void creation(int i)  //creates fields based on the the number
	{
		List<string> switchList = new List<string> ();
		switch(i){
		case 0: 
			if (File.Exists (Application.persistentDataPath + "/Classes/Classes.gd")) {
				FileStream classesFile = File.Open (Application.persistentDataPath + "/Classes/Classes.gd", FileMode.Open);
				BinaryFormatter bf = new BinaryFormatter ();
				Saveclasses.Classes classesObj = (Saveclasses.Classes)bf.Deserialize (classesFile);
				switchList = classesObj.perm;
				classesFile.Close ();
			}
			break;
		case 1: 
			switchList = types;
			break;
		case 2:
			switchList = prios;
			break;
		}
		if (i == 3) {  //this sends it to the voerview script if you are looking at past assignments
			overScript.posY = -200;
			overScript.events = overScript.events.OrderBy (e => System.DateTime.Parse(e.dueDate)).ToList ();
			overScript.creation ();
		}
		else {  //this sets up the rows otherwise, with switch list dynamically changing based on what view it is
			foreach (string y in switchList) {
				GameObject newRow = Instantiate (estimatedTimeLine);
				newRow.transform.Find ("Name").GetComponent<Text> ().text = y;
				List<int> avList = new List<int> ();
				foreach (CalendarSwitcher.EventObj x in files.Keys)
					if (files [x].Equals (y))
						avList.Add (x.expectedTime);
				if (avList.Count != 0) {
					double averaged = avList.Average ();
					int hours = (int)(averaged / 60);
					int minutes = (int)(averaged % 60);
					if(hours+minutes!=0)					
						newRow.transform.Find ("Time").GetComponent<Text> ().text = hours + ":" + (minutes<10?"0":"")+minutes;
					else
						newRow.transform.Find ("Time").GetComponent<Text> ().text = "Not Availible";
				} 
				else
					newRow.transform.Find ("Time").GetComponent<Text> ().text = "Not Availible";
				newRow.transform.SetParent (holderCanForAssign.transform);  //sets it on the canvas
				newRow.transform.localPosition = new Vector2 (0, posY);
				newRow.transform.localScale = new Vector3 (1, 1, 1);
				posY -= 100;
			}				
		}
	}
}
