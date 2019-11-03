using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class Saveclasses : MonoBehaviour {

	List<string> classes = new List <string> ();
	List<string> perm = new List <string> ();
	List<string> container = new List <string> ();
	public GameObject[] temp;
	public GameObject DestroyTarget;
	public GameObject DestroyTargetBackground;
	public GameObject classInstan;
	public GameObject can;
	public GameObject can2;
	public GameObject AddClass;
	public GameObject NameField;
	public GameObject CurrentObject;
	public GameObject NewClass;
	public GameObject NewClassBackground;
	public GameObject createdObj;
	public GameObject ApprovalPrompt;
	public GameObject YesButton;
	public GameObject NoButton;
	public GameObject DeleteButton;
	public GameObject DeleteX;
	public GameObject InstantiatedX;
	public GameObject Warning1;
	public GameObject Warning2; 
	public GameObject WarningText1;
	public GameObject WarningText2; 
	public GameObject OKButton1;
	public GameObject OKButton2;
	public GameObject Background;
	public GameObject EditButton;
	public GameObject BaseClass;
	public GameObject RightArrow;
	public GameObject LeftArrow;
	public GameObject Number;

	public bool DeleteMode = false;
	public bool DeleteApproval = false;
	public bool PromptSelected = false;
	public bool OKselected = false;
	public bool Unclick = false;
	public static bool Suspend= false; // More or less not used, need to figure out if I can remove this
	public int x;
	public int page = 1;
	public int upperbound = 3;
	public int lowerbound = 0;
	public int maxpage;
	public int classesrendered;
	public int range;
	public bool createactive;
	public Overview overView;

	[System.Serializable]
	public struct Classes
	{
		public List<string> perm;

		public Classes(List<string> myList)
		{
			perm=myList;
		}
	}

	// I'm pretty sure that something's wrong inside the declaration above that causes the scope of the list to be restricted to loading somehow
	// but that doesn't make sense because it has the public keyword in front???
	public void Start() {
		LeftArrow.SetActive (false);
		NameField.SetActive (false);
		ApprovalPrompt.SetActive (false);
		Suspend = false;
		DeleteX.SetActive (false);
		load ();
	}

	public void generate ()
	{
		classes.Clear ();
		temp = GameObject.FindGameObjectsWithTag ("Classes");
		foreach (GameObject x in temp)  { 
			classes.Add (x.transform.Find ("Text").GetComponent<Text> ().text);
		}
	}
	public void save () {
		generate ();
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/Classes/Classes.gd");
		bf.Serialize (file, new Classes (perm)); //Does the new Classes struct end up interfering with perm?
		foreach (string x in perm) {
			int number = perm.FindIndex (d => d.Equals (x));
		}
		file.Close();
		foreach (string x in perm) {
			int number = perm.FindIndex (d => d.Equals (x));
		}
	}

		
	public void load () {
		classesrendered = 0;
		range = 6;
		float floatingrange = range;
		upperbound = (range - 1 ) + (range*(page-1));
		lowerbound = 0 + (range *(page-1));
		if (File.Exists (Application.persistentDataPath + "/Classes/Classes.gd")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/Classes/Classes.gd", FileMode.Open);
			Classes loaded = (Classes)bf.Deserialize (file);
			perm = loaded.perm;
			file.Close ();
			maxpage = (int)Mathf.Ceil ((perm.Count) / floatingrange);

			float xpos = 0;
			float ypos = GameObject.Find ("Add Class").transform.localPosition.y - 150;
			foreach (string x in perm) {
				int number = perm.FindIndex (d => d.Equals (x));
			}
			for (int i = lowerbound; i <= upperbound; i++) {
				if (i >= perm.Count || perm [i] == null)
					break;
				GameObject NewClass = Instantiate (classInstan);
				GameObject NewClassBackground = Instantiate (Background);
				NewClassBackground.transform.parent = can.transform;
				NewClass.transform.parent = can.transform;
				NewClass.SetActive (true);
				NewClassBackground.SetActive (true);
				NewClass.transform.localPosition = new Vector2 (xpos, ypos);
				NewClassBackground.transform.localPosition = new Vector2 (xpos, ypos);
				NewClass.transform.localScale = new Vector3 (1f, 1f, 1f);
				NewClassBackground.transform.localScale = new Vector3 (1f, 1f, 1f);
				NewClass.transform.Find ("Text").GetComponent<Text> ().text = perm [i];
				NewClass.name = perm [i];
				NewClassBackground.name = perm [i] + " Background";
				ypos -= 150;
				var btn = NewClass.GetComponent<Button> ();
				btn.onClick.AddListener (() => SendTexttoViewSwitcher (NewClass.name));
				NameField.transform.localPosition = new Vector2 (xpos, ypos);
				classes.Add (perm [i]);
				classesrendered++;
			}
		}
	}
	public void ExtendClassesList ()
	{
		perm.Add (null);
	}

	public void AddClasses () {
		if (!createactive) {
			createactive = true;
			if (DeleteMode == true) {
				DeleteButtonController ();
			}
			int count = classesrendered + 1;
			float xpos=0;
			float ypos = GameObject.Find ("Add Class").transform.localPosition.y-(150*count);
			Suspend = true;
			if (classesrendered >= range) {
				DestroyImmediate (NewClass);
				Suspend = false;
				createactive = false;
				StartCoroutine (WaitForLoad ());
			} else {
				NewClass = Instantiate (classInstan);
				NewClassBackground = Instantiate (Background);
				NewClassBackground.transform.parent = can.transform;
				NewClass.transform.parent = can.transform;
				NewClassBackground.transform.localPosition = new Vector2 (xpos, ypos);
				NewClassBackground.SetActive (true);
				NameField.SetActive (true);
				NewClass.SetActive (true);
				NewClass.transform.localPosition = new Vector2 (xpos, ypos);
				NewClass.transform.localScale = new Vector3 (1, 1, 1);
				NewClassBackground.transform.localScale = new Vector3 (1, 1, 1);
				NameField.transform.localPosition = new Vector2 (xpos, ypos);
				NameField.transform.SetAsLastSibling ();
				NewClass.transform.Find ("Text").GetComponent<Text> ().text = "";
				NewClass.transform.Find ("Text").GetComponent<Text> ().text = "Class " + count;
				NewClass.name = "Class " + count;
				createdObj = NewClass;
			}
		}
	}
	IEnumerator WaitForLoad () {
		SpecialIncreasePage ();
		yield return new WaitForSeconds (.1f);
		AddClasses ();
	}
	public void Deactivate () {
		string ClassName = NameField.transform.Find ("Text").GetComponent<Text>().text;
		if (ClassName == "" || ClassName == null) {
			DestroyImmediate (createdObj);
			DestroyImmediate (NewClassBackground);
			NameField.GetComponent<InputField> ().text = "";
			Suspend = false;
			NameField.SetActive(false);
			createactive = false;
			return;
		} 
		else {
			createdObj.transform.Find ("Text").GetComponent<Text> ().text = ClassName;
			createdObj.name = ClassName;
			NewClassBackground.name = ClassName + " Background";
		}
		foreach (string y in perm) {
			if (y == ClassName) {
				Warning2.SetActive (true);
				Warning2.transform.SetAsLastSibling ();
				WarningText2.SetActive (true);
				OKButton2.SetActive (true);
				Suspend = true;
				StartCoroutine (WarningCheck2 ());
				DestroyImmediate (createdObj);
				DestroyImmediate (NewClassBackground);
				break;
			}
		}
		if (DeleteMode == true)
		{	
			StartCoroutine (DeleteXs());
			StartCoroutine (CreateXs ());
		}
		NameField.GetComponent<InputField> ().text = "";
		NameField.SetActive(false);
		string name = createdObj.name;
		var btn = createdObj.GetComponent<Button> ();
		btn.onClick.AddListener (() => SendTexttoViewSwitcher (name));
		classesrendered++;
		perm.Add (createdObj.transform.Find ("Text").GetComponent<Text> ().text);
		save ();
		Suspend = false;
		createactive = false;
	}


	public void DeleteButtonController () {
		if (Suspend == false || Unclick == true) {
			if (DeleteMode == false) {
				if (NameField.activeInHierarchy == true) {
					DestroyImmediate (NewClass);
					DestroyImmediate (NewClassBackground);
					createactive = false;
					NameField.SetActive (false);
				}
				DeleteMode = true;			
				StartCoroutine (CreateXs ());
			} else if (DeleteMode == true) {
				DeleteMode = false;
				Suspend = false;
				StartCoroutine (DeleteXs ());

			}
		}
	}

	IEnumerator DeleteClass(int Position)
	{
		if (DeleteMode) {
			string targetname = classes [Position - 1];
			DestroyTarget = GameObject.Find (targetname);
			DestroyTargetBackground = GameObject.Find (targetname + " Background");
			Destroy (DestroyTarget);
			Destroy (DestroyTargetBackground);
			yield return new WaitForSeconds (.1F);
			foreach (string x in perm) {
				int number = perm.FindIndex (d => d.Equals (x));
			}

			perm.Remove (targetname);
			foreach (string x in perm) {
				int number = perm.FindIndex (d => d.Equals (x));
			}
			generate ();
			save ();
			foreach (string x in classes) {
				DestroyTarget = GameObject.Find (x);
				DestroyTargetBackground = GameObject.Find (x + " Background");
				Destroy (DestroyTarget);
				Destroy (DestroyTargetBackground);
			}
			yield return new WaitForSeconds (.01F);
			DeleteButtonController ();
			load ();
			if (classesrendered == 0) {
				DecreasePage ();
			}
		}
		yield return null;
	}
	public void StartApprovalPrompt (int pos)
	{
		if (DeleteMode==true) {
			Suspend = true;
			createactive = true;
			ApprovalPrompt.SetActive (true);
			YesButton.SetActive (true);
			NoButton.SetActive (true);
			StartCoroutine (ApprovalCheck (pos));
			ApprovalPrompt.transform.SetAsLastSibling ();
		}
	}
	public void ClickYesButton ()
	{
		DeleteApproval = true;
		PromptSelected = true;
		Suspend = false;
	}
	public void ClickNoButton ()
	{
		DeleteApproval = false;
		PromptSelected = true;
		Suspend = false;
	}
	public void ClickOKButton() {
		OKselected = true;
	}
	IEnumerator WarningCheck1 ()
	{
		yield return new WaitUntil (() => OKselected == true);
		Warning1.SetActive (false);
		WarningText1.SetActive (false);
		OKButton1.SetActive (false);
		Suspend = false;
		OKselected = false;

	}
	IEnumerator WarningCheck2 ()
	{
		yield return new WaitUntil (() => OKselected == true);
		Warning2.SetActive (false);
		WarningText2.SetActive (false);
		OKButton2.SetActive (false);
		Suspend = false;
		OKselected = false;
		createactive = false;
		if (classesrendered == 0) {
			DecreasePage ();
		}

	}
	IEnumerator ApprovalCheck (int pos) 
	{
		yield return new WaitUntil (() => PromptSelected == true);
		if (DeleteApproval == true) {
			StartCoroutine (DeleteClass(pos));
		}
		DeleteApproval = false;
		PromptSelected = false;
		ApprovalPrompt.SetActive (false);
		YesButton.SetActive (false);
		NoButton.SetActive (false);
		createactive = false;
}
	IEnumerator CreateXs ()
	{
		Suspend = true;
		Unclick = true;
		generate ();
		float xpos=-430;
		float ypos = GameObject.Find ("Add Class").transform.localPosition.y - 150;
		yield return new WaitForSeconds (.01f);
		for (int i=0;i<classes.Count;i++)
		{
			if (classes [i] != null) {
				GameObject InstantiatedX = Instantiate (DeleteX);
				InstantiatedX.transform.parent = can.transform;
				InstantiatedX.SetActive (true);
				var btn = InstantiatedX.GetComponent<Button> ();
				int temp = i+1; 
				btn.onClick.AddListener (() => StartApprovalPrompt (temp));
				InstantiatedX.transform.localPosition = new Vector2 (xpos, ypos);
				ypos -= 150;
			}
			else {
				break;
			}
		}
	}
	IEnumerator DeleteXs ()
	{
		GameObject[] Delete;
		Delete = GameObject.FindGameObjectsWithTag ("ActiveDeleteX");
		foreach (GameObject x in GameObject.FindGameObjectsWithTag ("ActiveDeleteX")) {
			Destroy(x);
		}
		yield return new WaitForSeconds (.01F);
	}

	public void SendTexttoViewSwitcher (string name) {
		if (Suspend == false) {
			DirectoryInfo info = new DirectoryInfo (Application.persistentDataPath);
			FileInfo[] fileInfo = info.GetFiles ();
			BinaryFormatter bf = new BinaryFormatter ();
			overView.events.Clear ();
			overView.files.Clear ();
			foreach (FileInfo x in fileInfo) {
				FileStream file2 = File.Open (x.FullName, FileMode.Open);
				CalendarSwitcher.EventObj obj = (CalendarSwitcher.EventObj)bf.Deserialize (file2);
				if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name == "Classes View") {
					if (obj.className == name) {
						overView.events.Add (obj);
						overView.files.Add (obj, x);
					}
				}
				file2.Close ();
			}
			overView.events = overView.events.OrderBy (e => System.DateTime.Parse (e.dueDate)).ToList (); 
			overView.sorter (1);
			can.SetActive (false);
			can2.SetActive (true);
			GameObject.Find ("Title").transform.GetComponent<Text> ().text = name;
		}
	}
	public void BackButton () {
		foreach (GameObject x in GameObject.FindGameObjectsWithTag("OverviewRow"))
			Destroy (x);
		overView.posY = -200;
		overView.events.Clear ();
		overView.files.Clear ();
		overView.whereInList = 0;
		overView.closeExtendView ();
		can.SetActive (true);
		can2.SetActive (false);
	}
	public void IncreasePage () {	
		if (page < maxpage) {
			if (DeleteMode == true) {
				DeleteButtonController ();
				StartCoroutine (WaitForChangePage ());
			}
			page++;
			StartCoroutine (CheckforClassPrompt ());
			StartCoroutine (ChangePage ());				
		}
	}
	public void SpecialIncreasePage () {
		if (classesrendered == range) {
			if (DeleteMode == true) {
				DeleteButtonController ();
				StartCoroutine (WaitForChangePage ());
			}
			page = maxpage;
			StartCoroutine (CheckforClassPrompt ());
			StartCoroutine (SpecialChangePage ());
		
		}
	}
	public void DecreasePage () {
		if (page > 1) {
			if (DeleteMode == true) {
				DeleteButtonController ();
				StartCoroutine (WaitForChangePage ());
			}
			if (createactive == true) {
				createactive = false;
			}
			page--;
			StartCoroutine (CheckforClassPrompt ());
			StartCoroutine (ChangePage ());		
		}
	}
	IEnumerator CheckforClassPrompt () {
		if (NameField.activeSelf == true) {
			Destroy (NewClass);
			Destroy (NewClassBackground);
			Suspend = false;
			NameField.SetActive (false);
			yield return new WaitForSeconds (.01f);
		}
	}
	IEnumerator WaitForChangePage () {
		yield return new WaitForSeconds (.1f);
		DeleteButtonController ();
	}

	IEnumerator ChangePage () {
		foreach (string x in classes) {
			DestroyTarget = GameObject.Find (x);
			DestroyTargetBackground = GameObject.Find (x + " Background");
			Destroy (DestroyTarget);
			Destroy (DestroyTargetBackground);
		}
		yield return new WaitForSeconds (.01f);
		if (page == 1) {
			LeftArrow.SetActive (false);
		} else
			LeftArrow.SetActive (true);
		if (page == maxpage) {
			RightArrow.SetActive (false);
		} else
			RightArrow.SetActive (true);
		load ();
		Number.transform.GetComponent<Text> ().text = "" + page;
	}
	IEnumerator SpecialChangePage () {
		foreach (string x in classes) {
			DestroyTarget = GameObject.Find (x);
			DestroyTargetBackground = GameObject.Find (x + " Background");
			Destroy (DestroyTarget);
			Destroy (DestroyTargetBackground);	
		}
		yield return new WaitForSeconds (.01f);
		load ();
		yield return new WaitForSeconds (.01f);
		Number.transform.GetComponent<Text> ().text = "" + page;
		if (classesrendered == range) {
			maxpage++;
			page++;
			StartCoroutine (SpecialChangePage ());
		}
		if (page == 1) {
			LeftArrow.SetActive (false);
		} else
			LeftArrow.SetActive (true);
		if (page == maxpage) {
			RightArrow.SetActive (false);
		} else
			RightArrow.SetActive (true);
	}
}