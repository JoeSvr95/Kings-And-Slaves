using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_grid : MonoBehaviour {

	/* void Awake() {
 
        List<Dictionary<string,object>> data = CSVReader.Read ("paths");
        for(var i=0; i < data.Count; i++) {
            print ("initial " + data[i]["initial"] + " " +
                   "finish " + data[i]["finish"] + " " +
                   "02 " + data[i]["02"] + " " +
                   "03 " + data[i]["03"]);
        }
    } */

    // Point[] point_list = GameObject.FindObjectOfType<Point>();

    void asign_row_column(){

        string number = "";
        Point point;
        List<Dictionary<string,object>> data = CSVReader.Read ("paths");

        for (var i = 0; i < 4; i++ ){
            for (var j = 0; j < 4; j++ ){
                number = "";
                number = i.ToString() + j.ToString();
                point = GameObject.Find("Point" + number).GetComponent<Point>();
                point.row = i;
                point.column = j;
            }
        } 


    }

	// Use this for initialization
	void Start () {
		asign_row_column();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
