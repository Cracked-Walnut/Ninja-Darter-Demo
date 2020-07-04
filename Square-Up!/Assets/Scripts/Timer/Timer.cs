using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField] private Text timerDisplay;
    private float totalTime;
    private bool isTimerRunning;


    

    void Start() {
        totalTime = 0.0f;
        isTimerRunning = true;
    }

    void Update() {
        if (isTimerRunning) {
            totalTime += Time.deltaTime;
            string roundedTotalTime = totalTime.ToString("#.00");
            timerDisplay.text = roundedTotalTime;
        }
    }

    public float getTotalTime() { return totalTime; }
    
    public bool getIsTimerRunning() { return isTimerRunning; }
    public void setIsTimerRunning(bool isTimerRunning) { this.isTimerRunning = isTimerRunning; }
}
