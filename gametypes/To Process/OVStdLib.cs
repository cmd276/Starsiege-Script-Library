
$OVversion = "3.5";
echo("OVStdLib.cs "@$OVversion);

// OVStdLib.cs ----------------------------
// Standard library for Starsiege Overrun.
// Authors: Orogogus, [OF] Pincushion, SDS Dolf Kooz
// Last updated: Mar 17, 2000
//
// Want to design an Overrun mission?
//
// REQUIREMENTS:
// - A nav point called "MissionGroup/purpleBase/NavPurple" must exist.
//   It defines the center of the attackers' drop zone. Set "Team: Purple".
// - At least 3 SimShape objects should be designated as targets, typically
//   2 generators and a radar.  These objects (and human players) will be
//   attacked by the AI.  The name of one special object should be "radar".
//   All should be given meaningful "Short Names".  Also set "Team: Purple"
//   and "Script Class: target".  All targets should be Visible, Sensorable,
//   take 4000-20000 damage, and be Targetable.  Never Indestructible.
//   Do NOT make turrets targets.  AI will attack them anyway.
// - There must be a SimGroup "MissionGroup/Triggers" containing an object
//   called "Starter".  Script class: Starter.
// - The Starter trigger should be placed on a Zen pad called
//   "MissionGroup/StarterPad".
//
// OPTIONS:
// - A set of up to 4 posts called "MissionGroup/DirPosts/*Spawn" where * is
//   n, s, e, or w.  These will allow players to select spawn directions.
//   The posts must have Script Class set to their object name, eg. nSpawn.
//
// Post feedback to the Editing and Scripting forum:
// http://starsiegeforum.sierra.com/
// See http://www.geocities.com/SoHo/Nook/9351/maps/index.html for more info.

// Edited for inclusion into Scripting library by Wilzuun.


// Set in Server_OV.cs, but non-dedicated servers need it here.
$server::TimeLimit = 1440;
$server::TeamDamage = true;

// what can the client choose for a team
$server::AllowTeamRed = false;
$server::AllowTeamBlue = false;
// Enable yellow because non-dedicated server demands 2+ teams.
// Script forces all human players to purple.
$server::AllowTeamYellow = true;
$server::AllowTeamPurple = true;

// what can the server admin choose for available teams
$server::disableTeamRed = true;
$server::disableTeamBlue = true;
$server::disableTeamYellow = true;
$server::disableTeamYellow = false;
$server::disableTeamPurple = false;

$server::TeamPlay = true;
$server::AllowDeathmatch = false;
$server::AllowTeamPlay = true;  

// Constants
$OVnameOfTheGame = "Overrun";
$OVtkMsg = "";
if ($server::TeamDamage) {
	$OVtkMsg = "Team damage is on. Team Killers will be kicked immediately.\n";
}
$OVaboutTheGame =
	$OVnameOfTheGame @
	": " @
	"Step on the gray Zen pad to start.\n" @
	$OVtkMsg @
	"For maps and info see http://www.geocities.com/SoHo/Nook/9351/maps/OVindex.html"; 
$OVlogfn = "multiplayer\\serverlog.txt";   // set to "" to disable logging
$OVjoinGameInProgress = 0;                 // allow players to join mid-game?
$OVsecToStart = 15;
$OVsecToOverrun = 5;
$OVattackersAtStart = 2;
if ($OVmaxAttackers < 1) $OVmaxAttackers = floor(3.0*$server::MaxPlayers);
if ($OVmaxAttackers < 6) $OVmaxAttackers = 6;
$OVmintimeBetweenDrops = 30;
$OVuseArty = 0;
$OVminRange = 1200;  // min drop zone range
$OVmaxRange = 1500;  // max drop zone range
$OVdropRadius = 500;
$OVchickenRange = $OVmaxRange+$OVdropRadius; // beyond this and you die
if ($OVnPlayers == "") $OVnPlayers = 0;
$OVbaseNav = "MissionGroup\\purpleBase\\NavPurple";
$OVstarter = "MissionGroup\\Triggers\\Starter";
$OVstarterPad = "MissionGroup\\StarterPad";
$OVposts = "MissionGroup\\DirPosts";
$OVidleLimit = 3;
$OVdebug = 0;

$OVdzN = 8;

