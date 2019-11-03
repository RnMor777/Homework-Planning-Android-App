using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class Classes : MonoBehaviour {

		float posY=-200;
		public GameObject assignmentLine;
		public GameObject scrollViewContent;
		List<CalendarSwitcher.EventObj> events = new List<CalendarSwitcher.EventObj>();
		Dictionary<CalendarSwitcher.EventObj,FileInfo> files = new Dictionary<CalendarSwitcher.EventObj,FileInfo> ();
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

		void Start()
		{
			DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath);
			FileInfo[] fileInfo = info.GetFiles();
			BinaryFormatter bf = new BinaryFormatter ();

			foreach (FileInfo x in fileInfo) {
				FileStream file2 = File.Open (x.FullName, FileMode.Open);
				CalendarSwitcher.EventObj obj = (CalendarSwitcher.EventObj)bf.Deserialize (file2);
				files.Add (obj, x);
				events.Add (obj);
				file2.Close ();
			} 
			events = events.OrderBy (e => System.DateTime.Parse(e.dueDate)).ToList ();
			creation ();
		}
		public void sorter(int whereToSort)
		{
			rightArrow.SetActive (false);
			leftArrow.SetActive (false);
			whereInList = 0;
			switch (whereToSort) {
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
			foreach (GameObject x in GameObject.FindGameObjectsWithTag("OverviewRow")) {
				x.transform.Find ("Button").GetComponent<Button> ().onClick.RemoveAllListeners ();
				Destroy (x);
			}
			posY = -200;
			creation ();
		}
		void creation()
		{
			whichPage = 1;
			pageNumb.text = whichPage + "/" + Mathf.Ceil ((float)((events.Count/4.0)-.1)); 
			foreach (CalendarSwitcher.EventObj y in events) {
				if (whereInList < 4) {
					GameObject newRow = Instantiate (assignmentLine);
					newRow.transform.Find ("Name").GetComponent<Text> ().text = y.name;
					newRow.transform.Find ("Type").GetComponent<Text> ().text = y.type;
					newRow.transform.Find ("Class").GetComponent<Text> ().text = y.className;
					newRow.transform.Find ("DueDate").GetComponent<Text> ().text = y.dueDate;
					newRow.name = "assignment";
					newRow.transform.Find ("Button").GetComponent<Button> ().onClick.AddListener (()=>{seeMore(y);});
					Color prioColor = new Color (0, 0, 0);
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
					newRow.transform.SetParent (GameObject.Find ("Canvas").transform);//scrollViewContent.transform);
					newRow.transform.localPosition = new Vector2 (0, posY);
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
			pageNumb.text = whichPage + "/" + Mathf.Ceil ((float)((events.Count/4.0)-.1));

			int tempInt = whereInList;
			posY = -200;
			int trackHowManyObjs = 0;
			foreach (GameObject x in GameObject.FindGameObjectsWithTag("OverviewRow")) {
				trackHowManyObjs++;
				x.transform.Find ("Button").GetComponent<Button> ().onClick.RemoveAllListeners ();
				Destroy (x);
			}
			if (!right) {
				whereInList -= trackHowManyObjs;
				whereInList -= 4;
			}
			tempInt = whereInList;
			for (int i = tempInt; right ? (i < tempInt + 4 && i <= events.Count - 1) : (i >= tempInt && i<tempInt+4); i++) {				
				GameObject newRow = Instantiate (assignmentLine);
				newRow.transform.Find ("Name").GetComponent<Text> ().text = events [whereInList].name;
				newRow.transform.Find ("Type").GetComponent<Text> ().text = events [whereInList].type;
				newRow.transform.Find ("Class").GetComponent<Text> ().text = events [whereInList].className;
				newRow.transform.Find ("DueDate").GetComponent<Text> ().text = events [whereInList].dueDate;
				newRow.name = "assignment";
				int localInt = whereInList;
				newRow.transform.Find ("Button").GetComponent<Button> ().onClick.AddListener (()=>{seeMore(events[localInt]);});
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
				newRow.transform.SetParent (GameObject.Find ("Canvas").transform);//scrollViewContent.transform);
				newRow.transform.localPosition = new Vector2 (0, posY);
				posY -= 200;
				whereInList++;
			}
			leftArrow.SetActive (whereInList>4?true:false);
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
			canvasView.transform.Find ("Name").GetComponent<Text> ().text = obj.name;
			canvasView.transform.Find ("DueDate").GetComponent<Text> ().text = "Due Date: " + obj.dueDate;
			canvasView.transform.Find ("Class").GetComponent<Text> ().text = "Class: " + obj.className;
			canvasView.transform.Find ("Type").GetComponent<Text> ().text = "Type: " + obj.type;
			canvasView.transform.Find ("Priority").GetComponent<Text> ().text = "Priority: " + obj.priority;
			picsList.Clear();
			DirectoryInfo info = new DirectoryInfo (Application.persistentDataPath+"/Images");
			FileInfo[] fileInfo = info.GetFiles ();
			foreach (FileInfo file in fileInfo) 
			{
				print (file.Name.Substring (0, 6) + "\n" + obj.link.ToString ());
				if (file.Name.Substring (0, 6) == obj.link.ToString ())
					picsList.Add (LoadPNG (file.FullName));
			}						
			canvasView.transform.Find ("Images").GetComponent<Text> ().text="Images: "+picsList.Count;
			canvasView.transform.Find ("Done").GetComponent<Button> ().onClick.RemoveAllListeners ();
			canvasView.transform.Find ("Done").GetComponent<Button> ().onClick.AddListener (() => {markAsDone(obj);});
			if (picsList.Count > 0)
				canvasView.transform.Find ("Images").GetComponent<Button> ().onClick.AddListener (()=>{lookAtImages();});
			if (obj.type == "Project") {
				int posLine = -350;
				canvasView.transform.Find ("ProjectLabel").gameObject.SetActive (true);
				for (int i = 0; i <= obj.project.Count / 2; i += 2) {
					GameObject newProj = Instantiate (projectLine);
					newProj.transform.Find ("ProjectName").GetComponent<Text> ().text = obj.project [i];
					newProj.transform.Find ("ProjectDue").GetComponent<Text> ().text = obj.project [i+1];
					newProj.name="projLine";
					newProj.transform.SetParent (canvasView.transform);
					newProj.transform.SetSiblingIndex (1);
					newProj.transform.localPosition = new Vector2 (0, posLine);
					posLine -= 100;
				}
			}
		}
		public void markAsDone(CalendarSwitcher.EventObj obj)
		{
			foreach (KeyValuePair<CalendarSwitcher.EventObj,FileInfo> x in files) {
				print (obj.name + "\n" + x.Key.name + "\n");
			}
			string fileName = files [obj].Name;
			string newFile = Application.persistentDataPath + "/Finished/" + fileName;
			if (!System.IO.Directory.Exists (Application.persistentDataPath + "/Finished")) //creates the folder
				System.IO.Directory.CreateDirectory (Application.persistentDataPath + "/Finished");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (newFile);
			bf.Serialize (file, obj);
			file.Close ();
			FileInfo oldFile = files [obj];
			oldFile.Delete ();
			events.Remove (obj);
			files.Remove (obj);
			closeExtendView ();
		}
		public void closeExtendView()
		{
			foreach(GameObject x in GameObject.FindGameObjectsWithTag ("ExProjectSlot"))
				Destroy(x);
			picsList.Clear ();
			canvasView.transform.Find ("ProjectLabel").gameObject.SetActive (false);
			canvasView.SetActive (false);
			sorter (1);
		}
		public void lookAtImages()
		{
			imageCan.SetActive (true);
			imageCan.transform.Find("Image").GetComponent<RawImage> ().texture = picsList [0];
			imageCan.transform.Find ("leftBtn").gameObject.SetActive (false);
			if (picsList.Count >= 2)
				imageCan.transform.Find ("rightBtn").gameObject.SetActive (true);
			else
				imageCan.transform.Find ("rightBtn").gameObject.SetActive (false);
		}
		public void nextImg(bool right)
		{
			if (right && whereInList+1<picsList.Count) {
				imageCan.GetComponent<RawImage> ().texture = picsList [whereInList + 1];
				whereInList++;
			} else if(!right && whereInList>0) {
				imageCan.GetComponent<RawImage> ().texture = picsList [whereInList - 1];
				whereInList--;
			}
			imageCan.transform.Find ("rightBtn").gameObject.SetActive ((whereInList + 1) <= (picsList.Count - 1) ? true : false);
			imageCan.transform.Find ("leftBtn").gameObject.SetActive ((whereInList - 1) >= 0 ? true : false);
		}
	}
