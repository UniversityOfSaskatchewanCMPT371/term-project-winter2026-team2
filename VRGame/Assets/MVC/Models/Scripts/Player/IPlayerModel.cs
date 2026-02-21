using UnityEngine;

public interface IPlayerModel
{
    void Initialize(string name, int id);
    string getPlayerName();
    int getId();
}
