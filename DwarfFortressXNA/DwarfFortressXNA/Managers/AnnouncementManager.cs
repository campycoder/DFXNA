using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DwarfFortressXNA.Managers
{
    public class Announcement
    {
// ReSharper disable InconsistentNaming
        [JsonConverter(typeof(StringEnumConverter))]
        public AnnouncementType announcementType;
        public string text;
        [JsonConverter(typeof(StringEnumConverter))]
        public ColorRaw color;
        public bool box;
        public bool recenter;
        public bool pause;
        public bool fortress;
        public bool adventure;
        public bool report;
        public bool reportActive;
// ReSharper restore InconsistentNaming
    }

    public class AnnouncementInstance
    {
        public AnnouncementType Type;
        public ColorRaw Color;
        public List<string> Lines;
        public int NumberOfLines;
        public string Constructed;
        public List<string> Arguments;
        public Vector2 RecenterPosition;
        public AnnouncementInstance(AnnouncementType type, ColorRaw color, List<string> args, int reX = -1, int reY = -1)
        {
            Type = type;
            Color = color;
            Arguments = args;
            Constructed = DwarfFortress.AnnouncementManager.ConstructAnnouncement(type, args);
            Lines = ReconstructLineArray();
            NumberOfLines = Lines.Count;
            if(reX != -1 && reY != -1) RecenterPosition = new Vector2(reX, reY);
        }

        public List<string> ReconstructLineArray()
        {
            var splitBuffer = new List<string>();
            var constructedAnnouncement = Constructed;
            NumberOfLines = (int)Math.Ceiling((double)constructedAnnouncement.Length / (DwarfFortress.Cols - 2));
            for (var j = 0; j < NumberOfLines; j++)
            {
                splitBuffer.Add(constructedAnnouncement.Substring(j * (DwarfFortress.Cols - 2), j == NumberOfLines - 1 ? (constructedAnnouncement.Length - j * (DwarfFortress.Cols - 2)) : (DwarfFortress.Cols - 2)));
            }
            for (var j = 0; j < NumberOfLines - 1; j++)
            {
                var spaceIndex = splitBuffer[j].LastIndexOf(' ');
                var spaceChop = splitBuffer[j].Substring(spaceIndex);
                splitBuffer[j] = splitBuffer[j].Remove(spaceIndex);
                splitBuffer[j + 1] = spaceChop.Replace(" ", "") + splitBuffer[j + 1];
            }
            return splitBuffer;
        }
    }

    public enum AnnouncementType
    {
        NULL,
        REACHED_PEAK,
        ERA_CHANGE,
        ENDGAME_EVENT_1,
        ENDGAME_EVENT_2,
        FEATURE_DISCOVERY,
        STRUCK_DEEP_METAL,
        STRUCK_MINERAL,
        STRUCK_ECONOMIC_MINERAL,
        COMBAT_TWIST_WEAPON,
        COMBAT_LET_ITEM_DROP,
        COMBAT_START_CHARGE,
        COMBAT_SURPRISE_CHARGE,
        COMBAT_JUMP_DODGE_PROJ,
        COMBAT_JUMP_DODGE_STRIKE,
        COMBAT_DODGE,
        COMBAT_COUNTERSTRIKE,
        COMBAT_BLOCK,
        COMBAT_PARRY,
        COMBAT_CHARGE_COLLISION,
        COMBAT_CHARGE_DEFENDER_TUMBLES,
        COMBAT_CHARGE_DEFENDER_KNOCKED_OVER,
        COMBAT_CHARGE_ATTACKER_TUMBLES,
        COMBAT_CHARGE_ATTACKER_BOUNCE_BACK,
        COMBAT_CHARGE_TANGLE_TOGETHER,
        COMBAT_CHARGE_TANGLE_TUMBLE,
        COMBAT_CHARGE_RUSH_BY,
        COMBAT_CHARGE_MANAGE_STOP,
        COMBAT_CHARGE_OBSTACLE_SLAM,
        COMBAT_WRESTLE_LOCK,
        COMBAT_WRESTLE_CHOKEHOLD,
        COMBAT_WRESTLE_TAKEDOWN,
        COMBAT_WRESTLE_THROW,
        COMBAT_WRESTLE_RELEASE_LOCK,
        COMBAT_WRESTLE_RELEASE_CHOKE,
        COMBAT_WRESTLE_RELEASE_GRIP,
        COMBAT_WRESTLE_STRUGGLE,
        COMBAT_WRESTLE_RELEASE_LATCH,
        COMBAT_WRESTLE_STRANGLE_KO,
        COMBAT_WRESTLE_ADJUST_GRIP,
        COMBAT_GRAB_TEAR,
        COMBAT_STRIKE_DETAILS,
        COMBAT_STRIKE_DETAILS_2,
        COMBAT_EVENT_ENRAGED,
        COMBAT_EVENT_STUCKIN,
        COMBAT_EVENT_LATCH_BP,
        COMBAT_EVENT_LATCH_GENERAL,
        COMBAT_EVENT_PROPELLED_AWAY,
        COMBAT_EVENT_KNOCKED_OUT,
        COMBAT_EVENT_STUNNED,
        COMBAT_EVENT_WINDED,
        COMBAT_EVENT_NAUSEATED,
        MIGRANT_ARRIVAL_NAMED,
        MIGRANT_ARRIVAL,
        DIG_CANCEL_WARM,
        DIG_CANCEL_DAMP,
        AMBUSH_DEFENDER,
        AMBUSH_RESIDENT,
        AMBUSH_THIEF,
        AMBUSH_THIEF_SUPPORT_SKULKING,
        AMBUSH_THIEF_SUPPORT_NATURE,
        AMBUSH_THIEF_SUPPORT,
        AMBUSH_MISCHIEVOUS,
        AMBUSH_SNATCHER,
        AMBUSH_SNATCHER_SUPPORT,
        AMBUSH_AMBUSHER_NATURE,
        AMBUSH_AMBUSHER,
        AMBUSH_INJURED,
        AMBUSH_OTHER,
        AMBUSH_INCAPACITATED,
        CARAVAN_ARRIVAL,
        NOBLE_ARRIVAL,
        D_MIGRANTS_ARRIVAL,
        D_MIGRANT_ARRIVAL,
        D_MIGRANT_ARRIVAL_DISCOURAGED,
        D_NO_MIGRANT_ARRIVAL,
        ANIMAL_TRAP_CATCH,
        ANIMAL_TRAP_ROBBED,
        MISCHIEF_LEVER,
        MISCHIEF_PLATE,
        MISCHIEF_CAGE,
        MISCHIEF_CHAIN,
        DIPLOMAT_ARRIVAL,
        LIAISON_ARRIVAL,
        TRADE_DIPLOMAT_ARRIVAL,
        CAVE_COLLAPSE,
        BIRTH_CITIZEN,
        BIRTH_ANIMAL,
        BIRTH_WILD_ANIMAL,
        //Regular strange mood token was reliant
        //upon multiple messages and colours -
        //the first wouldn't have been a problem
        //but the second was something I was far
        //too lazy to implement.
        //So, multiple announcement tokens for
        //strange moods.
        STRANGE_MOOD_FEY,
        STRANGE_MOOD_SECRET,
        STRANGE_MOOD_POSSESED,
        STRANGE_MOOD_FELL,
        STRANGE_MOOD_MACABRE,
        MADE_ARTIFACT,
        NAMED_ARTIFACT,
        ITEM_ATTACHMENT,
        VERMIN_CAGE_ESCAPE,
        TRIGGER_WEB,
        MOOD_BUILDING_CLAIMED,
        ARTIFACT_BEGUN,
        MEGABEAST_ARRIVAL,
        BERSERK_CITIZEN,
        MAGMA_DEFACES_ENGRAVING,
        ENGRAVING_MELTS,
        MASTERPIECE_ARCHITECTURE,
        MASTERPIECE_CONSTRUCTION,
        MASTER_ARCHITECTURE_LOST,
        MASTER_CONSTRUCTION_LOST,
        ADV_AWAKEN,
        ADV_SLEEP_INTERRUPTED,
        ADV_REACTION_PRODUCTS,
        CANCEL_JOB,
        ADV_CREATURE_DEATH,
        CITIZEN_DEATH,
        PET_DEATH,
        FALL_OVER,
        CAUGHT_IN_FLAMES,
        CAUGHT_IN_WEB,
        UNIT_PROJECTILE_SLAM_BLOW_APART,
        UNIT_PROJECTILE_SLAM,
        UNIT_PROJECTILE_SLAM_INTO_UNIT,
        VOMIT,
        LOSE_HOLD_OF_ITEM,
        REGAIN_CONSCIOUSNESS,
        FREE_FROM_WEB,
        PARALYZED,
        OVERCOME_PARALYSIS,
        NOT_STUNNED,
        EXHAUSTION,
        PAIN_KO,
        BREAK_GRIP,
        NO_BREAK_GRIP,
        BLOCK_FIRE,
        BREATHE_FIRE,
        SHOOT_WEB,
        PULL_OUT_DROP,
        STAND_UP,
        MARTIAL_TRANCE,
        MAT_BREATH,
        NIGHT_ATTACK_STARTS,
        NIGHT_ATTACK_ENDS,
        NIGHT_ATTACK_TRAVEL,
        GHOST_ATTACK,
        TRAVEL_SITE_DISCOVERY,
        TRAVEL_SITE_BUMP,
        ADVENTURE_INTRO,
        CREATURE_SOUND,
        MECHANISM_SOUND,
        CREATURE_STEALS_OBJECT,
        FOUND_TRAP,
        BODY_TRANSFORMATION,
        INTERACTION_ACTOR,
        INTERACTION_TARGET,
        UNDEAD_ATTACK,
        CITIZEN_MISSING,
        PET_MISSING,
        STRANGE_RAIN_SNOW,
        STRANGE_CLOUD,
        SIMPLE_ANIMAL_ACTION,
        FLOUNDER_IN_LIQUID,
        TRAINING_DOWN_TO_SEMI_WILD,
        TRAINING_FULL_REVERSION,
        ANIMAL_TRAINING_KNOWLEDGE,
        SKIP_ON_LIQUID,
        DODGE_FLYING_OBJECT,
        REGULAR_CONVERSATION,
        CONFLICT_CONVERSATION,
        FLAME_HIT,
        EMBRACE,
        BANDIT_EMPTY_CONTAINER,
        BANDIT_GRAB_ITEM,
        COMBAT_EVENT_ATTACK_INTERRUPTED,
        COMBAT_WRESTLE_CATCH_ATTACK,
        FAIL_TO_GRAB_SURFACE,
        LOSE_HOLD_OF_SURFACE,
        TRAVEL_COMPLAINT,
        LOSE_EMOTION,
        REORGANIZE_POSSESSIONS,
        PUSH_ITEM,
        DRAW_ITEM,
        STRAP_ITEM,
        GAIN_SITE_CONTROL,
        FORT_POSITION_SUCCESSION,
        STRESSED_CITIZEN,
        CITIZEN_LOST_TO_STRESS,
        CITIZEN_TANTRUM,
        MOVED_OUT_OF_RANGE,
        CANNOT_JUMP,
        NO_TRACKS,
        ALREADY_SEARCHED_AREA,
        SEARCH_FOUND_SOMETHING,
        SEARCH_FOUND_NOTHING,
        NOTHING_TO_INTERACT,
        NOTHING_TO_EXAMINE,
        YOU_YIELDED,
        YOU_UNYIELDED,
        YOU_STRAP_ITEM,
        YOU_DRAW_ITEM,
        NO_GRASP_TO_DRAW_ITEM,
        NO_ITEM_TO_STRAP,
        NO_INV_TO_REMOVE,
        NO_INV_TO_WEAR,
        NO_INV_TO_EAT,
        NO_INV_TO_CONTAIN,
        NO_INV_TO_DROP,
        NOTHING_TO_PICK_UP,
        NO_INV_TO_THROW,
        NO_INV_TO_FIRE,
        CURRENT_SMELL,
        CURRENT_WEATHER,
        CURRENT_TEMPERATURE,
        CURRENT_DATE,
        NO_GRASP_FOR_PICKUP,
        TRAVEL_ADVISORY,
        CANNOT_CLIMB,
        CANNOT_STAND,
        MUST_UNRETRACT_FIRST,
        CANNOT_REST,
        CANNOT_MAKE_CAMPFIRE,
        MADE_CAMPFIRE,
        CANNOT_SET_FIRE,
        SET_FIRE,
        DAWN_BREAKS,
        NOON,
        NIGHTFALL,
        NO_INV_INTERACTION,
        EMPTY_CONTAINER,
        TAKE_OUT_OF_CONTAINER,
        NO_CONTAINER_FOR_ITEM,
        PUT_INTO_CONTAINER,
        EAT_ITEM,
        DRINK_ITEM,
        CONSUME_FAILURE,
        DROP_ITEM,
        PICK_UP_ITEM,
        YOU_BUILDING_INTERACTION,
        YOU_ITEM_INTERACTION,
        YOU_TEMPERATURE_EFFECTS,
        RESOLVE_SHARED_ITEMS,
        COUGH_BLOOD,
        VOMIT_BLOOD,
        POWER_LEARNED,
        YOU_FEED_ON_SUCKEE,
        PROFESSION_CHANGES,
        RECRUIT_PROMOTED,
        SOLDIER_BECOMES_MASTER,
        MERCHANTS_UNLOADING,
        MERCHANTS_NEED_DEPOT,
        MERCHANT_WAGONS_BYPASSED,
        MERCHANTS_LEAVING_SOON,
        MERCHANTS_EMBARKED,
        PET_LOSES_DEAD_OWNER,
        PET_ADOPTS_OWNER,
        VERMIN_BITE,
        UNABLE_TO_COMPLETE_BUILDING,
        JOBS_REMOVED_FROM_UNPOWERED_BUILDING,
        CITIZEN_SNATCHED,
        VERMIN_DISTURBED,
        LAND_GAINS_STATUS,
        LAND_ELEVATED_STATUS,
        MASTERPIECE_CRAFTED,
        ARTWORK_DEFACED,
        ANIMAL_TRAINED,
        DYED_MASTERPIECE,
        COOKED_MASTERPIECE,
        MANDATE_ENDS,
        SLOWDOWN_ENDS,
        FAREWELL_HELPER,
        ELECTION_RESULTS,
        SITE_PRESENT,
        CONSTRUCTION_SUSPENDED,
        LINKAGE_SUSPENDED,
        QUOTA_FILLED,
        JOB_OVERWRITTEN,
        NOTHING_TO_CATCH_IN_WATER,
        DEMAND_FORGOTTEN,
        NEW_DEMAND,
        NEW_MANDATE,
        PRICES_ALTERED,
        NAMED_RESIDENT_CREATURE,
        SOMEBODY_GROWS_UP,
        GUILD_REQUEST_TAKEN,
        GUILD_WAGES_CHANGED,
        NEW_WORK_MANDATE,
        CITIZEN_BECOMES_SOLDIER,
        CITIZEN_BECOMES_NONSOLDIER,
        PARTY_ORGANIZED,
        POSSESSED_TANTRUM,
        BUILDING_TOPPLED_BY_GHOST,
        MASTERFUL_IMPROVEMENT,
        MASTERPIECE_ENGRAVING,
        MARRIAGE,
        NO_MARRIAGE_CELEBRATION,
        CURIOUS_GUZZLER,
        WEATHER_BECOMES_CLEAR,
        WEATHER_BECOMES_SNOW,
        WEATHER_BECOMES_RAIN,
        SEASON_WET,
        SEASON_DRY,
        SEASON_SPRING,
        SEASON_SUMMER,
        SEASON_AUTUMN,
        SEASON_WINTER
    }

    public class AnnouncementManager
    {
        public Dictionary<AnnouncementType, Announcement> AnnouncementTextList;
        public List<AnnouncementInstance> AnnouncementBuffer;
        public int AnnouncementTimer = DwarfFortress.FrameLimit*3;
        public int NumberBuffered = 0;
        public int PageNumber = 0;
        public int NumberOfPages = 1;

        public void Update()
        {
            if (NumberBuffered != 0)
            {
                AnnouncementTimer--;
            }
            if (AnnouncementTimer != 0) return;
            NumberBuffered--;
            if (NumberBuffered != 0 &&
                AnnouncementTextList[AnnouncementBuffer[AnnouncementBuffer.Count - NumberBuffered].Type].box)
            {
                DwarfFortress.BoxLocked = true;
            }
            AnnouncementTimer = DwarfFortress.FrameLimit*3;
        }

        public void Render(SpriteBatch spriteBatch, Texture2D font)
        {
            if (NumberBuffered != 0)
            {
                RenderAnnouncement(AnnouncementBuffer[AnnouncementBuffer.Count - NumberBuffered], spriteBatch, font);
            }
        }

        public void WindowResize()
        {
            foreach (AnnouncementInstance announcement in AnnouncementBuffer)
            {
                announcement.Lines = announcement.ReconstructLineArray();
            }
        }

        public void RenderAnnouncementList(SpriteBatch spriteBatch, Texture2D font)
        {
            var currentScroll = PageNumber*(DwarfFortress.Rows - 2) + 1;
            var currentPosition = 0;
            var renderPosition = 0;
            foreach (var instance in AnnouncementBuffer)
            {
                currentPosition += instance.NumberOfLines;
                if (currentPosition < currentScroll || currentPosition >= currentScroll + DwarfFortress.Rows-2) continue;
                for (var renderOffset = 0;renderOffset < instance.Lines.Count;renderOffset++)
                {
                    DwarfFortress.FontManager.DrawString(instance.Lines[renderOffset], spriteBatch, font, new Vector2(1, renderPosition + renderOffset + 1), new ColorPair(ColorManager.ColorList[(int)instance.Color], ColorManager.Black));
                }
                renderPosition += instance.Lines.Count;
            }
            NumberOfPages = (int) Math.Ceiling((double) currentPosition/(DwarfFortress.Rows - 2));
            if (NumberOfPages == 0) NumberOfPages++;
            if (PageNumber > NumberOfPages) PageNumber = NumberOfPages-1;
            if (PageNumber < 0) PageNumber = 0;
            DwarfFortress.FontManager.DrawString("Page " + (PageNumber + 1) + "/" + NumberOfPages, spriteBatch, font, new Vector2(2, 0), new ColorPair(ColorManager.Black, ColorManager.LightGrey));
        }

        public AnnouncementManager()
        {
            AnnouncementTextList = new Dictionary<AnnouncementType, Announcement>();
            var serializer = new JsonSerializer();
            var announcementList = serializer.Deserialize<List<Announcement>>(
                new JsonTextReader(new StreamReader("./Data/Properties/AnnouncementList.json")));
            foreach (var ann in announcementList)
            {
                AnnouncementTextList.Add(ann.announcementType, ann);
            }
            AnnouncementBuffer = new List<AnnouncementInstance>();
        }

        public void ClearBuffered()
        {
            for (var i = NumberBuffered; i > 0; i--)
            {
                if (AnnouncementTextList[AnnouncementBuffer[AnnouncementBuffer.Count - i].Type].box)
                {
                    NumberBuffered = i;
                    AnnouncementTimer = 300;
                    return;
                }
            }
            NumberBuffered = 0;
            AnnouncementTimer = 300;
        }

        public string ConstructAnnouncement(AnnouncementType announcementType, List<string> arguments)
        {
            // ReSharper disable once CoVariantArrayConversion
            var final = String.Format(AnnouncementTextList[announcementType].text, arguments.ToArray());
            if (!char.IsPunctuation(final.Last())) final += ".";
            return final;
        }

        public void RenderAnnouncement(AnnouncementInstance instance,
            SpriteBatch spriteBatch, Texture2D font)
        {
            var finalText = ConstructAnnouncement(instance.Type, instance.Arguments);
            var extraText = "";
            if (!Char.IsPunctuation(finalText[finalText.Length - 1])) finalText += '.';
            if (!DwarfFortress.BoxLocked)
            {
                if (finalText.Length/2 > (DwarfFortress.Cols - 2)/2)
                {
                    finalText = finalText.Substring((int)(Math.Abs(AnnouncementTimer - 300) * ((finalText.Length - ((DwarfFortress.Cols - 2) / 2)) / 300f)), ((DwarfFortress.Cols - 2) / 2));
                    DwarfFortress.FontManager.DrawString(finalText, spriteBatch, font, new Vector2((DwarfFortress.Cols / 2) - (finalText.Length / 2), DwarfFortress.Rows - 1), new ColorPair(ColorManager.ColorList[(int)instance.Color], ColorManager.Black));
                    /*else
                    {
                        DwarfFortress.FontManager.DrawString(finalText, spriteBatch, font, new Vector2((DwarfFortress.Cols / 2) - (finalText.Length / 2) - 7, DwarfFortress.Rows - 1), new ColorPair(ColorManager.ColorList[(int)instance.Color], ColorManager.Black));
                        DwarfFortress.FontManager.DrawString("[CONT.]", spriteBatch, font, new Vector2((DwarfFortress.Cols / 2) - (finalText.Length / 2) + (finalText.Length-7), DwarfFortress.Rows - 1), new ColorPair(ColorManager.DarkGrey, ColorManager.Black));

                    }*/
                }
                else DwarfFortress.FontManager.DrawString(finalText, spriteBatch, font, new Vector2((DwarfFortress.Cols / 2) - (finalText.Length / 2), DwarfFortress.Rows - 1), new ColorPair(ColorManager.ColorList[(int)instance.Color], ColorManager.Black));
            }
            if (NumberBuffered > 1) DwarfFortress.FontManager.DrawString(NumberBuffered.ToString(CultureInfo.InvariantCulture), spriteBatch, font, new Vector2(1, DwarfFortress.Rows - 1), new ColorPair(ColorManager.Black, ColorManager.LightGrey));
            if (AnnouncementTextList[instance.Type].box && DwarfFortress.BoxLocked)
            {
                DwarfFortress.FontManager.DrawBoxedText(finalText + extraText, spriteBatch, font, new Vector2((DwarfFortress.Cols / 2) - (40 / 2) - 6, 4), new Vector2(53, 3 + (int)Math.Floor(finalText.Length / 53d)), new ColorPair(ColorManager.ColorList[(int)instance.Color], ColorManager.Black));
            }
        }

        public void AnnouncementEvent(AnnouncementType announcementType, List<string> arguments, int recenterX = -1, int recenterY = -1)
        {
            // ReSharper disable once CoVariantArrayConversion
            if (AnnouncementTextList[announcementType].pause) DwarfFortress.Paused = true;
            if (AnnouncementTextList[announcementType].box && NumberBuffered == 0)
            {
                DwarfFortress.BoxLocked = true;
                AnnouncementTimer = DwarfFortress.FrameLimit*3;
            }
            /*if (AnnouncementTextList[announcementType].recenter)
            {
                if(recenterX < 0 || recenterY < 0) throw new Exception("Bad recenter coords: " + recenterX + "/" + recenterY + "!");
            }*/
            var announcement = new AnnouncementInstance(announcementType, AnnouncementTextList[announcementType].color, arguments);
            AnnouncementBuffer.Add(announcement);
            NumberBuffered++;

        }
    }
}
