using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData{
    public static bool isOpenEnterNameScreen;
    public static string isCurrentTime;
    public static List<PlayerData> playersData=new List<PlayerData>() ;

}

public class PlayerData{
    public string name;
    public string time;

    public PlayerData(string name,string time){
        this.name = name;
        this.time = time;
    }
}