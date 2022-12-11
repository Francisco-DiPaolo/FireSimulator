using UnityEngine;

public class LvlEditor : MonoBehaviour
{
    [SerializeField] GameObject fireTerrain, waterTerrain, treeTerrain, dryTreeTerrain, mountainTerrain;

    private GameObject objectActual;
    private int number;

    void Update()
    {
        TipeObject();
        Action("Replace");
    }

    public void ChangeNumberBtn(int numberBtn)
    {
        number = numberBtn;
    }

    private void TipeObject()
    {
        switch (number)
        {
            case 0:
                objectActual = fireTerrain;
                break;
            case 1:
                objectActual = waterTerrain;
                break;
            case 2:
                objectActual = treeTerrain;
                break;
            case 3:
                objectActual = dryTreeTerrain;
                break;
            case 4:
                objectActual = mountainTerrain;
                break;
            default:
                break;
        }
    }

    void Action(string Type)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                Terrain terrain = hit.collider.GetComponent<Terrain>();
                if (terrain != null && Type == "Replace")
                {
                    Vector3 position = hit.transform.position;
                    Destroy(hit.collider.gameObject);
                    Instantiate(objectActual, position, Quaternion.identity);
                }
            }
        }
    }
}
