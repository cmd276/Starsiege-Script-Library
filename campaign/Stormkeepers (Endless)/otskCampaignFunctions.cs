function win()
{
    dbecho(3, "TRIGGER: win();");
    $otsk::missionsCompleted++;
    %newRank = ($otsk::missionsCompleted / $otsk::missionsPerRank) + 2;
    if (%newRank > $otsk::rank)
    {
        $otsk::rank = %newRank;
        updatePlanetInventory2($otsk::rank);
        schedule("forceToDebrief();", 5.0);
    }
    
    $otsk::missionType = "";
    %randomInt = randomInt(1,2);
    if (%randomInt == 1)
    {
        $otsk::missionType = "BaseDefense";
        $otsk::title = "Base Invasion";
        $otsk::dateOnMissionEnd = $otsk::dateOnMissionEnd + 375;
        $otsk::shortDesc = "We've received reports of a small incoming force thinking we're part of a group we're not. We're putting you in charge of protecting the base. Good luck out there.";
        $otsk::longDescRichText = "";
        $otsk::successDescRichText = "";
        $otsk::failDescRichText = "";
    }
    else
    {
        $otsk::missionType = "SalvageRecovery";
    }
}

function updatePlanetInventory2(%rank)
{
    dbecho(3, "TRIGGER: updatePlanetInventory2(" @ %rank @ ");");
    if (%rank == 2)
    {
        // Player Ranked up... Award Tech.
        InventoryweaponAdjust( mars,     10,  3  );    #Laser G. Bomb
        InventoryweaponAdjust( mars,    124,  3  );    #Pit Viper 8
        InventoryweaponAdjust( mars,    126,  3  );    #Sparrow 6
        InventoryweaponAdjust( mars,    135,  3  );    #Proximity 10
        
        InventoryComponentAdjust( mars, 201,  3  );    # Small Human Reactor 2 -- small
        InventoryComponentAdjust( mars, 301,  3  );    # Human Protector Shield
        InventoryComponentAdjust( mars, 408,  3  );    #Standard Human Sensor
        InventoryComponentAdjust( mars, 409,  3  );    #Human Longbow Sensor
        InventoryComponentAdjust( mars, 410,  3  );    #Human Infiltrator Sensor
        InventoryComponentAdjust( mars, 412,  3  );    #Human Ultralight Sensor
        InventoryComponentAdjust( mars, 101,  3  );    # Human High Output Light Engine
        InventoryComponentAdjust( mars, 104,  3  );    # Human High Output Medium Engine
        InventoryComponentAdjust( mars, 900,  3  );    #Angle Life Support
        InventoryComponentAdjust( mars, 801,  3  );    # Human Improved Computer
        InventoryComponentAdjust( mars, 860,  3  );    # Laser Targeting Module
        InventoryComponentAdjust( mars, 870,  3  );    # Reactor Capacitor
        InventoryComponentAdjust( mars, 928,  3  );    #DURAC (Depleteted Uranium)

        InventoryVehicleAdjust(   mars,   5,  3  ); // Terran Basilisk
        InventoryVehicleAdjust(   mars,   6,  3  ); // Paladin Tank
        InventoryVehicleAdjust(   mars,  15,  3  ); // Knight's Paladin
        InventoryVehicleAdjust(   mars,  31,  3  ); // Avenger Tank
        InventoryVehicleAdjust(   mars,  33,  3  ); // Rebel Olympian
        InventoryVehicleAdjust(   mars,  26,  3  ); // Recluse Tank
    }
    if (%rank == 3)
    {
        InventoryweaponAdjust(    mars, 102, 3 ); // Heavy Laser
        InventoryweaponAdjust(    mars, 105, 3 ); // Emp
        InventoryweaponAdjust(    mars, 117, 3 ); // Hvy Autocannon
        InventoryweaponAdjust(    mars, 125, 3 ); // Pit Viper 12
        InventoryweaponAdjust(    mars, 127, 3 ); // Sparrow 10
        InventoryweaponAdjust(    mars, 147, 3 ); // Aphid
        InventoryweaponAdjust(    mars, 136, 3 ); // Proximity 15
        InventoryComponentAdjust( mars, 203, 3 ); // Medium Human Reactor 2 medium
        InventoryComponentAdjust( mars, 304, 3 ); // Human Centurian Shield
        InventoryComponentAdjust( mars, 401, 3 ); // Long Range Sensor -- Ranger
        InventoryComponentAdjust( mars, 411, 3 ); // Human Crossbow Sensor
        InventoryComponentAdjust( mars, 108, 3 ); // Human Heavy Cruise Engine
        InventoryComponentAdjust( mars, 802, 3 ); // Human Advanced Computer
        InventoryComponentAdjust( mars, 810, 3 ); // Guardian ECM
        InventoryComponentAdjust( mars, 840, 3 ); // Shield Modulator
        InventoryComponentAdjust( mars, 850, 3 ); // Shield Amplifier (increases shield constant)
        InventoryComponentAdjust( mars, 929, 3 ); // Ceramic
        InventoryVehicleAdjust(  mars,    1, 3 ); // Terran Apocalypse
        InventoryVehicleAdjust(  mars,    3, 3 ); // Terran Gorgon
        InventoryVehicleAdjust(  mars,    8, 3 ); // Disruptor Tank
        InventoryVehicleAdjust(  mars,   17, 3 ); // Knight's Disruptor
        InventoryVehicleAdjust(  mars,   11, 3 ); // Knight's Minotaur
        InventoryVehicleAdjust(  mars,   13, 3 ); // Knight's Talon

    }
    if (%rank == 4)
    {
        InventoryweaponAdjust(    mars, 103, 3 ); // Comp Laser
        InventoryweaponAdjust(    mars, 104, 3 ); // Twin Laser
        InventoryweaponAdjust(    mars, 107, 3 ); // Blaster
        InventoryweaponAdjust(    mars, 114, 3 ); // Nano Infuser
        InventoryweaponAdjust(    mars, 118, 3 ); // EMC Autocannon
        InventoryweaponAdjust(    mars, 119, 3 ); // Blast Cannon
        InventoryweaponAdjust(    mars, 130, 3 ); // Shrike 8
        InventoryComponentAdjust( mars, 303, 3 ); // Human FastCharge Shield
        InventoryComponentAdjust( mars, 305, 3 ); // Human Repulsor Shield
        InventoryComponentAdjust( mars, 413, 3 ); // Human Hound Dog Sensor
        InventoryComponentAdjust( mars, 414, 3 ); // Thermal Sensor
        InventoryComponentAdjust( mars, 102, 3 ); // Human Agile Light Engine
        InventoryComponentAdjust( mars, 105, 3 ); // Human Medium Agility Engine
        InventoryComponentAdjust( mars, 109, 3 ); // Human High Output Heavy Engine
        InventoryComponentAdjust( mars, 111, 3 ); // Human Standard Assault Engine
        InventoryComponentAdjust( mars, 830, 3 ); // Chameleon
        InventoryComponentAdjust( mars, 885, 3 ); // Turbine Boost
        InventoryComponentAdjust( mars, 914, 3 ); // UAP
        InventoryComponentAdjust( mars, 930, 3 ); // Crystaluminum
        InventoryVehicleAdjust(   mars,   7, 3 ); // Myrmidon Tank
        InventoryVehicleAdjust(   mars,  14, 3 ); // Knight's Basilisk
        InventoryVehicleAdjust(   mars,  16, 3 ); // Knight's Myrmidon

    }
    if (%rank == 5)
    {
        InventoryweaponAdjust(    mars, 106, 3 ); // ELF
        InventoryweaponAdjust(    mars, 109, 3 ); // PBW
        InventoryweaponAdjust(    mars, 111, 3 ); // Blink Gun
        InventoryweaponAdjust(    mars, 115, 3 ); // Nanite Cannon
        InventoryweaponAdjust(    mars, 120, 3 ); // Hvy Blast Can
        InventoryweaponAdjust(    mars, 121, 3 ); // Rail Gun
        InventoryweaponAdjust(    mars, 128, 3 ); // SWARM 6
        InventoryweaponAdjust(    mars, 129, 3 ); // Minion
        InventoryweaponAdjust(    mars, 142, 3 ); // Radiation Gun
        InventoryweaponAdjust(    mars,   1, 3 ); // Nike
        InventoryweaponAdjust(    mars,   2, 3 ); // AML
        InventoryComponentAdjust( mars, 205, 3 ); // Large Human Reactor 2-- Maxim
        InventoryComponentAdjust( mars, 306, 3 ); // Human Titan Shield
        InventoryComponentAdjust( mars, 110, 3 ); // Human Agile Heavy Engine
        InventoryComponentAdjust( mars, 112, 3 ); // Human Improved Assault Engine
        InventoryComponentAdjust( mars, 811, 3 ); // Doppleganger ECM
        InventoryComponentAdjust( mars, 820, 3 ); // Thermal Diffuser
        InventoryComponentAdjust( mars, 831, 3 ); // Cuttlefish cloak
        InventoryComponentAdjust( mars, 880, 3 ); // Rocket Booster
        InventoryVehicleAdjust(   mars,  10, 3 ); // Knight's Apocalypse
        InventoryVehicleAdjust(   mars,  41, 3 ); // Harabec's Predator

    }
    if (%rank == 6)
    {
        InventoryweaponAdjust(    mars,   3, 3 ); // Disrupter
        InventoryweaponAdjust(    mars,   4, 3 ); // Electrohull
        InventoryweaponAdjust(    mars,  11, 3 ); // Hades Bomb
        InventoryweaponAdjust(    mars, 108, 3 ); // Heavy Blaster
        InventoryweaponAdjust(    mars, 110, 3 ); // Plasma
        InventoryComponentAdjust( mars, 113, 3 ); // Human heavy turbine engine
        InventoryVehicleAdjust(   mars,  12, 3 ); // Knight's Gorgon

    }
    if (%rank == 7)
    {
        InventoryweaponAdjust(    mars, 112, 3 ); // Qgun
        InventoryweaponAdjust(    mars, 150, 3 ); // SMART Gun
        InventoryComponentAdjust( mars, 114, 3 ); // High Output Turbine (HOT)

    }
    if (%rank == 8)
    {
        InventoryweaponAdjust(    mars, 113, 3 ); // MFAC
        InventoryComponentAdjust( mars, 307, 3 ); // Human Medusa Shield
        InventoryComponentAdjust( mars, 115, 3 ); // Human super heavy engine
        InventoryComponentAdjust( mars, 890, 3 ); // NanoRepair

    }
    if (%rank == 9)
    {
        InventoryComponentAdjust( mars, 931, 3 ); // Quicksilver
    }
    if (%rank == 10)
    {
        InventoryComponentAdjust( mars, 910, 3 ); // Agrav Generator
    }
}

