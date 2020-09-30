//------------------------------------------------------------------------------
//
// Default Mission Behavior
//


//--------------------------------------
// the default pilots -- very important!
Pilot pilotWeakAggressive
{
   id = 16;
   
   skill = 0.2;
   accuracy = 0.2;
   aggressiveness = 0.8;
   activateDist = 400.0;
   deactivateBuff = 200.0;
   targetFreq = 2.0;
   trackFreq = 2.3;
   fireFreq = 2.0;
   LOSFreq = 1.2;
   orderFreq = 8.0;
};

Pilot pilotWeakCautious
{
   id = 17;
   
   skill = 0.2;
   accuracy = 0.2;
   aggressiveness = 0.2;
   activateDist = 300.0;
   deactivateBuff = 100.0;
   targetFreq = 6.0;
   trackFreq = 1.5;
   fireFreq = 3.0;
   LOSFreq = 1.0;
   orderFreq = 4.0;
};

Pilot pilotAverageAggressive
{
   id = 0;  // the original default
   
   skill = 0.4;
   accuracy = 0.4;
   aggressiveness = 0.8;
   activateDist = 400.0;
   deactivateBuff = 200.0;
   targetFreq = 1.8;
   trackFreq = 2.0;
   fireFreq = 1.5;
   LOSFreq = 2.0;
   orderFreq = 7.0;
};

Pilot pilotAverageCautious
{
   id = 18;
   
   skill = 0.4;
   accuracy = 0.4;
   aggressiveness = 0.2;
   activateDist = 300.0;
   deactivateBuff = 100.0;
   targetFreq = 5.4;
   trackFreq = 1.0;
   fireFreq = 2.0;
   LOSFreq = 0.8;
   orderFreq = 3.5;
};

Pilot pilotGoodAggressive
{
   id = 19;
   
   skill = 0.6;
   accuracy = 0.6;
   aggressiveness = 0.8;
   activateDist = 400.0;
   deactivateBuff = 200.0;
   targetFreq = 1.6;
   trackFreq = 1.2;
   fireFreq = 1.0;
   LOSFreq = 0.8;
   orderFreq = 6.0;
};

Pilot pilotGoodCautious
{
   id = 20;
   
   skill = 0.6;
   accuracy = 0.6;
   aggressiveness = 0.2;
   activateDist = 300.0;
   deactivateBuff = 100.0;
   targetFreq = 4.8;
   trackFreq = 0.8;
   fireFreq = 1.5;
   LOSFreq = 0.6;
   orderFreq = 3.0;
};

Pilot pilotExcellentAggressive
{
   id = 21;
   
   skill = 0.8;
   accuracy = 0.8;
   aggressiveness = 0.8;
   activateDist = 400.0;
   deactivateBuff = 200.0;
   targetFreq = 1.4;
   trackFreq = 1.0;
   fireFreq = 0.5;
   LOSFreq = 0.6;
   orderFreq = 5.0;
};

Pilot pilotExcellentCautious
{
   id = 22;
   
   skill = 0.8;
   accuracy = 0.8;
   aggressiveness = 0.2;
   activateDist = 300.0;
   deactivateBuff = 100.0;
   targetFreq = 4.2;
   trackFreq = 0.6;
   fireFreq = 1.0;
   LOSFreq = 0.4;
   orderFreq = 2.5;
};

Pilot pilotEliteAggressive
{
   id = 23;
   
   skill = 1.0;
   accuracy = 1.0;
   aggressiveness = 0.8;
   activateDist = 400.0;
   deactivateBuff = 200.0;
   targetFreq = 1.2;
   trackFreq = 0.4;
   fireFreq = 0.2;
   LOSFreq = 0.4;
   orderFreq = 4.0;
};

Pilot pilotEliteCautious
{
   id = 24;
   
   skill = 1.0;
   accuracy = 1.0;
   aggressiveness = 0.2;
   activateDist = 300.0;
   deactivateBuff = 100.0;
   targetFreq = 3.6;
   trackFreq = 0.2;
   fireFreq = 0.75;
   LOSFreq = 0.2;
   orderFreq = 2.0;
};


