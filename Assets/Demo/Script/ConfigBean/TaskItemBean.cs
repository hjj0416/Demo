using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class TaskItemBean:Bean
{
  public int ID   { get; private set; }  /*id */

  public string name   { get; private set; }  /*名称 */

  public string icon   { get; private set; }  /*图标 */

  public string description   { get; private set; }  /*描述 */

  public bool isMainLine   { get; private set; }  /*是否是主线 */

  public int openLv   { get; private set; }  /*开启等级 */

  public string completeCondition   { get; private set; }  /*完成条件 */

  public int completeParam1   { get; private set; }  /*完成条件参数1 */

  public int completeParam2   { get; private set; }  /*完成条件参数2 */

  public int rewardCoin   { get; private set; }  /*奖励金币 */

  public int rewardExp   { get; private set; }  /*奖励经验 */

  public int reward1   { get; private set; }  /*奖励id */

  public int rewardCount1   { get; private set; }  /*数量 */

  public int reward2   { get; private set; }  /*奖励id */

  public int rewardCount2   { get; private set; }  /*数量 */

  public int reward3   { get; private set; }  /*奖励id */

  public int rewardCount3   { get; private set; }  /*数量 */


  public static Dictionary<int, TaskItemBean> beans = new Dictionary<int, TaskItemBean>();

  public TaskItemBean()
  {

  }
  public TaskItemBean this[int index]
  {
      get { return beans[index]; }
  }
  public static void Init()
  {
      Type tp =  typeof(TaskItemBean);
      string tableName = tp.ToString().ToLower().Replace("bean", "");
      List<List<string>> values = TableLists[tableName];
      for(int i=0;i<values.Count;i++){
          List<string> line = values[i];
          TaskItemBean bean = new TaskItemBean();
          bean.ID = ParseData<int>(DataType.Int, line[0]);  /*id */
          bean.name = ParseData<string>(DataType.String, line[1]);  /*名称 */
          bean.icon = ParseData<string>(DataType.String, line[2]);  /*图标 */
          bean.description = ParseData<string>(DataType.String, line[3]);  /*描述 */
          bean.isMainLine = ParseData<bool>(DataType.Bool, line[4]);  /*是否是主线 */
          bean.openLv = ParseData<int>(DataType.Int, line[5]);  /*开启等级 */
          bean.completeCondition = ParseData<string>(DataType.String, line[6]);  /*完成条件 */
          bean.completeParam1 = ParseData<int>(DataType.Int, line[7]);  /*完成条件参数1 */
          bean.completeParam2 = ParseData<int>(DataType.Int, line[8]);  /*完成条件参数2 */
          bean.rewardCoin = ParseData<int>(DataType.Int, line[9]);  /*奖励金币 */
          bean.rewardExp = ParseData<int>(DataType.Int, line[10]);  /*奖励经验 */
          bean.reward1 = ParseData<int>(DataType.Int, line[11]);  /*奖励id */
          bean.rewardCount1 = ParseData<int>(DataType.Int, line[12]);  /*数量 */
          bean.reward2 = ParseData<int>(DataType.Int, line[13]);  /*奖励id */
          bean.rewardCount2 = ParseData<int>(DataType.Int, line[14]);  /*数量 */
          bean.reward3 = ParseData<int>(DataType.Int, line[15]);  /*奖励id */
          bean.rewardCount3 = ParseData<int>(DataType.Int, line[16]);  /*数量 */
          beans[bean.ID] = bean;
      }
  }
  public static TaskItemBean Get(int index)
  {
      if (beans.ContainsKey(index))
      {
           return beans[index];
      }
      Debug.LogError("Table index error:[TaskItemBean]:"+index);
      return null;
  }

}