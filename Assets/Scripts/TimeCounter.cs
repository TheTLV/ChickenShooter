using UnityEngine;
using TMPro; // Thêm namespace cho TextMeshPro

public class TimeCounter : MonoBehaviour
{
    private TextMeshProUGUI timeUI; // Tham chiếu đến TMP UI
    private float startTime;
    private bool startCounter;

    void Start()
    {
        startCounter = false;
        timeUI = GetComponent<TextMeshProUGUI>(); // Lấy component TMP từ GameObject
    }

    public void StartTimeCounter()
    {
        startTime = Time.time;
        startCounter = true;
    }

    public void StopTimeCounter()
    {
        startCounter = false;
    }

    void Update()
    {
        if (startCounter)
        {
            float elapsedTime = Time.time - startTime;
            int minutes = (int)(elapsedTime / 60);
            int seconds = (int)(elapsedTime % 60);

            timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
