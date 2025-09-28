using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // Tốc độ bay của đạn

    private Vector2 _direction; // Hướng bay của đạn
    private bool isReady = false; // Cờ kiểm tra xem đạn đã được thiết lập hướng chưa

    void Awake()
    {
        isReady = false; // Mặc định chưa sẵn sàng
    }

    // Hàm được gọi để thiết lập hướng bay cho đạn
    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized; // Chuẩn hóa vector để đảm bảo tốc độ ổn định
        isReady = true;// Đạn đã sẵn sàng bay
    }

    void Update()
    {
        if (!isReady) return; // Nếu chưa sẵn sàng thì không di chuyển

        // Di chuyển đạn theo hướng đã thiết lập
        transform.Translate(_direction * speed * Time.deltaTime);

        // Kiểm tra nếu đạn vượt ra khỏi màn hình thì hủy
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector2 pos = transform.position;// Vị trí hiện tại của đạn

        // Nếu đạn vượt ra khỏi màn hình thì hủy
        if (pos.x < min.x || pos.x > max.x || pos.y < min.y || pos.y > max.y)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col) // Xử lý va chạm với tàu người chơi
    {
        if (col.tag == "PlayerShipTag")// Nếu va chạm với tàu người chơi
        {
            Destroy(gameObject);
        }
    }
}
