using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace underDOGS.SDKEvents
{
    public abstract class SDKEventHandler : ScriptableObject
    {
        public abstract void InitSDK();
        public virtual void OnApplicationPausedCallback(bool isPaused) { }
        public abstract void SendEvent(SDKEventData data);
    }
}