function awardItems(%rank, %numberOfItems)
{
    dbecho(3, "awardItems(" @ %rank @ ", "@ %numberOfItems @ ");");
    if (%numberOfItems == "") %numberOfItems = 1;
    for(%item = 1; %item < %numberOfItems; %item++)
    {
        %chance = randonInt(1,3);
        
        if (%chance == 1)
        {
            awardWeapon(%rank);
        }
        if (%chance == 2)
        {
            awardComponent(%rank);
        }
        if (%chance == 3)
        {
            awardVehicle(%rank);
        }
    }
}    

function awardWeapon(%rank)
{
    dbecho(3, "TRIGGER: awardWeapon(" @ %rank @ ");");
    %c = 0;
    if (%rank >= 1)
    {
        %w[%c++] = 101;
        %w[%c++] = 116;
        %w[%c++] = 134;
    }
    if (%rank >= 2)
    {
        %w[%c++] = 10;
        %w[%c++] = 124;
        %w[%c++] = 126;
        %w[%c++] = 135;
    }
    if (%rank >= 3)
    {
        %w[%c++] = 102;
        %w[%c++] = 105;
        %w[%c++] = 117;
        %w[%c++] = 125;
        %w[%c++] = 127;
        %w[%c++] = 147;
        %w[%c++] = 136;
    }
    if (%rank >= 4)
    {
        %w[%c++] = 104;
        %w[%c++] = 103;
        %w[%c++] = 107;
        %w[%c++] = 114;
        %w[%c++] = 118;
        %w[%c++] = 119;
        %w[%c++] = 130;
        
    }
    if (%rank >= 5)
    {
        %w[%c++] = 106;
        %w[%c++] = 109;
        %w[%c++] = 111;
        %w[%c++] = 115;
        %w[%c++] = 120;
        %w[%c++] = 121;
        %w[%c++] = 128;
        %w[%c++] = 129;
        %w[%c++] = 142;
        %w[%c++] = 1;
        %w[%c++] = 2;
    }
    if (%rank >= 6)
    {
        %w[%c++] = 3;
        %w[%c++] = 4;
        %w[%c++] = 11;
        %w[%c++] = 108;
        %w[%c++] = 110;
    }
    if (%rank >= 7)
    {
        %w[%c++] = 112;
        %w[%c++] = 150;
    }
    if (%rank >= 8)
    {
        %w[%c++] = 113;
    }
    
    %r = randomInt(1,%c);
    %v = randomInt(1,3);
    InventoryweaponAdjust( mars, %w[%r], %v);
}