//-------------------------------------------------------------------------------
function updatePlanetInventory(%key)
{
    if(%key == ha0)
    {
    
		InventoryVehicleAdjust(	 mars,	52,	1	);	#Pirate Emancipator (outrider)
		InventoryweaponAdjust(	 -1,	107,	2	);	
		InventoryComponentAdjust( mars,		104,	1	);	
		InventoryComponentAdjust( mars,		204,	1	);	
		InventoryComponentAdjust( mars,		801,	1	);	
		InventoryComponentAdjust( mars,		301,	1	);	
		InventoryComponentAdjust( mars,		927,	1	);	
		InventoryComponentAdjust( mars,		400,	1	);	
		InventoryComponentAdjust( mars,		810,	1	);	
		InventoryComponentAdjust( mars,		860,	1	);	
        campaignInfo.techlevel=4;
        InventoryweaponAdjust(	   -1,	113,	1	);	#MFAC's are COOL.
    
    }
    
    if(%key == ha1)
    {
		InventoryVehicleAdjust(	mars,	31,	1	);	#Avenger
		InventoryweaponAdjust(	  -1,	106,	2	);	
		InventoryComponentAdjust(  mars,		101,	1	);	
		InventoryComponentAdjust(  mars,		202,	1	);	
		InventoryComponentAdjust(  mars,		801,	1	);	
		InventoryComponentAdjust(  mars,		927,	1	);	
		InventoryComponentAdjust(  mars,		400,	1	);	
		InventoryComponentAdjust(  mars,		885,	1	);	
		InventoryComponentAdjust(  -1,		811,	1	);	
		InventoryComponentAdjust(  mars,		865,	1	);	
        					        
		InventoryVehicleAdjust(	 mars,	52,	1	);	#Pirate Emancipator (outrider)
		InventoryweaponAdjust(	 -1,	107,	2	);	
		InventoryComponentAdjust( mars,		104,	1	);	
		InventoryComponentAdjust( mars,		204,	1	);	
		InventoryComponentAdjust( mars,		801,	1	);	
		InventoryComponentAdjust( mars,		301,	1	);	
		InventoryComponentAdjust( mars,		927,	1	);	
		InventoryComponentAdjust( mars,		400,	1	);	
		InventoryComponentAdjust( -1,		810,	1	);	
		InventoryComponentAdjust( -1,		860,	1	);	

        InventoryComponentAdjust(  -1,	914,	1	);	#UAP AMMO PACK


    }
    
    if(%key == ha2)
    {
        InventoryPilotSet(	mars,	11,	TRUE	);	#Saxon, Rebel	
   
        InventoryWeaponAdjust(	-1,	103,	2	);	#Comp Laser
        InventoryWeaponAdjust(	-1,	105,	2	);	#Emp
        InventoryWeaponAdjust(	-1,	120,	2	);	#Hvy Blast Can
        InventoryWeaponAdjust(	-1,	128,	2	);	#SWARM 6
        InventoryWeaponAdjust(	-1,	134,	2	);	#Proximity 6 
   }
      
    if(%key == ha3)
    {
        					
		InventoryVehicleAdjust(	mars,	32,	1	);	#Dreadlock
		InventoryweaponAdjust(	 -1,	108,	2	);	
		InventoryComponentAdjust( mars,		108,	1	);	
		InventoryComponentAdjust( mars,		204,	1	);	
		InventoryComponentAdjust( mars,		801,	1	);	
		InventoryComponentAdjust( mars,		927,	1	);	
		InventoryComponentAdjust( mars,		412,	1	);	
		InventoryComponentAdjust( mars,		865,	1	);	
		InventoryComponentAdjust( mars,		830,	1	);	
		InventoryComponentAdjust( mars,		885,	1	);	

		InventoryVehicleAdjust(	mars,	30,	1	);	#Emancipator
		InventoryweaponAdjust(	mars,	107,	2	);	
		InventoryComponentAdjust( mars,		105,	1	);	
		InventoryComponentAdjust( mars,		204,	1	);	
		InventoryComponentAdjust( mars,		801,	1	);	
		InventoryComponentAdjust( mars,		306,	1	);	
		InventoryComponentAdjust( mars,		927,	1	);	
		InventoryComponentAdjust( mars,		412,	1	);	
		InventoryComponentAdjust( mars,		840,	1	);	
		InventoryComponentAdjust( mars,		830,	1	);	
       					
        InventoryVehicleAdjust(	    -1, 33,    1	);  #DiMarco's Olympian
        InventoryweaponAdjust(	    -1, 107,	2	);
        InventoryweaponAdjust(	    -1, 115,	2	);
        InventoryweaponAdjust(	    -1, 105,	2	);
        InventoryComponentAdjust(	-1,	114,	1	);
        InventoryComponentAdjust(	-1,	205,	1	);
        InventoryComponentAdjust(	-1,	801,	1	);
        InventoryComponentAdjust(	-1,	305,	1	);
        InventoryComponentAdjust(	-1,	928,	1	);
        InventoryComponentAdjust(	-1,	411,	1	);
        InventoryComponentAdjust(	-1,	810,	1	);
        InventoryComponentAdjust(	-1,	865,	1	);

        InventoryComponentAdjust(  mars,		885,	2	);	

        campaignInfo.techlevel=5;
					
        InventoryPilotSet(	mars,	2,	TRUE	);	# Hunter Otobe, Rebel	
    }
    
    if(%key == ha4)
    {
         //call after HA4					

        InventoryVehicleAdjust(		mars, 4,  1	);	#Terran Talon
        InventoryweaponAdjust(		mars, 101,  	2	);	
        InventoryComponentAdjust(	mars, 101,		1	);	
        InventoryComponentAdjust(	mars, 202,		1	);	
        InventoryComponentAdjust(	mars, 801,		1	);	
        InventoryComponentAdjust(	mars, 300,		1	);	
        InventoryComponentAdjust(	mars, 926,		1	);	
        InventoryComponentAdjust(	mars, 410,		1	);	
        InventoryComponentAdjust(	mars, 810,		1	);	
        InventoryComponentAdjust(	mars, 850,		1	);	
        
        InventoryPilotSet(	mars,	10,	TRUE	);	#Riana, Rebel	
        InventoryPilotSet(	mars,	12,	TRUE	);	#Verity, Rebel	
        InventoryPilotSet(	mars,	3,	TRUE	);	# Bioderm Delta-Six Niner, Rebel	
        InventoryPilotSet(	mars,	14,	TRUE	);	#Generic		
        InventoryPilotSet(	mars,	13,	TRUE	);	#Generic	

        InventoryVehicleAdjust(	-1,	41,	1	);	#Predator
        InventoryweaponAdjust(	-1,	108,	2	);	
        InventoryComponentAdjust(	-1,	110,	1	);	
        InventoryComponentAdjust(	-1,	204,	1	);	
        InventoryComponentAdjust(	-1,	802,	1	);	
        InventoryComponentAdjust(	-1,	928,	1	);	
        InventoryComponentAdjust(	-1,	413,	1	);	
        InventoryComponentAdjust(	-1,	820,	1	);	
        InventoryComponentAdjust(	-1,	811,	1	);	
        InventoryComponentAdjust(	-1,	831,	1	);	

    }

    if(%key == ha5)
    {
                //call after HA5					
        InventoryVehicleAdjust(	    mars, 33,    1	);  #Olympian
        InventoryweaponAdjust(	    mars, 107,	2	);
        InventoryweaponAdjust(	    mars, 115,	2	);
        InventoryweaponAdjust(	    mars, 105,	2	);
        InventoryComponentAdjust(	mars,	114,	1	);
        InventoryComponentAdjust(	mars,	205,	1	);
        InventoryComponentAdjust(	mars,	801,	1	);
        InventoryComponentAdjust(	mars,	305,	1	);
        InventoryComponentAdjust(	mars,	928,	1	);
        InventoryComponentAdjust(	mars,	411,	1	);
        InventoryComponentAdjust(	mars,	810,	1	);
        InventoryComponentAdjust(	mars,	865,	1	);

		InventoryVehicleAdjust(	mars,	31,	1	);	#Avenger
		InventoryweaponAdjust(	mars,	106,	2	);	
		InventoryComponentAdjust(  mars,		101,	1	);	
		InventoryComponentAdjust(  mars,		202,	1	);	
		InventoryComponentAdjust(  mars,		801,	1	);	
		InventoryComponentAdjust(  mars,		927,	1	);	
		InventoryComponentAdjust(  mars,		400,	1	);	
		InventoryComponentAdjust(  mars,		865,	1	);	
		InventoryComponentAdjust(  mars,		811,	1	);	
		InventoryComponentAdjust(  mars,		865,	1	);	

        					        
		InventoryVehicleAdjust(	mars,	30,	1	);	#Emancipator
		InventoryweaponAdjust(	mars,	107,	2	);	
		InventoryComponentAdjust( mars,		105,	1	);	
		InventoryComponentAdjust( mars,		204,	1	);	
		InventoryComponentAdjust( mars,		801,	1	);	
		InventoryComponentAdjust( mars,		306,	1	);	
		InventoryComponentAdjust( mars,		927,	1	);	
		InventoryComponentAdjust( mars,		412,	1	);	
		InventoryComponentAdjust( mars,		840,	1	);	
		InventoryComponentAdjust( mars,		830,	1	);	

		InventoryVehicleAdjust(	mars,	32,	1	);	#Dreadlock
		InventoryweaponAdjust(	 -1,	108,	2	);	
		InventoryComponentAdjust( mars,		108,	1	);	
		InventoryComponentAdjust( mars,		204,	1	);	
		InventoryComponentAdjust( mars,		801,	1	);	
		InventoryComponentAdjust( mars,		927,	1	);	
		InventoryComponentAdjust( mars,		412,	1	);	
		InventoryComponentAdjust( mars,		865,	1	);	
		InventoryComponentAdjust( mars,		830,	1	);	
		InventoryComponentAdjust( mars,		885,	1	);	
        					


    }

    if(%key == ha6)
    {
            //call after HA6					
        InventoryVehicleAdjust(	mars,	6,	1	);	#Paladin
        InventoryweaponAdjust(	mars,	129,	2	);	
        InventoryComponentAdjust(	mars,	109,	1	);	
        InventoryComponentAdjust(	mars,	202,	1	);	
        InventoryComponentAdjust(	mars,	802,	1	);	
        InventoryComponentAdjust(	mars,	928,	1	);	
        InventoryComponentAdjust(	mars,	410,	1	);	
        InventoryComponentAdjust(	mars,	810,	1	);	
        InventoryComponentAdjust(	mars,	820,	1	);	
        InventoryComponentAdjust(	mars,	860,	1	);	
        
    
        InventoryVehicleAdjust(	mars,	2,	1	);	#Terran Minotaur
        InventoryweaponAdjust(	mars,	101,	2	);	
        InventoryweaponAdjust(	mars,	102,	2	);	
        InventoryComponentAdjust(	mars,	104,	1	);	
        InventoryComponentAdjust(	mars,	203,	1	);	
        InventoryComponentAdjust(	mars,	801,	1	);	
        InventoryComponentAdjust(	mars,	302,	1	);	
        InventoryComponentAdjust(	mars,	927,	1	);	
        InventoryComponentAdjust(	mars,	400,	1	);	
        InventoryComponentAdjust(	mars,	865,	1	);	
		InventoryComponentAdjust(	mars,	870,	1	);	

    
    }
    
    if(%key == hb1)
    {
            //call after HB1					
        InventoryVehicleAdjust(	mars,	5,	1	);	#Terran Basilisk
        InventoryweaponAdjust(	mars,	105,	2	);	
        InventoryweaponAdjust(	mars,	125,	2	);	
        InventoryComponentAdjust(	mars,	108,	1	);	
        InventoryComponentAdjust(	mars,	201,	1	);	
        InventoryComponentAdjust(	mars,	801,	1	);	
        InventoryComponentAdjust(	mars,	303,	1	);	
        InventoryComponentAdjust(	mars,	929,	1	);	
        InventoryComponentAdjust(	mars,	408,	1	);	
        InventoryComponentAdjust(	mars,	810,	1	);	
        InventoryComponentAdjust(	mars,	840,	1	);	
        
		InventoryVehicleAdjust(	mars,	1,	1	);	#Terran Apocalypse
		InventoryweaponAdjust(	mars,	117,	4	);	
		InventoryweaponAdjust(	mars,	105,	2	);	
		InventoryComponentAdjust(	mars,	108,	1	);	
		InventoryComponentAdjust(	mars,	202,	1	);	
		InventoryComponentAdjust(	mars,	801,	1	);	
		InventoryComponentAdjust(	mars,	304,	1	);	
		InventoryComponentAdjust(	mars,	928,	1	);	
		InventoryComponentAdjust(	mars,	400,	1	);	
		InventoryComponentAdjust(	mars,	820,	1	);	
		InventoryComponentAdjust(	mars,	850,	1	);	
					

        InventoryweaponAdjust(	mars,	115,	2	);	
        InventoryweaponAdjust(	mars,	116,	2	);	


        campaignInfo.techlevel=6;

    }
    
    if(%key == hb3)
    {
            //call after HB3					


		InventoryVehicleAdjust(	mars,	3,	1	);	#Terran Gorgon
		InventoryweaponAdjust(	mars,	105,	2	);	
		InventoryweaponAdjust(	mars,	119,	2	);	
		InventoryComponentAdjust(	mars,	113,	1	);	
		InventoryComponentAdjust(	mars,	204,	1	);	
		InventoryComponentAdjust(	mars,	801,	1	);	
		InventoryComponentAdjust(	mars,	305,	1	);	
		InventoryComponentAdjust(	mars,	927,	1	);	
		InventoryComponentAdjust(	mars,	400,	1	);	
		InventoryComponentAdjust(	mars,	811,	1	);	
		InventoryComponentAdjust(	mars,	850,	1	);	

		InventoryVehicleAdjust(	mars,	7,	1	);	#Myrmidon
		InventoryweaponAdjust(	mars,	120,	2	);	
		InventoryComponentAdjust(	mars,	113,	1	);	
		InventoryComponentAdjust(	mars,	200,	1	);	
		InventoryComponentAdjust(	mars,	801,	1	);	
		InventoryComponentAdjust(	mars,	927,	1	);	
		InventoryComponentAdjust(	mars,	412,	1	);	
		InventoryComponentAdjust(	mars,	914,	1	);	
		InventoryComponentAdjust(	mars,	820,	1	);	
		InventoryComponentAdjust(	mars,	811,	1	);	


        InventoryPilotSet(	mars,	1,	TRUE	);	# Collossa (Jaguar), Imperial	
        InventoryPilotSet(	mars,	5,	TRUE	);	# Deathwish, Imperial	
        InventoryPilotSet(	mars,	6,	TRUE	);	# Hangman, Imperial	
        InventoryPilotSet(	mars,	8,	TRUE	);	#Tigress, Imperial	
        InventoryPilotSet(	mars,	9,	TRUE	);	#Oliphant, Imperial	
    }
    
    if(%key ==  hc1)
    {
		InventoryVehicleAdjust(	venus,	15,	1	);	#Knight Paladin
		InventoryweaponAdjust(	venus,	119,	2	);	
		InventoryComponentAdjust(	venus,	110,	1	);	
		InventoryComponentAdjust(	venus,	201,	1	);	
		InventoryComponentAdjust(	venus,	802,	1	);	
		InventoryComponentAdjust(	venus,	928,	1	);	
		InventoryComponentAdjust(	venus,	411,	1	);	
		InventoryComponentAdjust(	venus,	820,	1	);	
		InventoryComponentAdjust(	venus,	811,	1	);	
		InventoryComponentAdjust(	venus,	885,	1	);	

		InventoryVehicleAdjust(	venus,	17,	1	);	#Knight Disrupter
		InventoryweaponAdjust(	venus,	3,	1	);	
		InventoryComponentAdjust(	venus,	110,	1	);	
		InventoryComponentAdjust(	venus,	204,	1	);	
		InventoryComponentAdjust(	venus,	801,	1	);	
		InventoryComponentAdjust(	venus,	928,	1	);	
		InventoryComponentAdjust(	venus,	400,	1	);	
		InventoryComponentAdjust(	venus,	810,	1	);	
		InventoryComponentAdjust(	venus,	912,	1	);	
		InventoryComponentAdjust(	venus,	885,	1	);	
       
     
		InventoryVehicleAdjust(	venus,	14,	1	);	#Knight's Basilisk
		InventoryweaponAdjust(	venus,	124,	2	);	
		InventoryweaponAdjust(	venus,	127,	2	);	
		InventoryComponentAdjust(	venus,	109,	1	);	
		InventoryComponentAdjust(	venus,	202,	1	);	
		InventoryComponentAdjust(	venus,	801,	1	);	
		InventoryComponentAdjust(	venus,	303,	1	);	
		InventoryComponentAdjust(	venus,	930,	1	);	
		InventoryComponentAdjust(	venus,	408,	1	);	
		InventoryComponentAdjust(	venus,	810,	1	);	
		InventoryComponentAdjust(	venus,	840,	1	);	

        
        campaignInfo.techlevel=7;
    }
    
    if(%key == hc2)
    {
        					
        InventoryVehicleAdjust(	venus,	10,	1	);	#Knight's Apocalypse
        InventoryweaponAdjust(	venus,	102,	2	);	
        InventoryweaponAdjust(	venus,	117,	2	);	
        InventoryweaponAdjust(	venus,	126,	2	);	
        InventoryComponentAdjust(	venus,	109,	1	);	
        InventoryComponentAdjust(	venus,	202,	1	);	
        InventoryComponentAdjust(	venus,	801,	1	);	
        InventoryComponentAdjust(	venus,	305,	1	);	
        InventoryComponentAdjust(	venus,	928,	1	);	
        InventoryComponentAdjust(	venus,	413,	1	);	
        InventoryComponentAdjust(	venus,	820,	1	);	
        InventoryComponentAdjust(	venus,	850,	1	);	

        InventoryVehicleAdjust(	venus,	12,	1	);	#Knight's Gorgon
        InventoryweaponAdjust(	venus,	102,	2	);	
        InventoryweaponAdjust(	venus,	129,	2	);	
        InventoryComponentAdjust(	venus,	114,	1	);	
        InventoryComponentAdjust(	venus,	204,	1	);	
        InventoryComponentAdjust(	venus,	801,	1	);	
        InventoryComponentAdjust(	-1,	307,	1	);	
        InventoryComponentAdjust(	venus,	927,	1	);	
        InventoryComponentAdjust(	venus,	400,	1	);	
        InventoryComponentAdjust(	venus,	820,	1	);	
        InventoryComponentAdjust(	venus,	850,	1	);	

        InventoryVehicleAdjust(	venus,	16,	1	);	#Knight Myrmidon
        InventoryweaponAdjust(	venus,	110,	2	);	
        InventoryComponentAdjust(	venus,	114,	1	);	
        InventoryComponentAdjust(	venus,	205,	1	);	
        InventoryComponentAdjust(	venus,	801,	1	);	
        InventoryComponentAdjust(	venus,	927,	1	);	
        InventoryComponentAdjust(	venus,	408,	1	);	
        InventoryComponentAdjust(	venus,	900,	1	);	
        InventoryComponentAdjust(	venus,	820,	1	);	
        InventoryComponentAdjust(	venus,	811,	1	);	

        InventoryWeaponAdjust(	 -1,	104,	2	);	#Twin Laser


    }
    
    if(%key == hc3)
    {
        campaignInfo.techlevel=8;
    }

    if(%key == hd1)
    {
        campaignInfo.techlevel=9;
    }

    if(%key == hd2)
    {
        campaignInfo.techlevel=10;
        InventoryWeaponAdjust(	 -1,	114,	2	);	#Nano Infuser
    }

//Cybrid Missions

    if(%key == CA0)
    {
		InventoryComponentAdjust(	-1,	820,	1	);	#Thermal Diffuser
	}


    if(%key == CA1)
    {
        campaignInfo.techlevel=4;
        InventoryPilotSet(mercury,	3,	TRUE	);	#Eats Only Heads
        InventoryPilotSet(mercury,	10,	TRUE	);	#Generic Cybrid #4

        InventoryVehicleAdjust(	mercury,	21,	1	);	#Goad
        InventoryweaponAdjust(	mercury,	102,	2	);	
        InventoryComponentAdjust( mercury,		132,	1	);	
        InventoryComponentAdjust( mercury,		227,	1	);	
        InventoryComponentAdjust( mercury,		806,	1	);	
        InventoryComponentAdjust( mercury,		328,	1	);	
        InventoryComponentAdjust( mercury,		929,	1	);	
        InventoryComponentAdjust( mercury,		432,	1	);	
        InventoryComponentAdjust( mercury,		812,	1	);	
        InventoryComponentAdjust( mercury,		840,	1	);	
    }

    if(%key == CA2)
    {
        InventoryPilotSet(mercury,	7,	TRUE	);	#Generic Cybrid	#1 (metagen)
        InventoryPilotSet(mercury,	2,	TRUE	);	#Plague Dog
        InventoryVehicleAdjust(	mercury,	21,	1	);	#Goad
        InventoryweaponAdjust(	mercury,	102,	2	);	
        InventoryComponentAdjust( mercury,		132,	1	);	
        InventoryComponentAdjust( mercury,		227,	1	);	
        InventoryComponentAdjust( mercury,		806,	1	);	
        InventoryComponentAdjust( mercury,		328,	1	);	
        InventoryComponentAdjust( mercury,		929,	1	);	
        InventoryComponentAdjust( mercury,		432,	1	);	
        InventoryComponentAdjust( mercury,		812,	1	);	
        InventoryComponentAdjust( mercury,		840,	1	);	
   
    }


    if(%key == CA3)
    {
        InventoryVehicleAdjust(	mercury,	22,	1	);	#Shepherd
        InventoryweaponAdjust(	mercury,	118,	2	);	
        InventoryweaponAdjust(	mercury,	104,	2	);	
        InventoryComponentAdjust(	mercury,	136,	1	);	
        InventoryComponentAdjust(	mercury,	228,	1	);	
        InventoryComponentAdjust(	mercury,	807,	1	);	
        InventoryComponentAdjust(	-1,	332,	1	);	
        InventoryComponentAdjust(	-1,	930,	1	);	
        InventoryComponentAdjust(	mercury,	427,	1	);	
        InventoryComponentAdjust(	mercury,	845,	1	);	
        InventoryComponentAdjust(	mercury,	850,	1	);	

    }
	


    if(%key == CA4)
    {
        campaignInfo.techlevel=5;
        InventoryComponentAdjust(	-1,	230,	1	);	
    }

#moon
    else if(%key == CB1)
    {

        campaignInfo.techlevel=6;
        InventoryVehicleAdjust(	moon,	22,	1	);	#Shepherd
        InventoryweaponAdjust(	moon,	118,	2	);	
        InventoryweaponAdjust(	moon,	104,	2	);	
        InventoryComponentAdjust(	moon,	136,	1	);	
        InventoryComponentAdjust(	moon,	228,	1	);	
        InventoryComponentAdjust(	moon,	807,	1	);	
        InventoryComponentAdjust(	-1,	332,	1	);	
        InventoryComponentAdjust(	-1,	930,	1	);	
        InventoryComponentAdjust(	moon,	427,	1	);	
        InventoryComponentAdjust(	moon,	845,	1	);	
        InventoryComponentAdjust(	moon,	850,	1	);	

    }

    if(%key == CB3)
    {
            
    }

    if(%key == CB4)
    {
        campaignInfo.techlevel=7;
    }

#ice
    if(%key == CC1)
    {
        InventoryPilotSet(ice,	4,	TRUE	);	#Sepsis
    
    }

    if(%key == CC2)
    {
		InventoryVehicleAdjust(	ice,	22,	1	);	#Shepherd
		InventoryweaponAdjust(	ice,	118,	2	);	
		InventoryweaponAdjust(	ice,	104,	2	);	
		InventoryComponentAdjust(	ice,	136,	1	);	
		InventoryComponentAdjust(	ice,	228,	1	);	
		InventoryComponentAdjust(	ice,	807,	1	);	
		InventoryComponentAdjust(	ice,	332,	1	);	
		InventoryComponentAdjust(	ice,	930,	1	);	
		InventoryComponentAdjust(	ice,	427,	1	);	
		InventoryComponentAdjust(	ice,	845,	1	);	
		InventoryComponentAdjust(	ice,	850,	1	);	

        InventoryPilotSet(ice,	5,	TRUE	);	#Tyranny

        campaignInfo.techlevel=8;
    }

    if(%key == CC3)
    {

    }

#temperate
    if(%key == CD1)
    {
        campaignInfo.techlevel=9;
        InventoryPilotSet(temperate,	8,	TRUE	);	#Generic Cybrid	#2 (metagen)
    
    }

    if(%key == CD2)
    {
    }

    if(%key == CD3)
    {
		InventoryVehicleAdjust(	temperate,	23,	1	);	#Adjudicator
		InventoryweaponAdjust(	temperate,	121,	2	);	
		InventoryweaponAdjust(	temperate,	104,	1	);	
		InventoryweaponAdjust(	temperate,	142,	1	);	
		InventoryComponentAdjust(	temperate,	139,	1	);	
		InventoryComponentAdjust(	temperate,	229,	1	);	
		InventoryComponentAdjust(	temperate,	806,	1	);	
		InventoryComponentAdjust(	temperate,	331,	1	);	
		InventoryComponentAdjust(	temperate,	927,	1	);	
		InventoryComponentAdjust(	temperate,	432,	1	);	
		InventoryComponentAdjust(	temperate,	865,	1	);	
		InventoryComponentAdjust(	temperate,	813,	1	);	
    }

    if(%key == CD4)
    {
        campaignInfo.techlevel=10;
    
    }

#desert
    if(%key == CE1)
    {
        InventoryVehicleAdjust(	desert,	22,	1	);	#Shepherd
        InventoryweaponAdjust(	desert,	118,	2	);	
        InventoryweaponAdjust(	desert,	104,	2	);	
        InventoryComponentAdjust(	desert,	136,	1	);	
        InventoryComponentAdjust(	desert,	228,	1	);	
        InventoryComponentAdjust(	desert,	807,	1	);	
        InventoryComponentAdjust(	desert,	332,	1	);	
        InventoryComponentAdjust(	desert,	930,	1	);	
        InventoryComponentAdjust(	desert,	427,	1	);	
        InventoryComponentAdjust(	desert,	845,	1	);	
        InventoryComponentAdjust(	desert,	850,	1	);	
        					
        InventoryVehicleAdjust(	desert,	23,	1	);	#Adjudicator
        InventoryweaponAdjust(	desert,	121,	2	);	
        InventoryweaponAdjust(	desert,	104,	1	);	
        InventoryweaponAdjust(	desert,	142,	1	);	
        InventoryComponentAdjust(	desert,	139,	1	);	
        InventoryComponentAdjust(	desert,	229,	1	);	
        InventoryComponentAdjust(	desert,	806,	1	);	
        InventoryComponentAdjust(	desert,	331,	1	);	
        InventoryComponentAdjust(	desert,	927,	1	);	
        InventoryComponentAdjust(	desert,	432,	1	);	
        InventoryComponentAdjust(	desert,	865,	1	);	
        InventoryComponentAdjust(	desert,	813,	1	);	
        					
        InventoryVehicleAdjust(	desert,	24,	1	);	#Executioner
        InventoryweaponAdjust(	desert,	130,	2	);	
        InventoryweaponAdjust(	desert,	109,	2	);	
        InventoryComponentAdjust(	desert,	143,	1	);	
        InventoryComponentAdjust(	desert,	230,	1	);	
        InventoryComponentAdjust(	desert,	806,	1	);	
        InventoryComponentAdjust(	desert,	333,	1	);	
        InventoryComponentAdjust(	desert,	927,	1	);	
        InventoryComponentAdjust(	desert,	431,	1	);	
        InventoryComponentAdjust(	desert,	812,	1	);	
        InventoryComponentAdjust(	desert,	865,	1	);	

        InventoryVehicleAdjust(	desert,	25,	1	);	#Bolo
        InventoryweaponAdjust(	desert,	111,	2	);	
        InventoryComponentAdjust(	desert,	133,	1	);	
        InventoryComponentAdjust(	desert,	230,	1	);	
        InventoryComponentAdjust(	desert,	806,	1	);	
        InventoryComponentAdjust(	desert,	927,	1	);	
        InventoryComponentAdjust(	desert,	428,	1	);	
        InventoryComponentAdjust(	desert,	865,	1	);	
        InventoryComponentAdjust(	desert,	830,	1	);	
        InventoryComponentAdjust(	desert,	885,	1	);	
    
    }

    if(%key == CE2)
    {
    }

    if(%key == CE3)
    {
    }

}


