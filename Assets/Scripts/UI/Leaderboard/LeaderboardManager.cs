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
        foreach (var playerData in StaticData.playersData){
            AddNewUserToList(playerData.name,playerData.time);
        }
    }

    private void AddNewUserToList(string player,string time)
    {
        var newUser = Instantiate(_userDataPref, _userPanel.transform);
        newUser.SetData(player,  time);
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
