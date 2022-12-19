using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Terrain[] fires;

    public Slider sliderTemperature;
    public Slider sliderMoisture;
    public Slider sliderMusic;

    public AudioSource audioSource;

    public bool boolStop;

    public void ResetScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    private void Start()
    {
        StartCoroutine(FireList(0.1f));
        boolStop = true;
        sliderMusic.value = 0;
    }

    private void Update()
    {
        audioSource.volume = sliderMusic.value;
    }

    public void StartFire()
    {
        Terrain[] fires = FindObjectsOfType<Terrain>();

        foreach (var obj in fires)
        {
            obj.StartFire();
        }

        audioSource.Play();
    }

    public void StopGame()
    {
        if (boolStop)
        {
            Time.timeScale = 0;
            boolStop = false;
        }
        else
        {
            Time.timeScale = 1;
            boolStop = true;
        }
    }
    
    public IEnumerator FireList(float time)
    {
        fires = FindObjectsOfType<Terrain>();
        yield return new WaitForSeconds(0.1f);
    }
}
