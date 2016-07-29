using System;

namespace SevenKnightsAI.Classes.Mappings
{
	
	internal static class AdventureLootHeroPM
	{
        public static readonly PixelMapping twoStar = new PixelMapping
        {
            X = 472,
            Y = 272,
            Color = 16166693,
            Type = MappingType.BOTH
        };

        public static readonly PixelMapping oneStar = new PixelMapping
        {
            X = 480,
            Y = 271,
            Color = 16372553,
            Type = MappingType.BOTH
        };

        public static readonly PixelMapping AdventureButton = new PixelMapping
		{
			X = 895,
			Y = 363,
			Color = 13150816,
			Type = MappingType.BOTH
		};

		
		public static readonly PixelMapping AgainButton = new PixelMapping
		{
			X = 884,
			Y = 150,
			Color = 5383183,
			Type = MappingType.BOTH
		};

		
		public static readonly PixelMapping NextZoneButton = new PixelMapping
		{
			X = 884,
			Y = 250,
			Color = 4924176,
			Type = MappingType.BOTH
		};

		
		public static readonly PixelMapping QuickStartButton = new PixelMapping
		{
			X = 786,
			Y = 480,
			Color = 5317647,
			Type = MappingType.BOTH
		};
	}
}
