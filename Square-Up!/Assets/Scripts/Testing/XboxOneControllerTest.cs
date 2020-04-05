using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XboxOneControllerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void ControllerCheck() {
        float ltaxis = Input.GetAxis("XboxLeftTrigger");
        float rtaxis = Input.GetAxis("XboxRightTrigger");
        float dhaxis = Input.GetAxis("XboxDpadHorizontal");
        float dvaxis = Input.GetAxis("XboxDpadVertical");
    
        bool xbox_a = Input.GetButton("XboxA");
        bool xbox_b = Input.GetButton("XboxB");
        bool xbox_x = Input.GetButton("XboxX");
        bool xbox_y = Input.GetButton("XboxY");
        bool xbox_lb = Input.GetButton("XboxLB");
        bool xbox_rb = Input.GetButton("XboxRB");
        bool xbox_ls = Input.GetButton("XboxLS");
        bool xbox_rs = Input.GetButton("XboxRS");
        bool xbox_view = Input.GetButton("XboxView");
        bool xbox_menu = Input.GetButton("XboxMenu");        
    
        // DebugText.text =
        //     string.Format(
        //         "Horizontal: {14:0.000} Vertical: {15:0.000}\n" +
        //         "HorizontalTurn: {16:0.000} VerticalTurn: {17:0.000}\n" +
        //         "LTrigger: {0:0.000} RTrigger: {1:0.000}\n" +
        //         "A: {2} B: {3} X: {4} Y:{5}\n" +
        //         "LB: {6} RB: {7} LS: {8} RS:{9}\n" +
        //         "View: {10} Menu: {11}\n" +
        //         "Dpad-H: {12:0.000} Dpad-V: {13:0.000}\n",
        //         ltaxis, rtaxis,
        //         xbox_a, xbox_b, xbox_x, xbox_y,
        //         xbox_lb, xbox_rb, xbox_ls, xbox_rs,
        //         xbox_view, xbox_menu,
        //         dhaxis, dvaxis,
        //         hAxis, vAxis,
        //         htAxis, vtAxis);
    }
 
    void Update() {
        // Camera Rig Movement Control
        // bool hAxis = Input.GetAxis("Horizontal");
        // bool vAxis = Input.GetAxis("Vertical");
        // bool aAxis = Input.GetAxis("Altitude");
        // bool htAxis = Input.GetAxis("HorizontalTurn");
        // bool vtAxis = Input.GetAxis("VerticalTurn");

        ControllerCheck();
    }
}
