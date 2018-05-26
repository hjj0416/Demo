using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class LJSDK {
	public const int SUCCESS = 112501;
	public const int FAILURE = 112500;

	public const int LOGOUT  = 160105;

	public const int GAME_EXIT     = 112107;
	public const int CHANNEL_EXIT  = 112108;

	public const string enterServer = "enterServer";
	public const string createRole  = "createRole";
	public const string levelUp     = "levelUp";

	private static LJSDK _instance;
	public static LJSDK Instance
	{
		get
		{
			if(_instance == null){
				_instance = new LJSDK();
			}
			return _instance;
		}
	}

	/*****************************************基础接口***********************************************/

	//初始化接口
	public void init(string gameObject,string callbackMethod) {
		#if UNITY_IOS
		_init(gameObject,callbackMethod);
		#elif UNITY_ANDROID
		androidContext().Call("init",gameObject,callbackMethod);
		#endif
	}

	//登录接口 
	public void login(string customparams){
		#if UNITY_IOS
		_login(customparams);
		#elif UNITY_ANDROID
		androidContext().Call("login",customparams);
		#endif
	}

	//设置用户回调接口
	public void setUserCallback(string gameObject,string callbackMethod){
		#if UNITY_IOS
		_setUserCallback(gameObject,callbackMethod);
		#elif UNITY_ANDROID
		androidContext().Call("setUserCallback", gameObject,callbackMethod);
		#endif
	}

	//定额支付接口
	public void pay(string gameObject,string callbackMethod,int amount, string itemName, int count, 
					string chargePointName, string customParams, string callbackUrl) {
		#if UNITY_IOS
		_pay(gameObject,callbackMethod,amount,itemName,count,chargePointName,customParams,callbackUrl);
		#elif UNITY_ANDROID
		androidContext().Call("pay", gameObject,callbackMethod,amount,itemName,count,chargePointName,customParams,callbackUrl);
		#endif
	}
	
	//登出接口 
	public void logout(string customparams){
		#if UNITY_IOS
		_logout(customparams);
		#elif UNITY_ANDROID
		androidContext().Call("logout",customparams);
		#endif 
	}

	//用户扩展接口 
	public void setExtData(string id, string roleId, string roleName, int roleLevel, int zoneId, 
		string zoneName, int balance, int vip, string partyName,string extra){
		#if UNITY_IOS
		_setExtData(id,roleId,roleName,roleLevel,zoneId,zoneName,balance,vip,partyName);
		#elif UNITY_ANDROID
		androidContext().Call("setExtData", id, roleId, roleName, roleLevel, zoneId, zoneName, balance, vip, partyName,extra);
		#endif
	}

	/***************************************基础接口·完***********************************************/

	/****************************************平台差异性接口*********************************************/

	#if UNITY_ANDROID
	public AndroidJavaObject androidContext() {
		return new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
	}
    #endif

	//渠道标识，游戏开发者可根据此字段区分渠道，渠道对应关系请参看文档《如何区分渠道》 
	public string getChannelLabel() {
		string value = "";
		#if UNITY_IOS
		value = _getChannelLabel();
		#elif UNITY_ANDROID
		value = androidContext().Call<string>("getChannelLabel");
		#endif
		return value;
	}

	//可在iOS平台显示一个提示框
	#if UNITY_IOS
	public void showIOSDialog(string title,string content) {
		_showIOSDialog(title,content);
	}
	#endif

	#if UNITY_ANDROID
	//退出接口 
	public void exit(string gameObject,string callbackMethod){
		androidContext().Call("exit",gameObject,callbackMethod);
	}
	
	//释放SDK资源接口 
	public void releaseResource() {
		androidContext().Call("releaseResource");
	}

	//可在Android平台显示一个短暂提示条
	public void showAndroidToast(string info) {
		androidContext().Call("showToast", info);
	}
	
	//可通过该方法获取在后台配置的AndroidManifest meta-data值，方法参数为meta-data的name  
	public string getManifestData(string name) {
		string value = androidContext().Call<string>("getMainifestMetaData", name);
		return value;
	}
	#endif

	/***************************************平台差异性接口·完********************************************/

	#if UNITY_IOS
	[DllImport ("__Internal")]
	private static extern void _init(string gameObject,string callbackMethod);
	[DllImport ("__Internal")]
	private static extern void _setUserCallback(string gameObject,string callbackMethod);
	[DllImport ("__Internal")]
	private static extern void _login(string customparams);
	[DllImport ("__Internal")]
	private static extern void _pay(string gameObject,string callbackMethod,int amount,string itemName,int count,string chargePointName,string customParams,string callbackUrl);
	[DllImport ("__Internal")]
	private static extern void _logout(string customparams);
	[DllImport ("__Internal")]
	private static extern void _setExtData(string id,string roleId,string roleName,int roleLevel,int zoneId,string zoneName,int balance,int vip,string partyName,string extra);
	[DllImport ("__Internal")]
	private static extern void _showIOSDialog(string title,string content);
	[DllImport ("__Internal")] 
	private static extern string _getChannelLabel();
	#endif
}