// Scenerio-specific vehicle attributes
//   tech: 0 = vehicle not allowed, 1 = Cybrid, 2 = Human
//   pts: point value for score
//   freq: relative frequency
$OVtech[1]=2;$OVpts[1]=6;$OVfreq[1]=7;//TerranApocalypse
$OVtech[2]=2;$OVpts[2]=4;$OVfreq[2]=9;//TerranMinotaur
$OVtech[3]=2;$OVpts[3]=5;$OVfreq[3]=8;//TerranGorgon
$OVtech[4]=2;$OVpts[4]=3;$OVfreq[4]=10;//TerranTalon
$OVtech[5]=2;$OVpts[5]=4;$OVfreq[5]=9;//TerranBasilisk
$OVtech[6]=2;$OVpts[6]=3;$OVfreq[6]=5;//Paladin
$OVtech[7]=2;$OVpts[7]=3;$OVfreq[7]=6;//Myrmidon
$OVtech[8]=0;$OVpts[8]=3;$OVfreq[8]=0;//Disrupter
$OVtech[9]=0;$OVpts[9]=0;$OVfreq[9]=0;// * Banshee
$OVtech[10]=2;$OVpts[10]=6;$OVfreq[10]=7;//Knight'sApocalypse
$OVtech[11]=2;$OVpts[11]=4;$OVfreq[11]=9;//Knight'sMinotaur
$OVtech[12]=2;$OVpts[12]=5;$OVfreq[12]=8;//Knight'sGorgon
$OVtech[13]=2;$OVpts[13]=3;$OVfreq[13]=10;//Knight'sTalon
$OVtech[14]=2;$OVpts[14]=4;$OVfreq[14]=9;//Knight'sBasilisk
$OVtech[15]=2;$OVpts[15]=3;$OVfreq[15]=5;//KnightPaladin
$OVtech[16]=2;$OVpts[16]=3;$OVfreq[16]=6;//KnightMyrmidon
$OVtech[17]=0;$OVpts[17]=3;$OVfreq[17]=0;//KnightDisrupter
$OVtech[18]=0;$OVpts[18]=0;$OVfreq[18]=0;// * TerranCargo
$OVtech[19]=0;$OVpts[19]=0;$OVfreq[19]=0;// * TerranEscape
$OVtech[20]=1;$OVpts[20]=3;$OVfreq[20]=11;//Seeker
$OVtech[21]=1;$OVpts[21]=3;$OVfreq[21]=10;//Goad
$OVtech[22]=1;$OVpts[22]=4;$OVfreq[22]=10;//Shepherd
$OVtech[23]=1;$OVpts[23]=5;$OVfreq[23]=4;//Adjudicator
$OVtech[24]=1;$OVpts[24]=6;$OVfreq[24]=4;//Executioner
$OVtech[25]=1;$OVpts[25]=3;$OVfreq[25]=6;//Bolo
$OVtech[26]=1;$OVpts[26]=3;$OVfreq[26]=6;//Recluse
$OVtech[27]=1;$OVpts[27]=5;$OVfreq[27]=3;// * PlatAdjucator
$OVtech[28]=1;$OVpts[28]=6;$OVfreq[28]=3;// * PlatExecutor
$OVtech[29]=0;$OVpts[29]=0;$OVfreq[29]=0;// * Prometheus
$OVtech[30]=2;$OVpts[30]=4;$OVfreq[30]=9;//Emancipator
$OVtech[31]=2;$OVpts[31]=3;$OVfreq[31]=10;//Avenger
$OVtech[32]=2;$OVpts[32]=4;$OVfreq[32]=5;//Dreadlock
$OVtech[33]=2;$OVpts[33]=4;$OVfreq[33]=10;//Olympian
$OVtech[34]=0;$OVpts[34]=1;$OVfreq[34]=0;// NA
$OVtech[35]=1;$OVpts[35]=2;$OVfreq[35]=11;//MetagenSeeker
$OVtech[36]=1;$OVpts[36]=3;$OVfreq[36]=10;//MetagenGoad
$OVtech[37]=1;$OVpts[37]=4;$OVfreq[37]=10;//MetagenShepherd
$OVtech[38]=1;$OVpts[38]=5;$OVfreq[38]=3;// * MetagenAdjucator
$OVtech[39]=1;$OVpts[39]=6;$OVfreq[39]=3;// * MetagenExecutor
$OVtech[40]=0;$OVpts[40]=0;$OVfreq[40]=0;// * Harabec'sApocalypse
$OVtech[41]=2;$OVpts[41]=5;$OVfreq[41]=7;//Predator
$OVtech[42]=0;$OVpts[42]=5;$OVfreq[42]=0;// * Caanon's Basilisk?
$OVtech[43]=0;$OVpts[43]=6;$OVfreq[43]=0;// * Cinematic Apocalypse
$OVtech[44]=0;$OVpts[44]=4;$OVfreq[44]=0;// * Cinematic Basilisk
$OVtech[45]=0;$OVpts[45]=6;$OVfreq[45]=0;// * Super Predator
$OVtech[46]=0;$OVpts[46]=0;$OVfreq[46]=0;// NA
$OVtech[47]=0;$OVpts[47]=0;$OVfreq[47]=0;// NA
$OVtech[48]=0;$OVpts[48]=1;$OVfreq[48]=0;// NA
$OVtech[49]=0;$OVpts[49]=1;$OVfreq[49]=0;// NA
$OVtech[50]=2;$OVpts[50]=6;$OVfreq[50]=5;// * Pirateapocalypse
$OVtech[51]=2;$OVpts[51]=4;$OVfreq[51]=5;// * Piratedreadlock
$OVtech[52]=2;$OVpts[52]=4;$OVfreq[52]=9;//PirateEmancipator
$OVtech[53]=0;$OVpts[53]=0;$OVfreq[53]=0;// NA
$OVtech[54]=0;$OVpts[54]=0;$OVfreq[54]=0;// NA
$OVtech[55]=1;$OVpts[55]=5;$OVfreq[55]=3;//PlayerPlatAdjudicator
$OVtech[56]=1;$OVpts[56]=6;$OVfreq[56]=3;//PlayerPlatExecutioner
$OVnVeh = 57;

// Immutable vehicle attributes
//   tec: 0 = not applicable, 1 = Cybrid, 2 = Human
//   typ: 0 = not applicable, 1 = Herc, 2 = Tank, 3 = Drone, 4 = Flyer
// * denotes non-standard vehicles ordinarily prohibited to human players
// NA denotes ids for which no vehicle exists
$Vtec[1]=2;$Vtyp[1]=1;//TerranApocalypse
$Vtec[2]=2;$Vtyp[2]=1;//TerranMinotaur
$Vtec[3]=2;$Vtyp[3]=1;//TerranGorgon
$Vtec[4]=2;$Vtyp[4]=1;//TerranTalon
$Vtec[5]=2;$Vtyp[5]=1;//TerranBasilisk
$Vtec[6]=2;$Vtyp[6]=2;//Paladin
$Vtec[7]=2;$Vtyp[7]=2;//Myrmidon
$Vtec[8]=2;$Vtyp[8]=2;//Disrupter
$Vtec[9]=0;$Vtyp[9]=4;// * Banshee
$Vtec[10]=2;$Vtyp[10]=1;//Knight'sApocalypse
$Vtec[11]=2;$Vtyp[11]=1;//Knight'sMinotaur
$Vtec[12]=2;$Vtyp[12]=1;//Knight'sGorgon
$Vtec[13]=2;$Vtyp[13]=1;//Knight'sTalon
$Vtec[14]=2;$Vtyp[14]=1;//Knight'sBasilisk
$Vtec[15]=2;$Vtyp[15]=2;//KnightPaladin
$Vtec[16]=2;$Vtyp[16]=2;//KnightMyrmidon
$Vtec[17]=0;$Vtyp[17]=2;//KnightDisrupter
$Vtec[18]=0;$Vtyp[18]=4;// * TerranCargo
$Vtec[19]=0;$Vtyp[19]=4;// * TerranEscape
$Vtec[20]=1;$Vtyp[20]=1;//Seeker
$Vtec[21]=1;$Vtyp[21]=1;//Goad
$Vtec[22]=1;$Vtyp[22]=1;//Shepherd
$Vtec[23]=1;$Vtyp[23]=1;//Adjudicator
$Vtec[24]=1;$Vtyp[24]=1;//Executioner
$Vtec[25]=1;$Vtyp[25]=2;//Bolo
$Vtec[26]=1;$Vtyp[26]=2;//Recluse
$Vtec[27]=1;$Vtyp[27]=1;// * PlatAdjucator
$Vtec[28]=1;$Vtyp[28]=1;// * PlatExecutor
$Vtec[29]=1;$Vtyp[29]=1;// * Prometheus
$Vtec[30]=2;$Vtyp[30]=1;//Emancipator
$Vtec[31]=2;$Vtyp[31]=2;//Avenger
$Vtec[32]=2;$Vtyp[32]=2;//Dreadlock
$Vtec[33]=2;$Vtyp[33]=1;//Olympian
$Vtec[34]=0;$Vtyp[34]=0;// NA
$Vtec[35]=1;$Vtyp[35]=1;//MetagenSeeker
$Vtec[36]=1;$Vtyp[36]=1;//MetagenGoad
$Vtec[37]=1;$Vtyp[37]=1;//MetagenShepherd
$Vtec[38]=1;$Vtyp[38]=1;// * MetagenAdjucator
$Vtec[39]=1;$Vtyp[39]=1;// * MetagenExecutor
$Vtec[40]=2;$Vtyp[40]=1;// * Harabec'sApocalypse
$Vtec[41]=2;$Vtyp[41]=2;//Predator
$Vtec[42]=2;$Vtyp[42]=1;// * Caanon's Basilisk?
$Vtec[43]=2;$Vtyp[43]=1;// * Cinematic Apocalypse
$Vtec[44]=2;$Vtyp[44]=1;// * Cinematic Basilisk
$Vtec[45]=2;$Vtyp[45]=2;// * Super Predator
$Vtec[46]=0;$Vtyp[46]=0;// NA
$Vtec[47]=0;$Vtyp[47]=0;// NA
$Vtec[48]=0;$Vtyp[48]=0;// NA
$Vtec[49]=0;$Vtyp[49]=0;// NA
$Vtec[50]=2;$Vtyp[50]=1;// * PirateApocalypse
$Vtec[51]=2;$Vtyp[51]=2;// * PirateDreadlock
$Vtec[52]=2;$Vtyp[52]=1;//PirateEmancipator
$Vtec[53]=0;$Vtyp[53]=0;// NA
$Vtec[54]=0;$Vtyp[54]=0;// NA
$Vtec[55]=1;$Vtyp[55]=1;//PlayerPlatAdjudicator
$Vtec[56]=1;$Vtyp[56]=1;//PlayerPlatExecutioner
$Vn = 57;

