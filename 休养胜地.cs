using System;
using System.Linq;
using System.Numerics;
using Newtonsoft.Json;
using Dalamud.Utility.Numerics;
using KodakkuAssist.Script;
using KodakkuAssist.Module.GameEvent;
using KodakkuAssist.Module.Draw;

namespace BrayfloxsLongstop;

[ScriptType(guid: "243175e3-9c80-4b90-b378-6ca4d26da674", name: "休养胜地", territorys: [1041], version: "0.0.0.1", author: "DG")]
// 休养胜地，仅最后
public class BrayfloxsLongstop
{
    public void Init(ScriptAccessory accessory)
    {
        accessory.Method.RemoveDraw(".*");
    }

    [ScriptMethod(name: "毒液猛咬", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:28659"])]
    public void SalivousSnap(Event @event, ScriptAccessory accessory)
    {
        var dp = accessory.Data.GetDefaultDrawProperties();
        dp.Name = "毒液猛咬";
        dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
        dp.Owner = @event.SourceId;  
        dp.TargetObject = @event.TargetId;
        dp.TargetResolvePattern = PositionResolvePatternEnum.OwnerEnmityOrder;
        dp.TargetOrderIndex = 1;
        dp.Scale = new Vector2(0); 
        dp.Radian = 0f;  
        dp.DestoryAt = 5000; 
        accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
    }

    [ScriptMethod(name: "ToxicVomit", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:28656"])]
    public void ToxicVomit(Event @event, ScriptAccessory accessory)
    {
        var dp = accessory.Data.GetDefaultDrawProperties();
        dp.Name = "ToxicVomit";
        dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
        dp.Owner = @event.SourceId;  
        dp.Scale = new Vector2(2);  
        dp.Radian = 0f;  
        dp.DestoryAt = 3000;  
        accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
    }

    [ScriptMethod(name: "飞散", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:28658"])]
    public void Burst(Event @event, ScriptAccessory accessory)
    {
        var dp = accessory.Data.GetDefaultDrawProperties();
        dp.Name = "飞散";
        dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
        dp.Owner = @event.SourceId;  
        dp.Scale = new Vector2(10);  
        dp.Radian = 0f;  
        dp.DestoryAt = 9000;  
        accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
    }

    [ScriptMethod(name: "巨龙吐息", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:28660"])]
    public void DragonBreath(Event @event, ScriptAccessory accessory)
    {
        var dp = accessory.Data.GetDefaultDrawProperties();
        dp.Name = "巨龙吐息";
        dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
        dp.Owner = @event.SourceId; 
        dp.Scale = new Vector2(8, 30);  
        dp.Radian = 0f;  
        dp.DestoryAt = 3000;  
        accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Rect, dp);
    }
}
