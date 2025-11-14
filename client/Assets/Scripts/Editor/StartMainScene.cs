
using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine;

public class PlayModeUseDefaultScene
{
    [UnityEditor.MenuItem("BuildTools/PlayModeUseFirstScene")]
    static void UpdatePlayModeUseFirstScene()
    {
        // EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        //
        // if (scenes.Length > 0)
        // {
        //     SceneAsset firstScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenes[0].path);
        //     if (firstScene != null)
        //     {
        //         EditorSceneManager.playModeStartScene = firstScene;
        //         Debug.Log("Play mode start scene set to: " + firstScene.name);
        //     }
        //     else
        //     {
        //         Debug.LogError("Failed to load the first scene from the build settings.");
        //     }
        // }
        // else
        // {
        //     Debug.LogError("No scenes found in the build settings.");
        // }
    }

}