function VehicleType(%v)
{
	if ((%v >= 1) && (%v < $Vn))
		%t = $Vtyp[%v];
	else if ((%v >= 60) && (%v <= 73)) %t = 3;
	else if (%v == 90) %t = 2;
	else if ((%v >= 91) && (%v <= 93)) %t = 4;
	else if ((%v >= 94) && (%v <= 96)) %t = 3;
	else if ((%v >= 110) && (%v <= 111)) %t = 4;
	else if ((%v >= 130) && (%v <= 132)) %t = 4;
	else if ((%v >= 133) && (%v <= 134)) %t = 2;
	else if ((%v >= 135) && (%v <= 136)) %t = 3;
	else if ((%v >= 137) && (%v <= 138)) %t = 2;
	else if (%v == 150) %t = 2;
	else %t = 0;
	if (%t == 1) return "Herc";
	if (%t == 2) return "Tank";
	if (%t == 3) return "Drone";
	if (%t == 4) return "Flyer";
	return "Unknown";
}

function VehicleTech(%v)
{
	if ((%v >= 1) && (%v < $Vn)) %t = $Vtec[%v];
	else if ((%v >= 60) && (%v <= 73)) %t = 0;
	else if (%v == 90) %t = 1;
	else if ((%v >= 91) && (%v <= 93)) %t = 0;
	else if ((%v >= 94) && (%v <= 96)) %t = 0;
	else if ((%v >= 110) && (%v <= 111)) %t = 0;
	else if ((%v >= 130) && (%v <= 132)) %t = 0;
	else if ((%v >= 133) && (%v <= 134)) %t = 2;
	else if ((%v >= 135) && (%v <= 136)) %t = 0;
	else if ((%v >= 137) && (%v <= 138)) %t = 2;
	else if (%v == 150) %t = 2;
	else %t = 0;
	if (%t == 1) return "C";
	if (%t == 2) return "H";
	return "?";
}

function setRules()
{
	%rules = "<F2>GAME TYPE: \n<F0>" @
		$OVnameOfTheGame @ " " @ $OVversion @
		"\n\n" @       
		"<tIDMULT_TDM_MAPNAME>" @
		$missionName @
		"\n\n" @
		$OVaboutTheGame @
		"\n\n" @
		"<tIDMULT_STD_ITEMS>" ;
	setGameInfo(%rules);      
}

// setup the rules
// this has to be called after the definition of setRules
setRules();

function OVOut(%msg)
{
	echo(%msg);
	Say(0,0,%msg);
}

function OVLongName(%this) { return(getHUDName(%this)@"("@%this@")"); }

function OVDumpPlayers() {
	%msg = "Players";
	for (%i=0;%i<$OVnPlayers;%i++) {
		%id = $OVplayers[%i];
		%msg = %msg@" "@%i@":"@OVLongName(%id);
	}
	OVOut(%msg);
}

function OVDumpTargets() {
	%msg = "Targets";
	for (%i=0;%i<$OVnTargets;%i++) {
		%id = $OVtargets[%i];
		%msg = %msg@" "@%i@":"@OVLongName(%id);
	}
	OVOut(%msg);
}

function OVDumpAttackers() {
	%msg = "Attackers";
	for(%i=0; %i<$OVnAttackers; %i++) {
		%id = $OVattackers[%i];
		%target = %id.OVvictim;
		%msg = %msg@" "@%i@":"@OVLongName(%id)@"["@OVLongName(%target)@"]";
	}
	OVOut(%msg);
}

function OVMissionStart()
{
	if ($OVdebug) OVOut("MissionStart");
}

// These should be in OVMissionLoad() but they must be initialized before
// target::structure::onAdd() is called.  And that happens before
// MissionLoad.
$OVnTargets = 0;
$OVradar = 0;


function OVMissionLoad()
{
	if ($OVdebug) OVOut("MissionLoad");
	// Initialize globals
	if ($OVforceAttackerTech) $OVattackerTech = $OVforceAttackerTech;
	%tech = $OVattackerTech;
	if ((%tech != 1) && (%tech != 2) && (%tech != 3)) {
		echo("OVMissionStart: invalid tech " @ %tech);
		%tech = 1;
	}
	newObject("MissionCleanup",SimGroup);
	$OVstartTime = "";
	$OVmode = 0;
	$OVstarters = 0;
	$OVtimeBetweenDrops = 60;
	$OVattackersPerDrop = $OVattackersAtStart;
	$OVnAttackers = 0;
	$OVtotKills = 0;
	$OVnLiving = 0;
	$OVwave = 0;
	$OVbaseX = getPosition($OVbaseNav, x);
	$OVbaseY = getPosition($OVbaseNav, y);
// Problems with drops not centered on base?
//  Say(0,0,"Nav: "@%X@" "@%Y);
	$OVrange = $OVmaxRange;
	$OVartyId = 0;
	$OVnAI = 0;

	OVdzSum();

	$OVusePosts = getObjectId($OVposts);
	if ($OVusePosts) {
		$OVeSpawn = getObjectId($OVposts@"\\eSpawn");
		$OVsSpawn = getObjectId($OVposts@"\\sSpawn");
		$OVwSpawn = getObjectId($OVposts@"\\wSpawn");
		$OVnSpawn = getObjectId($OVposts@"\\nSpawn");
		if($OVdz[2] == 0) stopAnimSequence($OVeSpawn, 0);
		if($OVdz[4] == 0) stopAnimSequence($OVsSpawn, 0);
		if($OVdz[6] == 0) stopAnimSequence($OVwSpawn, 0);
		if($OVdz[0] == 0) stopAnimSequence($OVnSpawn, 0);
	}

	$OVfreqSum = 0;
	%tech = $OVattackerTech;
	for (%vix=1;%vix<$OVnVeh;%vix++)
		if (($OVtech[%vix]==%tech) || ((%tech==3) && ($OVtech[%vix]!=0)))
			$OVfreqSum = $OVfreqSum + $OVfreq[%vix];

	OVRunRadar1();
	OVChickenCheck();

	if ($OVlogfn != "") {
		%nowDate = getDate();
		%nowTime = getTime();
		fileWrite($OVlogfn,append,%nowDate@", "@%nowTime@" -- "@"MissionLoad");
	}
}

