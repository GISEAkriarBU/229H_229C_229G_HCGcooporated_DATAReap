using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using System.Threading.Tasks;
//using Unity.VisualScripting;

public class SceneAnalyticsTrigger : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Check which state this scene represents in the Inspector")]
    public bool isWinScene = false;
    public bool isSecretScene = false;
    public bool isLostScene = false;


    public static int MeteorUsing = 0;

    
    async void Start()
    {
        try
        {
            // 1. Initialize Unity Services if they haven't been started yet
            if (UnityServices.State == ServicesInitializationState.Uninitialized)
            {
                var options = new InitializationOptions();
                await UnityServices.InitializeAsync(options);
            }

            // 2. Start data collection (Consent)
            AnalyticsService.Instance.StartDataCollection();

            // 3. Send the data for this specific scene
            SendSceneData();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Analytics Initialization Failed: {e.Message}");
        }
    }

    void SendSceneData()
    {
        // 4. Create the Custom Event using the new v6.0 syntax
        CustomEvent myEvent = new CustomEvent("ContainsEnteringStage")
        {
            { "StageEnterWin", isWinScene },
            { "StageEnterSecret", isSecretScene },
            { "StageEnterLost", isLostScene },
            { "MeteorUse", MeteorUsing }

        };

        // 5. Record and Send
        AnalyticsService.Instance.RecordEvent(myEvent);

        // Flush ensures the event is sent immediately (useful for testing)
        AnalyticsService.Instance.Flush();

        Debug.Log($"<color=cyan>Analytics Success:</color> Sent event 'ContainsEnteringStage' " +
                  $"(Win: {isWinScene}, Secret: {isSecretScene}, Lost: {isLostScene}) using meteor {MeteorUsing} ");
    }
}