//--------------------------------------
function missionMsg(%_1, %_2, %_3, %_4, %_5, %_6, %_7, %_8, %_9, %_10)
{
   if ( !isMultiplayer() )
      return;

   if ($missionLogFile == "")
      return;

   // log to mission log file w/o date/time stamp
   fileWrite($missionLogFile, append, %_1, %_2, %_3, %_4, %_5, %_6, %_7, %_8, %_9, %_10);
}

function missionLog(%_1, %_2, %_3, %_4, %_5, %_6, %_7, %_8, %_9, %_10)
{
   if ( !isMultiplayer() )
      return;

   if ($missionLogFile == "")
      return;

   // log to mission log file WITH date/time stamp
   fileWrite($missionLogFile, append, getDate(1), ": ", %_1, %_2, %_3, %_4, %_5, %_6, %_7, %_8, %_9, %_10);
}


//--------------------------------------
function logStandardMissionStart()
{
   if ( !isMultiplayer() )
      return;

   if ($missionLogFile == "")
      return;

   missionMsg("----------------------------------------------------------------------");
   missionLog("Mission Started");
   missionMsg("   Mission:              ", $server::Mission);
   if ($server::MaxPlayers            != "") { missionMsg("   MaxPlayers:           ", $server::MaxPlayers); }
   if ($server::SpawnLimit            != "") { missionMsg("   SpawnLimit:           ", $server::SpawnLimit); }               
   if ($server::FragLimit             != "") { missionMsg("   FragLimit :           ", $server::FragLimit ); }               
   if ($server::TimeLimit             != "") { missionMsg("   TimeLimit :           ", $server::TimeLimit ); }               
   if ($server::TechLevelLimit        != "") { missionMsg("   TechLevelLimit:       ", $server::TechLevelLimit); }           
   if ($server::CombatValueLimit      != "") { missionMsg("   CombatValueLimit:     ", $server::CombatValueLimit); }         
   if ($server::MassLimit             != "") { missionMsg("   MassLimit:            ", $server::MassLimit); }                 
   if ($server::TeamCombatValueLimit  != "") { missionMsg("   TeamCombatValueLimit: ", $server::TeamCombatValueLimit); }
   if ($server::TeamMassLimit         != "") { missionMsg("   TeamMassLimit:        ", $server::TeamMassLimit); }             
   if ($server::DropInProgress        != "") { missionMsg("   DropInProgress:       ", $server::DropInProgress); }            
   if ($server::TeamPlay              != "") { missionMsg("   TeamPlay:             ", $server::TeamPlay); }                  
}      