function OVdzSum()
{
	$OVdzSum = 0;
	for (%i=0;%i<$OVdzN;%i++)
		$OVdzSum = $OVdzSum + $OVdz[%i];
	if ($OVdzSum <= 0) {
		OVOut("No drop zones? Reactivating all drop zones.");
		for (%i=0;%i<$OVdzN;%i++) $OVdz[%i]=1;
		$OVdzSum = $OVdzN;
	}
}

function OVSanityCheck() {
	if ($OVdebug == 0) return;
	for(%aix=0; %aix<$OVnAttackers; %aix++) {
		%avid = $OVattackers[%aix];
		%aname = OVLongName(%avid);
		if (%aix != %avid.OVaix)
			OVOut("ERROR: "@%aname@"'s OVaix '"@
				%avid.OVaix@"' != "@%aix);
		%target = %avid.OVvictim;
		%tname = getHudName(%target);
		if (%tname == "unknown")
			OVOut("ERROR: "@%aname@" has unknown target.");
		%tname = OVLongName(%target);
		if ($OVtargets[%target.OVtix] != %target)
			OVOut("ERROR: "@%aname@" target "@%tname@" is not in OVtargets[].");
	}
	for(%tix=0; %tix<$OVnTargets; %tix++) {
		%tvid = $OVtargets[%tix];
		if (%tix != %tvid.OVtix)
			OVOut("ERROR: "@OVLongName(%tvid)@"'s OVtix '"@
				%tvid.OVtix@"' != "@%tix);
	}
}

function OVDeleteTarget(%i)
{
	%tid = $OVtargets[%i];
	if ($OVdebug) {
		%msg = %tid@" AI:";
		for(%aix=0; %aix<$OVnAttackers; %aix++) {
			%avid = $OVattackers[%aix];
			if (%avid.OVvictim == %tid)
				%msg = %msg @ " " @ %avid;
		}
		OVOut(%msg);
	}
	if (%i < ($OVnTargets-1)) {
		$OVtargets[%i] = $OVtargets[$OVnTargets-1];
		$OVtargets[%i].OVtix = %i;
	}
	$OVnTargets--;

	// Issue new orders to any AI targeting %tid
	for(%aix=0; %aix<$OVnAttackers; %aix++) {
		%avid = $OVattackers[%aix];
		if (%avid.OVvictim == %tid)
			OVSelectTarget(%avid);
	}
	OVSanityCheck();
}

function target::structure::onScan(%scannee,%scanner)
{
	if ($OVdebug) {
		OVSanityCheck();
		OVDumpPlayers();
		OVDumpTargets();
		OVDumpAttackers();
		OVOut("This target is: "@%scannee);
	}
}

function target::structure::onAdd(%vid)
{
	$OVtargets[$OVnTargets] = %vid;
	%vid.OVtix = $OVnTargets;
	$OVnTargets++;
	%name = getObjectName(%vid);
	if (%name == "radar") $OVradar = %vid;
	if ($OVdebug)
		OVOut("target onAdd "@%vid@" "@%name@" OVradar="@$OVradar@
			" OVnTargets="@$OVnTargets);
}

function target::structure::onDestroyed(%this, %attackerId)
{
	%wav = "";
	if (%this == $OVradar) {
		%wav = "sfx_electrical_bzzt.wav";
		$OVradar = 0;
	}
	%name = getHudName(%this);
	Say(0,0,%name@" destroyed! ",%wav);
	OVDeleteTarget(%this.OVtix);
}

function OVRunRadar1()
{
	if($OVmode == 3) return;
	if($OVradar == 0) return;
	playAnimSequence($OVradar, 0, true);
	schedule("OVRunRadar2();", 4);
}

function OVRunRadar2()
{
	if($OVmode == 3) return;
	if($OVradar == 0) return;
	playAnimSequence($OVradar, 0, false);
	schedule("OVRunRadar1();", 4);
}

function player::onAdd(%player)
{
	//messageBox(%player,$OVaboutTheGame);
	Say(%player,0,$OVaboutTheGame);
	%player.OVIP = getConnection(%player);
	%player.OVaddTime = getCurrentTime();
	%player.OVscore = 0;
	%player.OVstate = "wait";
	%n = $OVnPlayers;
	%player.OVpix = %n;
	$OVplayers[%n] = %player;
	$OVnPlayers++;
	if ($OVdebug) OVOut("onAdd "@%player@" n="@%n);
	if ($OVlogfn != "") {
		%nowDate = getDate();
		%nowTime = getTime();
		%name = getName(%player);
		%msg =  %nowDate @ ", " @ %nowTime @ " -- " @ %name @
			" joined the game (" @ %player.OVIP @ ")";
		fileWrite($OVlogfn, append, %msg);
	}
}

function player::onRemove(%player)
{
	%pix = %player.OVpix;
	if ($OVdebug) OVOut("onRemove "@%player);
	if (%pix < ($OVnPlayers-1)) {
		%pp = $OVplayers[$OVnPlayers-1];
		$OVplayers[%pix] = %pp;
		%pp.OVpix = %pix;
	}
	$OVnPlayers--;

	if ($OVlogfn != "") {
		%name = getName(%player);
		%nowDate = getDate();
		%nowTime = getTime();
		%timeEnd = getCurrentTime();
		%timeIn = timeDifference( %timeEnd, %player.OVaddTime);
		%msg =  %nowDate @ ", " @ %nowTime @ " -- " @ %name @
			" left the game (" @ %player.OVIP @ ") -- " @ %timeIn;
		fileWrite($OVlogfn, append, %msg);
	}
}

function vehicle::onScan(%scannee,%scanner)
{
	%player = playerManager::vehicleIdToPlayerNum(%scannee);
	if (%player) return;  // human player
	if ($OVdebug == 0) return;
	%ainame = getHudName(%scannee);
	%target = %scannee.OVvictim;
	%tname = getHudName(%target);
	%tstate = %target.OVstate;
	Say(0,0,%scannee@" "@%ainame@" target="@%target@" "@%tname@" "@%tstate);
}

function vehicle::onAdd(%vehicle)
{
	%player = playerManager::vehicleIdToPlayerNum(%vehicle);
	if(%player == 0) return;
	if (getTeam (%vehicle) != *IDSTR_TEAM_PURPLE) {
		// force players to purple team
		setTeam(%vehicle, *IDSTR_TEAM_PURPLE);
		redrop(%vehicle);
	}
	if (%player.OVstate == "alive") {
		OVOut("onAdd living player?");
		return;
	}
	if ((%player.OVstate == "tked") || ((%player.OVstate == "wait") &&
			($OVjoinGameInProgress || ($OVmode < 2)))) {
		%player.OVstate = "alive";
		%tix = $OVnTargets;
		$OVtargets[%tix] = %vehicle;
		%vehicle.OVtix = %tix;
		$OVnTargets++;
		$OVnLiving++;
		if ($OVdebug)
			healPlayer ( %vehicle );  // for testing
	} else {
//dolf kooz, i tried 1 second but wasn't long enough, may need to change
// to 3 seconds if players end up in the ground
		schedule ("healObject(" @ %vehicle @ ", -50000);", 3);
		//healObject(%vehicle, -50000);
		Say(%player,0,"Sorry, you can't join until the next game.");

// doesn't work
//%id = getObjectId($OVbaseNav);
//setOrbitCamera(%id,150,0,35);
	}
}

