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

    void asign_row_column(){
        for (var i = 0; i < 4; i++ ){
            for (var j = 0; j < 4; j++ ){
                print(i);
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
