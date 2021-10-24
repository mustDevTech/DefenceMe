using Mst.Simple_Pool_Manager;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SPManager))]
public class SPEditorLayout : Editor
{
    private SPManager spm;
    private SerializedObject serializedSPM;
    private SerializedProperty poolCollection;
    private SerializedProperty searchTxt;

    private Texture2D SPM_Logo = null;
    private Texture2D SPM_Header = null;
    private Texture2D SPM_BG = null;
    private bool showCollection;

    private const int maxInstantiatingQty = 500;

    private void OnEnable()
    {
        /// Get Textures
        SPM_Logo = (Texture2D)Resources.Load<Texture2D>("SPMLogo");
        SPM_Header = (Texture2D)Resources.Load<Texture2D>("SPM_Header");
        SPM_BG = (Texture2D)Resources.Load<Texture2D>("SPM_BG");
        
        spm = (SPManager)target;
        serializedSPM = new SerializedObject(spm);
        poolCollection = serializedSPM.FindProperty("_collection");
        searchTxt = serializedSPM.FindProperty("searchTxt"); 
    }

    public override void OnInspectorGUI()
    {
        serializedSPM.Update();

        // ****************************************
        // Horizontal SPManager Section
        // ****************************************
        EditorGUILayout.LabelField("", "", new GUIStyle() { normal = { background = SPM_Logo } }, GUILayout.Height(EditorGUIUtility.currentViewWidth * 0.10f), GUILayout.Width(EditorGUIUtility.currentViewWidth - 24));

        // ****************************************
        // BOX AREA - Properties Section - Add & Clear list
        // ****************************************

        #region Properties Area With Add Button
        EditorGUILayout.BeginVertical(new GUIStyle() { normal = { background = SPM_BG } }, new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(150) });
        EditorGUILayout.Space(10);

        spm._poolCollectionInfo._Name = EditorGUILayout.TextField("Name", spm._poolCollectionInfo._Name, GUILayout.ExpandWidth(true));
        spm._poolCollectionInfo.ActiveFirstItem = EditorGUILayout.Toggle("Active First Item", spm._poolCollectionInfo.ActiveFirstItem);
        spm._poolCollectionInfo.InstantiateQty = (int)EditorGUILayout.Slider("Instantiate Qty", spm._poolCollectionInfo.InstantiateQty, 1, maxInstantiatingQty);
        spm._poolCollectionInfo.ParentTransform = (Transform)EditorGUILayout.ObjectField("Parent Transform", spm._poolCollectionInfo.ParentTransform, typeof(Transform), true);
        spm._poolCollectionInfo.Item = (GameObject)EditorGUILayout.ObjectField("Prefab", spm._poolCollectionInfo.Item, typeof(GameObject), false);

        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();

        GUI.backgroundColor = new Color(0.369f, 0.812f, 0.369f, 1f);
        if (GUILayout.Button("Add to list", new GUILayoutOption[] { GUILayout.Height(40), GUILayout.ExpandWidth(true) }))
        {
            if (string.IsNullOrEmpty(spm._poolCollectionInfo._Name) || spm._poolCollectionInfo._Name.Contains("Pool Name"))
            {
                Debug.Log("No name was provided for the Pool Item. Please add a name");
                return;
            }
            if (spm._poolCollectionInfo.Item == null)
            {
                Debug.Log("Please provide a prefab to instantiate for this Pool");
                return;
            }

            spm.AddToCollection(spm._poolCollectionInfo);
            ResetValues();
        }
        GUI.backgroundColor = Color.white;

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        var poolColHorizontalpos = EditorGUILayout.BeginHorizontal(EStyles.CustomBoxStyle(new Vector4(0, 0, 10, 0), Vector4.one * 5, SPM_Header));
        EditorGUILayout.LabelField("Pool Collection", EStyles.CustomLabel(TextAnchor.MiddleLeft, 14, Color.white, Vector4.zero, Vector4.one));
        EditorGUILayout.LabelField($"QTY: {(spm._collection.Count)}",
            EStyles.CustomLabel(TextAnchor.MiddleLeft, 14, Color.white, Vector4.zero, Vector4.one * 5),
            new GUILayoutOption[] { GUILayout.Width(50), GUILayout.ExpandWidth(true) });

        GUI.backgroundColor = Color.red;