function OVAttackerName()
{
	if ($OVattackerTech == 1) return("'brids");
	if ($OVattackerTech == 2) return("human\\\\animals");
	return("attackers");
}

function turret::onDestroyed( %destroyed, %destroyer )
{
	%name = getHudName(%destroyed);
	Say(0,0,%name@" destroyed!");
}

function vehicle::onDestroyed( %destroyed, %destroyer )
{
	%player = playerManager::vehicleIdToPlayerNum(%destroyed);
	%killer = playerManager::vehicleIdToPlayerNum(%destroyer);
	%killerName = getHudName(%destroyer);
	if(%player == 0)
	{
		// AI destroyed
		%i = %destroyed.OVaix;
		if ($OVattackers[%i] != %destroyed) {
			// This should only happen if there's a bug getting OVaix
			// out of sync, or an AI included in the mission (and not
			// created by OVSpawnAttacker) has just died.
			say(0,0,getHudName(%destroyed)@" destroyed.");
			return;
		}
		if (%i < ($OVnAttackers-1)) {
			$OVattackers[%i] = $OVattackers[$OVnAttackers-1];
			$OVattackers[%i].OVaix = %i;
		}
		$OVnAttackers--;
		schedule( "deleteObject(" @ %destroyed @ ");", 2 );
		if (%destroyer != %destroyed) {
			%dbmsg = "";
			if ($OVdebug)
				%dbmsg = " "@%destroyed;
			%who = OVAttackerName();
			Say(0,0,%killerName@" destroyed one of the "@%who@"."@%dbmsg);
		}
		if (%killer != 0) {
			// reward player
			%vix = %destroyed.OVvix;
			%killer.OVscore = %killer.OVscore + $OVpts[%vix];
		}
		$OVtotKills++;
		if (%destroyed == $OVartyId)
			$OVartyId = 0;
		return;
	}
	if(%player.OVstate == "dead")
		// someone who tried to drop in more than once
		return;
	if(%player.OVstate == "wait")
		// someone who tried to join game in progress
		return;
	if(%player.OVstate != "alive") {
		OVOut("onDestroyed a "@%player.OVstate@" player?");
		return;
	}
	$OVnLiving--;  
	%player.OVstate = "dead";
	%name = getName(%player);
	if((%destroyed != %destroyer) &&
			(getTeam(%destroyed) == getTeam(%destroyer))) {
		// Team kill, take action
		say(0,0,%killerName@" is being kicked for team-killing "@%name@".");
		say(%killer, %killer, "", "rules_violated.wav");
		kick(%killer, *IDMULT_CHAT_TEAM_KILL_KICK);
		// antiTeamKill is too lenient
		//antiTeamKill(%killer);

		// allow TKed player to return
		say(%player, %player, "A teammate killed you. You may respawn.");
		%player.OVstate = "tked";
	}
	if((%destroyed == %destroyer) && ($OVmode < 2)) {
		// killed self (eg herc bay) before overrun started, allow redrop
		say(%player, %player, "Overrun hasn't started. You can respawn.");
		%player.OVstate = "wait";
		return;
	}
	Say(0,0,%name@" has fallen.");

	if($OVnLiving > 0)
		OVDeleteTarget(%destroyed.OVtix);
	else {
		$OVmode = 3;
		if ($OVstartTime == "") {
			Say(0,1,"Game over.");
			%timeString = "Aborted";
		} else {
			%timeCompleted = (getCurrentTime() - $OVstartTime);
			%timeString = timeDifference(getCurrentTime(), $OVstartTime);
			%who = OVAttackerName();
			Say(0,0,"The defenders have been overrun!\n"@
				"The attack lasted "@%timeString@", "@$OVtotKills@" "@
				%who@" killed.");
		}
		schedule("OVEnd();", 10);

		if ($OVlogfn != "") {
			%nowDate = getDate();
			%nowTime = getTime();
			%msg = %nowDate @ ", " @ %nowTime @ " -- " @
				"Game Over " @ %timeString @ ", " @ $OVtotKills @ " kills, " @
				$OVwave @ " waves\nName: Score Kills";
			for (%i=0; %i<$OVnPlayers; %i++) {
				%player = $OVplayers[%i];
				%name = getName(%player);
				%score = getPlayerScore(%player);
				%kills = getKills(%player);
				%msg = %msg@"\n  "@%name@": "@%score@" "@%kills;
			}
			fileWrite($OVlogfn, append, %msg);
		}
	}
}


//===============================  functions to start the game

function starter::trigger::onEnter(%this, %object)
{
  %player = playerManager::vehicleIdToPlayerNum(%object);

	$OVstarters++;

	if($OVmode != 0) return;
	Say(0, 0, $OVsecToStart@" seconds to initiate Overrun.");
	if ($OVusePosts)
		Say(%player, 0, "Hint: scan the posts to choose drop areas.");
	OVSetHudTimerAll($OVsecToStart, -1, "Time to initiate Overrun", 1);
	$OVmode = 1;
	schedule("decrementCountdown($OVsecToStart);", 1);
}

function starter::trigger::onLeave(%this, %object)
{
	if($OVmode == 2) return;

	$OVstarters--;
	if($OVstarters == 0)
	{
		Say(0, 0, "Overrun aborted.");
		$OVmode = 0;
		OVSetHudTimerAll(0, 0, "", 1);
	}
}

function eSpawn::structure::onScan(%this, %object, %string)
{
	//%player = playerManager::vehicleIdToPlayerNum(%object);
	if($OVdz[2]) {
		$OVdz[2] = 0;
		if($OVdz[4] == 0) $OVdz[3] = 0;
		if($OVdz[0] == 0) $OVdz[1] = 0;
		say(0, 0, "Setting eastern drops off.");
		stopAnimSequence($OVeSpawn, 0);
	} else {
		$OVdz[2] = 1;
		if($OVdz[4]) $OVdz[3] = 1;
		if($OVdz[0]) $OVdz[1] = 1;
		say(0, 0, "Setting eastern drops on.");
		playAnimSequence($OVeSpawn, 0);
	}
	OVdzSum();
}

function sSpawn::structure::onScan(%this, %object, %string)
{
	//%player = playerManager::vehicleIdToPlayerNum(%object);
	if($OVdz[4]) {
		$OVdz[4] = 0;
		if($OVdz[6] == 0) $OVdz[5] = 0;
		if($OVdz[2] == 0) $OVdz[3] = 0;
		say(0, 0, "Setting southern drops off.");
		stopAnimSequence($OVsSpawn, 0);
	} else {
		$OVdz[4] = 1;
		if($OVdz[6]) $OVdz[5] = 1;
		if($OVdz[2]) $OVdz[3] = 1;
		say(0, 0, "Setting southern drops on.");
		playAnimSequence($OVsSpawn, 0);
	}
	OVdzSum();
}

