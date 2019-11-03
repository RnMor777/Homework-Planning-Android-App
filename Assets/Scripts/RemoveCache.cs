using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class RemoveCache : MonoBehaviour {

	static bool alreadyDone = false;
	public static Dictionary<CalendarSwitcher.EventObj,FileInfo> fullListAssigns = new Dictionary<CalendarSwitcher.EventObj,FileInfo>();
	public static Dictionary<ActivityNew.ActivitiesSave,string> fullListActs = new Dictionary<ActivityNew.ActivitiesSave,string> ();

	void Start () {
		if(fullListAssigns.Count==0) {  //populates the dictionary for use in other scripts on start. Only does this once
			DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath);
			FileInfo[] fileInfo = info.GetFiles();
			BinaryFormatter bf = new BinaryFormatter ();

			foreach (FileInfo x in fileInfo) {  //loads all of the assignments
				FileStream file2 = File.Open (x.FullName, FileMode.Open);
				CalendarSwitcher.EventObj obj = (CalendarSwitcher.EventObj)bf.Deserialize (file2);
				fullListAssigns.Add (obj,x);
				file2.Close ();
			} 
		}	
		if (fullListActs.Count == 0) {  //this is the same as above but for activities
			DirectoryInfo info = new DirectoryInfo (Application.persistentDataPath + "/Act");
			FileInfo[] fileInfo = info.GetFiles ();
			if (PlayerPrefs.GetInt ("DelActs", 0) == 1) {				
				BinaryFormatter bf = new BinaryFormatter ();

				foreach (FileInfo file in fileInfo) {
					FileStream file2 = File.Open (Application.persistentDataPath + "/Act/" + file.Name, FileMode.Open);  //opens all the files and adds them to an array
					ActivityNew.ActivitiesSave obj2 = (ActivityNew.ActivitiesSave)bf.Deserialize (file2); 
					fullListActs.Add (obj2, file.FullName);
					file2.Close ();
				}
			} else {
				foreach (FileInfo file in fileInfo) {
					File.Delete (file.FullName);
				}
				PlayerPrefs.SetInt ("DelActs", 1);
			}
		}
		if (!alreadyDone) {
			int compareDays = PlayerPrefs.GetInt ("ClearCache",30);  //clears the cache using a log file

			if (System.IO.Directory.Exists (Application.persistentDataPath + "/Finished") && File.Exists (Application.persistentDataPath + "/Finished/Log.gd")) {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (Application.persistentDataPath + "/Finished/Log.gd", FileMode.Open);
				Dictionary<string,System.DateTime> temp = (Dictionary<string,System.DateTime>)bf.Deserialize (file);
				for (int i = 0; i < temp.Count; i++) {
					if (System.DateTime.Compare (temp [temp.Keys.ElementAt (i)], System.DateTime.Now) > compareDays && compareDays!=0) {
						File.Delete (temp.Keys.ElementAt (i));
						temp.Remove (temp.Keys.ElementAt (i));
					}
				}
				file.Close ();
				File.Delete (Application.persistentDataPath + "/Finished/Log.gd");
				FileStream log = File.Create (Application.persistentDataPath + "/Finished/Log.gd");
				bf.Serialize (log, temp);
				log.Close ();
			}
			alreadyDone = true;
		}
		//print (printAll ());
	}
	public string printAll() {  //this is just a simple print statement used during testing
		string toReturn="";
		foreach (CalendarSwitcher.EventObj x in RemoveCache.fullListAssigns.Keys) {
			toReturn+=""+x.name+"\n";
		}
		foreach (ActivityNew.ActivitiesSave x in RemoveCache.fullListActs.Keys) {
			toReturn+=""+x.name+"\n";
		}
		return toReturn;
	}
}
