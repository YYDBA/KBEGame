﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class Client_LoginMoudleWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Client.LoginMoudle), typeof(System.Object));
		L.RegFunction("Init", Init);
		L.RegFunction("Login", Login);
		L.RegFunction("New", _CreateClient_LoginMoudle);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Instance", get_Instance, set_Instance);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateClient_LoginMoudle(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				Client.LoginMoudle obj = new Client.LoginMoudle();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: Client.LoginMoudle.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			Client.LoginMoudle obj = (Client.LoginMoudle)ToLua.CheckObject<Client.LoginMoudle>(L, 1);
			obj.Init();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Login(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			Client.LoginMoudle obj = (Client.LoginMoudle)ToLua.CheckObject<Client.LoginMoudle>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			obj.Login(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		try
		{
			ToLua.PushObject(L, Client.LoginMoudle.Instance);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Instance(IntPtr L)
	{
		try
		{
			Client.LoginMoudle arg0 = (Client.LoginMoudle)ToLua.CheckObject<Client.LoginMoudle>(L, 2);
			Client.LoginMoudle.Instance = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}
