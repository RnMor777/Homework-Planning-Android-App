using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class Overview : MonoBehaviour {

	public float posY=-200;
	public GameObject assignmentLine;
	public GameObject scrollViewContent;
	public List<CalendarSwitcher.EventObj> events = new List<CalendarSwitcher.EventObj>();
	public Dictionary<CalendarSwitcher.EventObj,FileInfo> files = new Dictionary<CalendarSwitcher.EventObj,FileInfo> ();
	public int whereInList=0;
	public GameObject leftArrow;
	public GameObject rightArrow;
	public GameObject canvasView;
	public GameObject projectLine;
	public GameObject imageCan;
	List<Texture2D> picsList = new List<Texture2D> ();
	int whereInPics=0;
	public Text pageNumb;
	int whichPage=1;
	public GameObject deletePrompt;
	public GameObject editOptions;
	public Text editError;
	public Button closeImageBtn;
	int linkToImages;
	CalendarSwitcher.EventObj extendedObj;
	public ImagesNew imageScript;
	public Transform holderCanForAssign;
	bool az = false;
	int compareAz=-1;
	public static bool edited=false;

	void Start()
	{
		foreach (CalendarSwitcher.EventObj x in RemoveCache.fullListAssigns.Keys) {
			if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name == "LateNew") {
				System.DateTime tempDate = System.DateTime.Parse (x.dueDate);
				tempDate = tempDate.AddHours (23);
				tempDate = tempDate.AddMinutes (59);
				if (tempDate > System.DateTime.Now) {
					continue;
				}
			}
			events.Add (x);
			files.Add (x, RemoveCache.fullListAssigns [x]);
		}
		events = events.OrderBy (e => System.DateTime.Parse(e.dueDate)).ToList ();  //sorts the list by due date
		creation ();
	}
	public void sorter(int whereToSort)
	{
		rightArrow.SetActive (false);
		leftArrow.SetActive (false);
		whereInList = 0;
		switch (whereToSort) {  //sorts everything based on different conditions
		case 1:
			events = events.OrderBy (e => System.DateTime.Parse (e.dueDate)).ToList ();
			break;
		case 2:
			events = events.OrderBy (e => e.className).ToList ();
			break;
		case 3:
			events = events.OrderBy (e => 
				e.priority == "High"?1:
				e.priority == "Medium"?2:
				3).ToList ();
			break;
		case 4:
			events = events.OrderBy (e => e.type).ToList ();
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
			x.transform.Find ("Button").GetComponent<Button> ().onClick.RemoveAllListeners ();
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
		foreach (CalendarSwitcher.EventObj y in events) {
			if (whereInList < 4) {  //loads 4 assignments max
				GameObject newRow = Instantiate (assignmentLine);
				newRow.transform.Find ("Name").GetComponent<Text> ().text = y.name;  //sets the values
				newRow.transform.Find ("Type").GetComponent<Text> ().text = y.type;
				newRow.transform.Find ("Class").GetComponent<Text> ().text = y.className;
				if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name == "LateNew") {
					System.DateTime tempDate = System.DateTime.Parse (y.dueDate);
					string tempString = (System.DateTime.Now.Subtract (tempDate)).ToString ();
					newRow.transform.Find ("DueDate").GetComponent<Text> ().text = tempString.Substring (0, tempString.IndexOf (".")) + " Days Overdue";
				}
				else
					newRow.transform.Find ("DueDate").GetComponent<Text> ().text = y.dueDate;
				newRow.name = "assignment";
				newRow.transform.Find ("Button").GetComponent<Button> ().onClick.AddListener (()=>{seeMore(y);});  //loads the more overview
				if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name=="Calendar")
					newRow.transform.Find ("Button").GetComponent<Button> ().onClick.AddListener (()=>{this.GetComponentInParent<CalendarController>().overviewX.SetActive(false);});
				Color prioColor = new Color (0, 0, 0);  //does priority colors
				switch (y.priority) {
				case "High":
					prioColor = new Color (1, 0, 0);
					break;
				case "Low":
					prioColor = new Color (0, 1, 0);
					break;
				case "Medium":
					prioColor = new Color (1, 1, 0);
					break;
				}
				newRow.transform.Find ("Prio").GetComponent<RawImage> ().color = prioColor;
				newRow.transform.SetParent (holderCanForAssign.transform);  //sets it on the canvas
				newRow.transform.localPosition = new Vector2 (0, posY);
				newRow.transform.localScale = new Vector3 (1, 1, 1);
				posY -= 200;
				whereInList++;
			} else {
				rightArrow.SetActive (true);  //if it wants to do more than 4 it sets the next page button active
				break;
			}
		}
	}
	public void loadMore(bool right)  //if the button was the right arrow
	{
		whichPage = right ? whichPage + 1 : whichPage - 1;
		pageNumb.text = whichPage + "/" + Mathf.Ceil ((float)((events.Count/4.0)-.1));  //sets the page numbers
		if (pageNumb.text == "1/0")
			pageNumb.text = "1/1"; 
		
		int tempInt = whereInList;
		posY = -200;
		int trackHowManyObjs = 0;
		foreach (GameObject x in GameObject.FindGameObjectsWithTag("OverviewRow")) {  //gets rid of any listeneres then destroys
			trackHowManyObjs++;
			x.transform.Find ("Button").GetComponent<Button> ().onClick.RemoveAllListeners ();
			Destroy (x);
		}
		if (!right) {  //corrects the where in list
			whereInList -= trackHowManyObjs;
			whereInList -= 4;
		}
		tempInt = whereInList;
		for (int i = tempInt; right ? (i < tempInt + 4 && i <= events.Count - 1) : (i >= tempInt && i<tempInt+4); i++) {  //runs for different conditions				
			GameObject newRow = Instantiate (assignmentLine);
			newRow.transform.Find ("Name").GetComponent<Text> ().text = events [whereInList].name;  //sets the data
			newRow.transform.Find ("Type").GetComponent<Text> ().text = events [whereInList].type;
			newRow.transform.Find ("Class").GetComponent<Text> ().text = events [whereInList].className;
			if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name == "LateNew") {
				System.DateTime tempDate = System.DateTime.Parse (events [whereInList].dueDate);
				string tempString = (System.DateTime.Now.Subtract (tempDate)).ToString ();
				newRow.transform.Find ("DueDate").GetComponent<Text> ().text = tempString.Substring (0, tempString.IndexOf (".")) + " Days Overdue";
			}
			else
				newRow.transform.Find ("DueDate").GetComponent<Text> ().text = events [whereInList].dueDate;
			newRow.name = "assignment";
			int localInt = whereInList;
			newRow.transform.Find ("Button").GetComponent<Button> ().onClick.AddListener (()=>{seeMore(events[localInt]);});  //sets the listeners for extended view
			Color prioColor = new Color (0, 0, 0);
			switch (events [whereInList].priority) {
			case "High":
				prioColor = new Color (1, 0, 0);
				break;
			case "Low":
				prioColor = new Color (0, 1, 0);
				break;
			case "Medium":
				prioColor = new Color (1, 1, 0);
				break;
			}
			newRow.transform.Find ("Prio").GetComponent<RawImage> ().color = prioColor;
			newRow.transform.SetParent (holderCanForAssign.transform);  //sets the parents
			newRow.transform.localPosition = new Vector2 (0, posY);
			newRow.transform.localScale = new Vector3 (1, 1, 1);
			posY -= 200;
			whereInList++;
		}
		leftArrow.SetActive (whereInList>4?true:false);  //sets button viewability
		rightArrow.SetActive ((events.Count - 1 >= whereInList) ? true : false);
	}
	public static Texture2D LoadPNG(string filePath) //loads the image from file.
	{
		Texture2D tex = null;
		byte[] fileData;
		if (File.Exists (filePath)) 
		{
			fileData = File.ReadAllBytes (filePath);
			tex = new Texture2D (2, 2);
			tex.LoadImage (fileData);
		}
		return tex;
	}
	public void seeMore(CalendarSwitcher.EventObj obj)
	{
		canvasView.SetActive (true);
		canvasView.transform.Find ("Name").GetComponent<Text> ().text = obj.name;   //sets the more page data based on the object
		canvasView.transform.Find ("DueDate").GetComponent<Text> ().text = "Due Date: " + obj.dueDate;
		canvasView.transform.Find ("Class").GetComponent<Text> ().text = "Class: " + obj.className;
		canvasView.transform.Find ("Type").GetComponent<Text> ().text = "Type: " + obj.type;
		canvasView.transform.Find ("Priority").GetComponent<Text> ().text = "Priority: " + obj.priority;
		picsList.Clear();
		DirectoryInfo info = new DirectoryInfo (Application.persistentDataPath+"/Images");  //finds the images associated with it
		FileInfo[] fileInfo = info.GetFiles ();
		foreach (FileInfo file in fileInfo) 
		{
			if (file.Name.Substring (0, 6) == obj.link.ToString ())
				picsList.Add (LoadPNG (file.FullName));
		}						
		//sets button listenes
		extendedObj=obj;
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name != "EstimatedTime") {
			canvasView.transform.Find ("Images").GetComponent<Text> ().text="Images: "+picsList.Count;
			canvasView.transform.Find ("Done").GetComponent<Button> ().onClick.RemoveAllListeners ();
			canvasView.transform.Find ("Done").GetComponent<Button> ().onClick.AddListener (() => {startDelete ();});
			canvasView.transform.Find ("Edit").GetComponent<Button> ().onClick.RemoveAllListeners ();
			canvasView.transform.Find ("Edit").GetComponent<Button> ().onClick.AddListener (() => {editAssign ();});
			canvasView.transform.Find ("Images").GetComponent<Button> ().onClick.RemoveAllListeners ();
		} 
		else {
			int hours = (int)(obj.expectedTime / 60);
			int minutes = (int)(obj.expectedTime % 60);
			if(hours+minutes!=0)					
				canvasView.transform.Find ("Estimate").GetComponent<Text> ().text="Time Estimate: "+hours+":"+(minutes<10?"0":"")+minutes;
			else
				canvasView.transform.Find ("Estimate").GetComponent<Text> ().text="Time Estimate: Not Availible";
			canvasView.transform.Find ("Delete").GetComponent<Button> ().onClick.RemoveAllListeners ();
			canvasView.transform.Find ("Delete").GetComponent<Button> ().onClick.AddListener (() => {deleteRestore (true);});
			canvasView.transform.Find ("Restore").GetComponent<Button> ().onClick.RemoveAllListeners ();
			canvasView.transform.Find ("Restore").GetComponent<Button> ().onClick.AddListener (() => {deleteRestore (false);});
		}
		linkToImages = obj.link;
		if (picsList.Count > 0)
			canvasView.transform.Find ("Images").GetComponent<Button> ().onClick.AddListener (()=>{lookAtImages();});
		if (obj.type == "Project") {
			int posLine = -350;
			canvasView.transform.Find ("ProjectLabel").gameObject.SetActive (true);
			for (int i = 0; i <= obj.project.Count / 2; i += 2) {
				GameObject newProj = Instantiate (projectLine);  //for any project lines it will do this
				newProj.transform.Find ("ProjectName").GetComponent<Text> ().text = obj.project [i];
				newProj.transform.Find ("ProjectDue").GetComponent<Text> ().text = obj.project [i+1];
				newProj.name="projLine";
				newProj.transform.SetParent (canvasView.transform);
				newProj.transform.SetSiblingIndex (1);
				newProj.transform.localPosition = new Vector2 (0, posLine);
				newProj.transform.localScale = new Vector3 (1, 1, 1);
				posLine -= 100;
			}
		}
	}
	public void startDelete()
	{
		deletePrompt.SetActive (true);
		deletePrompt.transform.Find ("Cancel").GetComponent<Button> ().onClick.RemoveAllListeners ();  //removes any listeners
		deletePrompt.transform.Find ("Delete").GetComponent<Button> ().onClick.RemoveAllListeners ();
		deletePrompt.transform.Find ("Mark").GetComponent<Button> ().onClick.RemoveAllListeners ();

		deletePrompt.transform.Find ("Cancel").GetComponent<Button> ().onClick.AddListener(()=>{  //adds the listeners to buttons
			deletePrompt.transform.Find ("Hours").GetComponent<InputField> ().text = "";
			deletePrompt.transform.Find ("Minutes").GetComponent<InputField> ().text = "";
			deletePrompt.SetActive (false);
		});
		deletePrompt.transform.Find ("Delete").GetComponent<Button> ().onClick.AddListener(()=>{markAsDone(true);});
		deletePrompt.transform.Find ("Mark").GetComponent<Button> ().onClick.AddListener(()=>{markAsDone(false);});
	}
	public void markAsDone(bool completely)
	{
		if (!completely) {  //if marked as archive
			string fileName = files [extendedObj].Name;  //gets the file name
			string newFile = Application.persistentDataPath + "/Finished/" + fileName;  //the new file name
			
			int estTime = 0;
			if (deletePrompt.transform.Find ("Hours").GetComponent<InputField> ().text != "")  //sets the estimated time through input fields
				estTime = 60*int.Parse(deletePrompt.transform.Find ("Hours").GetComponent<InputField> ().text);
			if(deletePrompt.transform.Find("Minutes").GetComponent<InputField>().text!="")	
				estTime += int.Parse(deletePrompt.transform.Find ("Minutes").GetComponent<InputField> ().text);
			
			CalendarSwitcher.EventObj objNew = extendedObj;  //sets a new object as the old with the estimated time changed
			objNew.expectedTime = estTime;
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (newFile);   //creates the new file at the new path
			bf.Serialize (file, objNew);
			file.Close ();
			Dictionary<string,System.DateTime> tempDict = new Dictionary<string,System.DateTime>();  //creates a dictionary to store as a log of assignments to auto delete after a month
			if (File.Exists (Application.persistentDataPath + "/Finished/Log.gd")) {
				FileStream file2 = File.Open (Application.persistentDataPath + "/Finished/Log.gd", FileMode.Open);  
				tempDict = (Dictionary<string,System.DateTime>)bf.Deserialize (file2);
				file2.Close ();
				File.Delete (Application.persistentDataPath + "/Finished/Log.gd");
			}
			FileStream log = File.Create (Application.persistentDataPath+"/Finished/Log.gd");  //creates the file
			tempDict.Add (newFile, System.DateTime.Now);
			bf.Serialize (log, tempDict);
			log.Close ();
		}

		
		DirectoryInfo images = new DirectoryInfo (Application.persistentDataPath+"/Images"); //all of the images 
		FileInfo[] imagesInfo = images.GetFiles ();
		foreach (FileInfo x in imagesInfo) 
		{
			if (x.Name.StartsWith(extendedObj.link.ToString())) {   //either moves the images or deletes them
				if (completely)
					File.Delete (x.FullName);
				else 
					File.Move (Application.persistentDataPath + "/Images/" + x.Name, Application.persistentDataPath + "/Finished/Images/" + x.Name);
			}
		}
		File.Delete (files [extendedObj].FullName);  //deletes the old file and removes it from the lists
		RemoveCache.fullListAssigns.Remove(extendedObj);
		events.Remove (extendedObj);
		files.Remove (extendedObj);

		deletePrompt.transform.Find ("Hours").GetComponent<InputField> ().text = "";   //clears the estimated time slots
		deletePrompt.transform.Find ("Minutes").GetComponent<InputField> ().text = "";
		edited = true;
		closeExtendView ();
	}
	public void closeExtendView()
	{
		foreach(GameObject x in GameObject.FindGameObjectsWithTag ("ExProjectSlot"))  //detroys any project assignment lines
			Destroy(x);
		picsList.Clear ();  //clears and associated images
		deletePrompt.SetActive (false);
		canvasView.transform.Find ("ProjectLabel").gameObject.SetActive (false);  //sets the project label to inactive
		canvasView.SetActive (false);
		if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name=="Calendar")
			this.GetComponentInParent<CalendarController>().overviewX.SetActive(true);
		az = false;
		compareAz = -1;
		sorter (1);  //resorts everything just in case something was deleted
	}
	public void editAssign()
	{
		canvasView.transform.Find ("Name").GetComponent<Text> ().text="";
		canvasView.transform.Find ("DueDate").GetComponent<Text> ().text="Due Date:";  //resets every text ln=ine
		canvasView.transform.Find ("Class").GetComponent<Text> ().text="Class:";
		canvasView.transform.Find ("Type").GetComponent<Text> ().text="Type:";
		canvasView.transform.Find ("Priority").GetComponent<Text> ().text="Priority:";

		canvasView.transform.Find ("Name/InputField").gameObject.SetActive (true);  //opens input fields
		canvasView.transform.Find ("DueDate/Month").gameObject.SetActive (true);
		canvasView.transform.Find ("DueDate/Day").gameObject.SetActive (true);
		canvasView.transform.Find ("DueDate/Year").gameObject.SetActive (true);
		canvasView.transform.Find ("Class/InputField").gameObject.SetActive (true);
		canvasView.transform.Find ("Type/Field").gameObject.SetActive (true);
		canvasView.transform.Find ("Priority/Field").gameObject.SetActive (true);
		editOptions.SetActive (true);
		editOptions.transform.Find ("Save").GetComponent<Button> ().onClick.RemoveAllListeners();  //listeners for saving
		editOptions.transform.Find ("Save").GetComponent<Button> ().onClick.AddListener(()=>{saveEdits();});
		editOptions.transform.Find ("Cancel").GetComponent<Button> ().onClick.RemoveAllListeners();
		editOptions.transform.Find ("Cancel").GetComponent<Button> ().onClick.AddListener(()=>{closeEdit();});

		canvasView.transform.Find ("Name/InputField").GetComponent<InputField> ().text = extendedObj.name;
		System.DateTime tempDate = System.DateTime.Parse(extendedObj.dueDate);
		canvasView.transform.Find ("DueDate/Month").GetComponent<InputField> ().text = tempDate.Month.ToString(); //fills the fields with previous data
		canvasView.transform.Find ("DueDate/Day").GetComponent<InputField> ().text = tempDate.Day.ToString();
		canvasView.transform.Find ("DueDate/Year").GetComponent<InputField> ().text = tempDate.Year.ToString();
		canvasView.transform.Find ("Class/InputField").GetComponent<InputField> ().text = extendedObj.className;
		canvasView.transform.Find ("Type/Field").GetComponent<Dropdown> ().captionText.text = extendedObj.type;
		canvasView.transform.Find ("Priority/Field").GetComponent<Dropdown> ().captionText.text = extendedObj.priority;
	}
	public void saveEdits()  //checks conditions and then saves the edits
	{
		CalendarSwitcher.EventObj newObj = extendedObj;
		string path = files [extendedObj].FullName;

		if (canvasView.transform.Find ("Name/InputField").GetComponent<InputField> ().text == "") {
			editError.text = "Please Enter a Name";
			return;
		}			
		newObj.name=canvasView.transform.Find ("Name/InputField").GetComponent<InputField> ().text;

		string testDate = canvasView.transform.Find ("DueDate/Month").GetComponent<InputField> ().text;
		testDate+="/"+canvasView.transform.Find ("DueDate/Day").GetComponent<InputField> ().text;
		testDate+="/"+canvasView.transform.Find ("DueDate/Year").GetComponent<InputField> ().text;
		if (canvasView.transform.Find ("DueDate/Year").GetComponent<InputField> ().text.Length < 4) {
			editError.text = "Please enter a year in yyyy format";
			return;
		}
		System.DateTime tempDate;
		if (!System.DateTime.TryParse (testDate, out tempDate)) {
			editError.text = "Please Enter a Valid Date";
			return;
		}
		newObj.dueDate = testDate;

		if (canvasView.transform.Find ("Class/InputField").GetComponent<InputField> ().text == "") {
			editError.text = "Please Enter a Name";
			return;
		}	
		newObj.className=canvasView.transform.Find ("Class/InputField").GetComponent<InputField> ().text;
		newObj.type=canvasView.transform.Find ("Type/Field").GetComponent<Dropdown> ().captionText.text;
		newObj.priority = canvasView.transform.Find ("Priority/Field").GetComponent<Dropdown> ().captionText.text;
		File.Delete (path);
		RemoveCache.fullListAssigns.Remove (extendedObj);
		files.Remove (extendedObj);
		events.Remove (extendedObj);

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (path);
		bf.Serialize (file, newObj);
		file.Close ();
		FileInfo forDict = new FileInfo (path);
		RemoveCache.fullListAssigns.Add (newObj, forDict);
		files.Add (newObj, forDict);
		events.Add (newObj);
		extendedObj = newObj;
		edited = true;
		closeEdit ();
	}
	public void closeEdit()
	{
		editError.text = "";
		editOptions.SetActive (false);
		canvasView.transform.Find ("Name/InputField").gameObject.SetActive (false);  //closes all the edits and sets text fields
		canvasView.transform.Find ("DueDate/Month").gameObject.SetActive (false);
		canvasView.transform.Find ("DueDate/Day").gameObject.SetActive (false);
		canvasView.transform.Find ("DueDate/Year").gameObject.SetActive (false);
		canvasView.transform.Find ("Class/InputField").gameObject.SetActive (false);
		canvasView.transform.Find ("Type/Field").gameObject.SetActive (false);
		canvasView.transform.Find ("Priority/Field").gameObject.SetActive (false);

		canvasView.transform.Find ("Name").GetComponent<Text> ().text=extendedObj.name;
		canvasView.transform.Find ("DueDate").GetComponent<Text> ().text = "Due Date: " + extendedObj.dueDate;
		canvasView.transform.Find ("Class").GetComponent<Text> ().text="Class: "+extendedObj.className;
		canvasView.transform.Find ("Type").GetComponent<Text> ().text="Type: "+extendedObj.type;
		canvasView.transform.Find ("Priority").GetComponent<Text> ().text="Priority: "+extendedObj.priority;
	}
	public void lookAtImages() //still to-do
	{
		//GameObject.Find ("Control").GetComponent<ImagesNew> ().images.Clear ();
		//foreach (Texture2D x in picsList)
		//	GameObject.Find ("Control").GetComponent<ImagesNew> ().images.Add (x);
		//GameObject.Find ("Control").GetComponent<ImagesNew> ().reopenImages ();
	}
	public void takeNewImages()  //this is listener for adding new images
	{
		closeImageBtn.onClick.RemoveAllListeners ();
		closeImageBtn.onClick.AddListener (() => {updateImages ();});
		imageScript.images.Clear ();
		foreach (Texture2D x in picsList)
			imageScript.images.Add (x);
		imageScript.startImage ();
	}
	public void updateImages()  //update images
	{
		picsList.Clear ();
		foreach (Texture2D x in imageScript.images)
			picsList.Add (x);
		if (linkToImages != 0) {
			DirectoryInfo images = new DirectoryInfo (Application.persistentDataPath+"/Images"); //all of the images 
			FileInfo[] imagesInfo = images.GetFiles ();
			foreach (FileInfo x in imagesInfo) {
				if (x.Name.StartsWith(linkToImages.ToString()))
					File.Delete(x.FullName);
			}
		}
		else
			linkToImages=imageScript.genLink();
		imageScript.linkFinal = linkToImages;
		canvasView.transform.Find ("Images").GetComponent<Text> ().text="Images: "+picsList.Count;  //sets button listeners
		canvasView.transform.Find ("Images").GetComponent<Button> ().onClick.RemoveAllListeners();
		if (picsList.Count > 0)
			canvasView.transform.Find ("Images").GetComponent<Button> ().onClick.AddListener (()=>{lookAtImages();});
		string filePathLink = files [extendedObj].FullName;
		CalendarSwitcher.EventObj newObj = extendedObj;
		RemoveCache.fullListAssigns.Remove (extendedObj);
		files.Remove (extendedObj);
		events.Remove (extendedObj);

		if (picsList.Count == 0)
			newObj.link = 0;
		else if (extendedObj.link == 0)
			newObj.link = linkToImages;
		
		File.Delete (filePathLink);
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file2 = File.Create (filePathLink);
		bf.Serialize (file2, newObj);
		file2.Close ();
		FileInfo forDict = new FileInfo (filePathLink);
		files.Add (newObj, forDict);
		events.Add (newObj);
		extendedObj = newObj;

		imageScript.addLetter ();
	}
	public void deleteRestore(bool delete) //either deletes or restores
	{
		if (delete)
			File.Delete (files [extendedObj].FullName);
		else {
			string fileName = files [extendedObj].Name;
			CalendarSwitcher.EventObj objNew = extendedObj;  //sets a new object as the old with the estimated time changed
			objNew.expectedTime = 0;
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Application.persistentDataPath+"/"+fileName);   //creates the new file at the new path
			bf.Serialize (file, objNew);
			file.Close ();
			Dictionary<string,System.DateTime> tempDict = new Dictionary<string,System.DateTime> ();  //creates a dictionary to store as a log of assignments to auto delete after a month
			if (File.Exists (Application.persistentDataPath + "/Finished/Log.gd")) {
				FileStream file2 = File.Open (Application.persistentDataPath + "/Finished/Log.gd", FileMode.Open);  
				tempDict = (Dictionary<string,System.DateTime>)bf.Deserialize (file2);
				file2.Close ();
				File.Delete (Application.persistentDataPath + "/Finished/Log.gd");
			}
			FileStream log = File.Create (Application.persistentDataPath + "/Finished/Log.gd");  //creates the file
			tempDict.Remove (Application.persistentDataPath + "/Finished/" + fileName);
			bf.Serialize (log, tempDict);
			log.Close ();
			RemoveCache.fullListAssigns.Add(objNew,new FileInfo(Application.persistentDataPath+"/"+fileName));
		}

		DirectoryInfo images = new DirectoryInfo (Application.persistentDataPath+"/Finished/Images"); //all of the images 
		FileInfo[] imagesInfo = images.GetFiles ();
		foreach (FileInfo x in imagesInfo) 
		{
			if (x.Name.StartsWith(extendedObj.link.ToString())) {   //either moves the images or deletes them
				if (delete)
					File.Delete (x.FullName);
				else 
					File.Move (Application.persistentDataPath + "/Finished/Images/" + x.Name, Application.persistentDataPath + "/Images/" + x.Name);
			}
		}
		File.Delete (files [extendedObj].FullName);  //deletes the old file and removes it from the lists
		events.Remove (extendedObj);
		files.Remove (extendedObj);
		closeExtendView ();
	}
}