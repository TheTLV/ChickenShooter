using UnityEngine; // Thư viện chính của Unity, cung cấp các lớp và hàm cần thiết cho game

// Script điều khiển hành vi của enemy (kẻ địch)
public class EnemyControl : MonoBehaviour
{
    public GameObject ExplosionGo; // Prefab hiệu ứng nổ khi enemy bị phá hủy
    [SerializeField] private float speed = 2f; // Tốc độ di chuyển của enemy (có thể chỉnh trong Inspector)

    private float bottomLimit; // Giới hạn dưới của màn hình, nếu enemy vượt qua sẽ bị hủy
    private GameObject scoreUITextGO; // Tham chiếu tới GameObject chứa điểm số (TextMeshPro hoặc UI Text)

    void Start()
    {
        // Tính toán giới hạn dưới của màn hình theo tọa độ thế giới
        bottomLimit = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;

        // Tìm GameObject chứa điểm số bằng tag (đặt tag là "SocreTextTag")
        scoreUITextGO = GameObject.FindGameObjectWithTag("SocreTextTag");
    }

    void Update()
    {
        // Di chuyển enemy xuống dưới theo trục Y với tốc độ đã định
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Nếu enemy vượt qua giới hạn dưới màn hình thì hủy nó để tránh tồn tại vô hạn
        if (transform.position.y < bottomLimit)
        {
            Destroy(gameObject);
        }
    }

    // Xử lý khi enemy va chạm với tàu người chơi hoặc đạn của người chơi
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion(); // Hiển thị hiệu ứng nổ tại vị trí enemy

            // Nếu tìm thấy GameObject điểm số thì cộng điểm
            if (scoreUITextGO != null)
            {
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
            }

            Destroy(gameObject); // Hủy enemy sau khi va chạm
        }
    }

    // Hàm tạo hiệu ứng nổ tại vị trí hiện tại của enemy
    void PlayExplosion()
    {
        GameObject explosion = Instantiate(ExplosionGo); // Tạo hiệu ứng nổ từ prefab
        explosion.transform.position = transform.position; // Đặt vị trí nổ trùng với enemy
    }
}
