using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField] private GameObject EnemyBulletGo; // Prefab của đạn enemy

    void Start()
    {
        // Gọi hàm bắn đạn sau 1 giây kể từ khi game bắt đầu
        Invoke(nameof(FireEnemyBullet), 1f);
    }

    void FireEnemyBullet()
    {
        // Tìm GameObject của người chơi theo tên
        GameObject playerShip = GameObject.Find("PlayerGo");

        if (playerShip != null) // Nếu người chơi vẫn còn tồn tại
        {
            // Tạo một viên đạn enemy mới
            GameObject bullet = Instantiate(EnemyBulletGo);

            // Đặt vị trí ban đầu của viên đạn tại vị trí của EnemyGun
            bullet.transform.position = transform.position;

            // Tính toán hướng bay từ enemy đến người chơi
            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            // Gửi hướng bay cho script EnemyBullet để xử lý di chuyển
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
