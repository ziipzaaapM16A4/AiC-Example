


# AmbientAICallouts.API.AiCallout

	/// <summary>Scene Information. Like "Shots fired" or "Dommestic disturbance"</summary>
    public string SceneInfo
	
	/// <summary>The manager object is the handler of an AiCallout.</summary>
    public AiCalloutManager MO

	/// <summary>Setup() will be executed before calling the Unit. Use it to setup the crime scene</summary>
	/// <returns>a bool which inidcates a successful or not successful Setup()</returns>
    public virtual bool Setup()

	/// <summary>Process() will be executed as soon as the unit responds to the call. you have full access to all given objects but its recomended to wait until the unit is nearby the scene.</summary>
	/// <returns>a bool which inidcates a successful or not successful Process()</returns>
    public virtual bool Process()

	/// <summary>End() will be executed as soon as Process() ends</summary>
	/// <returns>a bool which inidcates a successful or not successful End()</returns>
    public virtual bool End()


## Functions that are adressed to this AiCallout (not static functions)

## Log
	/// <summary>does LogTrivial but with an prefix about the Fiber number and AAIC</summary>
	protected void LogTrivial_withAiC(String Log)

	/// <summary>does LogTrivialDebug but with an prefix about the Fiber number and AAIC</summary>
	protected void LogTrivialDebug_withAiC(String Log)

	/// <summary>does LogVerbose but with an prefix about the Fiber number and AAIC</summary>
	protected void LogVerbose_withAiC(String Log)

	/// <summary>does LogVerboseDebug but with an prefix about the Fiber number and AAIC</summary>
	protected void LogVerboseDebug_withAiC(String Log)




## Location get&set
	/// <summary>The location of an AiCallout. This is where the unit will respond to. </summary>
	/// <param name="value">Vector3 location what the AiCallout location should be</param>
	protected Vector3 location



## RadioSettings
	/// <summary>Get the Callsign of your Unit</summary> 
	/// <returns>A string wich looks like this "1-L-24"</returns>
	protected String callSign


	/// <summary>The name of an audio file which will be played to explain via police radio what happend at that scene. should look like this: "CRIME_SHOTS_FIRED_AT_AN_OFFICER"</summary>
	/// <param name="value">should look like this: "CRIME_SHOTS_FIRED_AT_AN_OFFICER"</param>
	protected String calloutDetailsString



## Decicion Functions & arrival
	/// <summary>This function will count (as soon as called) how much time the unit need to reach a specific distance to the callout location.</summary>
	/// <param name="distance">The distance it needs to reach in order of not returning false</param>
	/// <param name="time">Time in seconds until the vehicle have to get a lower distance to the callout that given by distance</param>
	/// <returns>returns false if the unit had not a lower distance to the callout in the specific time</returns>
	protected bool IsUnitInTime(float distance, int time)


	/// <summary>if this Offcier have the files for reporting on Scene, this triggers it.</summary>
	protected void OfficerReportOnScene()


	/// <summary>Officers Leaving Vehicle</summary><param name="leaveDoorOpen">police officers leave door open</param>
	protected void OfficersLeaveVehicle(bool leaveDoorOpen)


	/// <summary>Dispatch tells the unit that this call is code 4 while the unit was code 3 responding - Example: if the unit is not IsUnitInTime() then execute Disregard() </summary>
	protected void Disregard()


	/// <summary>Get in the Vehicle and dismiss </summary>
	protected void EnterAndDismiss()


	/// <summary>for fatal Errors. This will be anyway executed by us if the try comes back as a catch or returning setup(), proccess() or end() as false </summary>
	protected void AbortCode()


	/// <summary>function that checks condition wheter is the possibility or the status of the player will may, maynot lead to an playercallout</summary>
	protected bool IsAiTakingCare()


	/// <summary>Triggering one of the universal PlayerCallouts</summary><param name="PlayerCalloutName">String name of the playercallout. Should look like this: "Officer in Pursuit/>"</param>
	protected void UnitCallsForBackup(string PlayerCalloutName) 

	/// <summary>This it the value for how close the unit trys to navigate and how big the blip radius is</summary>
	/// <param name="value">Enter a valid arrival distance threshold"</param>
	protected float arrivalDistanceThreshold



## Objects
	/// <summary> </summary>
	protected Vehicle Unit


	/// <summary> </summary>
	protected List<Ped> UnitOfficers


	/// <summary> </summary>
	protected List<Ped> Suspects


	/// <summary></summary>
	protected List<Vehicle> SuspectsVehicles


	/// <summary>Creates suspects which will be listed in the suspects list. Alternativly you can add them to the suspects list manualy to ensure a playercallout will be able to handle them.</summary>
	/// <param name="numberOfSuspects">amount of suspects to create</param>
	protected void SetupSuspects(int numberOfSuspects)


	/// <summary>Takes peds from the envoirement nearby and listed in the suspects list. Alternativly you can add them to the suspects list manualy to ensure a playercallout will be able to handle them. In case of an error it will create peds</summary>
	/// <param name="numberOfSuspects">amount of suspects to find</param>
	protected void SetupRandomSuspects(int numberOfSuspects, Vector3 location) 



## Player responding in addition
	/// <summary>Player direct response. Get true if player reacted aswell responding to the last called AiC. This feature is for detecting if the player is also present at this AiC</summary>
	protected bool playerRespondingInAdditon



