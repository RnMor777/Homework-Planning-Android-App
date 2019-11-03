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
public class SwitchToClassView : MonoBehaviour {
	//Overall obsolete i think
	public GameObject text; 
	public GameObject Control;
	public GameObject assignmentLine;
	public GameObject leftArrow;
	public GameObject rightArrow;
	public GameObject canvasView;
	public GameObject projectLine;
	public GameObject imageCan;
	public int whereInList=0;
	float posY=-200;
	int whichPage=1;
	public Text pageNumb;
	List<Texture2D> picsList = new List<Texture2D> ();


	List<CalendarSwitcher.EventObj> events = new List<CalendarSwitcher.EventObj>();
	Dictionary<CalendarSwitcher.EventObj,FileInfo> files = new Dictionary<CalendarSwitcher.EventObj,FileInfo> ();


	/*public void LoadAssignments (string name) {
		text.GetComponent<Text> ().text = name;
		print ("LoadAssignments active!");
		print (name + "was clicked!");
	}
*/
}