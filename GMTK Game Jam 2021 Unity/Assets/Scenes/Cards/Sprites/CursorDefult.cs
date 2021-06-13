using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDefult : MonoBehaviour
{
    public Texture2D baseCursor;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                Cursor.SetCursor(this.baseCursor, Vector2.zero, CursorMode.Auto);

    }
}
