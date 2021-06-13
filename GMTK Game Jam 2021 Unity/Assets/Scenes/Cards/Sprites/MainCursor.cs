using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCursor : MonoBehaviour
{

    public Texture2D mainCursor;

    void Update()
    {
        Cursor.SetCursor(this.mainCursor, Vector2.zero, CursorMode.Auto);
        if (Input.GetMouseButton(1))
        {
            Cursor.SetCursor(this.mainCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}

