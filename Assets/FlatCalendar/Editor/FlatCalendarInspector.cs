/**
 * Flat Calendar
 * 
 * This class override the inspector of Flat Calendar Game Object
 *
 * @version 1.0
 * @author  Gerardo Ritacco
 * @email   gerardo.ritacco@3dresearch.it
 * @company 3dresearchsrl
 * @website http://www.3dresearch.it/
 * 
 * Copyright © 2016 by 3dresearchsrl
 *
 * All rights reserved. No part of this publication may be reproduced, distributed, 
 * or transmitted in any form or by any means, including photocopying, recording, or 
 * other electronic or mechanical methods, without the prior written permission of the 
 * publisher, except in the case of brief quotations embodied in critical reviews and 
 * certain other noncommercial uses permitted by copyright law. For permission requests, 
 * write to the publisher, addressed “Attention: Permissions Coordinator,” at the address below.
 */

using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(FlatCalendar))]
[CanEditMultipleObjects]
[ExecuteInEditMode]
public class FlatCalendarInspector : Editor {

	SerializedProperty uiStyle;

	GUIContent[] guil = new GUIContent[] {  new GUIContent("JANUARY"),
											new GUIContent("FEBRUARY"),
											new GUIContent("MARCH"),
											new GUIContent("APRIL"),
											new GUIContent("MAY"),
											new GUIContent("JUNE"),
										    new GUIContent("JULY"),
											new GUIContent("AUGUST"),
											new GUIContent("SEPTEMBER"),
										    new GUIContent("OCTOBER"),
										 	new GUIContent("NOVEMBER"),
											new GUIContent("DECEMBER")
										 };

	public void OnEnable()
	{
		uiStyle = serializedObject.FindProperty("current_UiStyle");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		uiStyle.intValue = EditorGUILayout.Popup(uiStyle.intValue,guil);

		// UI STYLE CHOICE
		FlatCalendarStyle.changeUIStyle(uiStyle.intValue);

		serializedObject.ApplyModifiedProperties();


	}

}