function awardComponent(%rank)
{
    dbecho(3, "TRIGGER: awardComponent(" @ %rank @ ");");
    %c = 0;
    if (%rank >= 1)
    {
        %w[%c++] = 200;
        %w[%c++] = 202;
        %w[%c++] = 204;
        %w[%c++] = 300;
        %w[%c++] = 302;
        %w[%c++] = 400;
        %w[%c++] = 100;
        %w[%c++] = 103;
        %w[%c++] = 106;
        %w[%c++] = 107;
        %w[%c++] = 800;
        %w[%c++] = 845;
        %w[%c++] = 875;
        %w[%c++] = 865;
        %w[%c++] = 926;
        %w[%c++] = 927;
    }
    if (%rank >= 2)
    {
        %w[%c++] = 201;
        %w[%c++] = 301;
        %w[%c++] = 408;
        %w[%c++] = 409;
        %w[%c++] = 410;
        %w[%c++] = 412;
        %w[%c++] = 101;
        %w[%c++] = 104;
        %w[%c++] = 900;
        %w[%c++] = 801;
        %w[%c++] = 860;
        %w[%c++] = 870;
        %w[%c++] = 928;
    }
    if (%rank >= 3)
    {
        %w[%c++] = 203;
        %w[%c++] = 304;
        %w[%c++] = 401;
        %w[%c++] = 411;
        %w[%c++] = 108;
        %w[%c++] = 802;
        %w[%c++] = 810;
        %w[%c++] = 840;
        %w[%c++] = 850;
        %w[%c++] = 929;
    }
    if (%rank >= 4)
    {
        %w[%c++] = 303;
        %w[%c++] = 305;
        %w[%c++] = 413;
        %w[%c++] = 414;
        %w[%c++] = 102;
        %w[%c++] = 105;
        %w[%c++] = 109;
        %w[%c++] = 111;
        %w[%c++] = 830;
        %w[%c++] = 885;
        %w[%c++] = 914;
        %w[%c++] = 930;
    }
    if (%rank >= 5)
    {
        %w[%c++] = 205;
        %w[%c++] = 306;
        %w[%c++] = 110;
        %w[%c++] = 112;
        %w[%c++] = 811;
        %w[%c++] = 820;
        %w[%c++] = 831;
        %w[%c++] = 880;
    }
    if (%rank >= 6)
    {
        %w[%c++] = 113;
    }
    if (%rank >= 7)
    {
        %w[%c++] = 114;
    }
    if (%rank >= 8)
    {
        %w[%c++] = 307;
        %w[%c++] = 115;
        %w[%c++] = 890;
    }
    if (%rank >= 9)
    {
        %w[%c++] = 931;
    }
    if (%rank >= 10)
    {
        %w[%c++] = 910;
    }
    
    %r = randomInt(1,%c);
    %v = randomInt(1,3);
    InventoryComponentAdjust( mars, %w[%r], %v);
}

