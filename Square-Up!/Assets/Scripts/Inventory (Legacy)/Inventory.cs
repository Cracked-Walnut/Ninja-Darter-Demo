using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script will hold traits for Inventory*/

public class Inventory : MonoBehaviour {

    private string[] numberOfSlots;

    void Awake() {
        numberOfSlots = new string[10]; /*10 empty slots*/
    }

}//end of class