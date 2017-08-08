--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
require "Common/functions"
UILoginView = {};
local this = UILoginView;

local transform;
local gameObject;

--启动事件--
function UILoginView.Awake(obj)
	gameObject = obj;
	transform = obj.transform;
	print("Awake lua--->>"..gameObject.name);
end

function UILoginView.OnDO(args)
    print("OnDO lua--->>"..args);
end

--endregion
