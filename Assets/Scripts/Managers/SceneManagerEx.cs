using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    private BaseScene currentScene;

    public BaseScene CurrentScene { set { currentScene = value; } }

    public T GetCurrentScene<T>() where T : BaseScene
    {
        return currentScene as T;
    }

    public void LoadScene(string name)
    {
        Managers.Clear();
        SceneManager.LoadScene(name);
    }

    public void Clear()
    {
        currentScene?.Clear();
        currentScene = null;
    }
}
