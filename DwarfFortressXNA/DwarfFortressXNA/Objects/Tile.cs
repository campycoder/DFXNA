using System;
using DwarfFortressXNA.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DwarfFortressXNA.Objects
{
    public enum TileType
    {
        NULL                        	= 0x0000,
        DOWN_RAMP                   	= 0x0001,
        MURKY_POOL                  	= 0x0002,
        MURKY_POOL_SLOPE            	= 0x0003,
        UNDERWORLD_GATE_UP          	= 0x0004,
        UNDERWORLD_GATE_DOWN        	= 0x0005,
        UNDERWORLD_GATE_UPDOWN      	= 0x0006,
        DRIFTWOOD                   	= 0x0013,
        ICE_STAIR_UPDOWN            	= 0x0019,
        ICE_STAIR_DOWN              	= 0x001A,
        ICE_STAIR_UP                	= 0x001B,
        EMPTY                       	= 0x0020,
        SHRUB                       	= 0x0022,
        CHASM                       	= 0x0023,
        OBSIDIAN_STAIR_UPDOWN       	= 0x0024,
        OBSIDIAN_STAIR_DOWN         	= 0x0025,
        OBSIDIAN_STAIR_UP           	= 0x0026,
        SOIL_STAIR_UPDOWN           	= 0x0027,
        SOIL_STAIR_DOWN             	= 0x0028,
        SOIL_STAIR_UP               	= 0x0029,
        GLOWING_PIT                 	= 0x002A,
        SMOOTH_LAYER_STONE_FLOOR    	= 0x002B,
        SMOOTH_OBSIDIAN_FLOOR       	= 0x002C,
        SMOOTH_MAP_FEATURE_FLOOR    	= 0x002D,
        SMOOTH_MINERAL_FLOOR        	= 0x002E,
        SMOOTH_ICE_FLOOR            	= 0x002F,
        LIGHT_GRASSY_STAIR_UPDOWN   	= 0x0031,
        LIGHT_GRASSY_STAIR_DOWN     	= 0x0032,
        LIGHT_GRASSY_STAIR_UP       	= 0x0033,
        DARK_GRASSY_STAIR_UPDOWN    	= 0x0034,
        DARK_GRASSY_STAIR_DOWN      	= 0x0035,
        DARK_GRASSY_STAIR_UP        	= 0x0036,
        LAYER_STONE_STAIR_UPDOWN    	= 0x0037,
        LAYER_STONE_STAIR_DOWN      	= 0x0038,
        LAYER_STONE_STAIR_UP        	= 0x0039,
        MINERAL_STAIR_UPDOWN        	= 0x003A,
        MINERAL_STAIR_DOWN          	= 0x003B,
        MINERAL_STAIR_UP            	= 0x003C,
        MAP_FEATURE_STAIR_UPDOWN    	= 0x003D,
        MAP_FEATURE_STAIR_DOWN      	= 0x003E,
        MAP_FEATURE_STAIR_UP        	= 0x003F,
        LAYER_STONE_FORTIFICATION   	= 0x0041,
        CAMPFIRE                    	= 0x0043,
        BURNING_GRASS               	= 0x0046,
        BURNING_TREE_TRUNK          	= 0x0047,
        BURNING_TREE_BRANCHES       	= 0x0048,
        BURNING_TREE_TWIGS          	= 0x0049,
        BURNING_TREE_CAP_WALL       	= 0x004A,
        BURNING_TREE_CAP_RAMP       	= 0x004B,
        BURNING_TREE_CAP_FLOOR      	= 0x004C,
        LAYER_STONE_PILLAR          	= 0x004F,
        OBSIDIAN_PILLAR             	= 0x0050,
        MAP_FEATURE_PILLAR          	= 0x0051,
        MINERAL_PILLAR              	= 0x0052,
        ICE_PILLAR                  	= 0x0053,
        WATERFALL                   	= 0x0059,
        RIVER_SOURCE                	= 0x005A,
        SLOPING_TREE_ROOTS          	= 0x005B,
        TREE_ROOTS                  	= 0x005C,
        TREE_TRUNK_PILLAR           	= 0x005D,
        SLOPING_TREE_TRUNK          	= 0x005E,
        TREE_TRUNK_N                	= 0x005F,
        TREE_TRUNK_S                	= 0x0060,
        TREE_TRUNK_E                	= 0x0061,
        TREE_TRUNK_W                	= 0x0062,
        TREE_TRUNK_NW               	= 0x0063,
        TREE_TRUNK_NE               	= 0x0064,
        TREE_TRUNK_SW               	= 0x0065,
        TREE_TRUNK_SE               	= 0x0066,
        TREE_TRUNK_BRANCH_N         	= 0x0067,
        TREE_TRUNK_BRANCH_S         	= 0x0068,
        TREE_TRUNK_BRANCH_E         	= 0x0069,
        TREE_TRUNK_BRANCH_W         	= 0x006A,
        TREE_BRANCH_NS              	= 0x006B,
        TREE_BRANCH_EW              	= 0x006C,
        SMOOTH_TREE_BRANCHES        	= 0x006D,
        SMOOTH_DEAD_TREE_BRANCHES   	= 0x006E,
        TREE_BRANCH_NW              	= 0x006F,
        TREE_BRANCH_NE              	= 0x0070,
        TREE_BRANCH_SW              	= 0x0071,
        TREE_BRANCH_SE              	= 0x0072,
        TREE_BRANCHES               	= 0x0073,
        TREE_TWIGS                  	= 0x0074,
        TREE_CAP_RAMP               	= 0x0075,
        TREE_CAP_PILLAR             	= 0x0076,
        TREE_CAP_WALL_N             	= 0x0077,
        TREE_CAP_WALL_S             	= 0x0078,
        TREE_CAP_WALL_E             	= 0x0079,
        TREE_CAP_WALL_W             	= 0x007A,
        TREE_CAP_WALL_NW            	= 0x007B,
        TREE_CAP_WALL_NE            	= 0x007C,
        TREE_CAP_WALL_SW            	= 0x007D,
        TREE_CAP_WALL_SE            	= 0x007E,
        TREE_CAP_FLOOR_1            	= 0x007F,
        TREE_CAP_FLOOR_2            	= 0x0080,
        TREE_CAP_FLOOR_3            	= 0x0081,
        TREE_CAP_FLOOR_4            	= 0x0082,
        DEAD_SLOPING_TREE_ROOTS     	= 0x0083,
        DEAD_TREE_ROOTS             	= 0x0084,
        DEAD_TREE_TRUNK_PILLAR      	= 0x0085,
        DEAD_SLOPING_TREE_TRUNK     	= 0x0086,
        DEAD_TREE_TRUNK_N           	= 0x0087,
        DEAD_TREE_TRUNK_S           	= 0x0088,
        DEAD_TREE_TRUNK_E           	= 0x0089,
        DEAD_TREE_TRUNK_W           	= 0x008A,
        DEAD_TREE_TRUNK_NW          	= 0x008B,
        DEAD_TREE_TRUNK_NE          	= 0x008C,
        DEAD_TREE_TRUNK_SW          	= 0x008D,
        DEAD_TREE_TRUNK_SE          	= 0x008E,
        DEAD_TREE_TRUNK_BRANCH_N    	= 0x008F,
        DEAD_TREE_TRUNK_BRANCH_S    	= 0x0090,
        DEAD_TREE_TRUNK_BRANCH_E    	= 0x0091,
        DEAD_TREE_TRUNK_BRANCH_W    	= 0x0092,
        DEAD_TREE_BRANCH_NS         	= 0x0093,
        DEAD_TREE_BRANCH_EW         	= 0x0094,
        DEAD_TREE_BRANCH_NW         	= 0x0097,
        DEAD_TREE_BRANCH_NE         	= 0x0098,
        DEAD_TREE_BRANCH_SW         	= 0x0099,
        DEAD_TREE_BRANCH_SE         	= 0x009A,
        DEAD_TREE_BRANCHES          	= 0x009B,
        DEAD_TREE_TWIGS             	= 0x009C,
        DEAD_TREE_CAP_RAMP          	= 0x009D,
        DEAD_TREE_CAP_PILLAR        	= 0x009E,
        DEAD_TREE_CAP_WALL_N        	= 0x009F,
        DEAD_TREE_CAP_WALL_S        	= 0x00A0,
        DEAD_TREE_CAP_WALL_E        	= 0x00A1,
        DEAD_TREE_CAP_WALL_W        	= 0x00A2,
        DEAD_TREE_CAP_WALL_NW       	= 0x00A3,
        DEAD_TREE_CAP_WALL_NE       	= 0x00A4,
        DEAD_TREE_CAP_WALL_SW       	= 0x00A5,
        DEAD_TREE_CAP_WALL_SE       	= 0x00A6,
        DEAD_TREE_CAP_FLOOR_1       	= 0x00A7,
        DEAD_TREE_CAP_FLOOR_2       	= 0x00A8,
        DEAD_TREE_CAP_FLOOR_3       	= 0x00A9,
        DEAD_TREE_CAP_FLOOR_4       	= 0x00AA,
        LAYER_STONE_THREE_FOURTHS_MINED = 0x00AC,
        LAYER_STONE_TWO_FOURTHS_MINED   = 0x00AD,
        LAYER_STONE_ONE_FOURTHS_MINED   = 0x00AE,
        TREE_BRANCH_NSE                 = 0x00AF,
        TREE_BRANCH_NSW                 = 0x00B0,
        TREE_BRANCH_NEW                 = 0x00B1,
        TREE_BRANCH_SEW                 = 0x00B2,
        TREE_BRANCH_NSEW                = 0x00B3,
        DEAD_TREE_BRANCH_NSE            = 0x00B4,
        DEAD_TREE_BRANCH_NSW            = 0x00B5,
        DEAD_TREE_BRANCH_NEW            = 0x00B6,
        DEAD_TREE_BRANCH_SEW            = 0x00B7,
        DEAD_TREE_BRANCH_NSEW           = 0x00B8,
        TREE_TRUNK_NSE                  = 0x00B9,
        TREE_TRUNK_NSW                  = 0x00BA,
        TREE_TRUNK_NEW                  = 0x00BB,
        TREE_TRUNK_SEW                  = 0x00BC,
        TREE_TRUNK_NS                   = 0x00BD,
        TREE_TRUNK_EW                   = 0x00BE,
        TREE_TRUNK_NSEW                 = 0x00BF,
        TREE_TRUNK_INTERIOR             = 0x00C0,
        DEAD_TREE_TRUNK_NSE             = 0x00C1,
        DEAD_TREE_TRUNK_NSW             = 0x00C2,
        DEAD_TREE_TRUNK_NEW             = 0x00C3,
        DEAD_TREE_TRUNK_SEW             = 0x00C4,
        DEAD_TREE_TRUNK_NS              = 0x00C5,
        DEAD_TREE_TRUNK_EW              = 0x00C6,
        DEAD_TREE_TRUNK_NSEW            = 0x00C7,
        DEAD_TREE_TRUNK_INTERIOR        = 0x00C8,
        ROUGH_LAYER_STONE_WALL      	= 0x00D7,
        SAPLING                         = 0x00E3,
        DRY_GRASS_UPWARD_SLOPE          = 0x00E5,
        DEAD_GRASS_UPWARD_SLOPE         = 0x00E6,
        LIGHT_GRASSY_UPWARD_SLOPE       = 0x00E7,
        DARK_GRASSY_UPWARD_SLOPE        = 0x00E8,
        LAYER_STONE_UPWARD_SLOPE        = 0x00E9,
        OBSIDIAN_UPWARD_SLOPE           = 0x00EA,
        MAP_FEATURE_UPWARD_SLOPE        = 0x00EB,
        MINERAL_UPWARD_SLOPE            = 0x00EC,
        SOIL_UPWARD_SLOPE               = 0x00ED,
        ASHES_LEFT                      = 0x00EE,
        ASHES_RIGHT                     = 0x00EF,
        ASHES_SMALL                     = 0x00F0,
        GLACIAL_UPWARD_SLOPE            = 0x00F1,
        ROUGH_ICE_FLOOR_1               = 0x00FE,
        ROUGH_ICE_FLOOR_2               = 0x00FF,
        ROUGH_ICE_FLOOR_3               = 0x0100,
        FURROWED_SOIL                   = 0x0101,
        ROUGH_ICE_FLOOR_4               = 0x0102,
        SEMI_MOLTEN_ROCK                = 0x0103,
        MAGMA_FLOW                      = 0x0104,
        SOIL_WALL                       = 0x0105,
        GLOWING_BARRIER                 = 0x0106,
        GLOWING_FLOOR                   = 0x0107,
        SMOOTH_OBSIDIAN_WALL_NE_S       = 0x0109,
        SMOOTH_OBSIDIAN_WALL_ES_S       = 0x010A,
        SMOOTH_OBSIDIAN_WALL_EN_S       = 0x010B,
        SMOOTH_OBSIDIAN_WALL_SE_S       = 0x010C,
        SMOOTH_OBSIDIAN_WALL_WN_S       = 0x010D,
        SMOOTH_OBSIDIAN_WALL_SW_S       = 0x010E,
        SMOOTH_OBSIDIAN_WALL_WS_S       = 0x010F,
        SMOOTH_OBSIDIAN_WALL_NW_S       = 0x0110,
        SMOOTH_OBSIDIAN_WALL_NSEW       = 0x0111,
        SMOOTH_OBSIDIAN_WALL_NSE        = 0x0112,
        SMOOTH_OBSIDIAN_WALL_SEW        = 0x0113,
        SMOOTH_OBSIDIAN_WALL_NEW        = 0x0114,
        SMOOTH_OBSIDIAN_WALL_NSW        = 0x0115,
        SMOOTH_OBSIDIAN_WALL_SE         = 0x0116,
        SMOOTH_OBSIDIAN_WALL_NE         = 0x0117,
        SMOOTH_OBSIDIAN_WALL_NW         = 0x0118,
        SMOOTH_OBSIDIAN_WALL_SW         = 0x0119,
        SMOOTH_OBSIDIAN_WALL_NS         = 0x011A,
        SMOOTH_OBSIDIAN_WALL_EW         = 0x011B,
        SMOOTH_MAP_FEATURE_WALL_NE_S    = 0x011C,
        SMOOTH_MAP_FEATURE_WALL_ES_S    = 0x011D,
        SMOOTH_MAP_FEATURE_WALL_EN_S    = 0x011E,
        SMOOTH_MAP_FEATURE_WALL_SE_S    = 0x011F,
        SMOOTH_MAP_FEATURE_WALL_WN_S    = 0x0120,
        SMOOTH_MAP_FEATURE_WALL_SW_S    = 0x0121,
        SMOOTH_MAP_FEATURE_WALL_WS_S    = 0x0122,
        SMOOTH_MAP_FEATURE_WALL_NW_S    = 0x0123,
        SMOOTH_MAP_FEATURE_WALL_NSEW    = 0x0124,
        SMOOTH_MAP_FEATURE_WALL_NSE     = 0x0125,
        SMOOTH_MAP_FEATURE_WALL_SEW     = 0x0126,
        SMOOTH_MAP_FEATURE_WALL_NEW     = 0x0127,
        SMOOTH_MAP_FEATURE_WALL_NSW     = 0x0128,
        SMOOTH_MAP_FEATURE_WALL_SE      = 0x0129,
        SMOOTH_MAP_FEATURE_WALL_NE      = 0x012A,
        SMOOTH_MAP_FEATURE_WALL_NW      = 0x012B,
        SMOOTH_MAP_FEATURE_WALL_SW      = 0x012C,
        SMOOTH_MAP_FEATURE_WALL_NS      = 0x012D,
        SMOOTH_MAP_FEATURE_WALL_EW      = 0x012E,
        SMOOTH_LAYER_STONE_WALL_NE_S    = 0x012F,
        SMOOTH_LAYER_STONE_WALL_ES_S    = 0x0130,
        SMOOTH_LAYER_STONE_WALL_EN_S    = 0x0131,
        SMOOTH_LAYER_STONE_WALL_SE_S    = 0x0132,
        SMOOTH_LAYER_STONE_WALL_WN_S    = 0x0133,
        SMOOTH_LAYER_STONE_WALL_SW_S    = 0x0134,
        SMOOTH_LAYER_STONE_WALL_WS_S    = 0x0135,
        SMOOTH_LAYER_STONE_WALL_NW_S    = 0x0136,
        SMOOTH_LAYER_STONE_WALL_NSEW    = 0x0137,
        SMOOTH_LAYER_STONE_WALL_NSE     = 0x0138,
        SMOOTH_LAYER_STONE_WALL_SEW     = 0x0139,
        SMOOTH_LAYER_STONE_WALL_NEW     = 0x013A,
        SMOOTH_LAYER_STONE_WALL_NSW     = 0x013B,
        SMOOTH_LAYER_STONE_WALL_SE      = 0x013C,
        SMOOTH_LAYER_STONE_WALL_NE      = 0x013D,
        SMOOTH_LAYER_STONE_WALL_NW      = 0x013E,
        SMOOTH_LAYER_STONE_WALL_SW      = 0x013F,
        SMOOTH_LAYER_STONE_WALL_NS      = 0x0140,
        SMOOTH_LAYER_STONE_WALL_EW      = 0x0141,
        OBSIDIAN_FORTIFICATION          = 0x0142,
        MAP_FEATURE_FORTIFICATION       = 0x0143,
        OBSIDIAN_THREE_FOURTHS_MINED    = 0x0144,
        OBSIDIAN_TWO_FOURTHS_MINED      = 0x0145,
        OBSIDIAN_ONE_FOURTHS_MINED      = 0x0146,
        ROUGH_OBSIDIAN_WALL             = 0x0147,
        MAP_FEATURE_THREE_FOURTHS_MINED = 0x0148,
        MAP_FEATURE_TWO_FOURTHS_MINED   = 0x0149,
        MAP_FEATURE_ONE_FOURTHS_MINED   = 0x014A,
        ROUGH_MAP_FEATURE_WALL          = 0x014B,
        ROUGH_LAYER_STONE_FLOOR_1       = 0x014C,
        ROUGH_LAYER_STONE_FLOOR_2       = 0x014D,
        ROUGH_LAYER_STONE_FLOOR_3       = 0x014E,
        ROUGH_LAYER_STONE_FLOOR_4       = 0x014F,
        ROUGH_OBSIDIAN_FLOOR_1          = 0x0150,
        ROUGH_OBSIDIAN_FLOOR_2          = 0x0151,
        ROUGH_OBSIDIAN_FLOOR_3          = 0x0152,
        ROUGH_OBSIDIAN_FLOOR_4          = 0x0153,
        ROUGH_MAP_FEATURE_FLOOR_1       = 0x0154,
        ROUGH_MAP_FEATURE_FLOOR_2       = 0x0155,
        ROUGH_MAP_FEATURE_FLOOR_3       = 0x0156,
        ROUGH_MAP_FEATURE_FLOOR_4       = 0x0157,
        DARK_GRASS_FLOOR_1              = 0x0158,
        DARK_GRASS_FLOOR_2              = 0x0159,
        DARK_GRASS_FLOOR_3              = 0x015A,
        DARK_GRASS_FLOOR_4              = 0x015B,
        SOIL_FLOOR_1                    = 0x015C,
        SOIL_FLOOR_2                    = 0x015D,
        SOIL_FLOOR_3                    = 0x015E,
        SOIL_FLOOR_4                    = 0x015F,
        WET_SOIL_FLOOR_1                = 0x0160,
        WET_SOIL_FLOOR_2                = 0x0161,
        WET_SOIL_FLOOR_3                = 0x0162,
        WET_SOIL_FLOOR_4                = 0x0163,
        ICE_FORTIFICATION               = 0x0164,
        ICE_THREE_FOURTHS_MINED         = 0x0165,
        ICE_TWO_FOURTHS_MINED           = 0x0166,
        ICE_ONE_FOURTHS_MINED           = 0x0167,
        ICE_WALL                        = 0x0168,
        RIVER_N                         = 0x0169,
        RIVER_S                         = 0x016A,
        RIVER_E                         = 0x016B,
        RIVER_W                         = 0x016C,
        RIVER_NW                        = 0x016D,
        RIVER_NE                        = 0x016E,
        RIVER_SW                        = 0x016F,
        RIVER_SE                        = 0x0170,
        BROOK_N                         = 0x0171,
        BROOK_S                         = 0x0172,
        BROOK_E                         = 0x0173,
        BROOK_W                         = 0x0174,
        BROOK_NW                        = 0x0175,
        BROOK_NE                        = 0x0176,
        BROOK_SW                        = 0x0177,
        BROOK_SE                        = 0x0178,
        BROOK_SURFACE                   = 0x0179,
        DRY_GRASS_FLOOR_1               = 0x017F,
        DRY_GRASS_FLOOR_2               = 0x0180,
        DRY_GRASS_FLOOR_3               = 0x0181,
        DRY_GRASS_FLOOR_4               = 0x0182,
        DEAD_SAPLING                    = 0x0184,
        DEAD_SHRUB                      = 0x0185,
        DEAD_GRASS_FLOOR_1              = 0x0186,
        DEAD_GRASS_FLOOR_2              = 0x0187,
        DEAD_GRASS_FLOOR_3              = 0x0188,
        DEAD_GRASS_FLOOR_4              = 0x0189,
        BRIGHT_GRASS_FLOOR_1            = 0x018A,
        BRIGHT_GRASS_FLOOR_2            = 0x018B,
        BRIGHT_GRASS_FLOOR_3            = 0x018C,
        BRIGHT_GRASS_FLOOR_4            = 0x018D,
        LAYER_STONE_BOULDER             = 0x018E,
        OBSIDIAN_BOULDER                = 0x018F,
        MAP_FEATURE_BOULDER             = 0x0190,
        LAYER_STONE_PEBBLES_1           = 0x0191,
        LAYER_STONE_PEBBLES_2           = 0x0192,
        LAYER_STONE_PEBBLES_3           = 0x0193,
        LAYER_STONE_PEBBLES_4           = 0x0194,
        OBSIDIAN_PEBBLES_1              = 0x0195,
        OBSIDIAN_PEBBLES_2              = 0x0196,
        OBSIDIAN_PEBBLES_3              = 0x0197,
        OBSIDIAN_PEBBLES_4              = 0x0198,
        MAP_FEATURE_PEBBLES_1           = 0x0199,
        MAP_FEATURE_PEBBLES_2           = 0x019A,
        MAP_FEATURE_PEBBLES_3           = 0x019B,
        MAP_FEATURE_PEBBLES_4           = 0x019C,
        SMOOTH_MINERAL_WALL_NE_S        = 0x019D,
        SMOOTH_MINERAL_WALL_ES_S        = 0x019E,
        SMOOTH_MINERAL_WALL_EN_S        = 0x019F,
        SMOOTH_MINERAL_WALL_SE_S        = 0x01A0,
        SMOOTH_MINERAL_WALL_WN_S        = 0x01A1,
        SMOOTH_MINERAL_WALL_SW_S        = 0x01A2,
        SMOOTH_MINERAL_WALL_WS_S        = 0x01A3,
        SMOOTH_MINERAL_WALL_NW_S        = 0x01A4,
        SMOOTH_MINERAL_WALL_NSEW        = 0x01A5,
        SMOOTH_MINERAL_WALL_NSE         = 0x01A6,
        SMOOTH_MINERAL_WALL_SEW         = 0x01A7,
        SMOOTH_MINERAL_WALL_NEW         = 0x01A8,
        SMOOTH_MINERAL_WALL_NSW         = 0x01A9,
        SMOOTH_MINERAL_WALL_SE          = 0x01AA,
        SMOOTH_MINERAL_WALL_NE          = 0x01AB,
        SMOOTH_MINERAL_WALL_NW          = 0x01AC,
        SMOOTH_MINERAL_WALL_SW          = 0x01AD,
        SMOOTH_MINERAL_WALL_NS          = 0x01AE,
        SMOOTH_MINERAL_WALL_EW          = 0x01AF,
        MINERAL_FORTIFICATION           = 0x01B0,
        MINERAL_THREE_FOURTHS_MINED     = 0x01B1,
        MINERAL_TWO_FOURTHS_MINED       = 0x01B2,
        MINERAL_ONE_FOURTHS_MINED       = 0x01B3,
        ROUGH_MINERAL_WALL          	= 0x01B4,
        ROUGH_MINERAL_FLOOR_1           = 0x01B5,
        ROUGH_MINERAL_FLOOR_2           = 0x01B6,
        ROUGH_MINERAL_FLOOR_3           = 0x01B7,
        ROUGH_MINERAL_FLOOR_4           = 0x01B8,
        MINERAL_BOULDER                 = 0x01B9,
        MINERAL_PEBBLES_1               = 0x01BA,
        MINERAL_PEBBLES_2               = 0x01BB,
        MINERAL_PEBBLES_3               = 0x01BC,
        MINERAL_PEBBLES_4               = 0x01BD,
        STRAIGHT_ICE_WALL_NE_S          = 0x01BE,
        STRAIGHT_ICE_WALL_ES_S          = 0x01BF,
        STRAIGHT_ICE_WALL_EN_S          = 0x01C0,
        STRAIGHT_ICE_WALL_SE_S          = 0x01C1,
        STRAIGHT_ICE_WALL_WN_S          = 0x01C2,
        STRAIGHT_ICE_WALL_SW_S          = 0x01C3,
        STRAIGHT_ICE_WALL_WS_S          = 0x01C4,
        STRAIGHT_ICE_WALL_NW_S          = 0x01C5,
        STRAIGHT_ICE_WALL_NSEW          = 0x01C6,
        STRAIGHT_ICE_WALL_NSE           = 0x01C7,
        STRAIGHT_ICE_WALL_SEW           = 0x01C8,
        STRAIGHT_ICE_WALL_NEW           = 0x01C9,
        STRAIGHT_ICE_WALL_NSW           = 0x01CA,
        STRAIGHT_ICE_WALL_SE            = 0x01CB,
        STRAIGHT_ICE_WALL_NE            = 0x01CC,
        STRAIGHT_ICE_WALL_NW            = 0x01CD,
        STRAIGHT_ICE_WALL_SW            = 0x01CE,
        STRAIGHT_ICE_WALL_NS            = 0x01CF,
        STRAIGHT_ICE_WALL_EW            = 0x01D0,
        RIVER_SLOPE_N                   = 0x01D1,
        RIVER_SLOPE_S                   = 0x01D2,
        RIVER_SLOPE_E                   = 0x01D3,
        RIVER_SLOPE_W                   = 0x01D4,
        RIVER_SLOPE_NW                  = 0x01D5,
        RIVER_SLOPE_NE                  = 0x01D6,
        RIVER_SLOPE_SW                  = 0x01D7,
        RIVER_SLOPE_SE                  = 0x01D8,
        CONSTRUCTED_FLOOR               = 0x01E9,
        CONSTRUCTED_FORTIFICATION       = 0x01EA,
        CONSTRUCTED_PILLAR              = 0x01EB,
        CONSTRUCTED_WALL_NE_S           = 0x01EC,
        CONSTRUCTED_WALL_ES_S           = 0x01ED,
        CONSTRUCTED_WALL_EN_S           = 0x01EE,
        CONSTRUCTED_WALL_SE_S           = 0x01EF,
        CONSTRUCTED_WALL_WN_S           = 0x01F0,
        CONSTRUCTED_WALL_SW_S           = 0x01F1,
        CONSTRUCTED_WALL_WS_S           = 0x01F2,
        CONSTRUCTED_WALL_NW_S           = 0x01F3,
        CONSTRUCTED_WALL_NSEW           = 0x01F4,
        CONSTRUCTED_WALL_NSE            = 0x01F5,
        CONSTRUCTED_WALL_SEW            = 0x01F6,
        CONSTRUCTED_WALL_NEW            = 0x01F7,
        CONSTRUCTED_WALL_NSW            = 0x01F8,
        CONSTRUCTED_WALL_SE             = 0x01F9,
        CONSTRUCTED_WALL_NE             = 0x01FA,
        CONSTRUCTED_WALL_NW             = 0x01FB,
        CONSTRUCTED_WALL_SW             = 0x01FC,
        CONSTRUCTED_WALL_NS             = 0x01FD,
        CONSTRUCTED_WALL_EW             = 0x01FE,
        CONSTRUCTED_STAIR_UPDOWN        = 0x01FF,
        CONSTRUCTED_STAIR_DOWN          = 0x0200,
        CONSTRUCTED_STAIR_UP            = 0x0201,
        CONSTRUCTED_RAMP                = 0x0202,
        LAYER_STONE_FLOOR_TRACK_N       = 0x0203,
        LAYER_STONE_FLOOR_TRACK_S       = 0x0204,
        LAYER_STONE_FLOOR_TRACK_E       = 0x0205,
        LAYER_STONE_FLOOR_TRACK_W       = 0x0206,
        LAYER_STONE_FLOOR_TRACK_NS      = 0x0207,
        LAYER_STONE_FLOOR_TRACK_NE      = 0x0208,
        LAYER_STONE_FLOOR_TRACK_NW      = 0x0209,
        LAYER_STONE_FLOOR_TRACK_SE      = 0x020A,
        LAYER_STONE_FLOOR_TRACK_SW      = 0x020B,
        LAYER_STONE_FLOOR_TRACK_EW      = 0x020C,
        LAYER_STONE_FLOOR_TRACK_NSE     = 0x020D,
        LAYER_STONE_FLOOR_TRACK_NSW     = 0x020E,
        LAYER_STONE_FLOOR_TRACK_NEW     = 0x020F,
        LAYER_STONE_FLOOR_TRACK_SEW     = 0x0210,
        LAYER_STONE_FLOOR_TRACK_NSEW    = 0x0211,
        OBSIDIAN_FLOOR_TRACK_N          = 0x0212,
        OBSIDIAN_FLOOR_TRACK_S          = 0x0213,
        OBSIDIAN_FLOOR_TRACK_E          = 0x0214,
        OBSIDIAN_FLOOR_TRACK_W          = 0x0215,
        OBSIDIAN_FLOOR_TRACK_NS         = 0x0216,
        OBSIDIAN_FLOOR_TRACK_NE         = 0x0217,
        OBSIDIAN_FLOOR_TRACK_NW         = 0x0218,
        OBSIDIAN_FLOOR_TRACK_SE         = 0x0219,
        OBSIDIAN_FLOOR_TRACK_SW         = 0x021A,
        OBSIDIAN_FLOOR_TRACK_EW         = 0x021B,
        OBSIDIAN_FLOOR_TRACK_NSE        = 0x021C,
        OBSIDIAN_FLOOR_TRACK_NSW        = 0x021D,
        OBSIDIAN_FLOOR_TRACK_NEW        = 0x021E,
        OBSIDIAN_FLOOR_TRACK_SEW        = 0x021F,
        OBSIDIAN_FLOOR_TRACK_NSEW       = 0x0220,
        MAP_FEATURE_FLOOR_TRACK_N       = 0x0221,
        MAP_FEATURE_FLOOR_TRACK_S       = 0x0222,
        MAP_FEATURE_FLOOR_TRACK_E       = 0x0223,
        MAP_FEATURE_FLOOR_TRACK_W       = 0x0224,
        MAP_FEATURE_FLOOR_TRACK_NS      = 0x0225,
        MAP_FEATURE_FLOOR_TRACK_NE      = 0x0226,
        MAP_FEATURE_FLOOR_TRACK_NW      = 0x0227,
        MAP_FEATURE_FLOOR_TRACK_SE      = 0x0228,
        MAP_FEATURE_FLOOR_TRACK_SW      = 0x0229,
        MAP_FEATURE_FLOOR_TRACK_EW      = 0x022A,
        MAP_FEATURE_FLOOR_TRACK_NSE     = 0x022B,
        MAP_FEATURE_FLOOR_TRACK_NSW     = 0x022C,
        MAP_FEATURE_FLOOR_TRACK_NEW     = 0x022D,
        MAP_FEATURE_FLOOR_TRACK_SEW     = 0x022E,
        MAP_FEATURE_FLOOR_TRACK_NSEW    = 0x022F,
        MINERAL_FLOOR_TRACK_N           = 0x0230,
        MINERAL_FLOOR_TRACK_S           = 0x0231,
        MINERAL_FLOOR_TRACK_E           = 0x0232,
        MINERAL_FLOOR_TRACK_W           = 0x0233,
        MINERAL_FLOOR_TRACK_NS          = 0x0234,
        MINERAL_FLOOR_TRACK_NE          = 0x0235,
        MINERAL_FLOOR_TRACK_NW          = 0x0236,
        MINERAL_FLOOR_TRACK_SE          = 0x0237,
        MINERAL_FLOOR_TRACK_SW          = 0x0238,
        MINERAL_FLOOR_TRACK_EW          = 0x0239,
        MINERAL_FLOOR_TRACK_NSE         = 0x023A,
        MINERAL_FLOOR_TRACK_NSW         = 0x023B,
        MINERAL_FLOOR_TRACK_NEW         = 0x023C,
        MINERAL_FLOOR_TRACK_SEW         = 0x023D,
        MINERAL_FLOOR_TRACK_NSEW        = 0x023E,
        ICE_FLOOR_TRACK_N               = 0x023F,
        ICE_FLOOR_TRACK_S               = 0x0240,
        ICE_FLOOR_TRACK_E               = 0x0241,
        ICE_FLOOR_TRACK_W               = 0x0242,
        ICE_FLOOR_TRACK_NS              = 0x0243,
        ICE_FLOOR_TRACK_NE              = 0x0244,
        ICE_FLOOR_TRACK_NW              = 0x0245,
        ICE_FLOOR_TRACK_SE              = 0x0246,
        ICE_FLOOR_TRACK_SW              = 0x0247,
        ICE_FLOOR_TRACK_EW              = 0x0248,
        ICE_FLOOR_TRACK_NSE             = 0x0249,
        ICE_FLOOR_TRACK_NSW             = 0x024A,
        ICE_FLOOR_TRACK_NEW             = 0x024B,
        ICE_FLOOR_TRACK_SEW             = 0x024C,
        ICE_FLOOR_TRACK_NSEW            = 0x024D,
        CONSTRUCTED_FLOOR_TRACK_N       = 0x024E,
        CONSTRUCTED_FLOOR_TRACK_S       = 0x024F,
        CONSTRUCTED_FLOOR_TRACK_E       = 0x0250,
        CONSTRUCTED_FLOOR_TRACK_W       = 0x0251,
        CONSTRUCTED_FLOOR_TRACK_NS      = 0x0252,
        CONSTRUCTED_FLOOR_TRACK_NE      = 0x0253,
        CONSTRUCTED_FLOOR_TRACK_NW      = 0x0254,
        CONSTRUCTED_FLOOR_TRACK_SE      = 0x0255,
        CONSTRUCTED_FLOOR_TRACK_SW      = 0x0256,
        CONSTRUCTED_FLOOR_TRACK_EW      = 0x0257,
        CONSTRUCTED_FLOOR_TRACK_NSE     = 0x0258,
        CONSTRUCTED_FLOOR_TRACK_NSW     = 0x0259,
        CONSTRUCTED_FLOOR_TRACK_NEW     = 0x025A,
        CONSTRUCTED_FLOOR_TRACK_SEW     = 0x025B,
        CONSTRUCTED_FLOOR_TRACK_NSEW    = 0x025C,
        LAYER_STONE_RAMP_TRACK_N        = 0x025D,
        LAYER_STONE_RAMP_TRACK_S        = 0x025E,
        LAYER_STONE_RAMP_TRACK_E        = 0x025F,
        LAYER_STONE_RAMP_TRACK_W        = 0x0260,
        LAYER_STONE_RAMP_TRACK_NS       = 0x0261,
        LAYER_STONE_RAMP_TRACK_NE       = 0x0262,
        LAYER_STONE_RAMP_TRACK_NW       = 0x0263,
        LAYER_STONE_RAMP_TRACK_SE       = 0x0264,
        LAYER_STONE_RAMP_TRACK_SW       = 0x0265,
        LAYER_STONE_RAMP_TRACK_EW       = 0x0266,
        LAYER_STONE_RAMP_TRACK_NSE      = 0x0267,
        LAYER_STONE_RAMP_TRACK_NSW      = 0x0268,
        LAYER_STONE_RAMP_TRACK_NEW      = 0x0269,
        LAYER_STONE_RAMP_TRACK_SEW      = 0x026A,
        LAYER_STONE_RAMP_TRACK_NSEW     = 0x026B,
        OBSIDIAN_RAMP_TRACK_N           = 0x026C,
        OBSIDIAN_RAMP_TRACK_S           = 0x026D,
        OBSIDIAN_RAMP_TRACK_E           = 0x026E,
        OBSIDIAN_RAMP_TRACK_W           = 0x026F,
        OBSIDIAN_RAMP_TRACK_NS          = 0x0270,
        OBSIDIAN_RAMP_TRACK_NE          = 0x0271,
        OBSIDIAN_RAMP_TRACK_NW          = 0x0272,
        OBSIDIAN_RAMP_TRACK_SE          = 0x0273,
        OBSIDIAN_RAMP_TRACK_SW          = 0x0274,
        OBSIDIAN_RAMP_TRACK_EW          = 0x0275,
        OBSIDIAN_RAMP_TRACK_NSE         = 0x0276,
        OBSIDIAN_RAMP_TRACK_NSW         = 0x0277,
        OBSIDIAN_RAMP_TRACK_NEW         = 0x0278,
        OBSIDIAN_RAMP_TRACK_SEW         = 0x0279,
        OBSIDIAN_RAMP_TRACK_NSEW        = 0x027A,
        MAP_FEATURE_RAMP_TRACK_N        = 0x027B,
        MAP_FEATURE_RAMP_TRACK_S        = 0x027C,
        MAP_FEATURE_RAMP_TRACK_E        = 0x027D,
        MAP_FEATURE_RAMP_TRACK_W        = 0x027E,
        MAP_FEATURE_RAMP_TRACK_NS       = 0x027F,
        MAP_FEATURE_RAMP_TRACK_NE       = 0x0280,
        MAP_FEATURE_RAMP_TRACK_NW       = 0x0281,
        MAP_FEATURE_RAMP_TRACK_SE       = 0x0282,
        MAP_FEATURE_RAMP_TRACK_SW       = 0x0283,
        MAP_FEATURE_RAMP_TRACK_EW       = 0x0284,
        MAP_FEATURE_RAMP_TRACK_NSE      = 0x0285,
        MAP_FEATURE_RAMP_TRACK_NSW      = 0x0286,
        MAP_FEATURE_RAMP_TRACK_NEW      = 0x0287,
        MAP_FEATURE_RAMP_TRACK_SEW      = 0x0288,
        MAP_FEATURE_RAMP_TRACK_NSEW     = 0x0289,
        MINERAL_RAMP_TRACK_N            = 0x028A,
        MINERAL_RAMP_TRACK_S            = 0x028B,
        MINERAL_RAMP_TRACK_E            = 0x028C,
        MINERAL_RAMP_TRACK_W            = 0x028D,
        MINERAL_RAMP_TRACK_NS           = 0x028E,
        MINERAL_RAMP_TRACK_NE           = 0x028F,
        MINERAL_RAMP_TRACK_NW           = 0x0290,
        MINERAL_RAMP_TRACK_SE           = 0x0291,
        MINERAL_RAMP_TRACK_SW           = 0x0292,
        MINERAL_RAMP_TRACK_EW           = 0x0293,
        MINERAL_RAMP_TRACK_NSE          = 0x0294,
        MINERAL_RAMP_TRACK_NSW          = 0x0295,
        MINERAL_RAMP_TRACK_NEW          = 0x0296,
        MINERAL_RAMP_TRACK_SEW          = 0x0297,
        MINERAL_RAMP_TRACK_NSEW         = 0x0298,
        ICE_RAMP_TRACK_N                = 0x0299,
        ICE_RAMP_TRACK_S                = 0x029A,
        ICE_RAMP_TRACK_E                = 0x029B,
        ICE_RAMP_TRACK_W                = 0x029C,
        ICE_RAMP_TRACK_NS               = 0x029D,
        ICE_RAMP_TRACK_NE               = 0x029E,
        ICE_RAMP_TRACK_NW               = 0x029F,
        ICE_RAMP_TRACK_SE               = 0x02A0,
        ICE_RAMP_TRACK_SW               = 0x02A1,
        ICE_RAMP_TRACK_EW               = 0x02A2,
        ICE_RAMP_TRACK_NSE              = 0x02A3,
        ICE_RAMP_TRACK_NSW              = 0x02A4,
        ICE_RAMP_TRACK_NEW              = 0x02A5,
        ICE_RAMP_TRACK_SEW              = 0x02A6,
        ICE_RAMP_TRACK_NSEW             = 0x02A7,
        CONSTRUCTED_RAMP_TRACK_N        = 0x02A8,
        CONSTRUCTED_RAMP_TRACK_S        = 0x02A9,
        CONSTRUCTED_RAMP_TRACK_E        = 0x02AA,
        CONSTRUCTED_RAMP_TRACK_W        = 0x02AB,
        CONSTRUCTED_RAMP_TRACK_NS       = 0x02AC,
        CONSTRUCTED_RAMP_TRACK_NE       = 0x02AD,
        CONSTRUCTED_RAMP_TRACK_NW       = 0x02AE,
        CONSTRUCTED_RAMP_TRACK_SE       = 0x02AF,
        CONSTRUCTED_RAMP_TRACK_SW       = 0x02B0,
        CONSTRUCTED_RAMP_TRACK_EW       = 0x02B1,
        CONSTRUCTED_RAMP_TRACK_NSE      = 0x02B2,
        CONSTRUCTED_RAMP_TRACK_NSW      = 0x02B3,
        CONSTRUCTED_RAMP_TRACK_NEW      = 0x02B4,
        CONSTRUCTED_RAMP_TRACK_SEW      = 0x02B5,
        CONSTRUCTED_RAMP_TRACK_NSEW     = 0x02B6
    }
    public class Tile
    {
        public int CurrentTemperature = 10043; // Approx. 75 deg F
        public State CurrentState = State.SOLID;
        public Material Material;
        public bool Outside = false;
        public bool Light = false;
        public bool AboveGround = false;
        public bool Revealed = false;
        public TileType Type = TileType.EMPTY;
        public Vector3 MapPosition;

        public Tile(Material material, TileType type, Vector3 mapPosition)
        {
            Material = material;
            Type = type;
            MapPosition = mapPosition;
            if (material == null) return;
            if (Material.IntProperties["MAT_FIXED_TEMP"] != 60001)
                CurrentTemperature = Material.IntProperties["MAT_FIXED_TEMP"];
        }

        public string GetNameBasedOnState(bool plural, bool dependsOnReveal)
        {
            if (Material == null) return "Open Space";
            //return dependsOnReveal && !Revealed ? "???" : Material.GetNameFromState(CurrentState, plural);
            return Type.ToString();
        }

        public void UpdateTile(GameTime gameTime)
        {
            if (!Revealed || Material == null) return;
            if (CurrentState == State.SOLID && 
                Material.IntProperties["MELTING_POINT"] != 60001 &&
                CurrentTemperature >= Material.IntProperties["MELTING_POINT"])
            {
                CurrentState = State.LIQUID;
            }
            //if(CurrentState == State.LIQUID && Material.IntProperties["BOILING_POINT"])
        }

        public void RenderTile(SpriteBatch spriteBatch, Texture2D font, Vector2 whereToRender)
        {
            //if (!Revealed) return;
            var renderChar = Material == null ? 'N' : Material.Tile;
            var renderColor = Material == null ? new ColorPair(ColorManager.Red, ColorManager.Blue) : Material.DisplayColor; 
            switch (Type)
            {
                case TileType.ROUGH_LAYER_STONE_WALL:
                case TileType.ROUGH_MINERAL_WALL:
                    if(Material == null) throw new Exception("ROUGH_*_WALL needs a Material!");
                    renderChar = Material.Tile;
                    renderColor = Material.DisplayColor; 
                    break;
                case TileType.SMOOTH_LAYER_STONE_FLOOR:
                case TileType.SMOOTH_OBSIDIAN_FLOOR:
                case TileType.SMOOTH_MAP_FEATURE_FLOOR:
                case TileType.SMOOTH_MINERAL_FLOOR:
                case TileType.SMOOTH_ICE_FLOOR:
                {
                    renderChar = '+';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.LAYER_STONE_PILLAR:
                case TileType.OBSIDIAN_PILLAR:
                case TileType.MAP_FEATURE_PILLAR:
                case TileType.MINERAL_PILLAR:
                case TileType.ICE_PILLAR:
                {

                    renderChar = 'O';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.DOWN_RAMP:
                {
                    renderChar = '▼';
                    renderColor.Background = ColorManager.Black;
                    renderColor.Foreground = (int) MapPosition.Z < DwarfFortress.MapDepth - 1 &&
                                             DwarfFortress.World.MapTiles[
                                                 (int) MapPosition.X, (int) MapPosition.Y, (int) MapPosition.Z + 1]
                                                 .Material != null
                        ? DwarfFortress.World.MapTiles[(int)MapPosition.X, (int)MapPosition.Y, (int)MapPosition.Z + 1]
                            .Material
                            .DisplayColor.Foreground
                        : ColorManager.DarkGrey;
                    break;
                }
                case TileType.BURNING_GRASS:
                {
                    renderChar = '.';
                    renderColor.Background = ColorManager.Black;
                    var color = DwarfFortress.Random.Next(100);
                    renderColor.Foreground = color <= 50 ? ColorManager.LightRed : ColorManager.Yellow;
                    break;
                }
                case TileType.BURNING_TREE_TRUNK:
                case TileType.BURNING_TREE_CAP_WALL:
                {
                    renderChar = '‼';
                    renderColor.Background = ColorManager.Black;
                    var color = DwarfFortress.Random.Next(100);
                    renderColor.Foreground = color <= 50 ? ColorManager.LightRed : ColorManager.Yellow;
                    break;
                }
                case TileType.UNDERWORLD_GATE_UPDOWN:
                {
                    renderChar = 'X';
                    renderColor.Background = ColorManager.Magenta;
                    renderColor.Foreground = ColorManager.LightMagenta;
                    break;
                }
                case TileType.UNDERWORLD_GATE_DOWN:
                {
                    renderChar = '>';
                    renderColor.Background = ColorManager.Magenta;
                    renderColor.Foreground = ColorManager.LightMagenta;
                    break;
                }
                case TileType.UNDERWORLD_GATE_UP:
                {
                    renderChar = '<';
                    renderColor.Background = ColorManager.Magenta;
                    renderColor.Foreground = ColorManager.LightMagenta;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_NE_S:
                case TileType.SMOOTH_MAP_FEATURE_WALL_NE_S:
                case TileType.SMOOTH_LAYER_STONE_WALL_NE_S:
                {
                    renderChar = '╓';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_ES_S:
                case TileType.SMOOTH_MAP_FEATURE_WALL_ES_S:
                case TileType.SMOOTH_LAYER_STONE_WALL_ES_S:
                {
                    renderChar = '╒';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_EN_S:
                case TileType.SMOOTH_MAP_FEATURE_WALL_EN_S:
                case TileType.SMOOTH_LAYER_STONE_WALL_EN_S:
                {
                    renderChar = '╘';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_SE_S:
                case TileType.SMOOTH_MAP_FEATURE_WALL_SE_S:
                case TileType.SMOOTH_LAYER_STONE_WALL_SE_S:
                {
                    renderChar = '╙';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_WN_S:
                case TileType.SMOOTH_MAP_FEATURE_WALL_WN_S:
                case TileType.SMOOTH_LAYER_STONE_WALL_WN_S:
                {
                    renderChar = '╛';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_SW_S:
                case TileType.SMOOTH_MAP_FEATURE_WALL_SW_S:
                case TileType.SMOOTH_LAYER_STONE_WALL_SW_S:
                {
                    renderChar = '╜';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_WS_S:
                case TileType.SMOOTH_MAP_FEATURE_WALL_WS_S:
                case TileType.SMOOTH_LAYER_STONE_WALL_WS_S:
                {
                    renderChar = '╕';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_NW_S:
                case TileType.SMOOTH_MAP_FEATURE_WALL_NW_S:
                case TileType.SMOOTH_LAYER_STONE_WALL_NW_S:
                {
                    renderChar = '╖';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_NSEW:
                case TileType.SMOOTH_MAP_FEATURE_WALL_NSEW:
                case TileType.SMOOTH_LAYER_STONE_WALL_NSEW:
                {
                    renderChar = '╬';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_NSE:
                case TileType.SMOOTH_MAP_FEATURE_WALL_NSE:
                case TileType.SMOOTH_LAYER_STONE_WALL_NSE:
                {
                    renderChar = '╠';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_SEW:
                case TileType.SMOOTH_MAP_FEATURE_WALL_SEW:
                case TileType.SMOOTH_LAYER_STONE_WALL_SEW:
                {
                    renderChar = '╦';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_NEW:
                case TileType.SMOOTH_MAP_FEATURE_WALL_NEW:
                case TileType.SMOOTH_LAYER_STONE_WALL_NEW:
                {
                    renderChar = '╩';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_NSW:
                case TileType.SMOOTH_MAP_FEATURE_WALL_NSW:
                case TileType.SMOOTH_LAYER_STONE_WALL_NSW:
                {
                    renderChar = '╣';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_SE:
                case TileType.SMOOTH_MAP_FEATURE_WALL_SE:
                case TileType.SMOOTH_LAYER_STONE_WALL_SE:
                {
                    renderChar = '╔';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_NE:
                case TileType.SMOOTH_MAP_FEATURE_WALL_NE:
                case TileType.SMOOTH_LAYER_STONE_WALL_NE:
                {
                    renderChar = '╚';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_NW:
                case TileType.SMOOTH_MAP_FEATURE_WALL_NW:
                case TileType.SMOOTH_LAYER_STONE_WALL_NW:
                {
                    renderChar = '╝';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_SW:
                case TileType.SMOOTH_MAP_FEATURE_WALL_SW:
                case TileType.SMOOTH_LAYER_STONE_WALL_SW:
                {
                    renderChar = '╗';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_NS:
                case TileType.SMOOTH_MAP_FEATURE_WALL_NS:
                case TileType.SMOOTH_LAYER_STONE_WALL_NS:
                {
                    renderChar = '║';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.SMOOTH_OBSIDIAN_WALL_EW:
                case TileType.SMOOTH_MAP_FEATURE_WALL_EW:
                case TileType.SMOOTH_LAYER_STONE_WALL_EW:
                {
                    renderChar = '═';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.ROUGH_LAYER_STONE_FLOOR_1:
                case TileType.ROUGH_OBSIDIAN_FLOOR_1:
                case TileType.ROUGH_MAP_FEATURE_FLOOR_1:
                case TileType.DARK_GRASS_FLOOR_1:
                case TileType.BRIGHT_GRASS_FLOOR_1:
                {
                    renderChar = '\'';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.ROUGH_LAYER_STONE_FLOOR_2:
                case TileType.ROUGH_OBSIDIAN_FLOOR_2:
                case TileType.ROUGH_MAP_FEATURE_FLOOR_2:
                case TileType.DARK_GRASS_FLOOR_2:
                case TileType.BRIGHT_GRASS_FLOOR_2:
                {
                    renderChar = ',';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.ROUGH_LAYER_STONE_FLOOR_3:
                case TileType.ROUGH_OBSIDIAN_FLOOR_3:
                case TileType.ROUGH_MAP_FEATURE_FLOOR_3:
                case TileType.DARK_GRASS_FLOOR_3:
                case TileType.BRIGHT_GRASS_FLOOR_3:
                {
                    renderChar = '`';
                    renderColor.Background = ColorManager.Black;
                    break;
                }
                case TileType.ROUGH_LAYER_STONE_FLOOR_4:
                case TileType.ROUGH_OBSIDIAN_FLOOR_4:
                case TileType.ROUGH_MAP_FEATURE_FLOOR_4:
                case TileType.DARK_GRASS_FLOOR_4:
                case TileType.BRIGHT_GRASS_FLOOR_4:
                {
                    renderChar = '.';
                    renderColor.Background = ColorManager.Black;
                    break;
                }  
                case TileType.EMPTY:
                {
                    renderChar = (int) MapPosition.Z < DwarfFortress.MapDepth - 1 &&
                                 DwarfFortress.World.MapTiles[
                                     (int) MapPosition.X, (int) MapPosition.Y, (int) MapPosition.Z + 1]
                                     .Material != null
                        ? '∙'
                        : '▓';
                    renderColor.Background = ColorManager.Black;
                    renderColor.Foreground = (int)MapPosition.Z < DwarfFortress.MapDepth - 1 &&
                                             DwarfFortress.World.MapTiles[
                                                 (int)MapPosition.X, (int)MapPosition.Y, (int)MapPosition.Z + 1]
                                                 .Material != null
                        ? DwarfFortress.World.MapTiles[(int)MapPosition.X, (int)MapPosition.Y, (int)MapPosition.Z + 1]
                            .Material
                            .DisplayColor.Foreground
                        : ColorManager.Cyan;
                    break;
                }
            }
            DwarfFortress.FontManager.DrawCharacter(renderChar, spriteBatch, font, whereToRender, renderColor);
        }
    }
}
