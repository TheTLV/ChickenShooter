using UnityEngine;

public class StarGeneration : MonoBehaviour
{
    [SerializeField] private GameObject StarGo;     // Prefab ngôi sao
    [SerializeField] private int MaxStars = 100;    // Số lượng ngôi sao

    private Color[] starColors = new Color[]
    {
        new Color(1, 1, 1, 1), // trắng
        new Color(0, 0, 1, 1), // xanh dương
        new Color(1, 0, 0, 1), // đỏ
        new Color(0, 1, 0, 1), // xanh lá
        new Color(1, 1, 0, 1), // vàng
    };

    void Start()
    {
        // Tính tọa độ màn hình
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        for (int i = 0; i < MaxStars; i++)
        {
            // Tạo ngôi sao mới
            GameObject star = Instantiate(StarGo);

            // Gán màu ngẫu nhiên
            star.GetComponent<SpriteRenderer>().color = starColors[i % starColors.Length];

            // Gán vị trí ngẫu nhiên trong màn hình
            float randomX = Random.Range(min.x, max.x);
            float randomY = Random.Range(min.y, max.y);
            star.transform.position = new Vector2(randomX, randomY);

            // Gán tốc độ rơi ngẫu nhiên
            float randomSpeed = 1f + Random.value * 2f;
            star.GetComponent<Star>().Speed = randomSpeed;

            // Gán star làm con của StarGeneration để dễ quản lý
            star.transform.parent = transform;
        }
    }
}
