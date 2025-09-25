using UnityEngine;


public class SpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject EnemyGo; // Prefab của enemy sẽ được sinh ra
    [SerializeField] private float maxSpawnRateInSeconds = 5f; // Thời gian tối đa giữa các lần spawn

    private float minX;
    private float maxX;
    private float spawnY;

    void Start()
    {
        // Tính giới hạn màn hình để spawn enemy trong phạm vi hợp lệ
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        minX = min.x;
        maxX = max.x;
        spawnY = max.y;

        // Gọi hàm spawn enemy lần đầu sau khoảng thời gian ban đầu
        Invoke(nameof(SpawnEnemy), maxSpawnRateInSeconds);

        // Gọi hàm tăng độ khó mỗi 30 giây
        InvokeRepeating(nameof(IncreaseSpawnRate), 0f, 30f);
    }

    void SpawnEnemy()
    {
        // Tạo vị trí ngẫu nhiên theo chiều ngang ở cạnh trên màn hình
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPosition = new Vector2(randomX, spawnY);

        // Tạo enemy mới tại vị trí spawn
        Instantiate(EnemyGo, spawnPosition, Quaternion.identity);

        // Lên lịch lần spawn tiếp theo với thời gian ngẫu nhiên
        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds;

        // Nếu thời gian spawn vẫn lớn hơn 1 giây, chọn ngẫu nhiên từ 1 đến max
        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
        {
            spawnInSeconds = 1f; // Giới hạn tối thiểu là 1 giây
        }

        // Gọi lại SpawnEnemy sau khoảng thời gian đã chọn
        Invoke(nameof(SpawnEnemy), spawnInSeconds);
    }

    void IncreaseSpawnRate()
    {
        // Giảm thời gian giữa các lần spawn để tăng độ khó
        if (maxSpawnRateInSeconds > 1f)
        {
            maxSpawnRateInSeconds--;
        }

        // Nếu đã đạt mức tối thiểu, ngừng tăng độ khó
        if (maxSpawnRateInSeconds == 1f)
        {
            CancelInvoke(nameof(IncreaseSpawnRate));
        }
    }
}


