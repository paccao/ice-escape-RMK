using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void SceneLoader(int level)
    {
        // Takes scene index from build settings
        SceneManager.LoadScene(level);
    }
}