//--------------------------------------
function onMissionLoad()
{
   // called just after the mission has loaded, before any players are added
   logStandardMissionStart();
}
   
//--------------------------------------
function onMissionStart()
{
   // called when the first player enters the sim (from the wait room)
   //echo("onMissionStart");
}

//--------------------------------------
function onCinematicStart()
{
   // called immediately after the player enters the sim.  This call
   // is valid only for cinematics.  Do not use for multiplayer or single player
}

//--------------------------------------

function logStandardMissionEnd()
{
   // figre out how many columns and what is the widest
   %columns = 0;
   for (%column = 1; $ScoreBoard::PlayerColumnHeader[%column] != ""; %column = %column + 1) 
      { %widths[%column] = strlen($ScoreBoard::PlayerColumnHeader[%column]); %columns = %columns + 1; }
   if (%columns == 0) { return; }
      
   MissionLog("Mission Ended.  Final Scores:");

   %buffer = strcat("   ", strAlign(20, l, "Name"), strAlign(20, l, "Connection"));
   for (%column = 1; %column <= %columns; %column = %column + 1) 
      { %buffer = strcat(%buffer, " ", strAlign(%widths[%column], r, $ScoreBoard::PlayerColumnHeader[%column])); }
   missionMsg(%buffer);

   for (%i = 0; %i < playerManager::getPlayerCount(); %i = %i + 1) {
      %playerNum = playerManager::getPlayerNum(%i);
      %buffer = strcat("   ", strAlign(20, l, getName(%playerNum)), strAlign(20, l, getConnection(%playerNum)));
      for (%column = 1; %column <= %columns; %column = %column + 1) {
         %score = eval(strcat($ScoreBoard::PlayerColumnFunction[%column], "(", %playerNum, ");"));
         %buffer = strcat(%buffer, " ", strAlign(%widths[%column], r, %score));
      }
      missionMsg(%buffer);
   }
}


