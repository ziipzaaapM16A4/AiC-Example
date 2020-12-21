        protected AiCallout() { }

        /// <summary>Scene Information. Like "Shots fired" or "Dommestic disturbance"</summary>
        public string SceneInfo { get; protected set; }

        /// <summary>The manager object is the handler of an AiCallout.</summary>
        public AiCalloutManager MO { get; internal set; }


        /// <summary>Setup() will be executed before calling the Unit. Use it to setup the crime scene</summary>
        /// <returns>a bool which inidcates a successful or not successful Setup()</returns>
        public virtual bool Setup() { return false; }

        /// <summary>Process() will be executed as soon as the unit responds to the call. you have full access to all given objects but its recomended to wait until the unit is nearby the scene.</summary>
        /// <returns>a bool which inidcates a successful or not successful Process()</returns>
        public virtual bool Process() { return false; }

        /// <summary>End() will be executed as soon as Process() ends</summary>
        /// <returns>a bool which inidcates a successful or not successful End()</returns>
        public virtual bool End() { return false; }


        #region direct Functions



        #region Log
        /// <summary>does LogTrivial but with an prefix about the Fiber number and AAIC</summary>
        protected void LogTrivial_withAiC(String Log) => Game.LogTrivial($"[AmbientAICallouts] [Fiber { MO.fiberNumber}] {Log}");

        /// <summary>does LogTrivialDebug but with an prefix about the Fiber number and AAIC</summary>
        protected void LogTrivialDebug_withAiC(String Log) => Game.LogTrivialDebug($"[AmbientAICallouts] [Fiber { MO.fiberNumber}] {Log}");

        /// <summary>does LogVerbose but with an prefix about the Fiber number and AAIC</summary>
        protected void LogVerbose_withAiC(String Log) => Game.LogVerbose($"[AmbientAICallouts] [Fiber { MO.fiberNumber}] {Log}");

        /// <summary>does LogVerboseDebug but with an prefix about the Fiber number and AAIC</summary>
        protected void LogVerboseDebug_withAiC(String Log) => Game.LogVerboseDebug($"[AmbientAICallouts] [Fiber { MO.fiberNumber}] {Log}");
        #endregion Log



        #region Location get&set
        /// <summary>The location of an AiCallout. This is where the unit will respond to. </summary>
        /// <param name="value">Vector3 location what the AiCallout location should be</param>
        protected Vector3 location { get => MO.location; set => MO.location = value; }
        #endregion



        #region RadioSettings
        /// <summary>Get the Callsign of your Unit</summary> <returns>A string wich looks like this "1-L-24"</returns>
        protected String callSign => MO.rndVl.CallSignString;


        /// <summary>The name of an audio file which will be played to explain via police radio what happend at that scene. should look like this: "CRIME_SHOTS_FIRED_AT_AN_OFFICER"</summary>
        /// <param name="value">should look like this: "CRIME_SHOTS_FIRED_AT_AN_OFFICER"</param>
        protected String calloutDetailsString { get => MO.calloutDetailsString; set => MO.calloutDetailsString = value; }
        #endregion



        #region Decicion Functions & arrival
        /// <summary>This function will count (as soon as called) how much time the unit need to reach a specific distance to the callout location.</summary>
        /// <param name="distance">The distance it needs to reach in order of not returning false</param>
        /// <param name="time">Time in seconds until the vehicle have to get a lower distance to the callout that given by distance</param>
        /// <returns>returns false if the unit had not a lower distance to the callout in the specific time</returns>
        protected bool IsUnitInTime(float distance, int time) => MO.IsUnitInTime(distance, time);


        /// <summary>if this Offcier have the files for reporting on Scene, this triggers it.</summary>
        protected void OfficerReportOnScene() => MO.COM_OnScene();


        /// <summary>Officers Leaving Vehicle</summary><param name="leaveDoorOpen">police officers leave door open</param>
        protected void OfficersLeaveVehicle(bool leaveDoorOpen) => MO.OfficersLeaveVehicle(leaveDoorOpen);


        /// <summary>Dispatch tells the unit that this call is code 4 while the unit was code 3 responding - Example: if the unit is not IsUnitInTime() then execute Disregard() </summary>
        protected void Disregard() => MO.Disregard(); 


        /// <summary>Get in the Vehicle and dismiss </summary>
        protected void EnterAndDismiss() => MO.EnterAndDismiss();


        /// <summary>for fatal Errors. This will be anyway executed by us if the try comes back as a catch or returning setup(), proccess() or end() as false </summary>
        protected void AbortCode() => MO.AbortCode();


        /// <summary>function that checks condition wheter is the possibility or the status of the player will may, maynot lead to an playercallout</summary>
        protected bool IsAiTakingCare() => MO.IsAiTakingCare();


        /// <summary>Triggering one of the universal PlayerCallouts</summary><param name="PlayerCalloutName">String name of the playercallout. Should look like this: "Officer in Pursuit/>"</param>
        protected void UnitCallsForBackup(string PlayerCalloutName) => MO.UnitCallsForBackup(PlayerCalloutName); 

        /// <summary>This it the value for how close the unit trys to navigate and how big the blip radius is</summary>
        /// <param name="value">Enter a valid arrival distance threshold"</param>
        protected float arrivalDistanceThreshold { get => MO.arrivalDistanceThreshold; set => MO.arrivalDistanceThreshold = value; } 
        #endregion



        #region Objects
        /// <summary> </summary>
        protected Vehicle Unit => MO.unit;


        /// <summary> </summary>
        protected List<Ped> UnitOfficers => MO.unitOfficers;


        /// <summary> </summary>
        protected List<Ped> Suspects => MO.suspects;


        /// <summary></summary>
        protected List<Vehicle> SuspectsVehicles => MO.suspectsVehicles;


        /// <summary>Creates suspects which will be listed in the suspects list. Alternativly you can add them to the suspects list manualy to ensure a playercallout will be able to handle them.</summary>
        /// <param name="numberOfSuspects">amount of suspects to create</param>
        protected void SetupSuspects(int numberOfSuspects) => MO.SetupSuspects(numberOfSuspects);


        /// <summary>Takes peds from the envoirement nearby and listed in the suspects list. Alternativly you can add them to the suspects list manualy to ensure a playercallout will be able to handle them. In case of an error it will create peds</summary>
        /// <param name="numberOfSuspects">amount of suspects to find</param>
        protected void SetupRandomSuspects(int numberOfSuspects, Vector3 location) => MO.SetupRandomSuspects(numberOfSuspects, location); 
        #endregion



        #region Player direct response
        /// <summary>Player direct response. Get true if player reacted aswell responding to the last called AiC. This feature is for detecting if the player is also present at this AiC</summary>
        protected bool playerRespondingInAdditon => MO.playerRespondingInAdditon;
        #endregion



        #region HeliAssistance
        /// <summary>Call HeliAssistance Helicopter via Radio.</summary>
        /// <param name="pos">position where to respond</param>
        protected void AiCandHA_RequestHelicopterTo(Vector3 pos) => MO.RequestHelicopterTo(pos);


        ///<summary>Dismiss HeliAssistance Helicopter via Radio.</summary> 
        protected void AiCandHA_DismissHelicopter() => MO.DismissHelicopter(); 


        /// <summary>Add HeliAssistance Helicopter to Pursuit via Radio.</summary>
        protected void AiCandHA_AddHelicopterToPursuit(LHandle lhandle) => MO.AddHelicopterToPursuit(lhandle); 


        /// <summary>using this for aaic not crashing when HeliAssistance is not installed and dont want to radio code 4</summary>
        protected void SilentDismissHelicopter() => MO.SilentDismissHelicopter(); 
        #endregion


        #endregion
    }


