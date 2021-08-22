using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

namespace underDOGS.SDKEvents
{
    [CreateAssetMenu(fileName = "GameAnalyticsSettings", menuName = "Settings/SDKs/GameAnalyticsSettings")]
    public class GameAnalyticsHandler : SDKEventHandler
    {
        public override void InitSDK()
        {
            GameAnalytics.Initialize();
        }

        public override void SendEvent(SDKEventData data)
        {
#if UNITY_EDITOR
            Debug.Log($"Game Analytics Send Event called: [{data.status},{data.level.ToString("000")}]");
#endif
            GameAnalytics.NewProgressionEvent(GetStatus(data.status), $"level{data.level.ToString("000")}");
        }

        private GAProgressionStatus GetStatus(string status)
        {
            switch (status)
            {
                case "start":
                    return GAProgressionStatus.Start;
                case "fail":
                    return GAProgressionStatus.Fail;
                case "complete":
                    return GAProgressionStatus.Complete;
                default:
                    return GAProgressionStatus.Undefined;
            }
        }
    }
}
