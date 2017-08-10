--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
UILoginCtrl = {};
local this = UILoginCtrl;

local panel;
local uiLogin;
local transform;
local gameObject;
--构建函数--
function UILoginCtrl.New()
	return this;
end

function UILoginCtrl.Awake()
	panelMgr:CreatePanel('UILogin', this.OnCreate);
end

--启动事件--
function UILoginCtrl.OnCreate(obj)
	gameObject = obj;
	transform = obj.transform;
	uiLogin = transform:GetComponent('LuaBehaviour');
	uiLogin:AddClick(UILoginPanel.btn, this.OnClick);
end

function UILoginCtrl.OnClick(go)
    print(go.name);
end

--endregion
