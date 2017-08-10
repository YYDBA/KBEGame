--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
require "Common/functions"
UILoginPanel = {};
local this = UILoginPanel;

local transform;
local gameObject;

--启动事件--
function UILoginPanel.Awake(obj)
	gameObject = obj;
	transform = obj.transform;
    this.InitPanel();
end

function UILoginPanel.InitPanel()
	this.btn = transform:Find("Button").gameObject;
end

--endregion
