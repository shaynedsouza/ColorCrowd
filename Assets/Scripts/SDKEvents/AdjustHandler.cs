using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.adjust.sdk;


namespace underDOGS.SDKEvents
{
    [CreateAssetMenu(fileName = "AdjustSDKSettings", menuName = "Settings/SDKs/AdjustSDKSettings")]
    public class AdjustHandler : SDKEventHandler
    {
        public string API_TOKEN = "YOUR_ANDROID_APP_TOKEN_HERE";

        public override void InitSDK()
        {
            AdjustConfig config = new AdjustConfig(
                                        API_TOKEN,
                                        AdjustEnvironment.Production, // AdjustEnvironment.Sandbox to test in dashboard
                                        true
                                    );
            new GameObject("Adjust").AddComponent<Adjust>();
            Adjust.start(config);
        }

        public override void SendEvent(SDKEventData data)
        {
#if UNITY_EDITOR
            Debug.Log($"Adjust SDK Send Event called {data.GetEventDataString()}");
#endif
            AdjustEvent adjustEvent = new AdjustEvent($"level_{data.status}");
            adjustEvent.addCallbackParameter("Level", data.level.ToString());
            Adjust.trackEvent(adjustEvent);
        }
    }
}