//--------------------------------------
function onMissionEnd()
{
   logStandardMissionEnd();
   //echo("onMissionEnd");
}


//------------------------------------------------------------------------------
//
// Default Behavior
//

function player::onAddLog(%this)
{
   missionLog(getName(%this), " (", %this, ", ", getConnection(%this), ") joined the game.");
}      

//--------------------------------------
function player::onRemoveLog(%this) 
{
   missionLog(getName(%this), " (", %this, ", ", getConnection(%this), ") left the game.");
}

//--------------------------------------
function player::onRemove(%this)
{
   player::onRemoveLog(%this);
   //echo("player::onRemove ", getName(%this));
}   

//--------------------------------------
function structure::onAdd(%this)
{
   //echo("onInit_structure = ", %this);
}

//--------------------------------------
function structure::onAttacked(%this, %object)
{
   //echo("onAttacked_structure = ", %this, " ", %object);
}

//--------------------------------------
function structure::onDisabled(%this)
{
   //echo("structure::onDisabled = ", %this);
}

//--------------------------------------
function structure::onDestroyed(%this)
{
   //echo("onDestroyed_structure = ", %this);
}

//--------------------------------------
function structure::onScan(%this, %object, %string)
{
	// echo("onScan = ", %this, " ", %object);
	say(%object,0,%string);
}
			       
