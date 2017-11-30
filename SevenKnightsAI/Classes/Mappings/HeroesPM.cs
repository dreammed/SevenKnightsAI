using System;
using System.Drawing;

namespace SevenKnightsAI.Classes.Mappings
{
    internal static class HeroesPM
    {
        #region Public Fields

        public static readonly PixelMapping BlockingChatCloseButton = new PixelMapping
        {
            X = 907,
            Y = 81,
            Color = 13843721,
            Type = MappingType.BOTH
        };

        public static readonly int CARD_LAST_Y_DELTA = 10;

        public static readonly int CARD_X_DELTA = 119;

        public static readonly int CARD_Y_DELTA = 207;

        public static readonly PixelMapping ElementButton = new PixelMapping
        {
            X = 494,
            Y = 507,
            Color = 5641984,
            Type = MappingType.BOTH
        };

        public static readonly PixelMapping F1B4_1 = new PixelMapping
        {
            X = 500,
            Y = 499,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F1B4_2 = new PixelMapping
        {
            X = 73,
            Y = 157,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F1B4_3 = new PixelMapping
        {
            X = 73,
            Y = 252,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F1B4_4 = new PixelMapping
        {
            X = 73,
            Y = 346,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F1B4_5 = new PixelMapping
        {
            X = 73,
            Y = 441,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F1B4_Finger_1 = new PixelMapping
        {
            X = 169,
            Y = 244,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F1B4_Finger_2 = new PixelMapping
        {
            X = 77,
            Y = 106,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F1B4_Finger_3 = new PixelMapping
        {
            X = 77,
            Y = 200,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F1B4_Finger_4 = new PixelMapping
        {
            X = 77,
            Y = 294,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F1B4_Finger_5 = new PixelMapping
        {
            X = 77,
            Y = 390,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F1B4_MaxLevel1_1 = new PixelMapping
        {
            X = 93,
            Y = 313,
            Color = 16304931,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F1B4_MaxLevel1_2 = new PixelMapping
        {
            X = 93,
            Y = 321,
            Color = 15633415,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F1B4_MaxLevel2_1 = new PixelMapping
        {
            X = 93,
            Y = 173,
            Color = 15906583,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F1B4_MaxLevel2_2 = new PixelMapping
        {
            X = 93,
            Y = 181,
            Color = 16289030,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F1B4_MaxLevel3_1 = new PixelMapping
        {
            X = 93,
            Y = 268,
            Color = 14653449,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F1B4_MaxLevel3_2 = new PixelMapping
        {
            X = 93,
            Y = 276,
            Color = 15631107,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F1B4_MaxLevel4_1 = new PixelMapping
        {
            X = 93,
            Y = 361,
            Color = 16238627,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F1B4_MaxLevel4_2 = new PixelMapping
        {
            X = 93,
            Y = 369,
            Color = 15304967,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F1B4_MaxLevel5_1 = new PixelMapping
        {
            X = 93,
            Y = 457,
            Color = 14653449,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F1B4_MaxLevel5_2 = new PixelMapping
        {
            X = 93,
            Y = 465,
            Color = 15631107,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F2B3_1 = new PixelMapping
        {
            X = 167,
            Y = 230,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F2B3_2 = new PixelMapping
        {
            X = 167,
            Y = 373,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F2B3_3 = new PixelMapping
        {
            X = 74,
            Y = 162,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F2B3_4 = new PixelMapping
        {
            X = 74,
            Y = 301,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F2B3_5 = new PixelMapping
        {
            X = 74,
            Y = 440,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F2B3_Finger_1 = new PixelMapping
        {
            X = 171,
            Y = 174,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F2B3_Finger_2 = new PixelMapping
        {
            X = 171,
            Y = 316,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F2B3_Finger_3 = new PixelMapping
        {
            X = 76,
            Y = 105,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F2B3_Finger_4 = new PixelMapping
        {
            X = 76,
            Y = 244,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F2B3_Finger_5 = new PixelMapping
        {
            X = 76,
            Y = 244,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F2B3_MaxLevel1_1 = new PixelMapping
        {
            X = 233,
            Y = 246,
            Color = 15643668,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F2B3_MaxLevel1_2 = new PixelMapping
        {
            X = 233,
            Y = 254,
            Color = 15828483,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F2B3_MaxLevel2_1 = new PixelMapping
        {
            X = 233,
            Y = 388,
            Color = 15382040,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F2B3_MaxLevel2_2 = new PixelMapping
        {
            X = 233,
            Y = 396,
            Color = 15894532,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F2B3_MaxLevel3_1 = new PixelMapping
        {
            X = 141,
            Y = 177,
            Color = 15776281,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F2B3_MaxLevel3_2 = new PixelMapping
        {
            X = 141,
            Y = 185,
            Color = 15829510,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F2B3_MaxLevel4_1 = new PixelMapping
        {
            X = 141,
            Y = 316,
            Color = 16172319,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F2B3_MaxLevel4_2 = new PixelMapping
        {
            X = 141,
            Y = 324,
            Color = 15830022,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F2B3_MaxLevel5_1 = new PixelMapping
        {
            X = 141,
            Y = 455,
            Color = 15907866,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F2B3_MaxLevel5_2 = new PixelMapping
        {
            X = 141,
            Y = 463,
            Color = 15567110,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F3B2_1 = new PixelMapping
        {
            X = 166,
            Y = 161,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F3B2_2 = new PixelMapping
        {
            X = 166,
            Y = 300,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F3B2_3 = new PixelMapping
        {
            X = 166,
            Y = 440,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F3B2_4 = new PixelMapping
        {
            X = 75,
            Y = 229,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F3B2_5 = new PixelMapping
        {
            X = 75,
            Y = 371,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F3B2_Finger_1 = new PixelMapping
        {
            X = 170,
            Y = 105,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F3B2_Finger_2 = new PixelMapping
        {
            X = 170,
            Y = 245,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F3B2_Finger_3 = new PixelMapping
        {
            X = 170,
            Y = 384,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F3B2_Finger_4 = new PixelMapping
        {
            X = 78,
            Y = 174,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F3B2_Finger_5 = new PixelMapping
        {
            X = 78,
            Y = 315,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F3B2_MaxLevel1_1 = new PixelMapping
        {
            X = 232,
            Y = 176,
            Color = 14530850,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F3B2_MaxLevel1_2 = new PixelMapping
        {
            X = 232,
            Y = 184,
            Color = 15503367,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F3B2_MaxLevel2_1 = new PixelMapping
        {
            X = 232,
            Y = 316,
            Color = 14789654,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F3B2_MaxLevel2_2 = new PixelMapping
        {
            X = 232,
            Y = 323,
            Color = 15701770,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F3B2_MaxLevel3_1 = new PixelMapping
        {
            X = 233,
            Y = 455,
            Color = 14523148,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F3B2_MaxLevel3_2 = new PixelMapping
        {
            X = 233,
            Y = 462,
            Color = 15503112,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F3B2_MaxLevel4_1 = new PixelMapping
        {
            X = 141,
            Y = 245,
            Color = 15445009,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F3B2_MaxLevel4_2 = new PixelMapping
        {
            X = 141,
            Y = 253,
            Color = 16091140,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F3B2_MaxLevel5_1 = new PixelMapping
        {
            X = 141,
            Y = 387,
            Color = 14982923,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F3B2_MaxLevel5_2 = new PixelMapping
        {
            X = 141,
            Y = 395,
            Color = 15959299,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F4B1_1 = new PixelMapping
        {
            X = 166,
            Y = 159,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F4B1_2 = new PixelMapping
        {
            X = 166,
            Y = 253,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F4B1_3 = new PixelMapping
        {
            X = 166,
            Y = 347,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F4B1_4 = new PixelMapping
        {
            X = 166,
            Y = 442,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F4B1_5 = new PixelMapping
        {
            X = 74,
            Y = 301,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping F4B1_Finger_1 = new PixelMapping
        {
            X = 169,
            Y = 106,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F4B1_Finger_2 = new PixelMapping
        {
            X = 169,
            Y = 201,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F4B1_Finger_3 = new PixelMapping
        {
            X = 169,
            Y = 294,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F4B1_Finger_4 = new PixelMapping
        {
            X = 169,
            Y = 390,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F4B1_Finger_5 = new PixelMapping
        {
            X = 77,
            Y = 249,
            Color = 16777215,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F4B1_MaxLevel1_1 = new PixelMapping
        {
            X = 232,
            Y = 174,
            Color = 14989341,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F4B1_MaxLevel1_2 = new PixelMapping
        {
            X = 232,
            Y = 182,
            Color = 15895558,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F4B1_MaxLevel2_1 = new PixelMapping
        {
            X = 232,
            Y = 269,
            Color = 15055647,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F4B1_MaxLevel2_2 = new PixelMapping
        {
            X = 232,
            Y = 277,
            Color = 15699206,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F4B1_MaxLevel3_1 = new PixelMapping
        {
            X = 232,
            Y = 363,
            Color = 15119642,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F4B1_MaxLevel3_2 = new PixelMapping
        {
            X = 232,
            Y = 371,
            Color = 15697924,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F4B1_MaxLevel4_1 = new PixelMapping
        {
            X = 232,
            Y = 458,
            Color = 15055903,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F4B1_MaxLevel4_2 = new PixelMapping
        {
            X = 232,
            Y = 466,
            Color = 15699206,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F4B1_MaxLevel5_1 = new PixelMapping
        {
            X = 141,
            Y = 317,
            Color = 15973145,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping F4B1_MaxLevel5_2 = new PixelMapping
        {
            X = 141,
            Y = 325,
            Color = 15632645,
            Type = MappingType.ANCHOR
        };

        public static readonly int GOLD_OFFSET_X = 367;

        public static readonly int GOLD_OFFSET_Y = 0;

        public static readonly PixelMapping HasNext_1 = new PixelMapping
        {
            X = 327,
            Y = 526,
            Color = 3352092,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping HasNext_2 = new PixelMapping
        {
            X = 462,
            Y = 527,
            Color = 1971986,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping HeroCard1 = new PixelMapping
        {
            X = 380,
            Y = 212,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping HeroCard2 = new PixelMapping
        {
            X = 500,
            Y = 212,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping HeroCard3 = new PixelMapping
        {
            X = 620,
            Y = 212,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping HeroCard4 = new PixelMapping
        {
            X = 735,
            Y = 212,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping HeroCard5 = new PixelMapping
        {
            X = 845,
            Y = 212,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping HeroCard6 = new PixelMapping
        {
            X = 380,
            Y = 418,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping HeroCard7 = new PixelMapping
        {
            X = 500,
            Y = 418,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping HeroCard8 = new PixelMapping
        {
            X = 620,
            Y = 418,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping HeroCard9 = new PixelMapping
        {
            X = 735,
            Y = 418,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping HeroCard10 = new PixelMapping
        {
            X = 845,
            Y = 418,
            Color = 0,
            Type = MappingType.BUTTON
        };


        public static readonly PixelMapping IconLeft = new PixelMapping
        {
            X = 321,
            Y = 84,
            Color = 14596172,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping IconMiddle = new PixelMapping
        {
            X = 339,
            Y = 78,
            Color = 15718767,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping IconRight = new PixelMapping
        {
            X = 348,
            Y = 80,
            Color = 12289595,
            Type = MappingType.ANCHOR
        };

        public static readonly int KEY_OFFSET_X = 353;

        public static readonly int KEY_OFFSET_Y = 0;

        public static readonly PixelMapping LastRow_1 = new PixelMapping
        {
            X = 327,
            Y = 115,
            Color = 920334,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping LastRow_2 = new PixelMapping
        {
            X = 447,
            Y = 115,
            Color = 1117450,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping OptimizeBorder = new PixelMapping
        {
            X = 49,
            Y = 505,
            Color = 16706473,
            Type = MappingType.ANCHOR
        };

        public static readonly Rectangle R_HeroCard_Base = new Rectangle
        {
            X = 383,
            Y = 163,
            Width = 20,
            Height = 20
        };

        public static readonly int RUBY_OFFSET_X = 376;

        public static readonly int RUBY_OFFSET_Y = 0;
        public static readonly int SCROLL_DELTA = 228;

        public static readonly int SCROLL_DOUBLE_DELTA = 275;

        public static readonly PixelMapping ScrollAreaDown = new PixelMapping
        {
            X = 618,
            Y = 510,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping ScrollAreaUp = new PixelMapping
        {
            X = 618,
            Y = 130,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping SortButton = new PixelMapping
        {
            X = 886,
            Y = 86,
            Color = 13670528,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping SortButtonAscending = new PixelMapping
        {
            X = 883,
            Y = 92,
            Color = 13670528,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping SortButtonDescending = new PixelMapping
        {
            X = 883,
            Y = 83,
            Color = 13473149,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping SortByAttack = new PixelMapping
        {
            X = 803,
            Y = 154,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping SortByBox = new PixelMapping
        {
            X = 803,
            Y = 87,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping SortByBoxExpanded = new PixelMapping
        {
            X = 803,
            Y = 342,
            Color = 4531734,
            Type = MappingType.ANCHOR
        };

        public static readonly PixelMapping SortByDefense = new PixelMapping
        {
            X = 803,
            Y = 188,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping SortByGeneral = new PixelMapping
        {
            X = 803,
            Y = 120,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping SortByHealth = new PixelMapping
        {
            X = 803,
            Y = 222,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping SortByLevel = new PixelMapping
        {
            X = 803,
            Y = 324,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping SortByPowerUp = new PixelMapping
        {
            X = 803,
            Y = 358,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping SortByRank = new PixelMapping
        {
            X = 803,
            Y = 290,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping SortBySpeed = new PixelMapping
        {
            X = 803,
            Y = 256,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping TeamAButton = new PixelMapping
        {
            X = 82,
            Y = 84,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping TeamBButton = new PixelMapping
        {
            X = 172,
            Y = 84,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly PixelMapping TeamCButton = new PixelMapping
        {
            X = 264,
            Y = 84,
            Color = 0,
            Type = MappingType.BUTTON
        };

        public static readonly Rectangle R_HeroCount = new Rectangle
        {
            X = 456,
            Y = 80,
            Width = 56,
            Height = 20
        };

        #endregion Public Fields
    }
}