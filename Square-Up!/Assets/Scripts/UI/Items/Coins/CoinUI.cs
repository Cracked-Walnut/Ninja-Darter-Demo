using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour {


    [SerializeField] private Text coinDisplay;
    private Player player;

    void Awake() {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update() {
        coinDisplay.text = player.getCoinCount().ToString();
    }
}
