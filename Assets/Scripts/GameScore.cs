using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    private TextMeshProUGUI scoreTextUI;
    private int score;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            UpdateScoreTextUI();
        }
    }

    void Start()
    {
        // Tìm GameObject có tag "ScoreTextTag"
        GameObject scoreGO = GameObject.FindGameObjectWithTag("SocreTextTag");

        if (scoreGO != null)
        {
            scoreTextUI = scoreGO.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("Không tìm thấy GameObject với tag 'SocreTextTag'");
        }

        UpdateScoreTextUI();
    }

    void UpdateScoreTextUI()
    {
        if (scoreTextUI != null)
        {
            string scoreStr = string.Format("{0:00000}", score); // Ví dụ: 00025
            scoreTextUI.text = scoreStr;
        }
    }
}
