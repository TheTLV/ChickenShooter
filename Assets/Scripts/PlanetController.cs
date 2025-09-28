using UnityEngine;
using System.Collections.Generic;

public class PlanetController : MonoBehaviour
{
    public GameObject[] Planets; // Mảng các prefab hành tinh
    private Queue<GameObject> availablePlanets = new Queue<GameObject>();

    void Start()
    {
        // Đưa tất cả hành tinh vào hàng đợi
        foreach (GameObject planet in Planets)
        {
            availablePlanets.Enqueue(planet);
        }

        // Gọi hàm MovePlanetDown mỗi 5 giây
        InvokeRepeating("MovePlanetDown", 0f, 5f);
    }

    void Update()
    {
        EnqueuePlanets(); // Kiểm tra và đưa hành tinh trở lại hàng đợi nếu cần
    }

    // Kích hoạt hành tinh rơi xuống
    void MovePlanetDown()
    {
        if (availablePlanets.Count == 0) return;

        GameObject aPlanet = availablePlanets.Dequeue();
        Planet planetScript = aPlanet.GetComponent<Planet>();
        planetScript.isMoving = true;
    }

    // Đưa hành tinh trở lại hàng đợi nếu nó đã rơi ra khỏi màn hình
    void EnqueuePlanets()
    {
        foreach (GameObject aPlanet in Planets)
        {
            Planet planetScript = aPlanet.GetComponent<Planet>();

            if (aPlanet.transform.position.y < planetScript.GetMinY() && !planetScript.isMoving)
            {
                planetScript.ResetPosition();
                planetScript.isMoving = true;
                availablePlanets.Enqueue(aPlanet);
            }
        }
    }
}
