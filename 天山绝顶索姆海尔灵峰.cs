using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using KodakkuAssist.Script;
using KodakkuAssist.Module.GameEvent;
using KodakkuAssist.Module.Draw;
using Dalamud.Utility.Numerics;

namespace dgcript.Heavensward.SohmAl;

[ScriptType(guid: "A46B8893-E51B-4835-9004-D9BE65660876", name: "天山绝顶索姆阿尔灵峰", territorys: [1064], version: "0.0.0.1", author: "DG")]

public class SohmAl
{
    private uint p1Raskovnik = 0;
    private uint p3Tioman = 0;

    public void Init(ScriptAccessory accessory)
    {
        accessory.Method.RemoveDraw(".*");
    }

    #region BOSS1 Raskovnik
    // 完工

    [ScriptMethod(name: "记录Boss", eventType: EventTypeEnum.ActionEffect, eventCondition: ["ActionId:3793"])]
    public void SaveBossId(Event @event, ScriptAccessory accessory)
    {
        if (ParseObjectId(@event["SourceId"], out var sid))
        {
            p1Raskovnik = sid;
        }
    }


    [ScriptMethod(name: "战斗开始Boss对MT扇形", eventType: EventTypeEnum.ActionEffect, eventCondition: ["ActionId:872"])] // 替换成实际开战技能 ID
    public void BossConeToMT(Event @event, ScriptAccessory accessory)
    {
    if (!uint.TryParse(@event["SourceId"], System.Globalization.NumberStyles.HexNumber, null, out var sourceId))
        return;
    if (sourceId != 0x400076B4)
        return; // 非目标Boss则完全忽略

    var dp = accessory.Data.GetDefaultDrawProperties();
    dp.Name = "Boss对MT扇形";
    dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
    dp.Owner = sourceId;
    dp.TargetObject = @event.TargetId;
    dp.TargetResolvePattern = PositionResolvePatternEnum.OwnerEnmityOrder;
    dp.TargetOrderIndex = 1;
    dp.Radian = float.Pi / 2; // 60度
    dp.Scale = new Vector2(8);
    dp.DestoryAt = 6000; // 30秒（战斗期间可重复刷新）
    accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Fan, dp);
    }

    [ScriptMethod(name: "点名扇形", eventType: EventTypeEnum.TargetIcon, eventCondition: ["Id:002E"])]
    public void MarkedFanAOE(Event @event, ScriptAccessory accessory)
    {
    var dp = accessory.Data.GetDefaultDrawProperties();
    dp.Name = "点名扇形";
    dp.Owner = p1Raskovnik;
    dp.TargetObject = @event.TargetId;
    dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
    dp.Radian = float.Pi / 6; // 30度
    dp.Scale = new Vector2(40); // 长度40米
    dp.DestoryAt = 10000; 
    accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Fan, dp);
    }

    #endregion

    #region BOSS2 Myath
    // 完工

    [ScriptMethod(name: "战斗开始Boss对MT扇形", eventType: EventTypeEnum.ActionEffect, eventCondition: ["ActionId:870"])] // 替换成实际开战技能 ID
    public void Boss2ConeToMT(Event @event, ScriptAccessory accessory)
    {
    if (!uint.TryParse(@event["SourceId"], System.Globalization.NumberStyles.HexNumber, null, out var sourceId))
        return;
    if (sourceId != 0x40007892)
        return; // 非目标Boss则完全忽略

    var dp = accessory.Data.GetDefaultDrawProperties();
    dp.Name = "Boss对MT扇形";
    dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
    dp.Owner = sourceId;
    dp.TargetObject = @event.TargetId;
    dp.TargetResolvePattern = PositionResolvePatternEnum.OwnerEnmityOrder;
    dp.TargetOrderIndex = 1;
    dp.Radian = float.Pi / 2; // 60度
    dp.Scale = new Vector2(8);
    dp.DestoryAt = 6000; 
    accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Fan, dp);
    }


    [ScriptMethod(name: "Primordial Roar", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:3810"])]
    public void PrimordialRoar(Event @event, ScriptAccessory accessory)
    {
        var dp = accessory.Data.GetDefaultDrawProperties();
        dp.Name = "Primordial Roar";
        dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
        dp.Owner = @event.SourceId;
        dp.Scale = new Vector2(60);
        dp.DestoryAt = 3000;
        accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
    }

    [ScriptMethod(name: "Mad Dash", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:3808"])]
    public void MadDash(Event @event, ScriptAccessory accessory)
    {
        var dp = accessory.Data.GetDefaultDrawProperties();
        dp.Name = "Mad Dash";
        dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
        dp.Owner = @event.TargetId;
        dp.Scale = new Vector2(6);
        dp.DestoryAt = 5000;
        accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
    }

    [ScriptMethod(name: "Mad Dash Stack", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:3809"])]
    public void MadDashStack(Event @event, ScriptAccessory accessory)
    {
        var dp = accessory.Data.GetDefaultDrawProperties();
        dp.Name = "Mad Dash Stack";
        dp.Color = accessory.Data.DefaultSafeColor;
        dp.Owner = @event.TargetId;
        dp.Scale = new Vector2(6);
        dp.DestoryAt = 5000;
        accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
    }

    #endregion

    #region BOSS3 Tioman

    [ScriptMethod(name: "记录Boss", eventType: EventTypeEnum.ActionEffect, eventCondition: ["ActionId:3811"])]
    public void SaveBoss1Id(Event @event, ScriptAccessory accessory)
    {
        if (ParseObjectId(@event["SourceId"], out var sid))
        {
            p3Tioman = sid;
        }
    }

    [ScriptMethod(name: "战斗开始Boss3对MT扇形", eventType: EventTypeEnum.ActionEffect, eventCondition: ["ActionId:870"])]
    public void Boss3ConeToMT(Event @event, ScriptAccessory accessory)
    {
    if (!uint.TryParse(@event["SourceId"], System.Globalization.NumberStyles.HexNumber, null, out var sourceId))
        return;
    if (sourceId != 0x400078AF)
        return; // 非目标Boss则完全忽略

    var dp = accessory.Data.GetDefaultDrawProperties();
    dp.Name = "Boss3对MT扇形";
    dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
    dp.Owner = sourceId;
    dp.TargetObject = @event.TargetId;
    dp.TargetResolvePattern = PositionResolvePatternEnum.OwnerEnmityOrder;
    dp.TargetOrderIndex = 1;
    dp.Radian = float.Pi / 2; // 60度
    dp.Scale = new Vector2(30);
    dp.DestoryAt = 6000; 
    accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Fan, dp);
    }

    [ScriptMethod(name: "Chaos Blast", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:3813"])]
    public void ChaosBlast(Event @event, ScriptAccessory accessory)
    {
        var dp = accessory.Data.GetDefaultDrawProperties();
        var pos = JsonConvert.DeserializeObject<Vector3>(@event["TargetPosition"]);

        dp.Name = "Chaos Blast";
        dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
        dp.Position = pos;
        dp.Scale = new Vector2(2);
        dp.DestoryAt = 2000;
        accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
    }

    [ScriptMethod(name: "Heavensfall", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:3818"])]
    public void Heavensfall(Event @event, ScriptAccessory accessory)
    {
        var dp = accessory.Data.GetDefaultDrawProperties();
        var pos = JsonConvert.DeserializeObject<Vector3>(@event["TargetPosition"]);

        dp.Name = "Heavensfall";
        dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
        dp.Position = pos;
        dp.Scale = new Vector2(5);
        dp.DestoryAt = 3000;
        accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
    }

    [ScriptMethod(name: "Dark Star", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:3812"])]
    public void DarkStar(Event @event, ScriptAccessory accessory)
    {
        var dp = accessory.Data.GetDefaultDrawProperties();
        dp.Name = "Dark Star";
        dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
        dp.Owner = @event.SourceId;
        dp.Scale = new Vector2(50);
        dp.DestoryAt = 5000;
        accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
    }

    [ScriptMethod(name: "Meteor Bait", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:4999"])]
    public void MeteorBait(Event @event, ScriptAccessory accessory)
    {
        var dp = accessory.Data.GetDefaultDrawProperties();
        dp.Name = "Meteor Bait";
        dp.Owner = @event.SourceId;
        dp.Scale = new Vector2(30);
        dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
        dp.DestoryAt = 3500;
        accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
    }

    [ScriptMethod(name: "Meteor Impact", eventType: EventTypeEnum.StartCasting, eventCondition: ["ActionId:4997"])]
    public void MeteorImpact(Event @event, ScriptAccessory accessory)
    {
        var dp = accessory.Data.GetDefaultDrawProperties();
        dp.Name = "Meteor Impact";
        dp.Owner = @event.SourceId;
        dp.Scale = new Vector2(30);
        dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
        dp.DestoryAt = 3000;
        accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
    }

    [ScriptMethod(name: "点名圆形", eventType: EventTypeEnum.TargetIcon, eventCondition: ["Id:0007"])]
    public void DarkStar2(Event @event, ScriptAccessory accessory)
    {
    var dp = accessory.Data.GetDefaultDrawProperties();
    dp.Name = "点名圆形";
    dp.Owner = @event.TargetId;
    dp.TargetObject = @event.TargetId;
    dp.Color = accessory.Data.DefaultDangerColor.WithW(0.4f);
    dp.Scale = new Vector2(20); 
    dp.DestoryAt = 10000; 
    accessory.Method.SendDraw(DrawModeEnum.Default, DrawTypeEnum.Circle, dp);
    }

    #endregion

    private static bool ParseObjectId(string? idStr, out uint id)
    {
        id = 0;
        if (string.IsNullOrEmpty(idStr)) return false;
        try
        {
            var idStr2 = idStr.Replace("0x", "");
            id = uint.Parse(idStr2, System.Globalization.NumberStyles.HexNumber);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
