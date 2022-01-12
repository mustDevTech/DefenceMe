using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    [SerializeField] private List<Texture2D> _cursorList;

    private void Update()
    {   
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.P))
        {
            SetDefaultCursor();
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            SetRandomCursor();
        }
#endif
    }
    private void OnEnable() => SetRandomCursor();
    private void OnDisable() => SetDefaultCursor();

    #region PrivateVariables
    private void SetRandomCursor()
    {
        Cursor.SetCursor(_cursorList[Random.Range(0,_cursorList.Count)],new Vector2(15f,15f),CursorMode.Auto);
    }
    private void SetDefaultCursor()
    {
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
    }
    #endregion
}