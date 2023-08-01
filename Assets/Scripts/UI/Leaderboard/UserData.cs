using TMPro;
using UnityEngine;

public class UserData : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _time;

    public void SetData(string userName, string userTime)
    {
        _name.text = userName;
        _time.text = userTime;
    }
}
