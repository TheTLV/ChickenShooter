using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 8f;

    private float topLimit;

    void Start()
    {
        // Tính giới hạn trên của màn hình một lần duy nhất
        topLimit = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).y;
    }

    void Update()
    {
        // Di chuyển đạn lên trên
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Nếu vượt quá màn hình thì hủy đạn
        if (transform.position.y > topLimit)
        {
            Destroy(gameObject);
        }
    }
}
