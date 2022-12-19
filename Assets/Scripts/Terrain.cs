using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Terrain : MonoBehaviour
{
    public string typeTerrain;
    public string typeAfterBurned;

    public GridController gridController;
    public GameManager gameManager;
    public Terrain[] terrains;
    public Animator animatorFire;

    public Color colorFire;

    public float timeDisplacement;
    public float timeVerifyType;
    public float timeBurnedTerrain;

    [SerializeField] List<Terrain> terrainsList;

    private float cellSize;

    private void Start()
    {
        gridController = FindObjectOfType<GridController>();
        gameManager = FindObjectOfType<GameManager>();

        this.cellSize = gridController.gridScriptableObject.cellSize;

        if (!(typeTerrain == "fire"))
        {
            terrains = FindObjectsOfType<Terrain>();
            AssignTileGrid();
        }
    }

    public void StartFire()
    {
        timeDisplacement -= gameManager.sliderTemperature.value;
        timeDisplacement += gameManager.sliderMoisture.value;

        if (typeTerrain == "fire")
        {
            animatorFire.SetBool("boolFire", true);
        }

        StartCoroutine(VerifyTypeTerrain(timeVerifyType, timeBurnedTerrain));
    }

    public void AssignTileGrid()
    {
        {
            terrainsList.Clear();

            foreach (var obj in terrains)
            {
                if (obj.gameObject.activeInHierarchy)
                {
                    if (transform.position.x - cellSize == obj.transform.position.x && transform.position.y == obj.transform.position.y)
                    {
                        terrainsList.Add(obj);
                    }

                    if (transform.position.x + cellSize == obj.transform.position.x && transform.position.y == obj.transform.position.y)
                    {
                        terrainsList.Add(obj);
                    }

                    if (transform.position.y + cellSize == obj.transform.position.y && transform.position.x == obj.transform.position.x)
                    {
                        terrainsList.Add(obj);
                    }

                    if (transform.position.y - cellSize == obj.transform.position.y && transform.position.x == obj.transform.position.x)
                    {
                        terrainsList.Add(obj);
                    }
                }
            }
        }
    }

    public IEnumerator Displacement(float time)
    {
        yield return new WaitForSeconds(time);

        for (int i = 0; i < terrainsList.Count; i++)
        {
            if (terrainsList[i] != null)
            {
                if (transform.position.x - cellSize == terrainsList[i].transform.position.x && transform.position.y == terrainsList[i].transform.position.y)
                {
                    StartCoroutine(TimeToInstantiate(i, terrainsList[i].timeDisplacement));
                }
                else if (transform.position.x + cellSize == terrainsList[i].transform.position.x && transform.position.y == terrainsList[i].transform.position.y)
                {
                    StartCoroutine(TimeToInstantiate(i, terrainsList[i].timeDisplacement));
                }
                else if (transform.position.y - cellSize == terrainsList[i].transform.position.y && transform.position.x == terrainsList[i].transform.position.x)
                {
                    StartCoroutine(TimeToInstantiate(i, terrainsList[i].timeDisplacement));
                }
                else if (transform.position.y + cellSize == terrainsList[i].transform.position.y && transform.position.x == terrainsList[i].transform.position.x)
                {
                    StartCoroutine(TimeToInstantiate(i, terrainsList[i].timeDisplacement));
                }
            }
        }
    }

    public IEnumerator TimeToInstantiate(int index, float time)
    {
        yield return new WaitForSeconds(time);
        
        if(typeTerrain != "coal")
        {
            if (terrainsList[index].typeTerrain != "water" && terrainsList[index].typeTerrain != "coal")
            {
                terrainsList[index].gameObject.GetComponent<SpriteRenderer>().color = colorFire;
                terrainsList[index].typeTerrain = "fire";
            }
        }
    }

    public IEnumerator VerifyTypeTerrain(float time, float timeBurnedTerrain)
    {
        yield return new WaitForSeconds(time);

        if (typeTerrain == "fire")
        {
            terrains = FindObjectsOfType<Terrain>();
            AssignTileGrid();
            StartCoroutine(Displacement(timeDisplacement));

            animatorFire.SetBool("boolFire", true);
        }
        else
        {
            StartCoroutine(VerifyTypeTerrain(time, timeBurnedTerrain));
        }
    }

    public void ChangeTypeToCoal()
    {
        typeTerrain = "coal";
        
    }
}
