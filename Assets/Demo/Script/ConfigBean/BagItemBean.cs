using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class BagItemBean:Bean
{
  public int ID   { get; private set; }  /*id */

  public string name   { get; private set; }  /*名称 */

  public string icon   { get; private set; }  /*图标 */

  public string description   { get; private set; }  /*描述 */

  public int type   { get; private set; }  /*类型 */

  public int part   { get; private set; }  /*部位 */

  public int buyPrice   { get; private set; }  /*购买价格 */

  public int sellPrice   { get; private set; }  /*卖出价格 */

  public int hp   { get; private set; }  /*加血 */

  public int mp   { get; private set; }  /*加蓝 */

  public int atk   { get; private set; }  /*加攻击 */

  public int def   { get; private set; }  /*加防御 */


  public static Dictionary<int, BagItemBean> beans = new Dictionary<int, BagItemBean>();

  public BagItemBean()
  {

  }
  public BagItemBean this[int index]
  {
      get { return beans[index]; }
  }
  public static void Init()
  {
      Type tp =  typeof(BagItemBean);
      string tableName = tp.ToString().ToLower().Replace("bean", "");
      List<List<string>> values = TableLists[tableName];
      for(int i=0;i<values.Count;i++){
          List<string> line = values[i];
          BagItemBean bean = new BagItemBean();
          bean.ID = ParseData<int>(DataType.Int, line[0]);  /*id */
          bean.name = ParseData<string>(DataType.String, line[1]);  /*名称 */
          bean.icon = ParseData<string>(DataType.String, line[2]);  /*图标 */
          bean.description = ParseData<string>(DataType.String, line[3]);  /*描述 */
          bean.type = ParseData<int>(DataType.Int, line[4]);  /*类型 */
          bean.part = ParseData<int>(DataType.Int, line[5]);  /*部位 */
          bean.buyPrice = ParseData<int>(DataType.Int, line[6]);  /*购买价格 */
          bean.sellPrice = ParseData<int>(DataType.Int, line[7]);  /*卖出价格 */
          bean.hp = ParseData<int>(DataType.Int, line[8]);  /*加血 */
          bean.mp = ParseData<int>(DataType.Int, line[9]);  /*加蓝 */
          bean.atk = ParseData<int>(DataType.Int, line[10]);  /*加攻击 */
          bean.def = ParseData<int>(DataType.Int, line[11]);  /*加防御 */
          beans[bean.ID] = bean;
      }
  }
  public static BagItemBean Get(int index)
  {
      if (beans.ContainsKey(index))
      {
           return beans[index];
      }
      Debug.LogError("Table index error:[BagItemBean]:"+index);
      return null;
  }

}