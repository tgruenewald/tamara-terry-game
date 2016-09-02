using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader
{
	static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
	static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
	static char[] TRIM_CHARS = { '\"' };

	public static List<Dictionary<string, string>> Read(string file)
	{
		var list = new List<Dictionary<string, string>>();
		TextAsset data = Resources.Load (file) as TextAsset;

		var lines = Regex.Split (data.text, LINE_SPLIT_RE);

		if(lines.Length <= 1) return list;

		var header = Regex.Split(lines[0], SPLIT_RE);
		for(var i=1; i < lines.Length; i++) {

			var values = Regex.Split(lines[i], SPLIT_RE);
			if(values.Length == 0 ||values[0] == "") continue;

			var entry = new Dictionary<string, string>();
			for(var j=0; j < header.Length && j < values.Length; j++ ) {
				string value = values[j];
				value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
				string finalvalue = value;

				entry[header[j]] = (string) finalvalue;
			}
			list.Add (entry);
		}
		return list;
	}

	public static string FindItem(string item, string itemProperty, string valueProperty, List<Dictionary<string, string>> data) 
	{

		string value = "0";
		for(var i=0; i < data.Count; i++) {
//			Debug.Log ("name " + data[i]["name"] + " " +
//				"hp " + data[i]["hp"]);
			
			if (item == (string) data [i] [itemProperty]) {
				value = (string) data [i] [valueProperty];
				break;
			}

		}		
		return value;
	}

}