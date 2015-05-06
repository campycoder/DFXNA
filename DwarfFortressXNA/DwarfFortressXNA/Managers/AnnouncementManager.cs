using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DwarfFortressXNA.Managers
{
    public class Announcement
    {
        public AnnouncementType Type;
        public string Text;
        public Color Color;
        public bool Box;
        public bool Recenter;
        public bool Pause;
        public bool Fortress;
        public bool Adventure;
        public bool Report;
        public bool ReportActive;

        public Announcement(AnnouncementType type, string text, Color color, bool box, bool recenter, bool pause,
            bool fortress, bool adventure, bool report, bool reportActive)
        {
            Type = type;
            Text = text;
            Color = color;
            Box = box;
            Recenter = recenter;
            Pause = pause;
            Fortress = fortress;
            Adventure = adventure;
            Report = report;
            ReportActive = reportActive;
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
        public List<KeyValuePair<AnnouncementType, List<string>>> AnnouncementBuffer;
        public int AnnouncementTimer = DwarfFortress.FrameLimit*3;
        public int NumberBuffered = 0;

        public void Update()
        {
            if (NumberBuffered != 0)
            {
                AnnouncementTimer--;
            }
            if (AnnouncementTimer != 0) return;
            NumberBuffered--;
            if (NumberBuffered != 0 &&
                AnnouncementTextList[AnnouncementBuffer[AnnouncementBuffer.Count - NumberBuffered].Key].Box)
            {
                DwarfFortress.BoxLocked = true;
            }
            AnnouncementTimer = DwarfFortress.FrameLimit*3;
        }

        public void Render(SpriteBatch spriteBatch, Texture2D font)
        {
            if (NumberBuffered != 0)
            {
                RenderAnnouncement(AnnouncementBuffer[AnnouncementBuffer.Count - NumberBuffered].Key, AnnouncementBuffer[AnnouncementBuffer.Count - NumberBuffered].Value, spriteBatch, font);
            }
            var i = 0;
            foreach (KeyValuePair<AnnouncementType, List<string>> pair in AnnouncementBuffer)
            {
                DwarfFortress.FontManager.DrawString(ConstructAnnouncement(pair.Key,pair.Value), spriteBatch, font, new Vector2(1, i+1), new ColorPair(AnnouncementTextList[pair.Key].Color, ColorManager.Black));
                i++;
            }
        }

        public AnnouncementManager()
        {
            AnnouncementTextList = new Dictionary<AnnouncementType, Announcement>
            {
                {
                    AnnouncementType.REACHED_PEAK,
                    new Announcement(AnnouncementType.REACHED_PEAK, "I have no clue!", ColorManager.LightRed, true,
                        false, false, true, true, false, false)
                },
                {
                    AnnouncementType.ERA_CHANGE,
                    new Announcement(AnnouncementType.ERA_CHANGE, "The world has passed into {0}", ColorManager.White, true, false, true, true, true, false, false)
                },
                {
                    AnnouncementType.ENDGAME_EVENT_1, 
                    new Announcement(AnnouncementType.ENDGAME_EVENT_1, "You have discovered an eerie cavern. The air above the dark stone floor is alive with vorticies of purple light and dark, boiling clouds. Seemingly bottomless glowing pits mark the surface", ColorManager.White, true, true, true, true, true, false, false)
                },
                {
                    AnnouncementType.ENDGAME_EVENT_2,
                    new Announcement(AnnouncementType.ENDGAME_EVENT_2, "Horrors! Demons in the deep!", ColorManager.Red, true, true , true, true, true, false, false)
                },
                {
                    AnnouncementType.FEATURE_DISCOVERY,
                    new Announcement(AnnouncementType.FEATURE_DISCOVERY, "You have discovered a {0}",ColorManager.White, true, true, true, true, true, false, false)
                },
                {
                    AnnouncementType.STRUCK_DEEP_METAL, 
                    new Announcement(AnnouncementType.STRUCK_DEEP_METAL, "{0}! Praise the miners!", ColorManager.LightCyan, true, true, true, true, true, false, false)
                },
                {
                    AnnouncementType.STRUCK_MINERAL,
                    new Announcement(AnnouncementType.STRUCK_MINERAL, "You have struck {0}!", ColorManager.Yellow, false, false, false, true, true, false, false)
                },
                {
                    AnnouncementType.STRUCK_ECONOMIC_MINERAL,
                    new Announcement(AnnouncementType.STRUCK_ECONOMIC_MINERAL, "You have struck {0}!", ColorManager.Yellow, false, false, false, true, true, false, false)
                },
                {
                    AnnouncementType.MIGRANT_ARRIVAL,
                    new Announcement(AnnouncementType.MIGRANT_ARRIVAL, "Some migrants have arrived.", ColorManager.DarkGrey, false, false, false, true, true, false, false)
                },
                {
                    AnnouncementType.WEATHER_BECOMES_CLEAR,
                    new Announcement(AnnouncementType.WEATHER_BECOMES_CLEAR, "The weather has cleared.", ColorManager.DarkGrey, false, false, false, true, true, false, false)
                },
                {
                    AnnouncementType.WEATHER_BECOMES_RAIN,
                    new Announcement(AnnouncementType.WEATHER_BECOMES_RAIN, "It is raining.", ColorManager.Blue, false, false, false, true, true, false, false)
                },
                {
                    AnnouncementType.WEATHER_BECOMES_SNOW,
                    new Announcement(AnnouncementType.WEATHER_BECOMES_SNOW, "A snow storm has come.", ColorManager.White, false, false, false, true, true, false, false)
                },
                {
                    AnnouncementType.STRANGE_CLOUD,
                    new Announcement(AnnouncementType.STRANGE_CLOUD, "A cloud of {0} has drifted nearby!", ColorManager.Red, false, false, false, true, true, false ,false)
                },
                {
                    AnnouncementType.STRANGE_MOOD_FEY,
                    new Announcement(AnnouncementType.STRANGE_MOOD_FEY, "{0} is taken by a fey mood!", ColorManager.White, false, true, true, true, true, false, false)
                },
                {
                    AnnouncementType.STRANGE_MOOD_SECRET, 
                    new Announcement(AnnouncementType.STRANGE_MOOD_SECRET, "{0} withdraws from society...", ColorManager.DarkGrey, false, true, true, true, true, false, false)
                },
                {
                    AnnouncementType.STRANGE_MOOD_POSSESED,
                    new Announcement(AnnouncementType.STRANGE_MOOD_POSSESED, "{0} has been possessed!", ColorManager.LightMagenta, false, true, true, true, true, false, false)
                },
                {
                    AnnouncementType.STRANGE_MOOD_FELL, 
                    new Announcement(AnnouncementType.STRANGE_MOOD_FELL, "{0} looses a roaring laughter, fell and terrible!", ColorManager.Magenta, false, true, true, true, true, false, false)
                },
                {
                    AnnouncementType.STRANGE_MOOD_MACABRE,
                    new Announcement(AnnouncementType.STRANGE_MOOD_MACABRE, "{0} begins to stalk and brood...", ColorManager.DarkGrey, false, true, true, true, true, false, false)
                }
            };
            AnnouncementBuffer = new List<KeyValuePair<AnnouncementType, List<string>>>();
        }

        public string ConstructAnnouncement(AnnouncementType announcementType, List<string> arguments)
        {
            // ReSharper disable once CoVariantArrayConversion
            return String.Format(AnnouncementTextList[announcementType].Text, arguments.ToArray());
        }

        public void RenderAnnouncement(AnnouncementType announcementType, List<string> arguments,
            SpriteBatch spriteBatch, Texture2D font)
        {
            var finalText = ConstructAnnouncement(announcementType, arguments);
            if (!Char.IsPunctuation(finalText[finalText.Length - 1])) finalText += '.';
            var extraText = "";
            if(!AnnouncementTextList[announcementType].Box)
            {
                if (finalText.Length/2 > (DwarfFortress.Cols - 2)/2)
                {
                    extraText = finalText.Substring(((DwarfFortress.Cols - 2) / 2));
                    finalText = finalText.Remove(((DwarfFortress.Cols - 2) / 2));
                    DwarfFortress.FontManager.DrawString(finalText, spriteBatch, font, new Vector2((DwarfFortress.Cols / 2) - (finalText.Length / 2), DwarfFortress.Rows - 1), new ColorPair(AnnouncementTextList[announcementType].Color, ColorManager.Black));
                }
                else DwarfFortress.FontManager.DrawString(finalText, spriteBatch, font, new Vector2((DwarfFortress.Cols / 2) - (finalText.Length / 2), DwarfFortress.Rows - 1), new ColorPair(AnnouncementTextList[announcementType].Color, ColorManager.Black));
            }
            if (NumberBuffered > 1) DwarfFortress.FontManager.DrawString(NumberBuffered.ToString(CultureInfo.InvariantCulture), spriteBatch, font, new Vector2((DwarfFortress.Cols/8), DwarfFortress.Rows - 1), new ColorPair(ColorManager.Black, ColorManager.LightGrey));
            if (AnnouncementTextList[announcementType].Box && DwarfFortress.BoxLocked)
            {
                DwarfFortress.FontManager.DrawBoxedText(finalText+extraText, spriteBatch, font, new Vector2((DwarfFortress.Cols / 2) - (40 / 2) - 6, 4), new Vector2(53, 3 + (int)Math.Floor(finalText.Length/53d)), new ColorPair(AnnouncementTextList[announcementType].Color, ColorManager.Black));
            }
        }

        public void AnnouncementEvent(AnnouncementType announcementType, List<string> arguments)
        {
            // ReSharper disable once CoVariantArrayConversion
            AnnouncementBuffer.Add(new KeyValuePair<AnnouncementType, List<string>>(announcementType, arguments));
            if (AnnouncementTextList[announcementType].Pause) DwarfFortress.Paused = true;
            if (AnnouncementTextList[announcementType].Box && NumberBuffered == 0)
            {
                DwarfFortress.BoxLocked = true;
                AnnouncementTimer = DwarfFortress.FrameLimit*3;
            }
            NumberBuffered++;
        }
    }
}
