using UnityEngine;
using UnityEngine.Pool;


public class SpawnManager : MonoBehaviour
{
    public GameObject roadPrefab;
    public ObjectPool<GameObject> pool;
    private GameObject _lastRoad = null;

    void Start()
    {
        pool = new ObjectPool<GameObject>(CreateRoad, OnGet, OnRelease, OnDestroyRoad, false, 5, 20);
    }

    private GameObject CreateRoad()
    {
        GameObject road = Instantiate(roadPrefab, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 90, 0)));
        return road;
    }

    public void OnGet(GameObject obj)
    {
        if (_lastRoad != null)
        {
            obj.transform.position =
                _lastRoad.transform.position + new Vector3(0, 0, _lastRoad.transform.localScale.x);
        }

        _lastRoad = obj;

        obj.gameObject.SetActive(true);
    }

    public void OnRelease(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyRoad(GameObject obj)
    {
        Destroy(obj);
    }

    public void InitialRoadSpawn()
    {
        for (int i = 0; i < 13; i++)
        {
            var road = pool.Get();
            if (i > 1)
            {
                road.GetComponent<Road>().ObstacleRandomize();
            }

            road.gameObject.GetComponent<Road>().pool = pool;
            road.gameObject.SetActive(true);
        }
    }
}