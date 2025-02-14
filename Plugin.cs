using System;
using BepInEx;
using GorillaLocomotion;
using MiniMod;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilla;
using Utilla.Attributes;

namespace MiniInModded
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin("com.kino.gorillatag.miniinmodded", "MiniInModded", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private GameObject TinySizerENT;
        private bool inRoom;
        private GameObject clonedObject;

        private void Start()
        {
            Events.GameInitialized += new EventHandler(OnGameInitialized);
        }

        private void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        private void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        private void OnGameInitialized(object sender, EventArgs e)
        {

        }

        private void Update()
        {

        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
            SceneManager.LoadScene("Basement", LoadSceneMode.Additive);
            SceneManager.sceneLoaded += OnSceneLoaded;
            GameObject.Find("TinySizerEntrance(Clone)").SetActive(true);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Basement" && NetworkSystem.Instance.GameModeString.Contains("MODDED"))
            {
                    SceneManager.sceneLoaded -= OnSceneLoaded;

                    TinySizerENT = GameObject.Find("Basement/DungeonRoomAnchor/DungeonBasement/MazeSizeChangers/TinySizerEntrance");
                    if (TinySizerENT != null)
                    {
                        TinySizerENT.transform.localScale = new Vector3(9999f, 9999f, 9999f);
                        if (clonedObject == null)
                        {
                            clonedObject = Instantiate(TinySizerENT, new Vector3(0, 0, 0), Quaternion.identity);
                        }
                    }

                    else
                    {
                        Debug.LogError("tse not found haha");
                    }
                    GameObject basementMusic = GameObject.Find("Basement/DungeonRoomAnchor/DungeonBasement/BasementMusicSpeaker");
                    if (basementMusic != null)
                    {
                        basementMusic.SetActive(false);
                    }
                    RemoveAnnoyingAmbience();
            }
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            SetPlayerPosition();
            GameObject.Find("TinySizerEntrance(Clone)").SetActive(false);
            inRoom = false;
        }

        private void SetPlayerPosition()
        {
            Player.Instance.transform.position = new Vector3(-68.5887f, 12.0883f, -83.958f);
        }

        private void RemoveAnnoyingAmbience()
        {
            GameObject crickets = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/WeatherDayNight/AudioCrickets");
            if (crickets != null)
            {
                crickets.SetActive(false);
            }

            GameObject rain = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/WeatherDayNight/rain");
            if (rain != null)
            {
                rain.SetActive(false);
            }
        }
    }
}
