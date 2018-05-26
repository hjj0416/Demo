using System;
using UnityEngine;
/**
 * 
 * SDK接入说明，请在开始接入前仔细阅读：
 * 客户端必接接口：登陆、支付、上传游戏数据、登出、退出、释放资源接口，详细说明如下
 * 
 * 1.登陆成功获取到SDK返回的用户信息后（如uid、token等），需上传用户信息到游戏服务器，游戏服务器与服务器进行通信，
 *   进行用户信息校验，收到校验成功的结果后，方可认为登录成功。本类中方法checkLogin为模拟信息校验过程，游戏方需自行与游戏服务器进行用户信息校验。
 * 2.支付只提供定额方式，非定额方式不再提供，游戏方如有需要，可自行实现
 * 3.上传数据接口需在三处调用，分别为进入服务器、玩家升级、玩家创建用户角色，有如下三点需注意：
 *   a.游戏中该接口必须调用三次，传入不同的_id，以区分不同的上传数据；
 *   b.若游戏中无对应接口功能，如游戏中无需创建角色，则可根据自身情况在合适位置进行调用，如登录成功后；
 *   c.在上传游戏数据时，若存在无对应字段值的情况，可传入默认值，具体参见接口说明；
 *   b.数据上传接口请务必按要求接入，否则部分渠道会出现退包情况；
 * 4.登出操作用于用户切换帐号等操作，有如下不同处理：
 *   a.游戏中存在登出或者切换帐号的按钮，则可在点击按钮时进行登出接口调用，在登出回调中进行重新登录等操作
 *   b.游戏中不存在登出或者切换帐号的按钮时，建议修改游戏添加登出或切换帐号按钮，若实在无法添加，可在退出游戏前调用登出接口，
 *     这种情况下会存在部分渠道会审核不通过的情况，需游戏方与渠道去进行沟通。
 * 5.在游戏退出前调用退出接口，有如下不同处理：
 *   a.渠道存在退出界面，如91、360等，此时游戏方只需在回调中进行退出游戏操作即可，无需再弹退出界面；
 *   b.渠道不存在退出界面，如百度移动游戏等，此时游戏方需在回调中弹出自己的游戏退出确认界面，否则会出现渠道审核不通过情况；
 *  
 * 游戏APK上传到开发者后台进行打包时，后台会检测SDK接口是否接入完整，未接入完整拒绝上传。
 * 详细信息请参考Unity3D接入文档，在线文档：http://www.ljsdk.com/docs_unity3d，离线文档在Unity SDK包中。
 */
namespace LJSDKDemo
{
	public class LJTest : MonoBehaviour
	{
		private bool isExit = false;

		void Awake(){
			LJSDK.Instance.init(this.gameObject.name,"initCallback");
		}

		void OnGUI(){
			if(GUILayout.Button("login",GUILayout.Height(200),GUILayout.Width(400))){
				//必须在调用登录之前，调用setUserCallback来设置用户回调 
				LJSDK.Instance.setUserCallback(this.gameObject.name,"userCallback");
				LJSDK.Instance.login("login");
			}
			
			if(GUILayout.Button("pay",GUILayout.Height(200),GUILayout.Width(400))){
				LJSDK.Instance.pay(this.gameObject.name,"payCallback",100, "金币", 10, "60钻石","XXX-XXX-XXX-XXX", "http://callbackurl");
			}
			
			if(GUILayout.Button("logout",GUILayout.Height(200),GUILayout.Width(400))){
				LJSDK.Instance.logout("logout");
			}
			
			#if UNITY_ANDROID
			if(GUILayout.Button("exit",GUILayout.Height(200),GUILayout.Width(400))){
				LJSDK.Instance.exit(this.gameObject.name,"exitCallback");
			}
			#endif
		}

		void initCallback(string result) {
			Debug.Log("initCallback result:" + result);

			JsonData jsonResult = JsonMapper.ToObject(result);
			int resultCode = (int)jsonResult["resultCode"];

			if (LJSDK.SUCCESS == resultCode) {
				//初始化成功
				Debug.Log ("init success");
			} else {
				//初始化失败
				Debug.Log("init fail");
			}
		}

