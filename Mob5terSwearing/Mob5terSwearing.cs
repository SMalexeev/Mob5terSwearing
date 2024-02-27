using Harmony;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using MSCLoader;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Mob5terSwearing
{
    public class Mob5terSwearing : Mod
    {
        public override string ID => "Mob5terSwearing"; // Your (unique) mod ID 
        public override string Name => "Mob5terSwearing"; // Your mod name
        public override string Author => "Sanchez Huykin"; // Name of the Author (your name)
        public override string Version => "1.0"; // Version
        public override string Description => "Нахуя тебе описание и так все понятно."; // Short description of your mod
        public override byte[] Icon => Properties.Resources.Icon;
        public override bool UseAssetsFolder => true;

        private Keybind _swearN = new Keybind("swear", "", KeyCode.N);
        private Keybind _swearM = new Keybind("fuck", "", KeyCode.M);

        private Regex regSwear = new Regex(@"swear(\w*)");
        private Regex regFuck = new Regex(@"fuck(\w*)");
        private int index = 0;
        private FsmFloat StressFsm;

        public override void ModSetup()
        {
            SetupFunction(Setup.OnLoad, Mod_OnLoad);
            SetupFunction(Setup.ModSettings, Mod_Settings);
            SetupFunction(Setup.Update, Update);
        }



        private void Mod_Settings()
        {
            // All settings should be created here. 
            // DO NOT put anything that isn't settings or keybinds in here!
        }

        public void Swear(int value)
        {
            StressFsm.Value -= value;
        }

        public override void Update()
        {
 
            if (_swearN.GetKeybindDown())
            {
                Swear(3);
            }
            if (_swearM.GetKeybindDown())
            {
                Swear(5);
            }
        }

        private void Mod_OnLoad()
        {
            Keybind.Add(this, _swearN);
            Keybind.Add(this, _swearM);
            // Called once, when mod is loading after game is fully loaded
            StressFsm = PlayMakerGlobals.Instance.Variables.FindFsmFloat("PlayerStress");
            AssetBundle ab = LoadAssets.LoadBundle(this, "swearingbundle.unity3d");
            Texture2D SlotMachine1Color = LoadAssets.LoadTexture(this, "slot_machine.png");
            Texture2D SlotMachine1Spec = LoadAssets.LoadTexture(this, "slot_machine_spec.png");
            Texture2D SlotMachineLightsColor = LoadAssets.LoadTexture(this, "slot_machine_screen.png");
            Texture2D SlotMachineLightsEmiColor = LoadAssets.LoadTexture(this, "slot_machine_screen_emission.png");
            Texture2D SlotMachineRollsColor = LoadAssets.LoadTexture(this, "slot_machine_symbols.png");

            GameObject slotMachine = GameObject.Find("STORE/LOD/GFX_Store/SlotMachine");
            if (slotMachine != null)
            {
                //Main
                GameObject sltBody = GameObject.Find("STORE/LOD/GFX_Store/SlotMachine/slot_machine 1");
                MeshRenderer sltBodyRender = sltBody.GetComponent<MeshRenderer>();
                sltBodyRender.material.SetTexture("_MainTex", SlotMachine1Color);
                sltBodyRender.material.SetTexture("_SpecGlossMap", SlotMachine1Spec);
                
                //Lights
                GameObject sltLights = GameObject.Find("STORE/LOD/GFX_Store/SlotMachine/slot_machine 1/slot_machine_lights");
                MeshRenderer sltLightsRender = sltLights.GetComponent<MeshRenderer>();
                sltLightsRender.material.SetTexture("_MainTex", SlotMachineLightsColor);
                sltLightsRender.material.SetTexture("_EmissionMap", SlotMachineLightsEmiColor);

                //Rolls
                GameObject sltRoll1 = GameObject.Find("STORE/LOD/GFX_Store/SlotMachine/Rolls/Roll1");
                GameObject sltRoll2 = GameObject.Find("STORE/LOD/GFX_Store/SlotMachine/Rolls/Roll2");
                GameObject sltRoll3 = GameObject.Find("STORE/LOD/GFX_Store/SlotMachine/Rolls/Roll3");

                MeshRenderer sltRoll1Render = sltRoll1.GetComponent<MeshRenderer>();
                MeshRenderer sltRoll2Render = sltRoll2.GetComponent<MeshRenderer>();
                MeshRenderer sltRoll3Render = sltRoll3.GetComponent<MeshRenderer>();

                sltRoll1Render.material.SetTexture("_MainTex", SlotMachineRollsColor);
                sltRoll2Render.material.SetTexture("_MainTex", SlotMachineRollsColor);
                sltRoll3Render.material.SetTexture("_MainTex", SlotMachineRollsColor);


                //BtnLock
                GameObject sltBtnLock1 = GameObject.Find("STORE/LOD/GFX_Store/SlotMachine/LockButtos/button_lock1");
                GameObject sltBtnLock2 = GameObject.Find("STORE/LOD/GFX_Store/SlotMachine/LockButtos/button_lock2");
                GameObject sltBtnLock3 = GameObject.Find("STORE/LOD/GFX_Store/SlotMachine/LockButtos/button_lock3");

                MeshRenderer sltBtnLock1Render = sltBtnLock1.GetComponent<MeshRenderer>();
                MeshRenderer sltBtnLock2Render = sltBtnLock2.GetComponent<MeshRenderer>();
                MeshRenderer sltBtnLock3Render = sltBtnLock3.GetComponent<MeshRenderer>();

                sltBtnLock1Render.material.SetTexture("_MainTex", SlotMachineLightsColor);
                sltBtnLock1Render.material.SetTexture("_EmissionMap", SlotMachineLightsEmiColor);
                sltBtnLock2Render.material.SetTexture("_MainTex", SlotMachineLightsColor);
                sltBtnLock2Render.material.SetTexture("_EmissionMap", SlotMachineLightsEmiColor);
                sltBtnLock3Render.material.SetTexture("_MainTex", SlotMachineLightsColor);
                sltBtnLock3Render.material.SetTexture("_EmissionMap", SlotMachineLightsEmiColor);

                //BtnPlay
                GameObject sltBtnBet = GameObject.Find("STORE/LOD/GFX_Store/SlotMachine/Buttons/button_bet");
                GameObject sltBtnCash = GameObject.Find("STORE/LOD/GFX_Store/SlotMachine/Buttons/button_cash");
                GameObject sltBtnStart = GameObject.Find("STORE/LOD/GFX_Store/SlotMachine/Buttons/button_start");

                MeshRenderer sltBtnBetRender = sltBtnBet.GetComponent<MeshRenderer>();
                MeshRenderer sltBtnCashRender = sltBtnCash.GetComponent<MeshRenderer>();
                MeshRenderer sltBtnStartRender = sltBtnStart.GetComponent<MeshRenderer>();

                sltBtnBetRender.material.SetTexture("_MainTex", SlotMachineLightsColor);
                sltBtnBetRender.material.SetTexture("_EmissionMap", SlotMachineLightsEmiColor);
                sltBtnCashRender.material.SetTexture("_MainTex", SlotMachineLightsColor);
                sltBtnCashRender.material.SetTexture("_EmissionMap", SlotMachineLightsEmiColor);
                sltBtnStartRender.material.SetTexture("_MainTex", SlotMachineLightsColor);
                sltBtnStartRender.material.SetTexture("_EmissionMap", SlotMachineLightsEmiColor);
            }


            foreach (AudioSource audio in GameObject.FindObjectsOfType<AudioSource>())
            {
                try
                {
                    string[] filename = ab.GetAllAssetNames();

                    string clipname = audio.clip.name + ".wav";

                    foreach (string file in filename)
                    {
                        if (regSwear.IsMatch(clipname) && file.Contains(audio.clip.name) && audio.clip.name != "swear1" || regFuck.IsMatch(clipname) && file.Contains(audio.clip.name))
                        {

                            audio.clip = ab.LoadAsset(file) as AudioClip;
                            index++;
                        }
                    }

                }
                catch (System.Exception)
                {

                }
            }
          
            ModConsole.Print($"{index} хуйкинов загружено!");
            ab.Unload(false);
        }
    }
}
