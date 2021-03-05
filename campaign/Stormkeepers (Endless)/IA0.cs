// IA0

MissionBriefInfo missionData
{
   title  			    = "Intro Mission Title";
   planet               = *IDSTR_PLANET_MARS;           
   campaign  			= "Operation Peek";		   
   dateOnMissionEnd     = *IDSTR_HA0_DATE; 			  
   shortDesc            = "Identify The Cargo";	   
   longDescRichText     = "Get in, find out what they are hiding, get out. Don't get caught.";		   
   // media                = *IDSTR_HA0_MEDIA; // smk file.
   nextMission          = "IA1";
   successDescRichText  = "Good job recruit. At least now we know what we're dealing with, and how to prep for it.";
   failDescRichText     = "This mission is a failure. We need to attempt another recon mission to find out what they are dealing iwth before they move this tech again.";
   location             = "Offsite Rebel Research Center";
   // successWavFile       = "HA0_Debriefing.wav";
   // endCinematicSmk      = "cin_HA.smk";
   soundvol             = "ha0.vol";
};

MissionBriefObjective missionObjective1
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= "";
	longTxt		= *IDSTR_HA0_OBJ1_LONG;
	// bmpname		= *IDSTR_HA0_OBJ1_BMPNAME;
};

MissionBriefObjective missionObjective1
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= *IDSTR_HA0_OBJ1_SHORT;
	longTxt		= *IDSTR_HA0_OBJ1_LONG;
	// bmpname		= *IDSTR_HA0_OBJ1_BMPNAME;
}; 

function onMissionStart()
{
    dbecho(3, "TRIGGER: onMissionStart();");
    cdAudioCycle(ss3, cyberntx, ss4);
}

function onSPClientInit()
{
    dbecho(3, "TRIGGER: onSPClientInit();");
    marsSounds();
    windSounds();
}

function player::onAdd( %this )
{
    dbecho(3, "TRIGGER: player::onAdd(" @ %this @ ");");
    // This makes no sense, since the game's first player number for single campaign
    // is 2049
	$ThePlayer = %this;
}

