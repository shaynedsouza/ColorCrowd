using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

namespace underDOGS.SDKEvents
{
    [CreateAssetMenu(fileName = "FacebookSDKSettings", menuName = "Settings/SDKs/FacebookSDKSettings")]
    public class FacebookHandler : SDKEventHandler
    {
        public override void InitSDK()
        {
            if (!FB.IsInitialized)
            {
                // Initialize the Facebook SDK
                FB.Init(InitCallback);
            }
            else
            {
                // Already initialized, signal an app activation App Event
                FB.ActivateApp();
            }
        }

        private void InitCallback()
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }

        public override void OnApplicationPausedCallback(bool isPaused)
        {
            if (!isPaused)
            {
                //app resume
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
                }
                else
                {
                    // Initialize the Facebook SDK
                    FB.Init(InitCallback);
                }
            }
        }

        public override void SendEvent(SDKEventData data)
        {
#if UNITY_EDITOR
            Debug.Log($"Facebook SDK Send Event called: {data.GetEventDataString()}");
#endif

            Dictionary<string, object> _params = new Dictionary<string, object>();
            _params.Add("Level", data.level);
            FB.LogAppEvent($"level_{data.status}",parameters: _params);
        }
    }
}