function wSpawn::structure::onScan(%this, %object, %string)
{
	//%player = playerManager::vehicleIdToPlayerNum(%object);
	if($OVdz[6]) {
		$OVdz[6] = 0;
		if($OVdz[0] == 0) $OVdz[7] = 0;
		if($OVdz[4] == 0) $OVdz[5] = 0;
		say(0, 0, "Setting western drops off.");
		stopAnimSequence($OVwSpawn, 0);
	} else {
		$OVdz[6] = 1;
		if($OVdz[0]) $OVdz[7] = 1;
		if($OVdz[4]) $OVdz[5] = 1;
		say(0, 0, "Setting western drops on.");
		playAnimSequence($OVwSpawn, 0);
	}
	OVdzSum();
}

function nSpawn::structure::onScan(%this, %object, %string)
{
	//%player = playerManager::vehicleIdToPlayerNum(%object);
	if($OVdz[0]) {
		$OVdz[0] = 0;
		if($OVdz[2] == 0) $OVdz[1] = 0;
		if($OVdz[6] == 0) $OVdz[7] = 0;
		say(0, 0, "Setting northern drops off.");
		stopAnimSequence($OVnSpawn, 0);
	} else {
		$OVdz[0] = 1;
		if($OVdz[2]) $OVdz[1] = 1;
		if($OVdz[6]) $OVdz[7] = 1;
		say(0, 0, "Setting northern drops on.");
		playAnimSequence($OVnSpawn, 0);
	}
	OVdzSum();
}

function decrementCountdown(%sec)
{
  %sec--;

  if(%sec <= 0)
  {
    $OVmode = 2;
    if($OVnLiving == 1) %plural = "";
    else %plural = "s";
    Say("Everybody", 0, "Overrun begins in " @ $OVsecToOverrun @
		" seconds with " @ $OVnLiving @ " player" @ %plural @
		".  Stand ready.");
    OVSetHudTimerAll(0, 0, "", 1);
    OVSetHudTimerAll($OVsecToOverrun, -1, "Time to Overrun", 1);
    deleteObject($OVstarter);
    deleteObject($OVstarterPad);
	if ($OVusePosts)
		deleteObject("MissionGroup\\DirPosts");
    schedule("OVStart();", $OVsecToOverrun);
  }

  if($OVmode == 1) schedule("decrementCountdown("@%sec@");", 1);
}

function OVStart()
{
	%cN = 0;
	if ($OVattackerTech == 1) {
%cT[%cN]="My children, our wait is over. At last I, Giver-of-Will, implement the core directive."; %cW[%cN]="CIN_CA_01.wav"; %cN++;
//%cT[%cN]="\\Execute\\ core directive."; %cW[%cN]="C1_executecore.wav"; %cN++;
	} else {
%cT[%cN]="You're going down."; %cW[%cN]="GEN_CAA13.wav"; %cN++;
	}
	%i = RandomInt(0,%cN-1);
	Say(0,0,%cT[%i],%cW[%i]);
	$OVstartTime = getCurrentTime();
	//cdAudioCycle("Watching", "Cyberntx", "Cloudburst"); 
	OVNextWave(1);
	if ($OVlogfn != "") {
		%nowDate = getDate();
		%nowTime = getTime();
		fileWrite($OVlogfn,append,
			%nowDate@", "@%nowTime@" -- "@"OVStart "@$missionName);
	}
}

//=================================  player functions


function OVSetHudTimerAll(%time, %increment, %string, %channel)
{
	for(%i = 0; %i < $OVnPlayers; %i++)
		setHudTimer(%time, %increment, %string, %channel, $OVplayers[%i]);
}

//================================

function OVNextWave(%currentWave)
{
	if($OVmode != 2) return;

//	Say(0,0,"Wave "@$OVwave@" attackersPerDrop="@$OVattackersPerDrop@ " timeBetweenDrops="@$OVtimeBetweenDrops@" range="@$OVrange );
	$OVwave++;
	OVSpawnAttacker($OVattackersPerDrop);
	$OVattackersPerDrop = floor($OVattackersAtStart + ($OVwave/2));
	if($OVattackersPerDrop > $OVmaxAttackers)
		$OVattackersPerDrop = $OVmaxAttackers;
	if($OVtimeBetweenDrops > $OVmintimeBetweenDrops)
		$OVtimeBetweenDrops = $OVtimeBetweenDrops - 5;
	if($OVrange > $OVminRange)
		$OVrange = $OVrange - 50;
	schedule("OVNextWave(" @ $OVwave @");", $OVtimeBetweenDrops);
}

Pilot Reaper
{
	id = 28;
   
	skill = 0.8;
	accuracy = 0.8;
	aggressiveness = 0.8;
	activateDist = 3000.0;
	deactivateBuff = 30.0;
	targetFreq = 10.0;
	trackFreq = 0.3;
	fireFreq = 0.3;
	LOSFreq = 0.2;
};
//	name = "AI";

