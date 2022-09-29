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
            //IMPORTANT: objects like --> SceneInfo, location, calloutDetailsString, responseType, arrivalDistanceThreshold
            //needs to be set in the Setup() function. If not Values will not be used correctly. 
            //responseType, arrivalDistanceThreshold dont need to be set. they got Default Values. 
            //Example idea: Place a Damaged Vehicle infront of a Pole and place a swearing ped nearby.
            try
            {
                SceneInfo = "Example AiCallout";                                        //What happend?
                CalloutDetailsString = "EMERGENCY_CALL";                                //What happend as scanner audio file
                ResponseType = EResponseType.Code3;   //Code 3 - lights and siren, Code 2 - normal response 
                Vector3 proposedPosition = Game.LocalPlayer.Character.Position.Around2D(AmbientAICallouts.API.Functions.minimumAiCalloutDistance + 15f, AmbientAICallouts.API.Functions.maximumAiCalloutDistance - 15f);
                bool posFound = false;
                int trys = 0;
                bool demandPavement = true;
                while (!posFound)
                {
                    proposedPosition = Game.LocalPlayer.Character.Position.Around2D(AmbientAICallouts.API.Functions.minimumAiCalloutDistance + 15f, AmbientAICallouts.API.Functions.maximumAiCalloutDistance - 15f);
                    Rage.Native.NativeFunction.Natives.GET_SAFE_COORD_FOR_PED<bool>(proposedPosition, demandPavement, out proposedPosition, 16);  //Finding a Place on the pavement
                    Location = proposedPosition;


                    if (Functions.IsLocationAcceptedBySystem(Location))
                        posFound = true;

                    trys++;
                    if (trys == 30) demandPavement = false;
                    if (trys >= 60) { LogTrivial_withAiC("ERROR: in AICallout object: At Setup(): unable to find safe coords for this event"); return false; }
                }
                return true;
            }
            catch (System.Threading.ThreadAbortException) { return false; }
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
                if (!IsUnitInTime(Units[0], 100f, 130))  //if vehicle is never reaching its location
                {
                    Disregard();
                }
                else  //if vehicle is reaching its location
                {

                    GameFiber.WaitWhile(() => Units[0].PoliceVehicle.Position.DistanceTo(Location) >= 40f, 25000); //Slow down the unit to not rush into the scene
                    Units[0].PoliceVehicle.IsSirenSilent = true;
                    Units[0].PoliceVehicle.TopSpeed = 12f;

                    GameFiber.SleepUntil(() => Location.DistanceTo(Units[0].PoliceVehicle.Position) < arrivalDistanceThreshold + 5f /* && Unit.Speed <= 1*/, 30000);
                    OfficersLeaveVehicle(Units[0], true);

                    if (playerRespondingInAdditon) //if the player responds as a additional unit to the AiCallout
                    {
                        // when player is also responding
                    }
                    else //if the player is not responding as additional
                    { 
                        if (IsAiTakingCare()) //do we escalate into an PlayerCallout or let the Ai handle it themself
                        {
                            //do something without calling the player for backup
                        }
                        else //do something and call the player for backup
                        {
                            switch (new Random().Next(0, 5))  //decision tree what callout shall get triggered
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
            catch (System.Threading.ThreadAbortException) { return false; }
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
            catch (System.Threading.ThreadAbortException) { return false; }
            catch (Exception e)
            {
                LogTrivial_withAiC("ERROR: in AICallout object: At End(): " + e);
                return false;
            }
        }

    }
}