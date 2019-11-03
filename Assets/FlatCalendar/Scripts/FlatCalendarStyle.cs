/**
 * Flat Calendar
 * 
 * This class manage Flat Calendar Themes
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
using System.Collections;
using UnityEngine.UI;

public class FlatCalendarStyle
{
	/*
	 * UI Colors Configuration 
	 */
	public enum COLORS_TYPE {ThemeColor, JANUARY, FEBRUARY, MARCH, APRIL, MAY, JUNE, JULY, AUGUST, SEPTEMBER, OCTOBER, NOVEMBER, DECEMBER};

	/*
	 * Single UI Colors Items
	 */
	public static Color color_header;
	public static Color color_subheader;
	public static Color color_body;
	public static Color color_footer;
	public static Color color_dayTextNormal;
	public static Color color_dayTextEvent;
	public static Color color_bubbleEvent;
	public static Color color_bubbleSelectionMarker;
	public static Color color_numberEvent;
	public static Color color_year;
	public static Color color_month;
	public static Color color_day;
	public static Color color_dayOfWeek;
	public static Color color_Events;
	public static Color color_ButtonRight;
	public static Color color_ButtonLeft;
	public static Color color_Home;

	public static void changeUIStyle(int style)
	{
		if(style == (int) FlatCalendarStyle.COLORS_TYPE.JANUARY)
		{
			color_header     			= new Color(142.0f/255.0f,184.0f/255.0f,218.0f/255.0f,255.0f/255.0f);
			color_subheader				= new Color(24.0f/255.0f, 36.0f/255.0f,  90.0f/255.0f,100.0f/255.0f);
			color_body 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_footer 				= new Color( 35.0f/255.0f, 59.0f/255.0f, 83.0f/255.0f,255.0f/255.0f);
			color_dayTextNormal			= new Color( 0.0f/255.0f, 0.0f/255.0f,  0.0f/255.0f,  255.0f/255.0f);
			color_dayTextEvent			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleEvent			= new Color( 21.0f/255.0f,101.0f/255.0f,192.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color( 21.0f/255.0f,101.0f/255.0f,192.0f/255.0f,255.0f/255.0f);
			color_numberEvent			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayOfWeek				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Events				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonRight			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
		}

		if (style == (int)FlatCalendarStyle.COLORS_TYPE.FEBRUARY) 
		{
			color_header 				= new Color(210.0f/255.0f,26.0f/255.0f, 53.0f/255.0f, 255.0f/255.0f);
			color_subheader 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,100.0f/255.0f);
			color_body 					= new Color(184.0f/255.0f, 28.0f/255.0f,62.0f/255.0f, 255.0f/255.0f);
			color_footer 				= new Color(126.0f/255.0f,15.0f/255.0f,15.0f/255.0f,  255.0f/255.0f);
			color_dayTextNormal 		= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent 			= new Color(184.0f/255.0f, 28.0f/255.0f,62.0f/255.0f, 255.0f/255.0f);
			color_bubbleEvent 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_numberEvent 			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayOfWeek 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Events 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonRight 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);

		}

		if (style == (int)FlatCalendarStyle.COLORS_TYPE.MARCH) 
		{
			color_header 				= new Color(4.0f/255.0f,  109.0f/255.0f, 0.0f/255.0f, 255.0f/255.0f);
			color_subheader 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,100.0f/255.0f);
			color_body 					= new Color(49.0f/255.0f, 143.0f/255.0f,46.0f/255.0f, 255.0f/255.0f);
			color_footer 				= new Color(0.0f/255.0f,56.0f/255.0f,  21.0f/255.0f,  255.0f/255.0f);
			color_dayTextNormal 		= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent 			= new Color(49.0f/255.0f, 143.0f/255.0f,46.0f/255.0f, 255.0f/255.0f);
			color_bubbleEvent 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_numberEvent 			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayOfWeek 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Events 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonRight 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);

		}

		if (style == (int)FlatCalendarStyle.COLORS_TYPE.APRIL) 
		{
			color_header 				= new Color(75.0f/255.0f,149.0f/255.0f,201.0f/255.0f, 255.0f/255.0f);
			color_subheader 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,100.0f/255.0f);
			color_body 					= new Color(0.0f/255.0f, 64.0f/255.0f, 109.0f/255.0f, 255.0f/255.0f);
			color_footer 				= new Color(0.0f/255.0f,118.0f/255.0f, 30.0f/255.0f,  255.0f/255.0f);
			color_dayTextNormal 		= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent 			= new Color(0.0f/255.0f, 64.0f/255.0f, 109.0f/255.0f, 255.0f/255.0f);
			color_bubbleEvent 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_numberEvent 			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayOfWeek 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Events 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonRight 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);

		}

		if (style == (int)FlatCalendarStyle.COLORS_TYPE.MAY) 
		{
			color_header 				= new Color(225.0f/255.0f,84.0f/255.0f,151.0f/255.0f, 255.0f/255.0f);
			color_subheader 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,100.0f/255.0f);
			color_body 					= new Color(70.0f/255.0f, 158.0f/255.0f,36.0f/255.0f, 255.0f/255.0f);
			color_footer 				= new Color(0.0f/255.0f, 92.0f/255.0f, 80.0f/255.0f,  255.0f/255.0f);
			color_dayTextNormal 		= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent 			= new Color(70.0f/255.0f, 158.0f/255.0f,36.0f/255.0f, 255.0f/255.0f);
			color_bubbleEvent 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_numberEvent 			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayOfWeek 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Events 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonRight 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);

		}

		if (style == (int)FlatCalendarStyle.COLORS_TYPE.JUNE) 
		{
			color_header 				= new Color(33.0f/255.0f,104.0f/255.0f,118.0f/255.0f, 255.0f/255.0f);
			color_subheader 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,100.0f/255.0f);
			color_body 					= new Color(43.0f/255.0f, 131.0f/255.0f,139.0f/255.0f,255.0f/255.0f);
			color_footer 				= new Color(186.0f/255.0f,191.0f/255.0f,101.0f/255.0f,255.0f/255.0f);
			color_dayTextNormal 		= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent 			= new Color(43.0f/255.0f, 131.0f/255.0f,139.0f/255.0f,255.0f/255.0f);
			color_bubbleEvent 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_numberEvent 			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day 					= new Color(0.0f/255.0f, 0.0f/ 255.0f, 0.0f / 255.0f, 255.0f/255.0f);
			color_dayOfWeek 			= new Color(0.0f/255.0f, 0.0f/ 255.0f, 0.0f / 255.0f, 255.0f/255.0f);
			color_Events 				= new Color(0.0f/255.0f, 0.0f/ 255.0f, 0.0f / 255.0f, 255.0f/255.0f);
			color_ButtonRight 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home 					= new Color(0.0f / 255.0f, 0.0f / 255.0f, 0.0f/255.0f,255.0f/255.0f);

		}

		if(style == (int) FlatCalendarStyle.COLORS_TYPE.JULY)
		{
			color_header     			= new Color( 251.0f/255.0f, 244.0f/255.0f,0.0f/255.0f,255.0f/255.0f);
			color_subheader				= new Color(0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,  100.0f/255.0f);
			color_body 					= new Color(135.0f/255.0f, 118.0f/255.0f,21.0f/255.0f,255.0f/255.0f);
			color_footer 				= new Color( 53.0f/255.0f, 40.0f/255.0f, 0.0f/255.0f, 255.0f/255.0f);
			color_dayTextNormal			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent			= new Color(135.0f/255.0f, 118.0f/255.0f,21.0f/255.0f,255.0f/255.0f);
			color_bubbleEvent			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_numberEvent			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year					= new Color(  0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,255.0f/255.0f);
			color_month					= new Color(  0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,255.0f/255.0f);
			color_day					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayOfWeek				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Events				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonRight			= new Color(  0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft			= new Color(  0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,255.0f/255.0f);
			color_Home					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
		}

		if (style == (int)FlatCalendarStyle.COLORS_TYPE.AUGUST) 
		{
			color_header 				= new Color(74.0f/255.0f,151.0f/255.0f,171.0f/255.0f, 255.0f/255.0f);
			color_subheader 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,100.0f/255.0f);
			color_body 					= new Color(219.0f/255.0f,123.0f/255.0f, 0.0f/255.0f, 255.0f/255.0f);
			color_footer 				= new Color(68.0f/255.0f, 49.0f/255.0f,  22.0f/255.0f,255.0f/255.0f);
			color_dayTextNormal 		= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent 			= new Color(219.0f/255.0f,123.0f/255.0f, 0.0f/255.0f, 255.0f/255.0f);
			color_bubbleEvent 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_numberEvent 			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayOfWeek 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Events 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonRight 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);

		}

		if (style == (int)FlatCalendarStyle.COLORS_TYPE.SEPTEMBER) 
		{
			color_header 				= new Color(31.0f/255.0f,113.0f/255.0f, 20.0f/255.0f, 255.0f/255.0f);
			color_subheader 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,100.0f/255.0f);
			color_body 					= new Color(200.0f/255.0f,62.0f/255.0f,  0.0f/255.0f, 255.0f/255.0f);
			color_footer 				= new Color(107.0f/255.0f, 20.0f/255.0f,  0.0f/255.0f,255.0f/255.0f);
			color_dayTextNormal 		= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent 			= new Color(200.0f/255.0f,62.0f/255.0f,  0.0f/255.0f, 255.0f/255.0f);
			color_bubbleEvent 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_numberEvent 			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayOfWeek 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Events 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonRight 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);

		}
			
		if(style == (int) FlatCalendarStyle.COLORS_TYPE.OCTOBER)
		{
			color_header     			= new Color(  0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,255.0f/255.0f);
			color_subheader				= new Color(  69.0f/255.0f, 69.0f/255.0f,69.0f/255.0f,255.0f/255.0f);
			color_body 					= new Color( 240.0f/255.0f, 89.0f/255.0f, 7.0f/255.0f,255.0f/255.0f);
			color_footer 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextNormal			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent			= new Color( 240.0f/255.0f, 89.0f/255.0f, 7.0f/255.0f,255.0f/255.0f);
			color_bubbleEvent			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_numberEvent			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day					= new Color(  0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,255.0f/255.0f);
			color_dayOfWeek				= new Color(  0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,255.0f/255.0f);
			color_Events				= new Color(  0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,255.0f/255.0f);
			color_ButtonRight			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home					= new Color(  0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,255.0f/255.0f);
		}

		if(style == (int) FlatCalendarStyle.COLORS_TYPE.NOVEMBER)
		{
			color_header     			= new Color( 223.0f/255.0f,130.0f/255.0f,13.0f/255.0f,255.0f/255.0f);
			color_subheader				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,100.0f/255.0f);
			color_body 					= new Color( 96.0f/255.0f, 67.0f/255.0f, 37.0f/255.0f,255.0f/255.0f);
			color_footer 				= new Color(30.0f/255.0f, 66.0f/255.0f,  16.0f/255.0f,255.0f/255.0f);
			color_dayTextNormal			= new Color(  0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent			= new Color( 96.0f/255.0f, 67.0f/255.0f, 37.0f/255.0f,255.0f/255.0f);
			color_bubbleEvent			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_numberEvent			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayOfWeek				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Events				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonRight			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
		}

		if (style == (int)FlatCalendarStyle.COLORS_TYPE.DECEMBER) 
		{
			color_header 				= new Color(225.0f/255.0f,13.0f/255.0f, 13.0f/255.0f, 255.0f/255.0f);
			color_subheader 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,100.0f/255.0f);
			color_body 					= new Color(0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,  255.0f/255.0f);
			color_footer 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextNormal 		= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent 			= new Color(0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,  255.0f/255.0f);
			color_bubbleEvent 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_numberEvent 			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day 					= new Color(0.0f/255.0f, 0.0f/ 255.0f, 0.0f / 255.0f, 255.0f/255.0f);
			color_dayOfWeek 			= new Color(0.0f/255.0f, 0.0f/ 255.0f, 0.0f / 255.0f, 255.0f/255.0f);
			color_Events 				= new Color(0.0f/255.0f, 0.0f/ 255.0f, 0.0f / 255.0f, 255.0f/255.0f);
			color_ButtonRight 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home 					= new Color(0.0f / 255.0f, 0.0f / 255.0f, 0.0f/255.0f,255.0f/255.0f);

		}
		if (style == (int)FlatCalendarStyle.COLORS_TYPE.ThemeColor) 
		{
			color_header 				= new Color(0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,  255.0f/255.0f);
			color_subheader 			= new Color(46.0f/255.0f,177.0f/255.0f, 255.0f/255.0f,255.0f/255.0f);
			color_body 					= new Color(0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,  255.0f/255.0f);
			color_footer 				= new Color(0.0f/255.0f,  0.0f/255.0f,  0.0f/255.0f,  255.0f/255.0f);
			color_dayTextNormal 		= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayTextEvent 			= new Color(184.0f/255.0f, 28.0f/255.0f,62.0f/255.0f, 255.0f/255.0f);
			color_bubbleEvent 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_bubbleSelectionMarker	= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_numberEvent 			= new Color(238.0f/255.0f,105.0f/255.0f,105.0f/255.0f,255.0f/255.0f);
			color_year 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_month 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_day 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_dayOfWeek 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Events 				= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonRight 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_ButtonLeft 			= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,255.0f/255.0f);
			color_Home 					= new Color(255.0f/255.0f,255.0f/255.0f,255.0f/255.0f,0f);
		}

		/* Change Colors */
		GameObject.Find("Header").GetComponent<Image>().color       = color_header;
		GameObject.Find("SubHeader").GetComponent<Image>().color    = color_subheader;
		GameObject.Find("Body").GetComponent<Image>().color         = color_body;
		GameObject.Find("Footer").GetComponent<Image>().color       = color_footer;
		GameObject.Find("Year").GetComponent<Text>().color          = color_year;
		GameObject.Find("Month").GetComponent<Text>().color         = color_month;
		GameObject.Find("Day_Title1").GetComponent<Text>().color    = color_day;
		GameObject.Find("Day_Title2").GetComponent<Text>().color    = color_dayOfWeek;
		GameObject.Find("NumberEvents").GetComponent<Text>().color  = color_numberEvent; 
		GameObject.Find("Events").GetComponent<Text>().color        = color_Events;
		GameObject.Find("Left_btn").GetComponent<Image>().color     = color_ButtonLeft;
		GameObject.Find("Right_btn").GetComponent<Image>().color    = color_ButtonRight;
		GameObject.Find("Calendar_Btn").GetComponent<RawImage>().color = color_Home;

		/* Change Text Color */
		if(Application.isPlaying)
		{
			GameObject obj = GameObject.Find("FlatCalendar");
			FlatCalendar f = obj.GetComponent<FlatCalendar>();
			f.setCurrentTime();
			f.updateCalendar(f.currentTime.month,f.currentTime.year);
			f.markSelectionDay(f.currentTime.day);
		}
	}
}
