using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;

public class ScoreboardManager : MonoBehaviour
{
    [SerializeField] private RowUI rowUi;
    [SerializeField] private Transform _content;

    [SerializeField] private Button _closeScoreboardButton;
    [SerializeField] private GameObject _scoreBoardPanel;

    private List<Score> _scores;
    private FirebaseUser _user;

    private void Awake()
    {
        _user = FirebaseAuth.DefaultInstance.CurrentUser;
        _closeScoreboardButton.onClick.AddListener(CloseScoreboard);
        Singleton.Instance.DatabaseManager.GetScores((scores) =>
        {
            _scores = scores;
            UpdateScoreboard();
        });
    }

    private void CloseScoreboard()
    {
        _scoreBoardPanel.SetActive(false);
    }

    private IEnumerable<Score> GetHighScores()
    {
        return _scores.OrderByDescending(x => x.score);
    }
    public void ClearBoard()
    {
        _scores.Clear();
        UpdateScoreboard();
    }

    public void UpdateScoreboard()
    {
        var scores = GetHighScores().ToArray();
        for (int i = 0; i < scores.Length; i++)
        {
            RowUI row = Instantiate(rowUi, _content);
            row.rank.text = (i + 1).ToString();
            row.name.text = scores[i].name;
            row.score.text = scores[i].score.ToString();

            if (scores[i].name == _user.DisplayName)
            {
                row.rank.color = Color.yellow;
                row.name.color = Color.yellow;
                row.score.color = Color.yellow;
            }
        }
    }
}
