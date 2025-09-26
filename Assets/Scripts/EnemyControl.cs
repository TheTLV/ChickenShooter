using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject ExplosionGo;
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
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
        {
            PlayExplosion();
            Destroy(gameObject);
        }
    }
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGo);

        explosion.transform.position = transform.position;
    }
}
