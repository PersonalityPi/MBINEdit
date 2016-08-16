using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MBINEdit
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    class MBINHeader
    {
        public int Magic;
        public int Unknown4;
        public int Unknown8;
        public int UnknownC;
        public long Unknown10;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x48)]
        public string TemplateName;

        public string GetXMLTemplateName()
        {
            if (!TemplateName.StartsWith("c") || TemplateName.Length < 2)
                return TemplateName;

            return TemplateName.Substring(1); // remove the "c" (compiled?) from the start of the template name
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    class cGcDebugOptions
    {
        [MarshalAs(UnmanagedType.I1)]
        /* 0x00 */ public bool SkipIntro;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x01 */ public bool VideoCaptureMode;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x02 */ public bool GodMode;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x03 */ public bool DisableVibration;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x04 */ public bool MapWarpCheckIgnoreFuel;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x05 */ public bool MapWarpCheckIgnoreDrive;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x06 */ public bool EverythingIsFree;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x07 */ public bool EverythingIsKnown;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x08 */ public bool EverythingIsStar;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x09 */ public bool UseScreenEffects;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x0A */ public bool UseGunImpactEffect;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x0B */ public bool RenderCreatureDetails;
        /* 0x0C */ public int ScreenWidth;
        /* 0x10 */ public int ScreenHeight;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x14 */ public bool DisableVSync;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        /* 0x15 */ public char[] Padding15;
        /* 0x18 */ public int GameWindowMode;
        public string[] GameWindowModeValues()
        {
            return new[] { "Bordered", "Borderless", "Fullscreen" };
        }

        /* 0x1C */ public int Monitor;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
        /* 0x20 */ public string ForceUniverseAddress; // 0x100
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
        /* 0x120 */ public string ForcePlayerPosition; // 0x100

        [MarshalAs(UnmanagedType.I1)]
        /* 0x220 */ public bool ForceInitialShip;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x221 */ public bool ForceInitialWeapon;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        /* 0x222 */ public char[] Padding222;
        /* 0x224 */ public int GameStateMode;
        public string[] GameStateModeValues()
        {
            return new[] { "LoadPreset", "UserStorage", "FreshStart" };
        }

        /* 0x228 */ public int BootMode;
        public string[] BootModeValues()
        {
            return new[] { "SolarSystem", "GalaxyMap", "SmokeTest", "SmokeTestGalaxyMap", "Scratchpad", "UnitTest" };
        }
        /* 0x22C */ public int PlayerSpawnLocationOverride;
        public string[] PlayerSpawnLocationOverrideValues()
        {
            return new[] { "None", "InNearestStation", "OnNearestPlanet", "OnFurthestPlanet", "NextToPlayerShip", "FromSettings", "OnRandomPlanet", "OnGameStartPlanet", "AwayFromYourShip" };
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
        /* 0x230 */ public string SceneSettings; // 0x80
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
        /* 0x2B0 */ public string WorkingDirectory; // 0x100

        /* 0x3B0 */ public int SolarSystemBoot;
        public string[] SolarSystemBootValues()
        {
            return new[] { "FromSettings", "Generate" };
        }
        /* 0x3B4 */ public int ShaderPreload;
        public string[] ShaderPreloadValues()
        {
            return new[] { "Off", "Full" };
        }
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3B8 */ public bool ShaderPreloadListExport;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3B9 */ public bool ShaderPreloadListImport;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3BA */ public bool ShaderCaching;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        /* 0x3BB */ public char[] Padding3BB;
        /* 0x3BC */ public int BootLoadDelay;
        public string[] BootLoadDelayValues()
        {
            return new[] { "LoadAll", "WaitForPlanet", "WaitForNothing" };
        }
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3C0 */ public bool UseParticles;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3C1 */ public bool UseVolumetrics;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3C2 */ public bool UseClouds;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3C3 */ public bool UseTerrain;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3C4 */ public bool UseInstances;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3C5 */ public bool UseObjects;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3C6 */ public bool UseBuildings;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3C7 */ public bool UseCreatures;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3C8 */ public bool UseElevation;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3C9 */ public bool SpawnPirates;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3CA */ public bool SpawnRobots;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3CB */ public bool SpawnShips;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3CC */ public bool InstanceCollision;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3CD */ public bool MouseLookEnabled;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3CE */ public bool StartPaused;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3CF */ public bool DisableDebugControls;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3D0 */ public bool DisableAsserts;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3D1 */ public bool FixedFramerate;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3D2 */ public bool ScreenshotMode;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3D3 */ public bool RenderHud;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3D4 */ public bool DebugDrawPlayerInteract;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3D5 */ public bool ForceInteractionToSettings;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        /* 0x3D6 */ public char[] Padding3D6;
        /* 0x3D8 */ public int ForceInteractionIndex;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3DC */ public bool InteractionsAllwaysGivesTech; // (sic)
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3DD */ public bool InfiniteInteractions;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x3DE */ public bool StopSwitchingToSecondaryInteractions;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        /* 0x3DF */ public char[] Padding3DF;
        /* 0x3E0 */ public int DebugLanguages; // unused?

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        /* 0x3E4 */ public string AllowedLanguagesFile; // 0x20
        [MarshalAs(UnmanagedType.I1)]
        /* 0x404 */ public bool DoAlienLanguage;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        /* 0x405 */ public char[] Padding405;
        /* 0x408 */ public int ForceInteractionRaceTo;
        /* 0x40C */ public int RealityMode; // unused?
        public string[] RealityModeValues()
        {
            return new[] { "LoadPreset", "Generate" };
        }
        [MarshalAs(UnmanagedType.I1)]
        /* 0x410 */ public bool DebugPersistentInteractions;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
        /* 0x411 */ public string RealityPresetFile; // 0x80
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        /* 0x491 */ public char[] Padding491;
        /* 0x492 */ public short RealityGenerationIteration;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
        /* 0x494 */ public string DefaultSaveData; // 0x80
        [MarshalAs(UnmanagedType.I1)]
        /* 0x514 */ public bool FormatDownloadStorageAreaOnBoot;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x515 */ public bool ForceBasicLoadScreen;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        /* 0x516 */ public char[] Padding516;
        /* 0x518 */ public float BootLogoFadeRate;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x51C */ public bool BootMusic;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x51D */ public bool LogMissingLocalisedText;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x51E */ public bool FleetDirectorAutoMode;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        /* 0x51F */ public char[] Padding51F;
        /* 0x520 */ public float _3dTextDistance;
        /* 0x524 */ public float _3dTextMinScale;
        /* 0x528 */ public int RecordSetting;
        public string[] RecordSettingValues()
        {
            return new[] { "None", "Record", "Playback" };
        }
        [MarshalAs(UnmanagedType.I1)]
        /* 0x52C */ public bool DebugBuildingSpawns;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x52D */ public bool StressTestLongNameDisplay;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x52E */ public bool ShowFramerate;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x52F */ public bool ShowPositionDebug;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x530 */ public bool ShowGPUMemory;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x531 */ public bool ShowMempoolOverlay;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x100)]
        /* 0x532 */ public string ShowUniverseAddressOnGalaxyMap; // 0x100
        [MarshalAs(UnmanagedType.I1)]
        /* 0x632 */ public bool ShowGraphs;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x633 */ public bool GraphCommandBuffer;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x634 */ public bool GraphGeneration;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x635 */ public bool GraphFPS;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x636 */ public bool SmokeTestDumpStatsMode;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        /* 0x637 */ public char[] Padding637;
        /* 0x638 */ public int SmokeTestCycleMode;
        public string[] SmokeTestCycleModeValues()
        {
            return new[] { "None", "TourSolarSystem", "RegeneratePlanet" };
        }
        [MarshalAs(UnmanagedType.I1)]
        /* 0x63C */ public bool SmokeTestCameraFly;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        /* 0x63D */ public char[] Padding63D;
        /* 0x640 */ public int SmokeTestConfigCaptureCycles;
        /* 0x644 */ public float SmokeTestConfigCaptureDurationInSeconds;
        /* 0x648 */ public int SmokeTestConfigCaptureFolderNameNumberOffset;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x64C */ public bool SmokeTestConfigRandomizePlanetSeed;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x64D */ public bool CreatureChatter;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x64E */ public bool CreatureErrors;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x64F */ public bool CreatureDrawVocals;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x650 */ public bool DrawCreaturesInRoutines;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x651 */ public bool CompressTextures;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x652 */ public bool DebugIBL;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x653 */ public bool DisableShadowSwitching;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
        /* 0x654 */ public string PipelineFile; // 0x80
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
        /* 0x6D4 */ public string PipelineFilePS4; // 0x80
        [MarshalAs(UnmanagedType.I1)]
        /* 0x754 */ public bool RenderLowFramerate;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x755 */ public bool SimulateNoNetworkConnection;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        /* 0x756 */ public char[] Padding756;
        /* 0x758 */ public int ProxyType;
        public string[] ProxyTypeValues()
        {
            return new[] { "None", "ManualURI", "InetProxy" };
        }
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
        /* 0x75C */ public string ProxyURI; // 0x80
        /* 0x7DC */ public int ServerEnv;
        public string[] ServerEnvValues()
        {
            return new[] { "default", "dev", "qa", "prodqa", "prod", "custom", "pentest" };
        }
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
        /* 0x7E0 */ public string AuthBaseUrl; // 0x80
        [MarshalAs(UnmanagedType.I1)]
        /* 0x860 */ public bool CertificateSecurityBypass;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        /* 0x861 */ public string OverrideUsernameForDev; // 0x20
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        /* 0x881 */ public char[] Padding881;
        /* 0x884 */ public int DiscoveryAutoSyncIntervalSeconds;
        /* 0x888 */ public int DiscoveryTrimLimitOverride;
        /* 0x88C */ public int DiscoveryTrimTriggerOverride;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x890 */ public bool EnableSynergy;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        /* 0x891 */ public string SynergyServer; // 0x20
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        /* 0x8B1 */ public char[] Padding8B1;
        /* 0x8B4 */ public int SynergyPort;
        /* 0x8B8 */ public int MaxNumDebugMessages;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x8BC */ public bool PreloadToolbox;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        /* 0x8BD */ public char[] Padding8BD;
        /* 0x8C0 */ public int DebugTextureSize;
        [MarshalAs(UnmanagedType.I1)]
        /* 0x8C4 */ public bool UseProcTextureDebugger;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        /* 0x8C5 */ public char[] Padding8C5;
        /* 0x8C8 */ public int ProceduralModelsShown;
        /* 0x8CC */ public int ProceduralModelBatchSize;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
        /* 0x8D0 */ public string DebugFont; // 0x80
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
        /* 0x950 */ public string DebugFontTexture; // 0x80
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
        /* 0x9D0 */ public string CursorTexture; // 0x80
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
        /* 0xA50 */ public string PauseTexture; // 0x80
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
        /* 0xAD0 */ public string PlayTexture; // 0x80
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
        /* 0xB50 */ public string StepTexture; // 0x80
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x80)]
        /* 0xBD0 */ public string RenderToTexture; // 0x80
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC50 */ public bool HmdEnable;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC51 */ public bool HmdOutput;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC52 */ public bool HmdTracking;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC53 */ public bool HmdStereoRender;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC54 */ public bool HmdDistortionPassthru;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        /* 0xC55 */ public char[] PaddingC55;
        /* 0xC58 */ public int HmdMonitor; // (int?)
        /* 0xC5C */ public int HmdEyeBufferWidth;
        /* 0xC60 */ public int HmdEyeBufferHeight;
        /* 0xC64 */ public float HmdEyeScalePos;
        /* 0xC68 */ public float HmdHeadScalePos;
        /* 0xC6C */ public float HmdImmersionFactor;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC70 */ public bool ForceExtremeWeather;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC71 */ public bool ForceExtremeSentinels;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC72 */ public bool ForceBiome;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        /* 0xC73 */ public char[] PaddingC73;
        /* 0xC74 */ public int ForceBiomeTo; // unused?
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC78 */ public bool ForceBuildingRace;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        /* 0xC79 */ public char[] PaddingC79;
        /* 0xC7C */ public int ForceBuildingRaceTo; // unused?
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC80 */ public bool ForceLifeLevel;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        /* 0xC81 */ public char[] PaddingC81;
        /* 0xC84 */ public int ForceLifeLevelTo; // unused?
        /* 0xC88 */ public int ForceAnomalyTo; // unused?
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC8C */ public bool DisableLimits;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC8D */ public bool LimitPerRegionInstances;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC8E */ public bool LimitPerRegionBodies;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC8F */ public bool LimitGlobalInstances;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC90 */ public bool LimitGlobalBodies;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        /* 0xC91 */ public char[] PaddingC91;
        /* 0xC94 */ public int GenerateFarLodBuildingDist;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xC98 */ public bool DeferRegionBodies;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        /* 0xC99 */ public char[] PaddingC99;
        /* 0xC9C */ public float GenerateCostDistance;
        /* 0xCA0 */ public float GenerateCostAngle;
        /* 0xCA4 */ public float GenerateCostLOD;
        /* 0xCA8 */ public float GenerateCostWait;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xCAC */ public bool DChecksEnabled;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xCAD */ public bool DChecksOutputJson;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xCAE */ public bool DChecksOutputBinary;
        [MarshalAs(UnmanagedType.I1)]
        /* 0xCAF */ public bool DChecksOutputFileLine;
        /* 0xCB0 */ public int FrameFlipRateDefault;
        /* 0xCB4 */ public int FrameFlipRateLoad;
        /* 0xCB8 */ public int FrameFlipRateGame;
        /* 0xCBC */ public float MaxFrameRate;
        /* 0xCC0 = END */
    }
}
