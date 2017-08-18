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
	this.btnLogin = transform:Find("btnLogin").gameObject;
    this.btnReg = transform:Find("btnReg").gameObject;
    this.inputAccount = transform:Find("InputAccount"):GetComponent('InputField');
    this.inputPwd = transform:Find("InputPwd"):GetComponent('InputField');
    this.txtTip = transform:Find("Tip"):GetComponent('Text');
end

--endregion
