using UnityEngine; // Thư viện chính của Unity, cung cấp các lớp và hàm cần thiết cho game

// Script điều khiển hành vi bắn đạn của enemy
public class EnemyGun : MonoBehaviour
{
    [SerializeField] private GameObject EnemyBulletGo; // Prefab viên đạn của enemy (gán trong Inspector)
    [SerializeField] private float fireRate = 0.5f; // Thời gian giữa các lần bắn (tính bằng giây)

    private bool isAlive = true; // Cờ kiểm tra xem enemy còn sống không

    void Start()
    {
        // Gọi hàm FireEnemyBullet lặp lại sau mỗi khoảng thời gian fireRate
        // Bắt đầu sau 0.5 giây kể từ khi enemy xuất hiện
        InvokeRepeating(nameof(FireEnemyBullet), 0.5f, fireRate);
    }

    // Hàm xử lý việc bắn đạn
    void FireEnemyBullet()
    {
        if (!isAlive) return; // Nếu enemy đã chết thì không bắn nữa

        // Tìm GameObject của tàu người chơi theo tên "PlayerGo"
        GameObject playerShip = GameObject.Find("PlayerGo");

        if (playerShip != null) // Nếu tìm thấy tàu người chơi
        {
            // Tạo một viên đạn mới từ prefab EnemyBulletGo
            GameObject bullet = Instantiate(EnemyBulletGo);

            // Đặt vị trí viên đạn tại vị trí của enemy
            bullet.transform.position = transform.position;

            // Tính hướng từ enemy đến tàu người chơi
            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            // Gán hướng bay cho viên đạn (chuẩn hóa để đảm bảo tốc độ ổn định)
            bullet.GetComponent<EnemyBullet>().SetDirection(direction.normalized);
        }
    }

    // Hàm này được gọi khi enemy bị tiêu diệt
    public void StopFiring()
    {
        isAlive = false; // Đánh dấu enemy đã chết
        CancelInvoke(nameof(FireEnemyBullet)); // Ngừng việc gọi lặp lại hàm FireEnemyBullet
    }
}
