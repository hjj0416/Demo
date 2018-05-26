using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITaskItem : MonoBehaviour {

    [SerializeField] Text text;
    [SerializeField] Button button;
    private TaskItemBean TaskItem;

    public void SetData(TaskItemBean taskItem)
    {
        TaskItem = taskItem;
        text.text = TaskItem.name;
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        UIManager.SendEvent<TaskItemBean>(UIEventType.ON_CLICK_TASKITEM, TaskItem);
    }
}
