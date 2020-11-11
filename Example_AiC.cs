//+--------------------------------------------+
//| Author: ziipzaaapM16A4                     |
//|                                            |
//+--------------------------------------------+

using System;
using Rage;
using AmbientAICallouts.API;

namespace Example_AiC
{
    //Its very important to use these unless the AiCallout will not be compatible with the player callouts
    ///Vector3 location;
    ///String callSign;
    ///String calloutDetailsString;
    ///Voicelines rndVl;
    ///Vehicle unit;
    ///List<Ped> unitOfficers
    ///List<Ped> suspects
    ///List<Vehicle> suspectsVehicle

    //Please use try catch blocks to get an error message when chrashing.
    ///For almost every API Function you will need an so called "functions Object". It knows to current managerObject which contains most of the features in AiCalloutManager. The object name is always "fO"
    //-------------------------->>> Important: Always use fO when calling functions to ensure you are using the right managerObject <<<-------------------------

    public class Example_AiC : AiCallout
    {
        public override bool Setup()
        {
            //Code for setting the scene. return true when Succesfull. 
            //Important: please set a calloutDetailsString with Set_AiCallout_calloutDetailsString(String calloutDetailsString) to ensure that your callout has a something a civilian can report.
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
                    Unit.Driver.Tasks.PerformDrivingManeuver(VehicleManeuver.Wait);
                    GameFiber.SleepUntil(() => Unit.Speed <= 1, 5000);
                    OfficersLeaveVehicle(true);

                    if (IsAiTakingCare()) //do something without involving the player.
                    {
                        //doStuff
                    }
                    else //do something and involve the player. 
                    {
                        
                        //decision maker what callout shall triggered
                        switch (new Random().Next(0, 5))                                                                                                           //FOR VIDEO EDITING
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
            //Code for finishing the the scene. return true when Succesfull.
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