        if (spm._collection.Count > 0)
        {
            if (!showCollection)
            {
                GUI.backgroundColor = Color.cyan;
                if (GUILayout.Button("View List", new GUILayoutOption[] { GUILayout.Width(65), GUILayout.Height(20) }))
                {
                    showCollection = true;
                }
            }
            else
            {
                GUI.backgroundColor = Color.grey;
                if (GUILayout.Button("Hide List", new GUILayoutOption[] { GUILayout.Width(65), GUILayout.Height(20) }))
                {
                    showCollection = false;
                }
            }
            GUI.backgroundColor = Color.yellow;
            if (GUILayout.Button(new GUIContent("Disable All","Disables all the active items"), GUILayout.ExpandHeight(true)))
            {
                spm.DisableAllExistingInPoolObjects();
            }
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button(new GUIContent("X", "Clears the entire Pool list"), new GUILayoutOption[] { GUILayout.Width(40), GUILayout.Height(20) }))
            {
                spm._collection.Clear();
            }

            GUI.backgroundColor = Color.white;
        }
        EditorGUILayout.EndHorizontal();

        #endregion Horizontal Pool Collection Section

        // ****************************************
        // Pool Collection List Display Section
        // ****************************************

        #region Pool Collection List Display

        if (EditorGUILayout.BeginFadeGroup(showCollection && poolCollection.arraySize > 0 ? 1 : 0))
        {
            var pos = EditorGUILayout.BeginHorizontal(EStyles.CustomBoxStyle(Vector4.zero, Vector4.one * 5), GUILayout.Height(30));
            EditorGUI.DrawRect(pos, Color.black);

            searchTxt.stringValue = EditorGUILayout.TextField("Search", searchTxt.stringValue);
            if (GUILayout.Button("clear", GUILayout.Width(75)))
            {
                searchTxt.stringValue = "";
            }
            EditorGUILayout.EndHorizontal();

            if (searchTxt.stringValue.Length >= 3)
            {
                SearchResult(searchTxt.stringValue);
            }
            else
            {
                ShowAllListItems();
            }
        }
        EditorGUILayout.EndFadeGroup();

        #endregion Pool Collection List Display

        // apply changes done to the properties
        serializedSPM.ApplyModifiedProperties();
    }

    private void ShowAllListItems()
    {
        for (int i = 0; i <= (poolCollection.arraySize - 1); i++)
        {
            SerializedProperty col = poolCollection.GetArrayElementAtIndex(i);
            SerializedProperty name = col.FindPropertyRelative("_Name");
            SerializedProperty activeFirst = col.FindPropertyRelative("ActiveFirstItem");
            SerializedProperty instQty = col.FindPropertyRelative("InstantiateQty");
            SerializedProperty parentTrans = col.FindPropertyRelative("ParentTransform");
            SerializedProperty colItem = col.FindPropertyRelative("Item");
            SerializedProperty toggleShowHide = col.FindPropertyRelative("toggleShowHide");

            EditorGUILayout.BeginVertical(EStyles.CustomAreaStyle(Vector4.zero, Vector4.one * 5, SPM_BG));
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"{name.stringValue}", EStyles.CustomLabel(TextAnchor.MiddleLeft, 14, Color.white, Vector4.zero, Vector4.one));
            GUI.backgroundColor = Color.cyan;
            if (GUILayout.Button("Toggle View", GUILayout.ExpandHeight(true)))
            {
                toggleShowHide.boolValue = !toggleShowHide.boolValue;
            }
            GUI.backgroundColor = Color.yellow;
            if (GUILayout.Button("Disable", GUILayout.ExpandHeight(true)))
            {
                spm.DisableAllPoolObjectByCategory(name.stringValue);
            }
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Delete Item", GUILayout.ExpandHeight(true)))
            {
                spm._collection.RemoveAt(i);
            }
            GUI.backgroundColor = Color.white;
            EditorGUILayout.EndHorizontal();

            if (toggleShowHide.boolValue)
            {
                name.stringValue = EditorGUILayout.TextField("Name", name.stringValue);
                activeFirst.boolValue = EditorGUILayout.Toggle("Active First Item", activeFirst.boolValue);
                instQty.intValue = (int)EditorGUILayout.Slider("Instantiate Qty", instQty.intValue, 1, maxInstantiatingQty);
                parentTrans.objectReferenceValue = EditorGUILayout.ObjectField("Parent Transform", parentTrans.objectReferenceValue, typeof(Transform), true);
                colItem.objectReferenceValue = EditorGUILayout.ObjectField("Prefab", colItem.objectReferenceValue, typeof(GameObject), false);
            }

            EditorGUILayout.EndVertical();

            GUI.backgroundColor = Color.white;
            EditorGUILayout.Space(5);
        }
    }

    private void SearchResult(string val)
    {
        for (int i = 0; i < poolCollection.arraySize; i++)
        {
            SerializedProperty col = poolCollection.GetArrayElementAtIndex(i);
            SerializedProperty name = col.FindPropertyRelative("_Name");

            if (name.stringValue.ToLower().Contains(val.ToLower()))
            {
                SerializedProperty activeFirst = col.FindPropertyRelative("ActiveFirstItem");
                SerializedProperty instQty = col.FindPropertyRelative("InstantiateQty");
                SerializedProperty parentTrans = col.FindPropertyRelative("ParentTransform");
                SerializedProperty colItem = col.FindPropertyRelative("Item");
                SerializedProperty toggleShowHide = col.FindPropertyRelative("toggleShowHide");

                EditorGUILayout.BeginVertical(EStyles.CustomAreaStyle(Vector4.zero, Vector4.one * 5, SPM_BG));
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"{name.stringValue}", EStyles.CustomLabel(TextAnchor.MiddleLeft, 14, Color.white, Vector4.zero, Vector4.one));
                GUI.backgroundColor = Color.cyan;
                if (GUILayout.Button("Toggle View", GUILayout.ExpandHeight(true)))
                {
                    toggleShowHide.boolValue = !toggleShowHide.boolValue;
                }
                GUI.backgroundColor = Color.yellow;
                if (GUILayout.Button("Disable", GUILayout.ExpandHeight(true)))
                {
                    spm.DisableAllPoolObjectByCategory(name.stringValue);
                }
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Delete Item", GUILayout.ExpandHeight(true)))
                {
                    spm._collection.RemoveAt(i);
                }
                GUI.backgroundColor = Color.white;
                EditorGUILayout.EndHorizontal();

                if (toggleShowHide.boolValue)
                {
                    name.stringValue = EditorGUILayout.TextField("Name", name.stringValue);
                    activeFirst.boolValue = EditorGUILayout.Toggle("Active First Item", activeFirst.boolValue);
                    instQty.intValue = (int)EditorGUILayout.Slider("Instantiate Qty", instQty.intValue, 1, maxInstantiatingQty);
                    parentTrans.objectReferenceValue = EditorGUILayout.ObjectField("Parent Transform", parentTrans.objectReferenceValue, typeof(Transform), true);
                    colItem.objectReferenceValue = EditorGUILayout.ObjectField("Prefab", colItem.objectReferenceValue, typeof(GameObject), false);
                }

                EditorGUILayout.EndVertical();

                GUI.backgroundColor = Color.white;
                EditorGUILayout.Space(5);
            }
        }
    }

    private void ResetValues()
    {
        spm._poolCollectionInfo = new SPManager.PoolCollectionInfo() { _Name = "Pool Name - Please change" };
    }
}