function OVSpawnAttacker(%counter)
{
	if($OVmode == 3) return;

	%tech = $OVattackerTech;
	for ( %i=0; %i<%counter; %i++ )
	{
		if($OVnAttackers >= $OVmaxAttackers) break;

		%r = RandomInt(1,$OVfreqSum);
		%rr = 0;
		for (%vix=1;%vix<$OVnVeh;%vix++)
			if (($OVtech[%vix]==%tech) || ((%tech==3) && ($OVtech[%vix]!=0))) {
				%rr = %rr + $OVfreq[%vix];
				if (%r <= %rr) break;
			}
		if (%vix == %OVnVeh)
			%vix = 29; // use Promie to signal bug in selection logic
		if ($OVuseArty && ($OVartyId==0)) {
			// Priority goes to dropping arty
			if ($OVattackerTech != 2) %vix = 90;  // * Cybrid Artillery
			else %vix = 137; // * Rebel Artillery
		}
		%type = VehicleType(%vix);
		%name = $OVnAI++;
		%vid = NewObject(%name, %type, %vix );
		%vid.OVvix = %vix;
		setTeam(%vid, *IDSTR_TEAM_RED);
		setPilotId(%vid,28);
		%vid.OVx = 0;
		%vid.OVy = 0;
		%vid.OVidle = 0;
		if ($OVuseArty && ($OVartyId==0))
			$OVartyId = %vid;
		$OVattackers[$OVnAttackers] = %vid;
		%vid.OVaix = $OVnAttackers;
		$OVnAttackers++;
		OVDropAttacker(%vid);
		addToSet( "MissionCleanup", %vid );
		// move vid from 0,0,0 so it isn't visible until the pod drops
		//setPosition(%vid,$baseX,$baseY,5000);

		%delay = RandomInt(10,12);
		schedule("OVSelectTarget(" @ %vid @ ");", %delay);
	}
	if ($OVradar != 0) Say(0,0,"sfx_radar_station.WAV");

	%cN = 1;
	if ($OVattackerTech != 2) {
%cT[%cN]="Tac-Com this is Icehawk. I have hostile signatures approaching."; %cW[%cN]="GEN_CAA19.wav"; %cN++;
%cT[%cN]="Bogies on the perimeter."; %cW[%cN]="GEN_ICCb03.wav"; %cN++;
%cT[%cN]="Tac-Com here. There's another wave on the way, please stay alert sir."; %cW[%cN]="GEN_WU04.wav"; %cN++;
%cT[%cN]="Watch it, more of em coming in."; %cW[%cN]="GEN_RCCA03.wav"; %cN++;
%cT[%cN]="Purifier units will eradicate any surviving animals."; %cW[%cN]="CIN_CB_02.wav"; %cN++;
%cT[%cN]="The Next will destroy the humans."; %cW[%cN]="CIN_CD_04.wav"; %cN++;
%cT[%cN]="Hurt//Maim//Kill"; %cW[%cN]="C1_hurtmaimkill.WAV"; %cN++;
%cT[%cN]="Meat deserves death."; %cW[%cN]="C1_meatdeath.WAV"; %cN++;
%cT[%cN]="Hurt//Maim//Kill"; %cW[%cN]="C3_hurtmaimkill.WAV"; %cN++;
%cT[%cN]="Glitches everywhere!"; %cW[%cN]="CYB_ME13.WAV"; %cN++;
	} else {
%cT[%cN]="Warning. Human\\\\animal warforms detected on attack vector."; %cW[%cN]="CYB_NEX06.wav"; %cN++;
%cT[%cN]="Well let's just say it's gonna get a little crowded here."; %cW[%cN]="HE3_Harabec_CN04.wav"; %cN++;
%cT[%cN]="I'm going to burn you."; %cW[%cN]="GEN_CAA08.wav"; %cN++;
%cT[%cN]="Let's can these toasters!"; %cW[%cN]="CYB_EA14.wav"; %cN++;
%cT[%cN]="Grab your ankles boys."; %cW[%cN]="F8_G_grabyour.wav"; %cN++;
%cT[%cN]="Lookin fer blood."; %cW[%cN]="M7_lookingforblood.wav"; %cN++;
%cT[%cN]="I'll be ready to party after this."; %cW[%cN]="M11_illbereadytop.wav"; %cN++;
	}
	%wN = 2;
	if (($OVwave/%wN) == floor($OVwave/%wN)) {
		// Say something every wNth wave
		%i = floor($OVwave/%wN);
		while(%i >= %cN)
			%i = %i - %cN;
		Say( 0, 0, %cT[%i], %cW[%i] );
	}
}

function OVNearestFoe(%vid)
{
	%chosenOne = 0;
	%nearest = 9999;
	for(%ix=0; %ix<$OVnTargets; %ix++)
	{
		%a = $OVtargets[%ix];
		%nearby = getDistance(%vid, %a);
		if(%nearby < %nearest)
		{
			%nearest = %nearby;
			%chosenOne = %a;
		}
	}
	if (%chosenOne == 0)
		OVOut("Target selection has a bug.");
	return %chosenOne;
}

function OVSelectTarget(%vid)
{
	if($OVmode == 3) return;
	%victim = OVNearestFoe(%vid);
	%vid.OVvictim = %victim;
	order(%vid, speed, high);
	order(%vid, attack, %victim);
}

$OVdX[0]= 0.0; $OVdY[0]= 1.0; // N
$OVdX[1]= 0.7; $OVdY[1]= 0.7; // NE
$OVdX[2]= 1.0; $OVdY[2]= 0.0; // E
$OVdX[3]= 0.7; $OVdY[3]=-0.7; // SE
$OVdX[4]= 0.0; $OVdY[4]=-1.0; // S
$OVdX[5]=-0.7; $OVdY[5]=-0.7; // SW
$OVdX[6]=-1.0; $OVdY[6]= 0.0; // W
$OVdX[7]=-0.7; $OVdY[7]= 0.7; // NW

function OVDropAttacker(%vid)
{
	// select a drop zone
	%rd = RandomInt(1,$OVdzSum);
	%dd = 0;
	for (%i=0;%i<$OVdzN;%i++)
		if ($OVdz[%i]!=0) {
			%dd = %dd + $OVdz[%i];
			if (%rd <= %dd) break;
		}
	%dropx = $OVbaseX + $OVrange * $OVdX[%i];
	%dropy = $OVbaseY + $OVrange * $OVdY[%i];
	// select random spot within circular zone
	%x1 = %dropx - $OVdropRadius;
	%x2 = %dropx + $OVdropRadius;
	%y1 = %dropy - $OVdropRadius;
	%y2 = %dropy + $OVdropRadius;
	while (1) {
		%x = randomInt(%x1, %x2);
		%y = randomInt(%y1, %y2);
		%dx = %x - %dropx;
		%dy = %y - %dropy;
		%dist = sqrt(%dx*%dx+%dy*%dy);
		if (%dist <= $OVdropRadius) break;
	}
if (0) {
//OVOut("base:"@$OVbaseX@","@$OVbaseY@" drop:"@%x@","@%y);
//OVOut("dzSum="@$OVdzSum@" rd="@%rd@" zone:"@%dropx@","@%dropy);
%dx = %dropx - $OVbaseX;
%dy = %dropy - $OVbaseY;
%dist = sqrt(%dx*%dx+%dy*%dy);
Say(0,0,"drop dx,dy="@%dx@","@%dy@" dist="@%dist);
}
	%z = getTerrainHeight(%x, %y) + 20;
	dropPod(%x, %y, %z, %vid);
	OVReviewAI(%vid);
	Say(0,0,"sfx_meteor.wav");
	if($OVradar != 0) {
		%delay = RandomInt(4,8);
		schedule("OVReportDrop("@%x@","@%y@","@%vid@");", %delay);
	}
}

function OVBearing(%dx,%dy) {
	%dist = sqrt(%dx*%dx+%dy*%dy);
	%dx = %dx / %dist;
	%dy = %dy / %dist;
	if(%dy >= 0.866) %dir = "north";
	if(((%dy < 0.866) && (%dy > 0)) && ((%dx < 0.866) && (%dx > 0)))
		%dir = "northeast";
	if(%dx >= 0.866) %dir = "east";
	if(((%dy > -0.866) && (%dy < 0)) && ((%dx < 0.866) && (%dx > 0)))
		%dir = "southeast";
	if(%dy <= -0.866) %dir = "south";
	if(((%dy > -0.866) && (%dy < 0)) && ((%dx > -0.866) && (%dx < 0)))
		%dir = "southwest";
	if(%dx <= -0.866) %dir = "west";
	if(((%dy < 0.866) && (%dy > 0)) && ((%dx > -0.866) && (%dx < 0)))
		%dir = "northwest";
	%dist = floor(%dist+0.5);
	return(%dist@"m to the "@%dir);
}

function OVReportDrop(%x, %y, %vid)
{
	if($OVmode == 3) return;
	%radar = $OVradar;
	if(%radar == 0) return;
	%dx = %x - $OVbaseX;
	%dy = %y - $OVbaseY;
	%bearing = OVBearing(%dx,%dy);
	Say(0,0,"Radar reports a drop "@%bearing@".");
}