## HeliAssistance
	/// <summary>Call HeliAssistance Helicopter via Radio.</summary>
	/// <param name="pos">position where to respond</param>
	protected void AiCandHA_RequestHelicopterTo(Vector3 pos)


	///<summary>Dismiss HeliAssistance Helicopter via Radio.</summary> 
	protected void AiCandHA_DismissHelicopter()


	/// <summary>Add HeliAssistance Helicopter to Pursuit via Radio.</summary>
	protected void AiCandHA_AddHelicopterToPursuit(LHandle lhandle) 


	/// <summary>using this for aaic not crashing when HeliAssistance is not installed and dont want to radio code 4</summary>
	protected void SilentDismissHelicopter() 


}


---



# AmbientAICallouts.API.Functions

## Functions that needs an ManagerObject (static functions)

## Name
	/// <summary> in short words the name of what happend here Example: "Shootout", "Motor Vehicle Accident", </summary>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static string Get_SceneInfo(AiCalloutManager given_mO)




## Location get&set
	/// <summary>Sets the location of an AiCallout. This is where the unit will respond to. </summary>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static void Set_location(AiCalloutManager given_mO, Vector3 location)


	/// <summary>Gets the location of an AiCallout. This is where the unit will respond to. </summary>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static Vector3 Get_Location(AiCalloutManager given_mO)



## RadioSettings
	/// <summary>Get the Callsign of your Unit</summary> <returns>A string wich looks like this "1-L-24"</returns>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static String Get_callSign(AiCalloutManager given_mO)



	/// <summary>Sets the name of an audio file which will be played to explain via police radio what happend at that scene</summary>
	/// <param name="calloutDetailsString">should look like this: "CRIME_SHOTS_FIRED_AT_AN_OFFICER"</param>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static void Set_calloutDetailsString(AiCalloutManager given_mO, String calloutDetailsString)


	/// <summary>Sets the name of an audio file which will be played to explain via police radio what happend at that scene. should look like this: "CRIME_SHOTS_FIRED_AT_AN_OFFICER"</summary>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static String Get_calloutDetailsString(AiCalloutManager given_mO)



## Decicion Functions
	/// <summary>Dispatch tells the unit that this call is code 4 while the unit was code 3 responding - Example: if the unit is not IsUnitInTime() then execute Disregard() </summary>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static void Disregard(AiCalloutManager given_mO)


	/// <summary>for fatal Errors. This will be anyway executed by us if the try comes back as a catch or returning setup(), proccess() or end() as false </summary>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static void AbortCode(AiCalloutManager given_mO)


	/// <summary>Triggering one of the universal PlayerCallouts</summary><param name="PlayerCalloutName">String name of the playercallout. Should look like this: "Officer in Pursuit/>"</param>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static void UnitCallsForBackup(AiCalloutManager given_mO, string PlayerCalloutName)



## Objects
	/// <summary>Gets the Unit from the given_mO</summary>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static Vehicle Unit(AiCalloutManager given_mO)


	/// <summary>Gets the UnitOfficers List from the given_mO</summary>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static List<Ped> UnitOfficers(AiCalloutManager given_mO)


	/// <summary>Gets the Suspects List from the given_mO</summary>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static List<Ped> Suspects(AiCalloutManager given_mO)


	/// <summary>Gets the SuspectVehicles List from the given_mO</summary>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static List<Vehicle> SuspectVehicles(AiCalloutManager given_mO)


	/// <summary>Creates suspects which will be listed in the suspects list. Alternativly you can add them to the suspects list manualy to ensure a playercallout will be able to handle them.</summary>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static void SetupSuspects(AiCalloutManager given_mO, int numberOfSuspects)


	/// <summary>Takes peds from the envoirement nearby and listed in the suspects list. Alternativly you can add them to the suspects list manualy to ensure a playercallout will be able to handle them. In case of an error it will create peds</summary>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param
    public static void SetupRandomSuspects(AiCalloutManager given_mO, int numberOfSuspects, Vector3 location)



## HeliAssistance
	///<summary>Dismiss HeliAssistance Helicopter via Radio.</summary> 
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static void AiCandHA_RequestHelicopterTo(AiCalloutManager given_mO, Vector3 pos)


	///<summary>Dismiss HeliAssistance Helicopter via Radio.</summary>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static void AiCandHA_DismissHelicopter(AiCalloutManager given_mO)


	/// <summary>Add HeliAssistance Helicopter to Pursuit via Radio.</summary>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static void AiCandHA_AddHelicopterToPursuit(AiCalloutManager given_mO, LHandle lhandle)



## AiCalloutDistance getter
	/// <summary>Get minimum AiCallout distance. If the location does not match the .ini settings the AiCallout will aborted after the Setup</summary>
    public static float minimumAiCalloutDistance
	/// <summary>Get maximum AiCallout distance. If the location does not match the .ini settings the AiCallout will aborted after the Setup</summary>
    public static float maximumAiCalloutDistance



## Player direct response
	/// <summary>Player direct response. Get true if player reacted aswell responding to the last called AiC. This feature is for detecting if the player is also present at this AiC</summary>
	/// <param name="given_mO">Calling a specific AiCalloutManager object. If you want to call your own manager object, use the AiCallout functions.</param>
    public static bool Get_playerRespondingInAdditon(AiCalloutManager give_mO)


	/// <summary>Player direct response. This feature is for telling an AiC that the player responds aswell and my change the usual behavior as if the player would be present</summary>
    public static void AAIC_Response()



## others
	/// <summary>simple code which deletes all peds and vehicles in that area</summary>
    public static void CleanArea(Vector3 Area, float areaRadius)