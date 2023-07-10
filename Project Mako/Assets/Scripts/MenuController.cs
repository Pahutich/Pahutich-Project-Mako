using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    public GameObject sideWindow = null;
    private void Awake()
    {
        //Singleton method
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void EnableGameObject(GameObject obj)
    {
        bool toEnable = false;
        toEnable = obj.activeInHierarchy ? false : true;
        obj.SetActive(toEnable);
        if (toEnable == false)
        {
            sideWindow = null;
        }
        else
        {
            if (sideWindow != null)
            {
                sideWindow.SetActive(false);
            }
            sideWindow = obj;
        }
    }
    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToScene(int sceneIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }

    public void GoToScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
