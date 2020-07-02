using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnabler : MonoBehaviour {

    [SerializeField] private GameObject[] TotalLevelsList;


    void Start() {
        LockAllSubsequentLevels();
    }


    // running through the list of levels EXCEPT level 1 and disabling them at the start of the game
    void LockAllSubsequentLevels () {
        for (int CurrentLevel = 0; CurrentLevel < TotalLevelsList.Length; CurrentLevel++) {
            TotalLevelsList[CurrentLevel].SetActive(false);
        }
    }

    void UnlockLevel (int level) {
        TotalLevelsList[level].SetActive(true);
    }
}
