using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Terrain[] fires;
    public Slider sliderTemperature;
    public Slider sliderMoisture;

    public void ResetScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private void Start()
    {
        StartCoroutine(FireList(0.1f));
    }

    public void StartFire()
    {
        Terrain[] fires = FindObjectsOfType<Terrain>();

        foreach (var obj in fires)
        {
            obj.StartFire();
        }
    }
    
    public IEnumerator FireList(float time)
    {
        fires = FindObjectsOfType<Terrain>();
        yield return new WaitForSeconds(0.1f);
    }
}
