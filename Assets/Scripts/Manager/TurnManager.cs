using Unity.Netcode;
using UnityEngine;

public class TurnManager : NetworkBehaviour
{
    public static TurnManager Instance;

    public NetworkVariable<int> CurrentTurn = new NetworkVariable<int>(1);
    public NetworkVariable<bool> IsTimerRunning = new NetworkVariable<bool>(false);
    private bool notifiedServer = false;
    private NetworkVariable<bool> player1Done = new NetworkVariable<bool>(false);
    private NetworkVariable<bool> player2Done = new NetworkVariable<bool>(false);

    private const float TURN_TIME = 30f;
    private const int MAX_TURN = 6;

    // ✅ LOCAL TIMER ONLY
    private float localTimer;
    private bool localEnded = false;

    private void Awake()
    {
        Instance = this;
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            StartGame();
        }

        // ✅ IMPORTANT: listen when turn value changes
        CurrentTurn.OnValueChanged += OnTurnChanged;

        ResetLocalTimer();
    }

    private void OnTurnChanged(int oldValue, int newValue)
    {
        Debug.Log($"[CLIENT] Turn changed from {oldValue} to {newValue}");

        // ✅ Reset client-side-only state
        localEnded = false;
        ResetLocalTimer();

        // ✅ Stop timer so Play Card button becomes enabled
       // IsTimerRunning.Value = false;
    }

    public void StartGame()
    {
        if (!IsServer) return;
        StartTurn();
    }

    private void StartTurn()
    {
        localEnded = false;
        ResetLocalTimer();

        // ✅ IMPORTANT so UI enables Play Card
        IsTimerRunning.Value = false;
    }

    private void Update()
    {
        // ✅ SERVER CHECK SHOULD COME FIRST
        if (IsServer && BothPlayersDone())
        {
            Debug.Log("[SERVER] Both players finished → Going to next turn");

            // Stop timer so PlayCard becomes interactable
            IsTimerRunning.Value = false;

            // Increase turn (this updates UI)
            CurrentTurn.Value += 1;

            // Reset state
            player1Done.Value = false;
            player2Done.Value = false;

            localEnded = false;
            ResetLocalTimer();

            return;
        }

        // ---- normal timer logic ----

        if (!IsTimerRunning.Value) return;
        if (localEnded) return;

        localTimer -= Time.deltaTime;

        if (localTimer <= 0)
        {
            localTimer = 0;
            localEnded = true;

            Debug.Log("[CLIENT] Local timer finished");
            NotifyTurnEndedServerRpc();
        }
    }


    private void ResetLocalTimer()
    {
        localTimer = TURN_TIME;
    }

    private void NextTurn()
    {
        if (CurrentTurn.Value >= MAX_TURN)
        {
            Debug.Log("Game Over");
            return;
        }

        CurrentTurn.Value++;
        StartTurn();
    }

    // ✅ Global start (shared)
    [ServerRpc(RequireOwnership = false)]
    public void StartTimerServerRpc()
    {
        IsTimerRunning.Value = true;
    }

    public void EndTurnLocal()
    {
        Debug.Log($"[CLIENT] I locally ended turn. ClientId: {NetworkManager.Singleton.LocalClientId}");

        localEnded = true;
        ResetLocalTimer();

        NotifyTurnEndedServerRpc();
    }


    [ServerRpc(RequireOwnership = false)]
    private void NotifyTurnEndedServerRpc(ServerRpcParams p = default)
    {
        ulong id = p.Receive.SenderClientId;

        Debug.Log($"[SERVER] Received TurnFinished from ClientId: {id}");

        if (id == 0)
        {
            player1Done.Value = true;
            Debug.Log("[SERVER] Marked Player 1 as DONE");
        }
        else
        {
            player2Done.Value = true;
            Debug.Log("[SERVER] Marked Player 2 as DONE");
        }

        DebugPlayersState();
    }
    private void DebugPlayersState()
    {
        Debug.Log($"[SERVER STATE] P1 Done: {player1Done.Value} | P2 Done: {player2Done.Value}");
    }

    private bool BothPlayersDone()
    {
        return player1Done.Value && player2Done.Value;
    }

    public float GetLocalTime()
    {
        return localTimer;
    }

    public bool HaveIEnded()
    {
        return localEnded;
    }
}
