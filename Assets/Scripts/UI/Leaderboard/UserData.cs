using TMPro;
using UnityEngine;

public class UserData : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _time;

    public void SetData(string userName, string userScore, string userTime)
    {
        _name.text = userName;
        _score.text = userScore;
        _time.text = userTime;
    }
}
