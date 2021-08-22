using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace underDOGS.SDKEvents
{
    // Custom Data Strut which will be passed to Handlers.
    public struct SDKEventData
    {
        // Might need a change with different needs
        public float level;
        public string status;

        public string GetEventDataString()
        {
            // Format this however you like it.
            return $"Level {level} {status}";
        }
    }

    public class SDKManager : MonoBehaviour
    {
        public static SDKManager instance;

        public SDKEventHandler[] SDK_Events;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                foreach (SDKEventHandler eventHandler in SDK_Events)
                {
                    eventHandler.InitSDK();
                }
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        /// <summary>
        /// This is required for Facebook SDK to reinitialize the SDK if it is not intialized.
        /// </summary>
        /// <param name="isPaused">Whether the application is paused or resumed</param>
        private void OnApplicationPause(bool isPaused)
        {
            foreach (SDKEventHandler eventHandler in SDK_Events)
            {
                eventHandler.OnApplicationPausedCallback(isPaused);
            }
        }

        /// <summary>
        /// Send the data to the respective Event Handlers to send the actual events.
        /// </summary>
        /// <param name="data"></param>
        public void SendEvent(SDKEventData data)
        {
            foreach (SDKEventHandler eventHandler in SDK_Events)
            {
                eventHandler.SendEvent(data);
            }
        }
    }
}