		void userCallback(string result) {
			Debug.Log("userCallback result:" + result);

			JsonData jsonResult = JsonMapper.ToObject(result);
			int resultCode = (int)jsonResult["resultCode"];
			JsonData jsonData = (JsonData)jsonResult["data"];
			if (LJSDK.SUCCESS == resultCode) {
				//登录成功
				Debug.Log("onLoginSuccess:" + JsonMapper.ToJson(jsonData));

				string userName     = (string)jsonData["userName"];
				string uid          = (string)jsonData["uid"];
				string channelCode  = (string)jsonData["channelCode"];
				string productCode  = (string)jsonData["productCode"];
				string token        = (string)jsonData["token"]; 
				string channelUid   = (string)jsonData["channelUid"]; 
				string customParams = (string)jsonData["customParams"];
				string channelLabel = (string)jsonData["channelLabel"];

				//登录成功之后需要游戏服务端进行二次验证 

				//此处为模拟，校验用户信息成功后进行数据上传 
				//此处数据上传仅为演示使用，请游戏方根据要求在合适的位置调用 

				//新增extra字段，传入特殊字段（某些渠道使用，如UC）
				LJSDK.Instance.setExtData(LJSDK.enterServer, "13524696", "fangmu", 24, 1, "mutu1qu", 88, 2, "wujintianya","extra");
				LJSDK.Instance.setExtData(LJSDK.createRole, "13524696", "fangmu", 24, 1, "mutu1qu", 88, 2, "wujintianya","extra");
				LJSDK.Instance.setExtData(LJSDK.levelUp, "13524696", "fangmu", 24, 1, "mutu1qu", 88, 2, "wujintianya","extra");
			} else if (LJSDK.FAILURE == resultCode){
				//登录失败
				Debug.Log("onLoginFailed:" + JsonMapper.ToJson(jsonData));

				string detail 		= (string)jsonData ["detail"];
				string customParams = (string)jsonData["customParams"];
			} else if (LJSDK.LOGOUT == resultCode) {
				//注销
				Debug.Log("onLogout result:" + JsonMapper.ToJson(jsonData));
			}
		}

		void payCallback(string result) {
			Debug.Log("payCallback result:" + result);

			JsonData jsonResult = JsonMapper.ToObject(result);
			int resultCode = (int)jsonResult["resultCode"];
			if (LJSDK.SUCCESS == resultCode) {
				//支付成功
				//该回调仅代表用户已发起支付操作，不代表是否充值成功，具体充值是否到账需以服务器间通知为准；
				//在此回调中游戏方可开始向游戏服务器发起请求，查看订单是否已支付成功，若支付成功则发送道具。
				Debug.Log ("pay success");
			} else {
				//支付失败
				//该回调代表用户已放弃支付，无需向服务器查询充值状态。
				Debug.Log("pay fail");
			}
		}

		#if UNITY_ANDROID
		void exitCallback(string result) {
			Debug.Log("exitCallback result:" + result);

			JsonData jsonResult = JsonMapper.ToObject(result);
			int resultCode = (int)jsonResult["resultCode"];
			JsonData jsonData = (JsonData)jsonResult["data"];

			if (LJSDK.GAME_EXIT == resultCode) {
				Debug.Log("LJSDK.CHANNEL_EXIT");
				//收到此回调，说明渠道不提供退出接口，需要游戏自行在此实现退出逻辑
				//游戏方弹出退出确认对话框
				//在用户确认退出后调用releaseResource方法释放SDK资源，并关闭游戏 
				onGameExit();
			} else if (LJSDK.CHANNEL_EXIT == resultCode){
				Debug.Log("LJSDK.CHANNEL_EXIT");
				//收到此回调，说明渠道已经提供了退出接口
				//游戏方收到该通知后直接调用releaseResource方法释放SDK资源，随后关闭游戏 
				onChannelExit();
			}
		}

		void onGameExit(){
			Debug.Log ("onGameExit");
			//TODO 弹出游戏退出确认框,用户确认退出后调用释放资源接口 
			LJSDK.Instance.releaseResource();
			//TODO 结束游戏
			
			Application.Quit();
		}

		void onChannelExit(){
			Debug.Log ("onChannelExit");
			LJSDK.Instance.releaseResource();
			//TODO 结束游戏
			
			Application.Quit();
		}

		void Update(){
			if(!this.isExit && Input.GetKeyDown(KeyCode.Escape)){
				this.isExit = true;
				LJSDK.Instance.exit(this.gameObject.name,"exitCallback");
			}
			if(Input.GetKeyUp(KeyCode.Escape)){
				this.isExit = false;
			}
		}
		#endif
	}
}

