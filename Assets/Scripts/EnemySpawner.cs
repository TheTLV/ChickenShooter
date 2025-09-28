using UnityEngine; // Thư viện chính của Unity, cung cấp các lớp và hàm cần thiết cho game

// Script điều khiển việc sinh (spawn) enemy theo thời gian
public class SpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject EnemyGo; // Prefab của enemy sẽ được sinh ra (gán trong Inspector)
    [SerializeField] private float maxSpawnRateInSeconds = 5f; // Thời gian tối đa giữa các lần spawn

    private float initialSpawnRate; // Biến lưu giá trị gốc ban đầu của thời gian spawn
    private float minX; // Tọa độ X nhỏ nhất (bên trái màn hình)
    private float maxX; // Tọa độ X lớn nhất (bên phải màn hình)
    private float spawnY; // Tọa độ Y để spawn enemy (trên cùng màn hình)

    void Start()
    {
        initialSpawnRate = maxSpawnRateInSeconds; // Ghi nhớ giá trị ban đầu để reset sau này

        // Tính giới hạn màn hình theo tọa độ thế giới
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // Góc dưới trái
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // Góc trên phải

        minX = min.x; // Giới hạn trái
        maxX = max.x; // Giới hạn phải
        spawnY = max.y; // Vị trí Y để sinh enemy (trên cùng màn hình)
    }

    // Hàm công khai để bắt đầu sinh enemy
    public void ScheduleEnemySpawner()
    {
        maxSpawnRateInSeconds = initialSpawnRate; // Reset về tốc độ spawn ban đầu
        Invoke(nameof(SpawnEnemy), maxSpawnRateInSeconds); // Gọi hàm SpawnEnemy sau một khoảng delay
        InvokeRepeating(nameof(IncreaseSpawnRate), 0f, 30f); // Cứ mỗi 30 giây thì tăng độ khó bằng cách giảm thời gian spawn
    }

    // Hàm công khai để dừng sinh enemy
    public void UnscheduleEnemySpawner()
    {
        CancelInvoke(nameof(SpawnEnemy)); // Ngừng gọi hàm SpawnEnemy
        CancelInvoke(nameof(IncreaseSpawnRate)); // Ngừng tăng độ khó
    }

    // Hàm tạo enemy tại vị trí ngẫu nhiên
    void SpawnEnemy()
    {
        if (EnemyGo == null) return; // Nếu chưa gán prefab thì không làm gì

        // Tạo vị trí ngẫu nhiên theo chiều ngang ở cạnh trên màn hình
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(randomX, spawnY);

        // Tạo enemy mới tại vị trí spawn
        Instantiate(EnemyGo, spawnPosition, Quaternion.identity); // Quaternion.identity = không xoay

        // Lên lịch lần spawn tiếp theo với thời gian ngẫu nhiên
        ScheduleNextEnemySpawn();
    }

    // Hàm lên lịch lần spawn tiếp theo
    void ScheduleNextEnemySpawn()
    {
        // Nếu thời gian spawn > 1 giây thì chọn ngẫu nhiên từ 1 đến max
        // Nếu đã <= 1 giây thì giữ ở mức 1 giây
        float spawnInSeconds = maxSpawnRateInSeconds > 1f
            ? Random.Range(1f, maxSpawnRateInSeconds)
            : 1f;

        Invoke(nameof(SpawnEnemy), spawnInSeconds); // Gọi lại SpawnEnemy sau thời gian spawnInSeconds
    }

    // Hàm tăng độ khó bằng cách giảm thời gian giữa các lần spawn
    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
        {
            // Giảm thời gian spawn, nhưng không nhỏ hơn 1 giây
            maxSpawnRateInSeconds = Mathf.Max(1f, maxSpawnRateInSeconds - 1f);
        }

        // Nếu đã đạt mức tối thiểu (1 giây), ngừng tăng độ khó
        if (maxSpawnRateInSeconds == 1f)
        {
            CancelInvoke(nameof(IncreaseSpawnRate));
        }
    }
}
