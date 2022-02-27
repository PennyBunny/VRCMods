﻿using System.Collections;
using ReMod.Core.UI.QuickMenu;
using TMPro;
using UnityEngine;
using VRC;
using VRC.UI.Core;

namespace MLConsoleViewer;

public static class UI
{
    //public static ReMenuCategory Console;
    private static ReCategoryPage ConsoleTab;
    public static GameObject MLMenu, consolePrefab;
    public static TextMeshProUGUI text;

    public static IEnumerator OnQuickMenu()
    {
        while (UIManager.prop_UIManager_0 == null) yield return null;
        while (Object.FindObjectOfType<VRC.UI.Elements.QuickMenu>() == null) yield return null;
        BuildTab();
    }

    public static void BuildTab()
    {
        ConsoleTab = new ReCategoryPage("MLConsoleViewer", true);
        ReTabButton.Create("MLConsoleViewer", "Open MLConsoleViewer", "MLConsoleViewer", BundleManager.ConsoleImg);
        MLMenu = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_MLConsoleViewer/");
        consolePrefab = Object.Instantiate(BundleManager.ConsolePrefab, MLMenu.transform);
        consolePrefab.transform.localPosition = new Vector3(0, -42, 0);
        consolePrefab.transform.localScale = new Vector3(1.8f, 1.8f, 1);
        Tools.SetLayerRecursively(consolePrefab, LayerMask.NameToLayer("InternalUI"));
        text = GameObject
            .Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_MLConsoleViewer/console(Clone)/Scroll View/Viewport/Content/")
            .GetComponentInChildren<TextMeshProUGUI>(true);
        foreach (var i in ConsoleManager.Cached)
            text.text += i;
    }
}