using System.Collections.Generic;
using UnityEngine;

namespace Mst.UI
{
public class CursorChanger : MonoBehaviour
{
    [SerializeField] private List<Texture2D> _cursorList;

    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.P))
        {
            Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
            Debug.Log("changed cursor");
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            Cursor.SetCursor(_cursorList[Random.Range(0,_cursorList.Count)],new Vector2(15f,15f),CursorMode.Auto);
            Debug.Log("changed cursor");
        }
    }
}
}
