using System;
using System.Drawing;

namespace SevenKnightsAI.Classes.Mappings
{
    internal static class SharedPM
    {
        #region Public Fields

        public static readonly PixelMapping BackButton = new PixelMapping
        {
            X = 58,
            Y = 28,
            Color = 12925963,
            Type = MappingType.BOTH
        };

        public static readonly PixelMapping BackButtonAnchor = new PixelMapping
        {
            X = 62,
            Y = 36,
            Color = 12005898,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_AutoSkillButton = new PixelMapping
        {
            X = 128,
            Y = 496,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Fight_AutoSkillOff = new PixelMapping
        {
            X = 127,
            Y = 524,
            Color = 7033650,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_AutoSkillOnBottom = new PixelMapping
        {
            X = 128,
            Y = 525,
            Color = 7481611,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_AutoSkillOnTop = new PixelMapping
        {
            X = 128,
            Y = 466,
            Color = 3547410,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_ChatButton = new PixelMapping
        {
            X = 926,
            Y = 100,
            Color = 7481868,
            Type = MappingType.BOTH
        };

        public static readonly PixelMapping Fight_PauseButton = new PixelMapping
        {
            X = 872,
            Y = 100,
            Color = 7285261,
            Type = MappingType.BOTH
        };

        public static readonly PixelMapping Fight_Skill1 = new PixelMapping
        {
            X = 576,
            Y = 425,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Fight_Skill1_Q1_1 = new PixelMapping
        {
            X = 593,
            Y = 399,
            Color = 16770446,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill1_Q1_2 = new PixelMapping
        {
            X = 593,
            Y = 406,
            Color = 15584893,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill10 = new PixelMapping
        {
            X = 918,
            Y = 498,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Fight_Skill10_Q1_1 = new PixelMapping
        {
            X = 936,
            Y = 472,
            Color = 16772254,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill10_Q1_2 = new PixelMapping
        {
            X = 935,
            Y = 480,
            Color = 15387516,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill2 = new PixelMapping
        {
            X = 662,
            Y = 425,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Fight_Skill2_Q1_1 = new PixelMapping
        {
            X = 679,
            Y = 399,
            Color = 16770962,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill2_Q1_2 = new PixelMapping
        {
            X = 679,
            Y = 406,
            Color = 15650947,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill3 = new PixelMapping
        {
            X = 747,
            Y = 425,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Fight_Skill3_Q1_1 = new PixelMapping
        {
            X = 764,
            Y = 399,
            Color = 16770446,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill3_Q1_2 = new PixelMapping
        {
            X = 764,
            Y = 406,
            Color = 15584893,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill4 = new PixelMapping
        {
            X = 832,
            Y = 425,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Fight_Skill4_Q1_1 = new PixelMapping
        {
            X = 850,
            Y = 399,
            Color = 16770962,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill4_Q1_2 = new PixelMapping
        {
            X = 850,
            Y = 406,
            Color = 15651203,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill5 = new PixelMapping
        {
            X = 918,
            Y = 425,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Fight_Skill5_Q1_1 = new PixelMapping
        {
            X = 935,
            Y = 399,
            Color = 16770446,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill5_Q1_2 = new PixelMapping
        {
            X = 935,
            Y = 406,
            Color = 15584893,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill6 = new PixelMapping
        {
            X = 576,
            Y = 498,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Fight_Skill6_Q1_1 = new PixelMapping
        {
            X = 594,
            Y = 472,
            Color = 16772254,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill6_Q1_2 = new PixelMapping
        {
            X = 593,
            Y = 480,
            Color = 15321723,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill7 = new PixelMapping
        {
            X = 662,
            Y = 498,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Fight_Skill7_Q1_1 = new PixelMapping
        {
            X = 679,
            Y = 472,
            Color = 16772254,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill7_Q1_2 = new PixelMapping
        {
            X = 679,
            Y = 480,
            Color = 15453052,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill8 = new PixelMapping
        {
            X = 747,
            Y = 498,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Fight_Skill8_Q1_1 = new PixelMapping
        {
            X = 765,
            Y = 472,
            Color = 16772254,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill8_Q1_2 = new PixelMapping
        {
            X = 764,
            Y = 480,
            Color = 15387516,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill9 = new PixelMapping
        {
            X = 832,
            Y = 498,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Fight_Skill9_Q1_1 = new PixelMapping
        {
            X = 851,
            Y = 472,
            Color = 16772254,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_Skill9_Q1_2 = new PixelMapping
        {
            X = 850,
            Y = 480,
            Color = 15453309,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_SpeedButton = new PixelMapping
        {
            X = 44,
            Y = 496,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Fight_SpeedOff = new PixelMapping
        {
            X = 43,
            Y = 491,
            Color = 5586469,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Fight_SpeedOn = new PixelMapping
        {
            X = 50,
            Y = 486,
            Color = 4465168,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Friends_DimmedBG_1 = new PixelMapping
        {
            X = 751,
            Y = 28,
            Color = 4012075,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Friends_DimmedBG_2 = new PixelMapping
        {
            X = 450,
            Y = 112,
            Color = 1122080,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Friends_PopupBorder = new PixelMapping
        {
            X = 260,
            Y = 203,
            Color = 16772008,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Full_DimmedBG = new PixelMapping
        {
            X = 473,
            Y = 23,
            Color = 788483,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Full_NoButton = new PixelMapping
        {
            X = 330,
            Y = 395,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Full_ProceedButton = new PixelMapping
        {
            X = 480,
            Y = 395,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Full_SellButton = new PixelMapping
        {
            X = 628,
            Y = 395,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping GiftFull_CancelButton = new PixelMapping
        {
            X = 375,
            Y = 396,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping GiftFull_DimmedBG = new PixelMapping
        {
            X = 885,
            Y = 18,
            Color = 4930058,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping GiftFull_PopupBorder = new PixelMapping
        {
            X = 260,
            Y = 200,
            Color = 16639654,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping GiftFull_RedCross = new PixelMapping
        {
            X = 314,
            Y = 394,
            Color = 14171656,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Hero_BlackBar = new PixelMapping
        {
            X = 856,
            Y = 35,
            Color = 0,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Hero_BottomLeftBorder = new PixelMapping
        {
            X = 170,
            Y = 533,
            //Color = 10972681,
            Color = 11170319,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Hero_LeaderButton = new PixelMapping
        {
            X = 710,
            Y = 479,
            Color = 13999149,
            Type = MappingType.BOTH
        };

        public static readonly PixelMapping Hero_Level30_1 = new PixelMapping
        {
            X = 658,
            Y = 74,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Hero_Level30_2 = new PixelMapping
        {
            X = 664,
            Y = 82,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Hero_Level30_3 = new PixelMapping
        {
            X = 657,
            Y = 93,
            Color = 16514043,
            Type = MappingType.ANCHOR
        };

        public static readonly Rectangle Hero_R_Level_30 = new Rectangle
        {
            X = 627,
            Y = 60,
            Width = 145,
            Height = 42
        };

        public static readonly PixelMapping Loot_HeroButton = new PixelMapping
        {
            X = 51,
            Y = 499,
            Color = 2231301,
            Type = MappingType.BOTH
        };

        public static readonly PixelMapping Loot_LobbyButton = new PixelMapping
        {
            X = 890,
            Y = 489,
            Color = 16177007,
            Type = MappingType.BOTH
        };

        public static readonly PixelMapping Lost_LobbyButton = new PixelMapping
        {
            X = 892,
            Y = 476,
            Color = 16177007,
            Type = MappingType.BOTH
        };

        public static readonly PixelMapping Lost_Moon = new PixelMapping
        {
            X = 552,
            Y = 123,
            Color = 16777215,
            Type = MappingType.BOTH
        };

        public static readonly PixelMapping Lost_PurpleBase = new PixelMapping
        {
            X = 298,
            Y = 425,
            Color = 6113452,
            Type = MappingType.BOTH
        };

        public static readonly PixelMapping PrepareFight_ManageButton = new PixelMapping
        {
            X = 366,
            Y = 478,
            Color = 12752445,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping PrepareFight_Mastery_1 = new PixelMapping
        {
            X = 396,
            Y = 152,
            Color = 15782941,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping PrepareFight_Mastery_2 = new PixelMapping
        {
            X = 399,
            Y = 162,
            Color = 15454237,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping PrepareFight_Mastery_3 = new PixelMapping
        {
            X = 400,
            Y = 159,
            Color = 16769568,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping PrepareFight_MasteryButton = new PixelMapping
        {
            X = 382,
            Y = 182,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping PrepareFight_StartButton = new PixelMapping
        {
            X = 650,
            Y = 500,
            Color = 5973262,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping PrepareFight_TeamAButton = new PixelMapping
        {
            X = 166,
            Y = 84,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping PrepareFight_TeamBButton = new PixelMapping
        {
            X = 264,
            Y = 84,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping PrepareFight_TeamCButton = new PixelMapping
        {
            X = 365,
            Y = 84,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Quests_Background = new PixelMapping
        {
            X = 410,
            Y = 63,
            Color = 12905957,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Quests_CharacterArmor = new PixelMapping
        {
            X = 84,
            Y = 368,
            Color = 16777213,
            Type = MappingType.ANCHOR
        };

        public static readonly Rectangle R_GoldBase = new Rectangle
        {
            X = 215,
            Y = 19,
            Width = 98,
            Height = 22
        };

        public static readonly Rectangle R_HonorBase = new Rectangle
        {
            X = 552,
            Y = 19,
            Width = 80,
            Height = 22
        };

        public static readonly Rectangle R_KeyNormalBase = new Rectangle
        {
            X = 70,
            Y = 19,
            Width = 60,
            Height = 21
        };

        public static readonly Rectangle R_KeyOnTopTimeBase = new Rectangle
        {
            X = 70,
            Y = 10,
            Width = 60,
            Height = 21
        };

        public static readonly Rectangle R_RubyBase = new Rectangle
        {
            X = 395,
            Y = 19,
            Width = 72,
            Height = 22
        };

        public static readonly Rectangle R_TimeBase = new Rectangle
        {
            X = 72,
            Y = 30,
            Width = 54,
            Height = 19
        };

        public static readonly Rectangle R_TopazBase = new Rectangle
        {
            X = 687,
            Y = 19,
            Width = 72,
            Height = 20
        };

        public static readonly PixelMapping Rewards_OkButton = new PixelMapping
        {
            X = 486,
            Y = 396,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping Rewards_PopupBorder = new PixelMapping
        {
            X = 260,
            Y = 200,
            Color = 16639654,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping Rewards_YellowTick = new PixelMapping
        {
            X = 421,
            Y = 401,
            Color = 16756754,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping ShopPopup_DimmedBG = new PixelMapping
        {
            X = 116,
            Y = 335,
            Color = 65793,
            Type = MappingType.ANCHOR
        };

        #endregion Public Fields
    }
}