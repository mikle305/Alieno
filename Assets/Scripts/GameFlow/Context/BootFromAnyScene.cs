#if UNITY_EDITOR

using Additional.Constants;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace GameFlow.Context
{
    [InitializeOnLoad]
    public class BootFromAnyScene
    {
        static BootFromAnyScene()
        {
            EditorApplication.playModeStateChanged += Run;
        }

        private static void Run(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.EnteredPlayMode)
                return;

            EditorApplication.playModeStateChanged -= Run;
            if (SceneManager.GetActiveScene().name != SceneNames.Boot) 
                SceneManager.LoadScene(SceneNames.Boot);
        }
    }
}

#endif
