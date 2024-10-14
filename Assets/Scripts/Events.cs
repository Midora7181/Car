using UnityEngine;
using UnityEngine.SceneManagement;
public class Events : MonoBehaviour
{
   
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
