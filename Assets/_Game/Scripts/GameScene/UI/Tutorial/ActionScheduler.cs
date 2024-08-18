using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionScheduler : MonoBehaviour
{
    private class ScheduledAction
    {
        public Action Action { get; set; }
        public Func<bool> Condition { get; set; }
    }

    private readonly List<ScheduledAction> _scheduledActions = new();

    public void ScheduleAction(Action action, Func<bool> condition)
    {
        _scheduledActions.Add(new ScheduledAction { Action = action, Condition = condition });
    }

    private void Update()
    {
        for (int i = _scheduledActions.Count - 1; i >= 0; i--)
        {
            if (_scheduledActions[i].Condition())
            {
                _scheduledActions[i].Action.Invoke();
                _scheduledActions.RemoveAt(i);
            }
        }
    }
}
