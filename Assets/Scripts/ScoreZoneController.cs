using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreZoneController : MonoBehaviour
{
    private static Dictionary<int, int> _scores = new Dictionary<int, int>();
    
    [SerializeField] private List<Text> _scoreTexts = new List<Text>();
    [SerializeField] private AudioClip _scoreSound = null;
    [SerializeField] [Range(0, 1)] private int _playerIndex = 0;
    [SerializeField] private BallController _ball = null;

    public static void ResetScores()
    {
        _scores[0] = 0;
        _scores[1] = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball")
        {
            PlayerScored();
        }
    }

    private void PlayerScored()
    {
        _ball.gameObject.SetActive(false);
        Util.PlayUISound(_scoreSound);
        if (_scores.Count == 0)
        {
            ResetScores();
        }
        _scores[_playerIndex]++;
        SetScoreboard();
        StartCoroutine(RestartRoutine());
    }

    private void SetScoreboard()
    {
        if (_scoreTexts.Count > 0)
        {
            _scoreTexts[0].text = _scores[0].ToString();
        }
        if (_scoreTexts.Count > 1)
        {
            _scoreTexts[1].text = _scores[1].ToString();
        }
    }

    private IEnumerator RestartRoutine()
    {
        yield return new WaitForSeconds(1f);
        _ball.ResetBall(_playerIndex);

    }
}
