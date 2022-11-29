using UnityEngine;

public class LvlEditor : MonoBehaviour
{
    [SerializeField] GameObject fireTerrain, waterTerrain, treeTerrain, dryTreeTerrain, mountainTerrain;

    private GameObject _objectActual;
    private int _number;

    void Update()
    {
        TipeObject();
        Action("Replace");
    }

    public void ChangeNumberBtn(int numberBtn)
    {
        _number = numberBtn;
    }

    private void TipeObject()
    {
        switch (_number)
        {
            case 0:
                _objectActual = fireTerrain;
                break;
            case 1:
                _objectActual = waterTerrain;
                break;
            case 2:
                _objectActual = treeTerrain;
                break;
            case 3:
                _objectActual = dryTreeTerrain;
                break;
            case 4:
                _objectActual = mountainTerrain;
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
                    Instantiate(_objectActual, position, Quaternion.identity);
                }
            }
        }
    }
}
