using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void ResetScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void StartFire()
    {
        BehaviorFire[] fires = FindObjectsOfType<BehaviorFire>();

        foreach (var obj in fires)
        {
            obj.StartFire();
        }
    }
}