//------------------------------------------------------------------------------
//--------------------------------------
function vehicle::onAdd(%this)
{
   //echo("onInit_herc = ", %this);
}

//--------------------------------------
function vehicle::salvage(%this)
{
   if(isMultiplayer())
   {
      return;
   }

   // salvage misc components
   %componentCount = getComponentCount(%this);
   %index = 0;
   while (%index < %componentCount) 
   {
      %id = getComponentId(%this, %index);
      if (componentIsInPlanetInventory(%id))
	  {
	     inventoryComponentAdjust(getPlanet(), %id, 1);
		 addComponentToPlanetSalvage(%id, 1);
	  }
	  else
	  {
         inventoryComponentAdjust(-1, %id, 1);
		 addComponentToStashSalvage(%id, 1);
	  }
      %index++;
   }

   // salvage weapons
   %weaponCount = getWeaponCount(%this);
   %index = 0;
   while (%index < %weaponCount) 
   {
      %id = getWeaponId(%this, %index);
      if (weaponIsInPlanetInventory(%id))
	  {
		 inventoryComponentAdjust(getPlanet(), %id, 1);
		 addWeaponToPlanetSalvage(%id, 1);
	  }
	  else
	  {
         inventoryWeaponAdjust(-1, %id, 1);
		 addWeaponToStashSalvage(%id, 1);
	  }
      %index++;
   }
}

//--------------------------------------
function vehicle::onDestroyedLog(%destroyed, %destroyer)
{
   %destroyedNum = playerManager::vehicleIdToPlayerNum(%destroyed);
   %c0 = getConnection(%destroyedNum);
   %destroyerNum = playerManager::vehicleIdToPlayerNum(%destroyer);
   %c1 = getConnection(%destroyerNum);
   missionLog(strcat(getName(%destroyedNum), " (", %destroyedNum, ", ", %c0, ")  killed by ", getName(%destroyer), " (", %destroyerNum, ", ", %c1, ")."));
}

//--------------------------------------
function vehicle::onTargeted(%targeted, %targeter)
{
}

//--------------------------------------
function vehicle::onAttacked(%this, %attacker)
{
}

//--------------------------------------
function vehicle::onScan(%this, %object, %string)
{
	// echo("onScan = ", %this, " ", %object);
	say(%object,0,%string);
}

function vehicle::onMessage(%this, %message, %_1, %_2, %_3, %_4, %_5, %_6, %_7, %_8, %_9)
{
   // message distpatch
}

//--------------------------------------
function turret::onAdd(%this)
{
   //echo("onInit_turret = ", %this);
}

//------------------------------------------------------------------------------
//--------------------------------------
function trigger::onEnter(%this, %object)
{
   //echo("onEnter_trigger");
   //echo("  ", %this);
   //echo("  ", %object);
}

//--------------------------------------
function trigger::onLeave(%this, %object)
{
   //echo("onLeave_trigger = ", %this, " ", %object);
}



//--------------------------------------
function heal::trigger::onEnter(%this, %object)
{
	//playSound(%object, IDCV_POWER_ON);
	say(%object, 0, "Entering repair area.");
}

//--------------------------------------
function heal::trigger::onContact(%this, %object)
{					  
   	//echo("onContact_trigger = ", %this, " ", %object);
   	
   	if(isShutdown(%object) == true)
   	{
   		healObject(%object, 120.0);
	}
}


//--------------------------------------
function reload::trigger::onEnter(%this, %object)
{
	//playSound(%object, IDCV_POWER_ON);
	say(%object, 0, "Entering reload area.");
}

//--------------------------------------
function reload::trigger::onContact(%this, %object)
{
   	//echo("onContact_trigger = ", %this, " ", %object);
   	
   	if(isShutdown(%object) == true)
   	{
   		reloadObject(%object, 10.0);
	}
}


function checkDistance( %obj1, %obj2, %distance, %callback, %time )
{
   %obj1 = getObjectId( %obj1 );
   %obj2 = getObjectId( %obj2 );

   dbEcho( 3, "DEBUG: checkDistance ( ", %obj1, " ", %obj2, " ) ", %distance, "m CALLBACK: ", %callback );

   %var = getDistance( %obj1, %obj2 );
   dbEcho( 5, "DEBUG: Distance difference: ", %var, "m" );

   if( getDistance( %obj1, %obj2 ) <= %distance )
   {
       %func = %callback @ "();";
       schedule( %func, 0 );
   }

   schedule( "checkDistance( "
             @ %obj1 @ ", "
             @ %obj2 @ ", "
             @ %distance @ ", "
             @ %callback @ ");",
             5 );
}

function checkBoundary( %dir, %obj1, %obj2, %distance, %callback, %time )
{
   %obj1 = getObjectId( %obj1 );
   %obj2 = getObjectId( %obj2 );

   dbEcho( 5, "DEBUG: checkDistance ( ", %obj1, " ", %obj2, " ) ", %distance, "m CALLBACK: ", %callback );

   %var = getGroupDistance( %obj1, %obj2 );
   dbEcho( 3, "DEBUG: Distance difference: ", %var, "m" );

   if( %dir == "enter" || %dir == "Enter" )
   {
      if( %var <= %distance )
      {
         %func = %callback @ "();";
         schedule( %func, 0 );
      }
   }
   else if( %dir == "leave" || %dir == "Leave" )
   {
      if( %var >= %distance )
      {
         %func = %callback @ "();";
         schedule( %func, 0 );
      }
   }

   schedule( "checkBoundary( "
             @ %dir @ ", "
             @ %obj1 @ ", "
             @ %obj2 @ ", "
             @ %distance @ ", "
             @ %callback @ ");",
             5 );
}

