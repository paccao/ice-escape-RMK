using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void SceneLoader(int level)
    {
        SceneManager.LoadScene(level);
    }
}
