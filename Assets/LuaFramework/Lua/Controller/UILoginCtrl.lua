--region *.lua
--Date
--此文件由[BabeLua]插件自动生成
UILoginCtrl = {};
local this = UILoginCtrl;

local panel;
local prompt;
local transform;
local gameObject;
--构建函数--
function UILoginCtrl.New()
	logWarn("UILoginCtrl.New--->>");
	return this;
end

function UILoginCtrl.Awake()
	logWarn("UILoginCtrl.Awake--->>");
	panelMgr:CreatePanel('UILogin', this.OnCreate);
end

--启动事件--
function UILoginCtrl.OnCreate(obj)
	gameObject = obj;
	transform = obj.transform;

	panel = transform:GetComponent('UIPanel');
	prompt = transform:GetComponent('LuaBehaviour');
	logWarn("Start lua--->>"..gameObject.name);

	--prompt:AddClick(PromptPanel.btnOpen, this.OnClick);
	--resMgr:LoadPrefab('prompt', { 'PromptItem' }, this.InitPanel);
end

--endregion
