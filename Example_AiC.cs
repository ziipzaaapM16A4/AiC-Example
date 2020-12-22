//+--------------------------------------------+
//| Author: ziipzaaapM16A4                     |
//|                                            |
//+--------------------------------------------+

using System;
using Rage;
using AmbientAICallouts.API;

namespace Example_AiC
{
    //Its very important to combine every created object with these below unless the AiCallout will not be compatible with the player callouts and cant be cleaned up correctly in case of an error.
    ///String SceneInfo
    ///Vector3 location
    ///String callSign
    ///String calloutDetailsString
    ///Voicelines rndVl
    ///Vehicle unit
    ///List<Ped> unitOfficers
    ///List<Ped> suspects
    ///List<Vehicle> suspectsVehicle

    //Please use try catch blocks to get an error message when chrashing.
    public class Example_AiC : AiCallout
    {
        public override bool Setup()
        {
            //Code for setting the scene. return true when Succesfull. 
            //Important: Please set a calloutDetailsString and SceneInfo to ensure that your callout has a something a civilian can report.
            //Example idea: Place a Damaged Vehicle infront of a Pole and place a swearing ped nearby.
            try
            {
                SceneInfo = "Example AiCallout";
                location = World.GetNextPositionOnStreet(Unit.Position.Around2D(Functions.minimumAiCalloutDistance, Functions.maximumAiCalloutDistance));
                calloutDetailsString = "EMERGENCY_CALL";
                return true;
            }
            catch (Exception e)
            {
                LogTrivial_withAiC("ERROR: in AICallout object: At Setup(): " + e);
                return false;
            }
        }

        public override bool Process()
        {
            //Code for processing the the scene. return true when Succesfull.
            //Example idea: Cops arrive; Getting out; Starring at suspects; End();
            try
            {
                if (!IsUnitInTime(100f, 130))  //if vehicle is never reaching its location
                {
                    Disregard();
                }
                else  //if vehicle is reaching its location
                {

                    GameFiber.WaitWhile(() => Unit.Position.DistanceTo(location) >= 40f, 0);
                    Unit.IsSirenSilent = true;
                    Unit.TopSpeed = 12f;

                    GameFiber.SleepUntil(() => location.DistanceTo(Unit.Position) < arrivalDistanceThreshold + 5f /* && Unit.Speed <= 1*/, 30000);
                    OfficersLeaveVehicle(true);

                    if (playerRespondingInAdditon) //if the player responds as a additional unit to the AiCallout
                    {
                        // doStuff 
                        // when player is also responding
                    }
                    else //if the player is not responding as additional
                    { 
                        if (IsAiTakingCare()) //do something without calling the player for backup
                        {
                            //doStuff
                        }
                        else //do something and call the player for backup
                        {
                        
                            //decision tree what callout shall get triggered
                            switch (new Random().Next(0, 5))
                            {
                                case 0:
                                    UnitCallsForBackup("AAIC-OfficerDown");
                                    break;
                                case 1:
                                    UnitCallsForBackup("AAIC-OfficerInPursuit");
                                    break;
                                default:
                                    UnitCallsForBackup("AAIC-OfficerRequiringAssistance");
                                    break;
                            }                    
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                LogTrivial_withAiC("ERROR: in AICallout object: At Process(): " + e);
                return false;
            }
        }

        public override bool End()
        {
            //Code that finishes the the scene. Return true when Succesfull.
            //Example idea: Cops getting back into their vehicle. drive away dismiss the rest. after 90 secconds delete if possible entitys that have not moved away.
            try
            {
                //doStuff
                return true;
            }
            catch (Exception e)
            {
                LogTrivial_withAiC("ERROR: in AICallout object: At End(): " + e);
                return false;
            }
        }

    }
}