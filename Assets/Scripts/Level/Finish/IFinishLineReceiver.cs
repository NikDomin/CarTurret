using UnityEngine;

namespace Level.Finish
{
    public interface IFinishLineReceiver
    {
        void SetFinishLine(Transform target);   
    }
}