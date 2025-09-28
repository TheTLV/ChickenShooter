using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    // Property công khai để truy cập và gán speed từ bên ngoài
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Max(0f, value); // đảm bảo không gán giá trị âm
    }

    private Vector2 minScreenBounds;
    private Vector2 maxScreenBounds;

    void Start()
    {
        // Tính toán giới hạn màn hình một lần để tối ưu hiệu suất
        minScreenBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxScreenBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Update()
    {
        // Di chuyển ngôi sao xuống dưới theo thời gian thực
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Nếu ngôi sao đi ra khỏi màn hình phía dưới
        if (transform.position.y < minScreenBounds.y)
        {
            // Đặt lại vị trí ở phía trên với tọa độ X ngẫu nhiên
            float randomX = Random.Range(minScreenBounds.x, maxScreenBounds.x);
            transform.position = new Vector2(randomX, maxScreenBounds.y);
        }
    }
}
