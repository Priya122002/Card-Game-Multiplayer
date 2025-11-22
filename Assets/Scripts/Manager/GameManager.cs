using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            TurnManager.Instance.StartGame();
        }
    }
}
