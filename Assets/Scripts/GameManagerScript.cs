using UnityEngine;
using UnityEngine.SceneManagement;

// NO LONGER USING THIS
public class GameManagerScript : MonoBehaviour
{

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
