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
	uiLogin:AddClick(UILoginPanel.btnLogin, this.OnLogin);
	uiLogin:AddClick(UILoginPanel.btnReg, this.OnReg);
    LoginMoudle:Init();
end

function UILoginCtrl.OnLogin(go)
    LoginMoudle:Login(UILoginPanel.inputAccount.text,UILoginPanel.inputPwd.text);
end

function UILoginCtrl.OnReg(go)
    --LoginMoudle.Login();
end

--endregion
