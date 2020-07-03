using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnabler : MonoBehaviour {

    [SerializeField] private GameObject[] TotalLevelsList;
    private int level;


    void Start() {
        LockAllSubsequentLevels();
    }

    // running through the list of levels EXCEPT level 1 and disabling them at the start of the game
    void LockAllSubsequentLevels () {
        for (int CurrentLevel = 0; CurrentLevel < TotalLevelsList.Length; CurrentLevel++) {
            TotalLevelsList[CurrentLevel].SetActive(false);
        }
    }

    public void UnlockLevel (int level) {
        this.level = level;
        TotalLevelsList[level].SetActive(true);
    }

    public void setLevel(int level) {
        this.level = level;
    }

    public int getLevel() {
        return level;
    }
}
