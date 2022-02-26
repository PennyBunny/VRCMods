﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace MLConsoleViewer;

public static class ConsoleManager
{
    public static readonly HashSet<string> Cached = new();
    public static void AttachTrackers()
    {
        MelonLogger.MsgCallbackHandler += OnLog;
        MelonLogger.WarningCallbackHandler += (callingMod, logText) => OnLog(true, callingMod, logText);
        MelonLogger.ErrorCallbackHandler += (callingMod, logText) => OnLog(false, callingMod, logText);
    }
    private static void OnLog(ConsoleColor melonColor, ConsoleColor txtColor, string callingMod, string logText)
    {
        var result = $"[<color=green>{CurrTime}</color>]" +
                     $" [<color={HexStrings[melonColor]}>{callingMod}</color>]" +
                     $" <color={HexStrings[txtColor]}>{logText}</color>\n";
        if (!UI.text)
            Cached.Add(result);
        else
            UI.text.text += result;
    }
    private static void OnLog(bool isWarn, string callingMod, string logText)
    {
        var result = $"<color={HexStrings[isWarn ? ConsoleColor.Yellow : ConsoleColor.Red]}>[{CurrTime}] [{callingMod}] {logText}</color>";
        if (!UI.text)
            Cached.Add(result);
        else
            UI.text.text += result;
    }
    private static string CurrTime => DateTime.Now.AddMilliseconds(-1.0).ToString("HH:mm:ss.fff");
    private static readonly Hashtable HexStrings = new()
    {
        {ConsoleColor.Black, "#000000"},
        {ConsoleColor.DarkBlue, "#00008b"},
        {ConsoleColor.DarkGreen, "#008b00"},
        {ConsoleColor.DarkCyan, "#008b8b"},
        {ConsoleColor.DarkRed, "#8b0000"},
        {ConsoleColor.DarkMagenta, "#8b008b"},
        {ConsoleColor.DarkYellow, "#8b8b00"},
        {ConsoleColor.Gray, "#c0c0c0"},
        {ConsoleColor.DarkGray, "#8b8b8b"},
        {ConsoleColor.Blue, "#0000ff"},
        {ConsoleColor.Green, "#00ff00"},
        {ConsoleColor.Cyan, "#00ffff"},
        {ConsoleColor.Red, "#ff0000"},
        {ConsoleColor.Magenta, "#ff00ff"},
        {ConsoleColor.Yellow, "#ffff00"},
        {ConsoleColor.White, "#ffffff"}
    };
}