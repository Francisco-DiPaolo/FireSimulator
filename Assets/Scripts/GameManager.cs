using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void ResetScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
