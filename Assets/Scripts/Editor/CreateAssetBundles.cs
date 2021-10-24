using UnityEditor;

public class CreateAssetBundles : Editor
{
    [MenuItem("Tools/AssetBundles/Build/Win64")]
    private static void BuildAllAssetbundlesWin()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles",BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        AssetDatabase.Refresh();
    }
}