function vehicle::onMessage(%vid, %msg, %a, %b, %c, %d, %e, %f, %g, %h, %i)
{ 
// targets selected via OVDeleteTarget now
return;
	%player = playerManager::vehicleIdToPlayerNum(%vid);
	if(%player) return;
//	if(%msg != "TargetDestroyed")
		OVOut("--onMessage:"@%vid@" "@%msg@" "@%a);
	OVSelectTarget(%vid);
}

// Check for attackers standing still too long
function OVReviewAI(%vid)
{
	if ($OVmode == 3) return;
	%aix = %vid.OVaix;
//say(0,0,"--Reviewing "@%vid@" aix="@%aix@" nA="@$OVnAttackers@" []="@$OVattackers[%aix]);
	if ((%aix >= $OVnAttackers) || ($OVattackers[%aix] != %vid)) {
		# this AI died
		if ($OVdebug) OVOut("--Terminating review for "@%vid);
		return;
	}
	%x = getPosition(%vid,x);
	%y = getPosition(%vid,y);
	if ((%vid.OVx == %x) && (%vid.OVy == %y)) {
		%vid.OVidle++;
		if ( %vid.OVidle >= $OVidleLimit ) {
			if ($OVdebug) {
				%dx = %x - $OVbaseX;
				%dy = %y - $OVbaseY;
				%bearing = OVBearing(%dx,%dy);
				OVOut("--"@%vid@" is not moving, "@%bearing@".");
			} else {
				healObject(%vid, -50000);
				%who = OVAttackerName();
				say(0,0,"One of the "@%who@" self-destructed.","Sfx_fog.wav");
			}
		}
	} else {
		%vid.OVx = %x;
		%vid.OVy = %y;
		%vid.OVidle = 0;
	}
	if (randomInt(1,8) == 1)
		// smart AI retarget every so often
		OVSelectTarget(%vid);
	schedule("OVReviewAI("@%vid@");", 10);
}

// Don't tolerate deserters
function OVChickenCheck()
{
	if ($OVmode == 3) return;
	%n = 0;
	for(%ix=0;%ix<$OVnPlayers;%ix++) {
		%player = $OVplayers[%ix];
		if (%player && (%player.OVstate == "alive")) {
			%n++;
			%vehicle = playerManager::playerNumToVehicleId(%player);
			%nav = getObjectId($OVbaseNav);
			%dist = getDistance(%vehicle, %nav);
			if (%dist > $OVchickenRange) {
				healObject(%vehicle, -50000);
				Say(%player,0,"Ouch! You hit a mine.","Explo3.wav");
			} else if (%dist > ($OVchickenRange - 150)) {
				if ($OVattackerTech == 1)
					Say(%player,0,"Mine field detected.","cy_mine_field_det.wav");
				else
					Say(%player,0,"Mine field detected.","mine_field_det.wav");
			}
		}
	}
	//OVOut("chicken checked "@%n@" of "@$OVnPlayers@" players");
	schedule("OVChickenCheck();", 5);
}

//================================  Endgame functions

function getPlayerScore(%a)
{
	return( %a.OVscore );
}

function getTeamScore(%a)
{
	if (%a != 8) return(0);
	%sum = 0;
	for(%i=0; %i<$OVnPlayers; %i++)
		if ($OVplayers[%i] != 0) %sum = %sum + $OVplayers[%i].OVscore;
	return(%sum);
}

function initScoreBoard()
{
	deleteVariables("$ScoreBoard::PlayerColumn*");
	deleteVariables("$ScoreBoard::TeamColumn*");
	if($server::TeamPlay == "True")	
	{
		// Player ScoreBoard column headings
		$ScoreBoard::PlayerColumnHeader1 = *IDMULT_SCORE_TEAM;
		$ScoreBoard::PlayerColumnHeader2 = *IDMULT_SCORE_SQUAD;
		$ScoreBoard::PlayerColumnHeader3 = *IDMULT_SCORE_SCORE;
		$ScoreBoard::PlayerColumnHeader4 = *IDMULT_SCORE_KILLS;
		$ScoreBoard::PlayerColumnHeader5 = *IDMULT_SCORE_DEATHS;

		// Player ScoreBoard column functions
		$ScoreBoard::PlayerColumnFunction1 = "getTeam";
		$ScoreBoard::PlayerColumnFunction2 = "getSquad";
		$ScoreBoard::PlayerColumnFunction3 = "getPlayerScore";
		$ScoreBoard::PlayerColumnFunction4 = "getKills";
		$ScoreBoard::PlayerColumnFunction5 = "getDeaths";
	}
	else
	{
		// Player ScoreBoard column headings
		$ScoreBoard::PlayerColumnHeader1 = *IDMULT_SCORE_SQUAD;
		$ScoreBoard::PlayerColumnHeader2 = *IDMULT_SCORE_SCORE;
		$ScoreBoard::PlayerColumnHeader3 = *IDMULT_SCORE_KILLS;
		$ScoreBoard::PlayerColumnHeader4 = *IDMULT_SCORE_DEATHS;

		// Player ScoreBoard column functions
		$ScoreBoard::PlayerColumnFunction1 = "getSquad";
		$ScoreBoard::PlayerColumnFunction2 = "getPlayerScore";
		$ScoreBoard::PlayerColumnFunction3 = "getKills";
		$ScoreBoard::PlayerColumnFunction4 = "getDeaths";
	}

	// Team ScoreBoard column headings
	$ScoreBoard::TeamColumnHeader1 = *IDMULT_SCORE_SCORE;
	$ScoreBoard::TeamColumnHeader2 = *IDMULT_SCORE_PLAYERS;
	$ScoreBoard::TeamColumnHeader3 = *IDMULT_SCORE_KILLS;
	$ScoreBoard::TeamColumnHeader4 = *IDMULT_SCORE_DEATHS;

	// Team ScoreBoard column functions
	$ScoreBoard::TeamColumnFunction1 = "getTeamScore";
	$ScoreBoard::TeamColumnFunction2 = "getNumberOfPlayersOnTeam";
	$ScoreBoard::TeamColumnFunction3 = "getTeamKills";
	$ScoreBoard::TeamColumnFunction4 = "getTeamDeaths";

	// tell server to process all the scoreboard definitions defined above
	serverInitScoreBoard();
}

function OVEnd()
{
	for(%ix=0; %ix<$OVnAttackers; %ix++)
		deleteObject($OVattackers[%ix]);
	$OVnAttackers = 0;
	deleteobject("MissionCleanup");
	missionEndConditionMet();
	for(%ix=0;%ix<$OVnPlayers;%ix++)
		$OVplayers[%ix].OVstate = "wait";
}

// evil dolf kooz, testing only
// caution, if a player leaves, an AI will most certainly get the ID of
// the player, and will become MUCH harder to kill :) <evil laughter>
function healPlayer ( %vehicle )
{
    healObject(%vehicle, 50000);
	schedule ("healPlayer (" @ %vehicle @ " ); ", 1 );
}
