using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script will output the desired sound track*/

public class BGM : MonoBehaviour {

    private AudioManager audioManager;
    [SerializeField] private string fileName = "Main Theme 1";

    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager.Play(fileName);
    }
}
