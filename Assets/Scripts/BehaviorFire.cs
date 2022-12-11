using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BehaviorFire : MonoBehaviour
{
    public GridController gridController;
    public GameManager gameManager;
    public Terrain[] terrain;
    public Animator animatorFire;

    public GameObject fireTerrain;
    public GameObject fireRight;
    public GameObject fireLeft;
    public GameObject fireUp;
    public GameObject fireDown;

    public GameObject fireRightWater;
    public GameObject fireLeftWater;
    public GameObject fireUpWater;
    public GameObject fireDownWater;

    public float timeDisplacement;

    [SerializeField] List<Terrain> terrainsList;
    [SerializeField] string typeFire;

    private float cellSize;

    private void Start()
    {
        gridController = FindObjectOfType<GridController>();
        gameManager = FindObjectOfType<GameManager>();

        this.cellSize = gridController.gridScriptableObject.cellSize;

        if (!(typeFire == "FireOriginal"))
        {
            terrain = FindObjectsOfType<Terrain>();
            AssignTileGrid();
            StartCoroutine(Displacement(timeDisplacement, 0.3f));
        }
    }

    public void StartFire()
    {
        terrain = FindObjectsOfType<Terrain>();
        AssignTileGrid();
        StartCoroutine(Displacement(timeDisplacement, 0.3f));
    }

    public void AssignTileGrid()
    {
        terrainsList.Clear();

        foreach (var obj in terrain)
        {
            if (obj.gameObject.activeInHierarchy)
            {
                if (typeFire == "FireLeft" || typeFire == "FireOriginal" || typeFire == "FireDown")
                {
                    if (transform.position.x - cellSize == obj.transform.position.x && transform.position.y == obj.transform.position.y)
                    {
                        terrainsList.Add(obj);
                    }
                }

                if (typeFire == "FireRight" || typeFire == "FireOriginal" || typeFire == "FireUp")
                {
                    if (transform.position.x + cellSize == obj.transform.position.x && transform.position.y == obj.transform.position.y)
                    {
                        terrainsList.Add(obj);
                    }
                }

                if (typeFire == "FireUp" || typeFire == "FireOriginal" || typeFire == "FireLeft")
                {
                    if (transform.position.y + cellSize == obj.transform.position.y && transform.position.x == obj.transform.position.x)
                    {
                        terrainsList.Add(obj);
                    }
                }

                if (typeFire == "FireDown" || typeFire == "FireOriginal" || typeFire == "FireRight")
                {
                    if (transform.position.y - cellSize == obj.transform.position.y && transform.position.x == obj.transform.position.x)
                    {
                        terrainsList.Add(obj);
                    }
                }
            }
        }
    }

    public IEnumerator Displacement(float time, float timeTree)
    {
        yield return new WaitForSeconds(time);

        for (int i = 0; i < terrainsList.Count; i++)
        {
            if(terrainsList[i] != null)
            {
                if (transform.position.x - cellSize == terrainsList[i].transform.position.x && transform.position.y == terrainsList[i].transform.position.y)
                {
                    if (terrainsList[i].typeTerrain == "water")
                    {
                        InstantiateFire(fireLeftWater, i);
                    }
                    else
                    {
                        InstantiateFire(fireLeft, i);
                    }

                    /*if (terrainsList[i].typeTerrain == "tree")
                    {
                        InstantiateFire(fireLeft, i);
                        yield return new WaitForSeconds(timeTree);

                        animatorFire.SetBool("boolFire", true);
                    }*/
                }
                else if (transform.position.x + cellSize == terrainsList[i].transform.position.x && transform.position.y == terrainsList[i].transform.position.y)
                {
                    if (terrainsList[i].typeTerrain == "water")
                    {

                        InstantiateFire(fireRightWater, i);
                        
                    }
                    else
                    {
                        InstantiateFire(fireRight, i);
                    }

                    /*if (terrainsList[i].typeTerrain == "tree")
                    {
                        InstantiateFire(fireRight, i);
                        yield return new WaitForSeconds(timeTree);

                        animatorFire.SetBool("boolFire", true);
                    }*/
                }
                else if (transform.position.y - cellSize == terrainsList[i].transform.position.y && transform.position.x == terrainsList[i].transform.position.x)
                {
                    if (terrainsList[i].typeTerrain == "water")
                    {
                        InstantiateFire(fireDownWater, i);
                        
                    }
                    else
                    {
                        InstantiateFire(fireDown, i);
                    }


                    /*if (terrainsList[i].typeTerrain == "tree")
                    {
                        
                        yield return new WaitForSeconds(timeTree);

                        animatorFire.SetBool("boolFire", true);
                    }*/
                }
                else if (transform.position.y + cellSize == terrainsList[i].transform.position.y && transform.position.x == terrainsList[i].transform.position.x)
                {
                    if (terrainsList[i].typeTerrain == "water")
                    {
                        InstantiateFire(fireUpWater, i);
                    }
                    else
                    {
                        InstantiateFire(fireUp, i);
                    }

                    /*if (terrainsList[i].typeTerrain == "tree")
                    {
                        InstantiateFire(fireUp, i);
                        yield return new WaitForSeconds(timeTree);

                        animatorFire.SetBool("boolFire", true);
                    }*/
                }
            }
        }
    }

    void InstantiateFire(GameObject fireTerrain, int index)
    {
        Instantiate(fireTerrain, terrainsList[index].transform.localPosition, Quaternion.identity);
        terrainsList[index].gameObject.SetActive(false);
        Destroy(terrainsList[index].gameObject);
    }
}
