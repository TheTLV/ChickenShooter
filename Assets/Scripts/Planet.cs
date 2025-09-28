using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    public bool isMoving = false;

    private float minY;
    private float maxY;
    private float minX;
    private float maxX;

    private float delayTimer = 0f;
    private float delayDuration = 0.5f; // ⏳ thời gian chờ sau khi rơi

    void Start()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        minY = min.y;
        maxY = max.y;
        minX = min.x;
        maxX = max.x;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;

            if (transform.position.y < minY)
            {
                isMoving = false;
                delayTimer = delayDuration; // bắt đầu đếm thời gian chờ
            }
        }
        else
        {
            if (delayTimer > 0f)
            {
                delayTimer -= Time.deltaTime;

                if (delayTimer <= 0f)
                {
                    ResetPosition();
                    isMoving = true;
                }
            }
        }
    }

    public void ResetPosition()
    {
        float randomX = Random.Range(minX, maxX);
        transform.position = new Vector2(randomX, maxY);

        // Tuỳ chọn: tốc độ rơi ngẫu nhiên
        speed = Random.Range(0.5f, 2f);
    }

    public float GetMinY()
    {
        return minY;
    }
}
