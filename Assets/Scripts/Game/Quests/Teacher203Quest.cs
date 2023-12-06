using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher203Quest : MonoBehaviour
{
    public GameObject target;
    private static int value, tmp;
    private int goal = 1;
    private static Teacher203Quest _internal;

    public static Teacher203Quest Internal
    {
        get { return _internal; }
    }

    void Awake()
    {
        ResetQuest();

        value = 0; // начальное значение статуса

        _internal = this;
        enabled = false;
    }

    void LateUpdate()
    {
        tmp = 0;

        if (!target.activeSelf)
        {
            tmp++;
        }

        if (tmp == goal)
        {
            value = 2; // цель достигнута
            enabled = false;
        }
    }

    public static int questValue
    {
        get { return value; }
    }

    public void QuestStatus(QuestManager.Status status)
    {
        switch (status)
        {
            case QuestManager.Status.Active:
                SetActiveQuest();
                break;
            case QuestManager.Status.Complete:
                SetCompleteQuest();
                break;
            case QuestManager.Status.Disable:
                ResetQuest();
                break;
        }
    }

    void SetActiveQuest()
    {
        value = 1; // квест активен
        enabled = true;
        target.SetActive(true);
    }

    void SetCompleteQuest()
    {
        enabled = false;
        value = -1; // квест сдан
    }

    void ResetQuest()
    {
        enabled = false;
        value = 0;       
        target.SetActive(false);
    }
}