//------------------------------------------------------------------------------
function cycleNavPoint( %playerId, %navToCycle, %nextNavPoint )
{
   if( getDistance( %playerId, %navToCycle ) < 250 )
   {
      setNavMarker( %navToCycle, False );
      setNavMarker( %nextNavPoint, True, -1 );
   }

   schedule( "cycleNavPoint( "
             @ %playerId @ ", "
             @ %navToCycle @ ", "
             @ %nextNavPoint @ ");",
             2 );
}

//--------------------------------------
function targetDamage()
{
   focusServer();
   // FOR DEBUGGING ONLY, lists the damage of whoever the player has targeted
   %playerVehId = playerManager::playerNumToVehicleId(2049);
   %targetId = getTargetId(%playerVehId);
   if (%targetId == "")
      echo("no target");
   else  
      dumpDamage(%targetId);
   focusClient();
}

//--------------------------------------
function shutup(%bool)
{
    if(%bool == "") %bool = false;
    %cm = 0;
    while(%cm = getNextObject(getObjectId("PLAYERSQUAD"),%cm))
        squawkEnabled(%cm,%bool);    
}

//-----------------------------------
function clearGeneralOrders()
{
    removeGeneralOrder( *IDSTR_ORDER_HA2_1 );
    removeGeneralOrder( *IDSTR_ORDER_HA3_1 );
    removeGeneralOrder( *IDSTR_ORDER_HA4_1 );
    removeGeneralOrder( *IDSTR_ORDER_HA5_1 );
    removeGeneralOrder( *IDSTR_ORDER_HA5_2 );
    removeGeneralOrder( *IDSTR_ORDER_HB4_1 );
    removeGeneralOrder( *IDSTR_ORDER_HB4_2 );
    removeGeneralOrder( *IDSTR_ORDER_HC2_1 );
    removeGeneralOrder( *IDSTR_ORDER_HC2_2 );
    removeGeneralOrder( *IDSTR_ORDER_HC3_1 );
    removeGeneralOrder( *IDSTR_ORDER_HC3_2 );
    removeGeneralOrder( *IDSTR_ORDER_HC3_3 );
    removeGeneralOrder( *IDSTR_ORDER_HC3_4 );
    removeGeneralOrder( *IDSTR_ORDER_HD2_1 );
    removeGeneralOrder( *IDSTR_ORDER_CA0_1 );
    removeGeneralOrder( *IDSTR_ORDER_CD1_1 );
    removeGeneralOrder( *IDSTR_ORDER_CD4_1 );
    removeGeneralOrder( *IDSTR_ORDER_CE3_1 );
    removeGeneralOrder( *IDSTR_ORDER_CD3_1 );
}

//--------------------------------------
function onMissionPreLoad()
{
   // base function does nothing
}

//--------------------------------------
function enableNavMarker(%marker, %who)
{
   setNavMarker(%marker, True);
}

//--------------------------------------
function disableNavMarker(%marker)
{
   setNavMarker(%marker, False);
}

//--------------------------------------
function selectNavMarker(%marker)
{
   setNavMarker(%marker, True, -1);
}

//--------------------------------------
function deselectNavMarker(%marker)
{
   setNavMarker(%marker, False, -1);
}



exec ("squadActions.cs");

clearGeneralOrders();
         

//--------------------------------------
// now for a real function
// ambient sound setup
// low-level functions

function createAmbientSoundSource(%source, %range, %minTime, %maxTime)
{   
   // straight copy of inputs
   %source.range = %range;
   %source.minTime = %minTime;
   %source.maxTime = %maxTime;
   
   // internals
   %source.soundCount = 0;
   %source.pause = false;
   %source.pauseTrip = true;  // sound is completely stopped
}

function startAmbientSoundSource(%source)
{
   %source.pause = false;

   if(%source.pauseTrip == true)
   {
      %source.pauseTrip = false;
      playRandomAmbientSound(getObjectId(%source));     
   }
}

function stopAmbientSoundSource(%source)
{
   // will interrupt on next scheduled sound
   %source.pause = true;   

   // see if it is a non-repeating sound
   if(%source.minTime == 0 || %source.maxTime == 0)
   {
      // pause it right now
      %source.pauseTrip = true;
   
      // it's a non-repeating sound
      // what can you do?  You can flush the channel, of course
      flushChannel(%source);
   }
}

function addToAmbientSoundSource(%source, %soundFile, %soundProfile)
{
   %source.soundFile[%source.soundCount] = %soundFile;
   %source.soundProfile[%source.soundCount] = %soundProfile;
   
   %source.soundCount++;
   
   // see if we should start making noise
   if(
      %source.soundCount == 1 &&
      %source.pause == false
   )
   {   
      startAmbientSoundSource(%source);     
   }
}

function playRandomAmbientSound(%source)
{
   if(%source.pause == true)
   {
      // trip the flag (notice that we don't reschedule)
      %source.pauseTrip = true;
      return;
   }
   
   if(%source.soundCount > 0)
   {
      // pick a random one
      %soundIndex = randomInt(0, %source.soundCount - 1);
      
      // pick a random location
      %x = getPosition(%source, "x") + randomInt((0 - %source.range), %source.range);
      %y = getPosition(%source, "y") + randomInt((0 - %source.range), %source.range);
      %z = getPosition(%source, "z");
      
      // play the random sound at the random location
      // use %source as the channel
      say3D(0, %source, "", %source.soundFile[%soundIndex], %source.soundProfile[%soundIndex], %x, %y, %z);
   }
   
   if(%source.minTime != 0 && %source.maxTime != 0)
   {
      %nextTime = randomInt(%source.minTime, %source.maxTime);   
      
      schedule("playRandomAmbientSound(" @ %source @ ");", %nextTime);
   }
} 


//--------------------------------------
// high-level ambient sounds

// terrain sounds
// all terrain sounds are based off of object MissionGroup\\World\\terrain

function desertSounds()
{
   %terrain = getObjectId("MissionGroup\\World\\terrain");

   createAmbientSoundSource(%terrain, 0, 15, 30);
   addToAmbientSoundSource(%terrain, "sfx_windgust.wav", IDPRF_2D);
}

function europaSounds()
{
}

function iceSounds()
{
   %terrain = getObjectId("MissionGroup\\World\\terrain");

   createAmbientSoundSource(%terrain, 0, 15, 30);
   addToAmbientSoundSource(%terrain, "sfx_ice1.wav", IDPRF_2D);
   addToAmbientSoundSource(%terrain, "sfx_ice2.wav", IDPRF_2D);
   addToAmbientSoundSource(%terrain, "sfx_ice3.wav", IDPRF_2D);
}

function marsSounds()
{
   %terrain = getObjectId("MissionGroup\\World\\terrain");

   createAmbientSoundSource(%terrain, 0, 15, 30);
   addToAmbientSoundSource(%terrain, "sfx_windgust.wav", IDPRF_2D);
}

function mercurySounds()
{
}

function moonSounds()
{
}

function plutoSounds()
{
}

