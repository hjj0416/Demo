using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UITask : UIWin {
    [SerializeField] GameObject TaskItem;
    [SerializeField] GameObject TaskGameObject;
    [SerializeField] Text DetialText;
    [SerializeField] Button OKButton;
    [SerializeField] Button CancelButton;
    private TaskItemBean taskItem;

    // Use this for initialization
    protected override void OnOpened()
    {
        base.OnOpened();
        UIManager.AddEventHandler<TaskItemBean>(UIEventType.ON_CLICK_TASKITEM, ShowTaskItem);
        TaskDataMgr.AddEventHandler<int>(TaskDataEvent.FINSH_TASK, Finsh);
        OKButton.onClick.AddListener(Get);
        CancelButton.onClick.AddListener(Delete);
        OKButton.gameObject.SetActive(false);
        CancelButton.gameObject.SetActive(false);
        ShowTask();
    }

    protected override void OnClosed()
    {
        UIManager.RemoveEventHandler<TaskItemBean>(UIEventType.ON_CLICK_TASKITEM,ShowTaskItem);
        TaskDataMgr.RemoveEventHandler<int>(TaskDataEvent.FINSH_TASK, Finsh);
        base.OnClosed();
    }

    void ShowTaskItem(short evt,TaskItemBean taskItemBean)
    {
        taskItem = taskItemBean;
        DetialText.text = GetText();
        ShowButton();
    }

    void ShowButton()
    {
        if (TaskDataMgr.Instance.IsTask(taskItem.ID))
        {
            OKButton.gameObject.SetActive(false);
            CancelButton.gameObject.SetActive(true);
        }
        else
        {
            OKButton.gameObject.SetActive(true);
            CancelButton.gameObject.SetActive(false);
        }
    }

    private string GetText()
    {
        if (taskItem == null)
            return "";
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<color=red>任务ID:{0}</color>\n\n", taskItem.ID,taskItem);
        sb.AppendFormat("任务名称：{0}\n\n",taskItem.name);
        sb.AppendFormat("任务描述：{0}\n\n",taskItem.description);
        sb.AppendFormat("任务奖励：经验：{0} 金币：{1}\n\n",taskItem.rewardExp,taskItem.rewardCoin);
        return sb.ToString();
    }

    void Get()
    {
        if (!TaskDataMgr.Instance.IsTask(taskItem.ID))
        {
            TaskDataMgr.Instance.GetTask(taskItem.ID,taskItem.completeParam2,true);
            UIManager.Instance.ShowTips("接受了任务");
            ShowButton();
        }
    }

    void Delete()
    {
        if (TaskDataMgr.Instance.IsTask(taskItem.ID))
        {
            TaskDataMgr.Instance.DeleteTask(taskItem.ID, true);
            UIManager.Instance.ShowTips("放弃了任务");
            ShowButton();
        }
    }

    void Finsh(short evt,int id)
    {
        Dictionary<int, TaskItemBean> beans = TaskItemBean.beans;
        foreach (var v in beans)
        {
            if(v.Value.ID==id)
            {
                UIManager.Instance.ShowTips(string.Format("任务完成！获得金币:{0},经验:{1}",v.Value.rewardCoin,v.Value.rewardExp));
                PlayerDataMgr.Instance.playerData.AddCoin(v.Value.rewardCoin);
                PlayerDataMgr.Instance.playerData.AddExp(v.Value.rewardExp);
            }
        }
    }

    void ShowTask()
    {
        Dictionary<int, TaskItemBean> beans = TaskItemBean.beans;
        foreach(var v in beans)
        {
            if(true)//预留接指定任务
            {
                GameObject item = Instantiate(TaskItem);
                item.transform.SetParent(TaskGameObject.transform);
                if (!item.activeSelf)
                    item.SetActive(true);
                item.transform.localScale = Vector3.one;
                UITaskItem uiTaskItem = item.GetComponent<UITaskItem>();
                uiTaskItem.SetData(v.Value);
            }
        }
    }
}
