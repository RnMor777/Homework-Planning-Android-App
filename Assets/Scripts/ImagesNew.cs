using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ImagesNew : MonoBehaviour {

	public List<Texture2D> images = new List<Texture2D> ();
	public GameObject imageAmounts;
	public GameObject imageCanvas;
	public GameObject cameraBck;
	public GameObject prevImages;
	public GameObject prevText; 
	public int imageTracker=0;
	public GameObject takeImage;
	public GameObject delImages;
	public GameObject rightBtn;
	public GameObject leftBtn;
	public GameObject backArrw;
	WebCamTexture CameraTexture;
	public int linkFinal;
	int cameraRotation;

	public void startImage()
	{
		StartCoroutine (createImages());
	}
	IEnumerator createImages() //initializes the camera and rotates the screen to match it
	{
		imageCanvas.SetActive (true);
		leftBtn.SetActive (false);
		rightBtn.SetActive (false);
		delImages.SetActive (false);
		takeImage.SetActive (true);
		prevImages.SetActive (true);
		prevText.SetActive (true);
		backArrw.SetActive (false);
		imageTracker = 0;
		if (images.Count > 0) {
			prevImages.GetComponent<RawImage> ().texture = images[0];
			prevText.GetComponent<Text> ().text = "[" + images.Count + "]";
			prevImages.GetComponent<Button> ().interactable = true;
		}
		yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
		if (Application.HasUserAuthorization(UserAuthorization.WebCam)) 
		{
			WebCamDevice[] devices = WebCamTexture.devices;
			string backCamName="";
			for( int i = 0 ; i < devices.Length ; i++ ) {
				Debug.Log("Device:"+devices[i].name+ "IS FRONT FACING:"+devices[i].isFrontFacing);

				if (!devices[i].isFrontFacing) 
					backCamName = devices[i].name;
			}
			CameraTexture = new WebCamTexture(backCamName,Screen.width,Screen.height);  
			CameraTexture.Play();
			switch (CameraTexture.videoRotationAngle) {  //this rotates everything so that it displayed right
			case 90:
				cameraBck.transform.localEulerAngles = new Vector3 (0, 0, -90);
				cameraBck.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1175, 1000);
				cameraBck.transform.localPosition = new Vector2 (500, -638);
				prevImages.transform.localEulerAngles = new Vector3 (0, 0, -90);
				prevImages.transform.localPosition = new Vector2 (400, -1350);
				break;
			case 180:
				cameraBck.transform.localEulerAngles = new Vector3 (0, 0, -180);
				cameraBck.transform.localPosition = new Vector2 (500, -1225);
				prevImages.transform.localEulerAngles = new Vector3 (0, 0, -180);
				prevImages.transform.localPosition = new Vector2 (300, -1450);
				break;
			case 270:
				cameraBck.transform.localEulerAngles = new Vector3 (0, 0, -90);
				cameraBck.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1175, 1000);
				cameraBck.transform.localPosition = new Vector2 (-500, -638);
				prevImages.transform.localEulerAngles = new Vector3 (0, 0, -270);
				prevImages.transform.localPosition = new Vector2 (200, -1350);
				break;
			}
			cameraRotation = CameraTexture.videoRotationAngle;
			cameraBck.GetComponent<RawImage>().texture = CameraTexture;
		} 

	}
	public void takePics()  //gets the picture at the moments
	{
		Texture2D tex;
		tex = new Texture2D(CameraTexture.width, CameraTexture.height);
		tex.SetPixels(CameraTexture.GetPixels());
		tex.Apply ();
		prevImages.GetComponent<RawImage> ().texture = tex;
		images.Add (tex);
		prevText.GetComponent<Text> ().text = "[" + images.Count + "]";
		prevImages.GetComponent<Button> ().interactable = true;
	}
	public void closeImages() //closes the images
	{
		CameraTexture.Stop ();
		imageCanvas.SetActive (false);
		if (images.Count != 0) {
			imageAmounts.SetActive (true);
			imageAmounts.GetComponent<Text> ().text = "[" + images.Count + "]";
		}
	}
	public void reopenImages()  //lets you see images
	{
		CameraTexture.Stop ();
		imageCanvas.SetActive (true);
		backArrw.SetActive (true);
		leftBtn.SetActive (false);
		rightBtn.SetActive (images.Count>1?true:false);
		delImages.SetActive (true);
		takeImage.SetActive (false);
		prevImages.SetActive (false);
		prevText.SetActive (false);
		cameraBck.GetComponent<RawImage> ().texture = images [0];
	}
	public void changePics(bool left)  //looking at pictures left+right buttons
	{
		if (!left && (imageTracker + 1) <= (images.Count - 1)) {
			cameraBck.GetComponent<RawImage> ().texture = images [imageTracker+1];
			imageTracker++;
		}
		if (left && (imageTracker - 1) >= 0) {
			cameraBck.GetComponent<RawImage> ().texture = images [imageTracker - 1];
			imageTracker--;
		}
		rightBtn.SetActive ((imageTracker + 1) <= (images.Count - 1) ? true : false);
		leftBtn.SetActive ((imageTracker - 1) >= 0 ? true : false);
	}
	public void deleteTheImage()  //destroys an image
	{
		if ((imageTracker + 1) <= (images.Count - 1))
			cameraBck.GetComponent<RawImage> ().texture = images [imageTracker + 1];
		else if ((imageTracker - 1) >= 0) {
			cameraBck.GetComponent<RawImage> ().texture = images [imageTracker - 1];		
			imageTracker--;
		}
		else
			startImage ();
		images.RemoveAt (imageTracker);
		rightBtn.SetActive ((imageTracker + 1) <= (images.Count - 1) ? true : false);
		leftBtn.SetActive ((imageTracker - 1) >= 0 ? true : false);
		if (images.Count > 0)
			prevImages.GetComponent<RawImage> ().texture = images [imageTracker];
		else
			prevImages.GetComponent<Button>().interactable=false;
		prevText.GetComponent<Text> ().text = "[" + images.Count + "]";
		prevText.SetActive (images.Count == 0 ? true : false);
	}
	public int genLink()  //creates a link
	{
		linkFinal = UnityEngine.Random.Range (100000, 999999);
		DirectoryInfo info = new DirectoryInfo (Application.persistentDataPath);
		FileInfo[] fileInfo = info.GetFiles ();
		foreach (FileInfo f in fileInfo) {
			BinaryFormatter bf2 = new BinaryFormatter ();
			FileStream file2 = File.Open (Application.persistentDataPath + "/"+ f.Name, FileMode.Open);
			CalendarSwitcher.EventObj test = (CalendarSwitcher.EventObj)bf2.Deserialize (file2); 
			while (test.link == linkFinal) {
				linkFinal = UnityEngine.Random.Range (100000, 999999);
			}
			file2.Close ();
		}
		return(linkFinal);
	}
	public void addLetter() //adds a letter to allow for 26 images
	{
		string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; 
		foreach (Texture2D x in images) {
			int tempInt = UnityEngine.Random.Range (0, 26);
			char letter = letters [tempInt];
			while (File.Exists (Application.persistentDataPath + "/Images/" + linkFinal + letter + ".png")) {
				tempInt = UnityEngine.Random.Range (0, 26);
				letter = letters [tempInt];
			}
			byte[] fileData=x.EncodeToPNG();
			/*switch (cameraRotation) {
			case 0:
				fileData = x.EncodeToPNG ();
				break;
			case 90:
				fileData = rotate90 (x).EncodeToPNG ();
				break;
			case 180:
				fileData = rotate180 (x).EncodeToPNG ();
				break;
			case 270:
				fileData = rotate270 (x).EncodeToPNG ();
				break;
			}*/
			var f = System.IO.File.Create (Application.persistentDataPath + "/Images/" + linkFinal + letter + ".png");
			f.Write (fileData, 0, fileData.Length);
			f.Close ();
		}
		images.Clear ();
	}
	/*
	   //this is another way to get the rotation
	   private Texture2D rotate90(Texture2D orig) {
		print("doing rotate90");
		Color32[] origpix = orig.GetPixels32(0);
		Color32[] newpix = new Color32[orig.width * orig.height];
		for(int c = 0; c < orig.height; c++) {
			for(int r = 0; r < orig.width; r++) {
				newpix[orig.width * orig.height - (orig.height * r + orig.height) + c] =
					origpix[orig.width * orig.height - (orig.width * c + orig.width) + r];
			}
		}
		Texture2D newtex = new Texture2D(orig.height, orig.width, orig.format, false);
		newtex.SetPixels32(newpix, 0);
		newtex.Apply();
		return newtex;
	}

	private Texture2D rotate180(Texture2D orig) {
		print("doing rotate180");
		Color32[] origpix = orig.GetPixels32(0);
		Color32[] newpix = new Color32[orig.width * orig.height];
		for(int i = 0; i < origpix.Length; i++) {
			newpix[origpix.Length - i - 1] = origpix[i];
		}
		Texture2D newtex = new Texture2D(orig.width, orig.height, orig.format, false);
		newtex.SetPixels32(newpix, 0);
		newtex.Apply();
		return newtex;
	}

	private Texture2D rotate270(Texture2D orig) {
		print("doing rotate270");
		Color32[] origpix = orig.GetPixels32(0);
		Color32[] newpix = new Color32[orig.width * orig.height];
		int i = 0;
		for(int c = 0; c < orig.height; c++) {
			for(int r = 0; r < orig.width; r++) {
				newpix[orig.width * orig.height - (orig.height * r + orig.height) + c] = origpix[i];
				i++;
			}
		}
		Texture2D newtex = new Texture2D(orig.height, orig.width, orig.format, false);
		newtex.SetPixels32(newpix, 0);
		newtex.Apply();
		return newtex;
	}*/
}
