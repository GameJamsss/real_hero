using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private UserData _userDataPref;
    [SerializeField] private GameObject _userPanel;

    private List<UserData> _users = new List<UserData>();
  
    private void OnEnable() //reset leaderboard
    {
        //delete old data
        ClearLeadboard();
        //create new and set new data
        for (int i = 0; i < 7; i++)
        {
            AddNewUserToList();
        }       
    }

    private void AddNewUserToList()
    {
        var newUser = Instantiate(_userDataPref, _userPanel.transform);
        newUser.SetData("Bob", "3291", "1:30");
        _users.Add(newUser);
    }

    private void ClearLeadboard()
    {
        if (_users.Count == 0)
            return;

        foreach (var user in _users)
            Destroy(user.gameObject);

        _users.Clear();
    }
}
