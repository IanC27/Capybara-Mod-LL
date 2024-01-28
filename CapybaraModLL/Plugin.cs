using BepInEx;
using System.IO;
using System.Reflection;
using UnityEngine;
using LethalLib.Modules;

namespace CapybaraModLL
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(LethalLib.Plugin.ModGUID)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic

            // Load Asset
            string assetDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "capybaramod");
            AssetBundle bundle = AssetBundle.LoadFromFile(assetDir);

            int capyRarity;
            int kiwiRarity;
#if DEBUG
            capyRarity = 1000;
            kiwiRarity = 1000;
#else
            capyRarity = 40;
            kiwiRarity = 25;
#endif
            Item capybara = bundle.LoadAsset<Item>("Assets/CapybaraMod/Scrap/Capybara/capybara.asset");
            Utilities.FixMixerGroups(capybara.spawnPrefab);
            NetworkPrefabs.RegisterNetworkPrefab(capybara.spawnPrefab);
            Items.RegisterScrap(capybara, capyRarity, Levels.LevelTypes.All);

            Item kiwi = bundle.LoadAsset<Item>("Assets/CapybaraMod/Scrap/Kiwi/kiwi.asset");
            Utilities.FixMixerGroups(kiwi.spawnPrefab);
            NetworkPrefabs.RegisterNetworkPrefab(kiwi.spawnPrefab);
            Items.RegisterScrap(kiwi, kiwiRarity, Levels.LevelTypes.All);

#if DEBUG
            // add to shop 4 testing
            TerminalNode capyNode = ScriptableObject.CreateInstance<TerminalNode>();
            capyNode.clearPreviousText = true;
            capyNode.displayText = "IDK how this got here. You can have it if u want\n\n";
            Items.RegisterShopItem(capybara, null, null, capyNode, 0);

            TerminalNode kiwiNode = ScriptableObject.CreateInstance<TerminalNode>();
            kiwiNode.clearPreviousText = true;
            kiwiNode.displayText = "what\n\n";
            Items.RegisterShopItem(kiwi, null, null, kiwiNode, 0);
#endif
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
