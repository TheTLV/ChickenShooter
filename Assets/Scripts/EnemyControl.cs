using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject ExplosionGo;
    [SerializeField] private float speed = 2f;

    private float bottomLimit;
    private GameObject scoreUITextGO; // Tham chiếu tới GameObject chứa TextMeshPro

    void Start()
    {
        bottomLimit = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;

        // Tìm GameObject chứa TextMeshPro bằng tag
        scoreUITextGO = GameObject.FindGameObjectWithTag("SocreTextTag");
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

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

            // Cộng điểm
            if (scoreUITextGO != null)
            {
                scoreUITextGO.GetComponent<GameScore>().Score += 100;
            }

            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = Instantiate(ExplosionGo);
        explosion.transform.position = transform.position;
    }
}
