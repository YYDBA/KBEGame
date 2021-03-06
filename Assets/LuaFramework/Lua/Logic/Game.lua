require "Logic/LuaClass"
require "Logic/CtrlManager"
require "Common/functions"
require "View/UILoginPanel"
--管理器--
Game = {};
local this = Game;

local game; 
local transform;
local gameObject;
local WWW = UnityEngine.WWW;

--初始化完成，发送链接服务器信息--
function Game.OnInitOK()
    CtrlManager.Init();
    local ctrl = CtrlManager.GetCtrl(CtrlNames.UILogin);
	if ctrl ~= nil then
		ctrl:Awake();
    end
end

--销毁--
function Game.OnDestroy()
	--logWarn('OnDestroy--->>>');
end
