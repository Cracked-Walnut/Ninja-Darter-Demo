using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour {

    [SerializeField] private Texture2D cursorArrow;

    // Start is called before the first frame update
    void Start() {
        
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }
}

/*
Sources:
1) O. T., OXMOND Tutorials, 'How To Change The Cursor In Unity! [Unity3D 2019 Beginner Tutorial]', 2019. [Online]. Available: https://www.youtube.com/watch?v=W4SE0_cfAqc [Accessed: 05-Jul-2020].
*/