function temperateSounds()
{
}

function titanSounds()
{
   %terrain = getObjectId("MissionGroup\\World\\terrain");

   createAmbientSoundSource(%terrain, 0, 15, 30);
   // swamp doesn't belong here?
   // addToAmbientSoundSource(%terrain, "sfx_swamp.wav", IDPRF_2D);
}

function venusSounds()
{
   %terrain = getObjectId("MissionGroup\\World\\terrain");

   createAmbientSoundSource(%terrain, 0, 15, 30);
   addToAmbientSoundSource(%terrain, "sfx_lava.wav", IDPRF_2D);
}







// weather effects and events
// all weather/events sounds are based off of:
// MissionGroup\\World\\terrainPal and MissionGroup\\World

function avalancheSounds() // for Ice
{
   %weather1 = getObjectId("MissionGroup\\World\\terrainPal");
   
   createAmbientSoundSource(%weather1, 0, 60, 180);
   addToAmbientSoundSource(%weather1, "sfx_avalanche.wav", IDPRF_2D);   
}

function meteorSounds() // for Titan
{
   %weather1 = getObjectId("MissionGroup\\World\\terrainPal");
   
   createAmbientSoundSource(%weather1, 0, 60, 180);
   addToAmbientSoundSource(%weather1, "sfx_meteor.wav", IDPRF_2D);   
}

function earthquakeSounds() // for Mercury
{
   %weather1 = getObjectId("MissionGroup\\World\\terrainPal");
   
   createAmbientSoundSource(%weather1, 0, 60, 180);
   addToAmbientSoundSource(%weather1, "sfx_quake.wav", IDPRF_2D);   
}

function stormSounds() // for Temperate
{
   %weather1 = getObjectId("MissionGroup\\World\\terrainPal");
   %weather2 = getObjectId("MissionGroup\\World");
   
   createAmbientSoundSource(%weather1, 0, 30, 60);
   addToAmbientSoundSource(%weather1, "sfx_thunder1.wav", IDPRF_2D);
   addToAmbientSoundSource(%weather1, "sfx_thunder2.wav", IDPRF_2D);
   
   createAmbientSoundSource(%weather2, 0, 0, 0);
   addToAmbientSoundSource(%weather2, "sfx_rain.wav", IDPRF_2D_LOOPING);         
}

function windSounds() // for Mars
{
   %weather1 = getObjectId("MissionGroup\\World\\terrainPal");
   
   createAmbientSoundSource(%weather1, 0, 0, 0);
   // nobody likes windloop.wav
   // addToAmbientSoundSource(%weather1, "sfx_windloop.wav", IDPRF_2D_LOOPING);
   addToAmbientSoundSource(%weather1, "sfx_windloop2.wav", IDPRF_2D_LOOPING);
}


// base sounds
// an object and radius must be passed in

function humanBaseSounds(%center, %radius)
{
   createAmbientSoundSource(%center, %radius, 5, 10);
   addToAmbientSoundSource(%center, "sfx_machine1.wav", IDPRF_NEAR);   
   addToAmbientSoundSource(%center, "sfx_machine2.wav", IDPRF_NEAR);   
   addToAmbientSoundSource(%center, "sfx_machine3.wav", IDPRF_NEAR);
   addToAmbientSoundSource(%center, "sfx_radar_station.wav", IDPRF_NEAR);   
   addToAmbientSoundSource(%center, "sfx_steam.wav", IDPRF_NEAR);         
}

function cybridBaseSounds(%center, %radius)
{
   createAmbientSoundSource(%center, %radius, 5, 10);
   addToAmbientSoundSource(%center, "sfx_cybrid_com.wav", IDPRF_NEAR);   
   addToAmbientSoundSource(%center, "sfx_cybrid_indust.wav", IDPRF_NEAR);   
   addToAmbientSoundSource(%center, "sfx_steam.wav", IDPRF_NEAR);         
}

// Force the command popup, check every thirty seconds to see if the 
// command has been issued; if not, pop it up again.
//
// IMPORTANT NOTE!: A flag is stored on the player... It's up to the 
//                  mission to CLEAR the flag once the order has been issued.
function repeatGeneralOrder(%playerId, %order)
{
   // the flag stored on the player is the actual text order name
   if(dataRetrieve(%playerId, %order) != True)
   {
      forceCommandPopup();
      schedule("repeatGeneralOrder(" @ %playerId @ ", \"" @ %order @ "\"" @ ");", 20 );
   }   
}

// alarm sounds
// an object and sound are passed in

function alarmSoundsOn(%center, %sound)
{
   createAmbientSoundSource(%center, 0, 0, 0);
   addToAmbientSoundSource(%center, %sound, IDPRF_ALARM);      
}

function alarmSoundsOff(%center)
{
   stopAmbientSoundSource(%center);
}

///////////////////////////////////////////////////////////////////////////////////////////////////
// squadmate stuff
///////////////////////////////////////////////////////////////////////////////////////////////////
function addSquadmateOrders()
{  
   addSquadOrder(*IDSTR_SQORDER_1,  "onAttackMyTarget();");    // 1
   addSquadOrder(*IDSTR_SQORDER_2,  "onJoinOnMe();");          // 2
   addSquadOrder(*IDSTR_SQORDER_3,  "onDefendMyTarget();");    // 3
   addSquadOrder(*IDSTR_SQORDER_4,  "onGoToNavPoint();");      // 4
   addSquadOrder(*IDSTR_SQORDER_5,  "onHoldFire();");          // 5
   addSquadOrder(*IDSTR_SQORDER_6,  "onFireAtWill();");        // 6
   addSquadOrder(*IDSTR_SQORDER_7,  "onHalt();");              // 7
}

function removeSquadmateOrders()
{
   removeSquadOrder(*IDSTR_SQORDER_1);
   removeSquadOrder(*IDSTR_SQORDER_2);          
   removeSquadOrder(*IDSTR_SQORDER_3);    
   removeSquadOrder(*IDSTR_SQORDER_4);     
   removeSquadOrder(*IDSTR_SQORDER_5);           
   removeSquadOrder(*IDSTR_SQORDER_6);        
   removeSquadOrder(*IDSTR_SQORDER_7);            
}

function onAttackMyTarget()
{
   orderSquadMate($Order::Recipient, Attack, $Order::Target);
   orderSquadMate($Order::Recipient, HoldPosition, True);
}

function onJoinOnMe()
{
   orderSquadMate($Order::Recipient, Formation);
   orderSquadMate($Order::Recipient, HoldPosition, True);
}

function onDefendMyTarget()
{
   orderSquadMate($Order::Recipient, Guard, $Order::Target);
   orderSquadMate($Order::Recipient, Acknowledge, True );
}

function onGoToNavPoint()
{
   if ($Order::NavPosSet == true)
      orderSquadMate($Order::Recipient, Guard, $Order::NavPosX, $Order::NavPosY, 0);
}

function onHoldFire()
{
   orderSquadMate($Order::Recipient, HoldFire, True );
}

function onFireAtWill()
{
   orderSquadMate($Order::Recipient, HoldFire, False );
}

function onHalt()
{
   orderSquadMate($Order::Recipient, Clear, True );
   orderSquadMate($Order::Recipient, HoldPosition, True );
   orderSquadMate($Order::Recipient, Acknowledge, True );
}

// at the start of every mission, make sure we have defined our squadmate orders
addSquadmateOrders();