public static class EStyles
{
    public static RectOffset rectOffset(int left, int right, int top, int bottom) => new RectOffset(left, right, top, bottom);
    public static RectOffset rectOffset(int val) => new RectOffset(val, val, val, val);
    public static GUIStyle CustomBoxStyle(Vector4 margin, Vector4 padding) => new GUIStyle()
    {
        margin = EStyles.rectOffset((int)margin.x, (int)margin.y, (int)margin.z, (int)margin.w),
        padding = EStyles.rectOffset((int)padding.x, (int)padding.y, (int)padding.z, (int)padding.w),
        stretchWidth = true,
        wordWrap = false,
    };
    public static GUIStyle CustomBoxStyle(Vector4 margin, Vector4 padding, Texture2D background) => new GUIStyle()
    {
        margin = EStyles.rectOffset((int)margin.x, (int)margin.y, (int)margin.z, (int)margin.w),
        padding = EStyles.rectOffset((int)padding.x, (int)padding.y, (int)padding.z, (int)padding.w),
        stretchWidth = true,
        wordWrap = false,
        normal = { background = background }
    };
    public static GUIStyle CustomLabel(TextAnchor alignment, int fontsize, Color color, Vector4 margin, Vector4 padding) => new GUIStyle()
    {
        alignment = alignment,
        fontSize = fontsize,
        fontStyle = FontStyle.Bold,
        normal = { textColor = color },
        margin = EStyles.rectOffset((int)margin.x, (int)margin.y, (int)margin.z, (int)margin.w),
        padding = EStyles.rectOffset((int)padding.x, (int)padding.y, (int)padding.z, (int)padding.w),
    };
    public static GUIStyle CustomAreaStyle(Vector4 margin, Vector4 padding, Texture2D background) => new GUIStyle()
    {
        margin = EStyles.rectOffset((int)margin.x, (int)margin.y, (int)margin.z, (int)margin.w),
        padding = EStyles.rectOffset((int)padding.x, (int)padding.y, (int)padding.z, (int)padding.w),
        normal = { background = background }
    };
}