---



        protected AiCallout() { }

        /// <summary>Scene Information. Like "Shots fired" or "Dommestic disturbance"</summary>
        public string SceneInfo { get; protected set; }

        /// <summary>The manager object is the handler of an AiCallout.</summary>
        public AiCalloutManager MO { get; internal set; }


        //---------------------------------------- Official -----------------------------------------------------

        /// <summary>Setup() will be executed before calling the Unit. Use it to setup the crime scene</summary>
        /// <returns>a bool which inidcates a successful or not successful Setup()</returns>
        public virtual bool Setup() { return false; }

        /// <summary>Process() will be executed as soon as the unit responds to the call. you have full access to all given objects but its recomended to wait until the unit is nearby the scene.</summary>
        /// <returns>a bool which inidcates a successful or not successful Process()</returns>
        public virtual bool Process() { return false; }

        /// <summary>End() will be executed as soon as Process() ends</summary>
        /// <returns>a bool which inidcates a successful or not successful End()</returns>
        public virtual bool End() { return false; }


        #region direct Functions



        #region Log
        /// <summary>does LogTrivial but with an prefix about the Fiber number and AAIC</summary>
        protected void LogTrivial_withAiC(String Log) => Game.LogTrivial($"[AmbientAICallouts] [Fiber { MO.fiberNumber}] {Log}");

        /// <summary>does LogTrivialDebug but with an prefix about the Fiber number and AAIC</summary>
        protected void LogTrivialDebug_withAiC(String Log) => Game.LogTrivialDebug($"[AmbientAICallouts] [Fiber { MO.fiberNumber}] {Log}");

        /// <summary>does LogVerbose but with an prefix about the Fiber number and AAIC</summary>
        protected void LogVerbose_withAiC(String Log) => Game.LogVerbose($"[AmbientAICallouts] [Fiber { MO.fiberNumber}] {Log}");

        /// <summary>does LogVerboseDebug but with an prefix about the Fiber number and AAIC</summary>
        protected void LogVerboseDebug_withAiC(String Log) => Game.LogVerboseDebug($"[AmbientAICallouts] [Fiber { MO.fiberNumber}] {Log}");
        #endregion Log



        #region Location get&set
        /// <summary>The location of an AiCallout. This is where the unit will respond to. </summary>
        /// <param name="value">Vector3 location what the AiCallout location should be</param>
        protected Vector3 location { get => MO.location; set => MO.location = value; }
        #endregion



        #region RadioSettings
        /// <summary>Get the Callsign of your Unit</summary> <returns>A string wich looks like this "1-L-24"</returns>
        protected String callSign => MO.rndVl.CallSignString;


        /// <summary>The name of an audio file which will be played to explain via police radio what happend at that scene. should look like this: "CRIME_SHOTS_FIRED_AT_AN_OFFICER"</summary>
        /// <param name="value">should look like this: "CRIME_SHOTS_FIRED_AT_AN_OFFICER"</param>
        protected String calloutDetailsString { get => MO.calloutDetailsString; set => MO.calloutDetailsString = value; }
        #endregion



        #region Decicion Functions & arrival
        /// <summary>This function will count (as soon as called) how much time the unit need to reach a specific distance to the callout location.</summary>
        /// <param name="distance">The distance it needs to reach in order of not returning false</param>
        /// <param name="time">Time in seconds until the vehicle have to get a lower distance to the callout that given by distance</param>
        /// <returns>returns false if the unit had not a lower distance to the callout in the specific time</returns>
        protected bool IsUnitInTime(float distance, int time) => MO.IsUnitInTime(distance, time);


        /// <summary>if this Offcier have the files for reporting on Scene, this triggers it.</summary>
        protected void OfficerReportOnScene() => MO.COM_OnScene();


        /// <summary>Officers Leaving Vehicle</summary><param name="leaveDoorOpen">police officers leave door open</param>
        protected void OfficersLeaveVehicle(bool leaveDoorOpen) => MO.OfficersLeaveVehicle(leaveDoorOpen);


        /// <summary>Dispatch tells the unit that this call is code 4 while the unit was code 3 responding - Example: if the unit is not IsUnitInTime() then execute Disregard() </summary>
        protected void Disregard() => MO.Disregard(); 


        /// <summary>Get in the Vehicle and dismiss </summary>
        protected void EnterAndDismiss() => MO.EnterAndDismiss();


        /// <summary>for fatal Errors. This will be anyway executed by us if the try comes back as a catch or returning setup(), proccess() or end() as false </summary>
        protected void AbortCode() => MO.AbortCode();


        /// <summary>function that checks condition wheter is the possibility or the status of the player will may, maynot lead to an playercallout</summary>
        protected bool IsAiTakingCare() => MO.IsAiTakingCare();


        /// <summary>Triggering one of the universal PlayerCallouts</summary><param name="PlayerCalloutName">String name of the playercallout. Should look like this: "Officer in Pursuit/>"</param>
        protected void UnitCallsForBackup(string PlayerCalloutName) => MO.UnitCallsForBackup(PlayerCalloutName); 

        /// <summary>This it the value for how close the unit trys to navigate and how big the blip radius is</summary>
        /// <param name="value">Enter a valid arrival distance threshold"</param>
        protected float arrivalDistanceThreshold { get => MO.arrivalDistanceThreshold; set => MO.arrivalDistanceThreshold = value; } 
        #endregion



        #region Objects
        /// <summary> </summary>
        protected Vehicle Unit => MO.unit;


        /// <summary> </summary>
        protected List<Ped> UnitOfficers => MO.unitOfficers;


        /// <summary> </summary>
        protected List<Ped> Suspects => MO.suspects;


        /// <summary></summary>
        protected List<Vehicle> SuspectsVehicles => MO.suspectsVehicles;


        /// <summary>Creates suspects which will be listed in the suspects list. Alternativly you can add them to the suspects list manualy to ensure a playercallout will be able to handle them.</summary>
        /// <param name="numberOfSuspects">amount of suspects to create</param>
        protected void SetupSuspects(int numberOfSuspects) => MO.SetupSuspects(numberOfSuspects);


        /// <summary>Takes peds from the envoirement nearby and listed in the suspects list. Alternativly you can add them to the suspects list manualy to ensure a playercallout will be able to handle them. In case of an error it will create peds</summary>
        /// <param name="numberOfSuspects">amount of suspects to find</param>
        protected void SetupRandomSuspects(int numberOfSuspects, Vector3 location) => MO.SetupRandomSuspects(numberOfSuspects, location); 
        #endregion



        #region Player direct response
        /// <summary>Player direct response. Get true if player reacted aswell responding to the last called AiC. This feature is for detecting if the player is also present at this AiC</summary>
        protected bool playerRespondingInAdditon => MO.playerRespondingInAdditon;
        #endregion



        #region HeliAssistance
        /// <summary>Call HeliAssistance Helicopter via Radio.</summary>
        /// <param name="pos">position where to respond</param>
        protected void AiCandHA_RequestHelicopterTo(Vector3 pos) => MO.RequestHelicopterTo(pos);


        ///<summary>Dismiss HeliAssistance Helicopter via Radio.</summary> 
        protected void AiCandHA_DismissHelicopter() => MO.DismissHelicopter(); 


        /// <summary>Add HeliAssistance Helicopter to Pursuit via Radio.</summary>
        protected void AiCandHA_AddHelicopterToPursuit(LHandle lhandle) => MO.AddHelicopterToPursuit(lhandle); 


        /// <summary>using this for aaic not crashing when HeliAssistance is not installed and dont want to radio code 4</summary>
        protected void SilentDismissHelicopter() => MO.SilentDismissHelicopter(); 
        #endregion


        #endregion
    }









