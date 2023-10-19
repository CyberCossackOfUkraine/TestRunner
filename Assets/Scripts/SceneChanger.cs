using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChanger
{
    public static void ChangeScene(string newScene)
    {
        if (IsSceneExist(newScene))
        {
            SceneManager.LoadScene(newScene);
        } else
        {
            Debug.LogError("Scene " + newScene + " does not exist");
        }
    }

    private static bool IsSceneExist(string newScene)
    {
        if (string.IsNullOrEmpty(newScene))
        {
            return false;
        }
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            var lastSlash = scenePath.LastIndexOf("/");
            var sceneName = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1);

            if (string.Compare(newScene, sceneName, true) == 0)
            {
                return true;
            }
        }

        return false;
    }
}
