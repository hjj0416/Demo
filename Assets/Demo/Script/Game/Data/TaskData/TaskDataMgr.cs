using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDataMgr : BaseDataMgr<TaskDataMgr> {

    private TaskData _taskData;
    public TaskData taskData
    {
        get
        {
            if (_taskData == null)
                _taskData = data as TaskData;
            return _taskData;
        }
    }

    private const string TASKITEM_FILE_PATH="TaskItem.txt";

    public override void Init()
    {
        base.Init();
        file_path = TASKITEM_FILE_PATH;
        Read(typeof(TaskData));
    }

    public bool IsTask(int id)
    {
        bool find = false;
        for (int i = 0; i < taskData.items.Count; i++)
        {
            var item = taskData.items[i];
            if (item.Id == id)
            {
                find = true;
                break;
            }
        }
        return find;
    }

    public void GetTask(int id,int maxNum,bool autoSave=false)
    {
        taskData.items.Add(new Task(id,0,maxNum,false));
        if(autoSave)
        {
            Save();
        }
    }

    public void DeleteTask(int id, bool autoSave = false)
    {
        for (int i = 0; i < taskData.items.Count; i++)
        {
            var item = taskData.items[i];
            if (item.Id == id)
            {
                taskData.items.Remove(item);
                break;
            }
        }
        if (autoSave)
        {
            Save();
        }
    }

    public void AddNum(int id, int num, bool autoSave = false)
    {
        for (int i = 0; i < taskData.items.Count; i++)
        {
            var item = taskData.items[i];
            if (item.Id == id&&item.IsFinsh==false)
            {
                item.Num += num;
                UIManager.Instance.ShowTips(string.Format("进行中:{0}/{1}", item.Num, item.MaxNum));
                if (item.Num == item.MaxNum)
                {
                    FinshTask(item,true);
                    break;
                }
                break;
            }
        }
        if (autoSave)
        {
            Save();
        }
        MarkDirty();
    }

    void FinshTask(Task item,bool autoSave=false)
    {
        item.IsFinsh = true;
        Dictionary<int, TaskItemBean> beans = TaskItemBean.beans;
        foreach (var v in beans)
        {
            if (v.Value.ID == item.Id)
            {
                UIManager.Instance.ShowTips(string.Format("任务完成！获得金币:{0},经验:{1}", v.Value.rewardCoin, v.Value.rewardExp));
                PlayerDataMgr.Instance.playerData.AddCoin(v.Value.rewardCoin);
                PlayerDataMgr.Instance.playerData.AddExp(v.Value.rewardExp);
            }
            if(v.Value.isMainLine==false)
            {
                DeleteTask(item.Id,true);
                break;
            }
        }
        if (autoSave)
        {
            Save();
        }
        
    }
}

[System.Serializable]
public class TaskData : BaseData
{
    public List<Task> items = new List<Task>();
}
