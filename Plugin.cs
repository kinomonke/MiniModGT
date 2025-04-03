using System;
using System.Collections;
using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilla;
using Utilla.Attributes;

namespace MiniInModded
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin("com.kino.gorillatag.miniinmodded", "MiniInModded", "1.0.4")]
    public class Plugin : BaseUnityPlugin
    {
        private GameObject TinyTrigger;
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

                TinyTrigger = GameObject.Find("Basement/DungeonRoomAnchor/DungeonBasement/MazeSizeChangers/TinySizerEntrance");
                if (TinyTrigger != null)
                {
                    TinyTrigger.transform.localScale = new Vector3(9999f, 9999f, 9999f);
                    if (clonedObject == null)
                    {
                        clonedObject = Instantiate(TinyTrigger, new Vector3(0, 0, 0), Quaternion.identity);
                    }
                }
                else
                {
                    Debug.LogError("i did smthn wrong");
                }
            }
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            GameObject.Find("TinySizerEntrance(Clone)").SetActive(false);
            inRoom = false;
        }
    }
}