function awardVehicle(%rank)
{
    dbecho(3, "TRIGGER: awardVehicle(" @ %rank @ ");");
    %c = 0;
    if (%rank >= 1)
    {
        %w[%c++] = 2;
        %w[%c++] = 4;
        %w[%c++] = 32;
        %w[%c++] = 30;
        %w[%c++] = 51;
        %w[%c++] = 52;
    }
    if (%rank >= 2)
    {
        %w[%c++] = 5;
        %w[%c++] = 6;
        %w[%c++] = 15;
        %w[%c++] = 31;
        %w[%c++] = 33;
        %w[%c++] = 26;
    }
    if (%rank >= 3)
    {
        %w[%c++] = 1;
        %w[%c++] = 3;
        %w[%c++] = 8;
        %w[%c++] = 17;
        %w[%c++] = 11;
        %w[%c++] = 13;
    }
    if (%rank >= 4)
    {
        %w[%c++] = 7;
        %w[%c++] = 14;
        %w[%c++] = 16;
    }
    if (%rank >= 5)
    {
        %w[%c++] = 10;
        %w[%c++] = 41;
    }
    if (%rank >= 6)
    {
        %w[%c++] = 12;
    }
    
    %r = randomInt(1,%c);
    InventoryVehicleAdjust( mars, %w[%r], 1);
}
