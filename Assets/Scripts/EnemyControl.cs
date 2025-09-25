using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private float bottomLimit;

    void Start()
    {
        // Tính giới hạn dưới của màn hình một lần duy nhất
        bottomLimit = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;
    }

    void Update()
    {
        // Di chuyển enemy xuống dưới
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Nếu vượt quá màn hình thì hủy enemy
        if (transform.position.y < bottomLimit)
        {
            Destroy(gameObject);
        }
    }
}
