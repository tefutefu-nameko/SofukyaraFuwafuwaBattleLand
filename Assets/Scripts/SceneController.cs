using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : ManagerBase
{
    public void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1;
    }
}
