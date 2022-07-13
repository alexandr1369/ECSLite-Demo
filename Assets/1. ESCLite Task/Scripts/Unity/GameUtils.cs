using System;
using System.Collections;

namespace _1._ESCLite_Task.Scripts.Unity
{
    public static class GameUtils
    {
        public static IEnumerator DelayedActionForNextFrame(Action action)
        {
            yield return null;
            action.Invoke();
        }
    }
}