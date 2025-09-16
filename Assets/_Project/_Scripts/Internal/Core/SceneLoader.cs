using UnityEngine.SceneManagement;

namespace Internal.Core
{
    public class SceneLoader
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ReloadScene()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}