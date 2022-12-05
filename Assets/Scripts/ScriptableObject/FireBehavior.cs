using UnityEngine;
using System.Collections;

public class FireBehavior : MonoBehaviour
{
    public GameObject fireTerrain;
    private Vector2 direction;

    public void StartFire()
    {
        direction = Vector3.up;
        Move(direction);
        StartCoroutine(DirectionSwap());
    }

    public void Move(Vector2 dir)
    {
        Debug.Log("Pepe");
        if (dir == Vector2.up)
        {
            RayCast(dir);
            Debug.Log("MiroUp");
        }
        else if (dir == Vector2.right)
        {
            RayCast(dir);
            Debug.Log("MiroRight");
        }
        else if (dir == Vector2.down)
        {
            RayCast(dir);
            Debug.Log("MiroDown");
        }
        else if (dir == Vector2.left)
        {
            RayCast(dir);
            Debug.Log("MiroLeft");
        }
    }

    public void RayCast(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);

        if (hit.collider != null)
        {
            Terrain terrain = hit.collider.GetComponent<Terrain>();
            if (terrain != null)
            {
                Vector3 position = hit.transform.position;
                Destroy(hit.collider.gameObject);
                Instantiate(fireTerrain, position, Quaternion.identity);
            }
        }
    }

    public IEnumerator DirectionSwap()
    {
        yield return new WaitForSeconds(.1f);
        Debug.Log("Entro La Corrutina");
        gameObject.transform.Rotate(transform.up);
        yield return new WaitForSeconds(.1f);
        gameObject.transform.Rotate(transform.right);
        yield return new WaitForSeconds(.1f);
        gameObject.transform.Rotate(-transform.up);
        yield return new WaitForSeconds(.1f);
        gameObject.transform.Rotate(-transform.right);
        yield return new WaitForSeconds(.1f);
        Debug.Log("Salio");
    }
}
