using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO; 
using UnityEngine.UI;

public class CalendarSwitcher : MonoBehaviour {

	public GameObject control;
	public GameObject flatCal;
	FlatCalendar flatCalendar;

	[System.Serializable]
	public struct EventObj: EventsInterface  //the hw event object
	{
		public string name;
		public string className;
		public string dueDate;
		public string type;
		public bool isProj;
		public string priority;
		public int expectedTime;
		public List<string> project;
		public int link;

		public string Name {
			set {name = value;}
			get { return name; }
		}
		public string Date {
			set { dueDate = value; }
			get { return dueDate; }
		}
	}
	public void Start()
	{	
		FlatCalendar_Demo demo = control.GetComponent<FlatCalendar_Demo> ();  //initializes all of the flat calendar stuff
		flatCalendar = flatCal.GetComponent<FlatCalendar>();
		flatCalendar.initFlatCalendar();
		repopulateEvents (System.DateTime.Now);
		flatCalendar.setCallback_OnDaySelected(demo.dayUpdated);  //sets up more flat calendar stuff
		flatCalendar.setCallback_OnMonthChanged(demo.monthUpdated);
		flatCalendar.setCallback_OnEventSelected(demo.eventsDiscovered);
		flatCalendar.setCallback_OnNowday(demo.backHome);

		flatCalendar.setUIStyle(0); // Set UI Style
	}
	public void repopulateEvents(System.DateTime dateUpdate) {  //just populates all of the spots
		foreach (CalendarSwitcher.EventObj x in RemoveCache.fullListAssigns.Keys) {
			System.DateTime tempDate = System.DateTime.Parse (x.dueDate);
			flatCalendar.addEvent(tempDate.Year,tempDate.Month,tempDate.Day,x);
		}
		foreach (ActivityNew.ActivitiesSave x in RemoveCache.fullListActs.Keys) {
			if (x.reacurring) {
				continue;
			}
			System.DateTime tempDate = System.DateTime.Parse (x.date);
			flatCalendar.addEvent (tempDate.Year, tempDate.Month, tempDate.Day, x);
		}
				flatCalendar.updateUiLabelEvents (dateUpdate.Year, dateUpdate.Month, dateUpdate.Day);
		flatCalendar.refreshCalendar ();
	}
}