---
Markdown Syntax
=

Normaler Text wird so dargestellt wie eingegeben.

Eine Leerzeile erzeugt einen Absatz.


Für alle Zeichen, die eine Formatierung bewirken, kann die Wirkung durch einen Backslash aufgehoben werden: 
\* \' \_ 2\. – Der Backslash selbst wird durch \\ eingefügt.

Zwei oder mehr Leerzeichen am Ende der Zeile  
erzeugen einen Zeilenumbruch.

*Kursiv*, **Fett** und ***Fett kursiv*** bzw.
_Kursiv_, __Fett__ und ___Fett kursiv___

Markiert Text als `Inline-Quelltext`

Ein normaler Absatz

    Ein Code-Block
    durch Einrückung
    mit vier Leerzeichen
	
	
* Ein Punkt in einer ungeordneten Liste
* Ein weiterer Punkt in einer ungeordneten Liste
    * Ein Unterpunkt, um vier Leerzeichen eingerückt
* Statt * funktionieren auch + oder -


1. Ein Punkt in einer geordneten Liste
2. Ein weiterer Punkt; bei der Eingabe muss nicht auf irgendeine Reihenfolge geachtet werden, sondern nur darauf, dass es beliebige Ziffern sind
1. Noch ein Punkt, der zeigt, dass auch die mehrfache Angabe derselben Ziffer möglich ist


# Überschrift in Ebene 1
#### Überschrift in Ebene 4


Überschrift in Ebene 1
======================
Überschrift in Ebene 2
----------------------

> Dieses Zitat wird in ein HTML-Blockquote-Element gepackt.

---

[Beschriftung des Hyperlinks](https://de.wikipedia.org/ "Titel, der beim Überfahren mit der Maus angezeigt wird")

allgemeine Syntax:
![Alternativtext](Bild-URL "Bildtitel hier")

konkretes Beispiel:
![nur ein Beispiel](https://commons.wikimedia.org/wiki/File:Example_de.jpg "Beispielbild")