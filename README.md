# Wilzuun Map Tools

This is the Single folder version of [This project](https://gitlab.com/cmd276/wilzuun-scripts-library)

# Wilzuun Map Tools
##### By ^TFW^ Wilzuun

## Installing Project
Place `WilzuunLoader.cs` and `WilzuunStdLib.cs` in the Multiplayer directory.
Place the `wilzuun` directory in the Multiplayer directory.

At the end of the mission script file, put `exec("WilzuunLoader.cs");`
Either in the `server_*.cs` file or the mission script file, put `$wilzuun::GameType = "dov";` Or any other game type included in this project.

This README.md file is undergoing Reconstruction currently.

This README.md file is being split into several files, so that each directory has a REDME.md file in it to explain those categories more effectively, while trying to keep this one minimalized. Those files are currently being worked on tonight, Aug 16th, 2020.

## DynCityStdLib
This StdLib will allow for several small cities to be spawned or for one large city to be spawned, it will account for building size, and passage between them.
<details>
<summary>Roadmap</summary>
* Cities spawn at distances noted in DynPathStdLib settings.

</details>

## DynPathStdLib
A dynamic pathway system for players to follow on a mission. Randomly generates where the next point should be while players are concentrating on mission objectives.
The idea for this project came from a conversation about Warframe, and how it had a dynamic setup of where to go to do what. I thought I could at least snag the idea of dynamic pathways and give normal Starsiege objectives at each pathway marker.

## logStdLib
A log feature set, set aside for the other scripts in this collection.

## Building Size Project
A project that is recording the size of the buildings in the game.

# Game Types

Each Game type will not have a README.md file.
All game types will their respective read me file here.

Those readme sections will be updated over the next several days.

<details>
<summary>Assassination (asn)</summary>

#### Description
Mission type that tasks the Tenno to seek and eliminate a unique enemy boss and then return to extraction.

#### Status
Planned, not yet started.

#### Roadmap
* Randomly Spawn and mark an AI as the Assassination Target.
* Randomly spawn mission entrance points, and extraction / leave mission points.
</details><details>
<summary>Defections (dfc)</summary>

#### Description
* Endless Mission type that tasks players with escorting small squads of Kavor Defectors to an extraction point while defending against the Infested.

#### Status
Planned, not yet started.
</details><details>
<summary>Defense (def)</summary>

#### Description
* Endless Mission type in which the players must defend the assigned primary objective or objectives from being destroyed by attacking waves of enemies.

#### Status
Initial configurations in testing. Game type balancing in progress.
</details><details>
<summary>Excavation (exc)</summary>

#### Description
* Endless Mission type tasks players with searching and then extracting various artifacts buried deep within a planet's surface.

#### Status
Planned, not yet started.
</details><details>
<summary>Exterminate (ext)</summary>

#### Description
* players to kill a limited number of enemies in the area and then make it to extraction

#### Status
Planned, not yet started.
</details><details>
<summary>Hijack (hij)</summary>

#### Description
* Mission type where players must take control of a large, mobile objective and safely lead it to extraction. 

#### Status
Planned, not yet started.
</details><details>
<summary>Interception (int)</summary>

#### Description
* Endless Mission type requiring players to capture and hold set locations on the map in order to intercept enemy transmissions by reaching 100% control sooner than the enemy faction does.

#### Status
Planned, not yet started.
</details><details>
<summary>Mobile Defense (mdf)</summary>

#### Description
* Mission type requiring players to carry a datamass to 2-3 computer terminals and upload it to them. Once uploaded at each terminal, players will have to defend the terminal until hacking is completed. 

#### Status
Planned, not yet started.
</details><details>
<summary>Rescue (res)</summary>

#### Description
* Mission type requiring the player to locate a hostage and escort them to extraction

#### Status
Planned, not yet started.
</details><details>
<summary>Sabotage (sab)</summary>

#### Description
* Mission type requiring players to reach an objective, and then destroy it before heading to extraction.

#### Status
Planned, not yet started.
</details><details>
<summary>Survival (sur)</summary>

#### Description
* mission type where players will have to fight an endless, steady stream of enemies to survive for as long as possible while slowly losing life support

#### Status
Planned, not yet started.
</details>

### Original ideas. 
* [x] Dark Overrun (dov)
  * Based loosely on the Overrun game type already made by some one else. In this game type, every time you kill an enemy, an increasing amount spawn.
* [x] Tag (tag)
  * Laser weapons only. No scanners. All other items allowed. One shot, if it hits, instant kill.
<details>
<summary>Dark Overrun (dov)</summary>

#### Description
* Based loosely on the Overrun game type already made by some one else. In this game type, every time you kill an enemy, an increasing amount spawn.

#### Status
Fully featured, ready to deploy game servers.
</details>

# Shapes
### Roadmap (For all shapes):
___
* Customizable 
  + Markers (shapes / buildings)
  + Sizes 
  + Locations (X, Y, Z locations, with Z being how far off the ground.)
+ Allow for several sets to be spawned at a time.
+ Rotation of objects to face towards / away from "origin" point.
  + For non-round shapes, such as squares, cubes, etc, this will be a face in/out option.

## SphereStdLib
___
<details>
<summary>Version History</summary>

    2.0 - 29 Apr 2019
        Started comments.

    1.0 - 28 Oct 2018
        Started ground works.
        Finished ground work on 29 Apr 2019.

</details>
<details>
<summary>Gallery</summary>

![Image](https://dump.cmdproj.net/1545613707.png)
![Image](https://dump.cmdproj.net/1545613562.png)
![Image](https://dump.cmdproj.net/1545613442.png)
![Image](https://dump.cmdproj.net/1545613926.png)
![Image](https://dump.cmdproj.net/1545614248.png)
![Image](https://dump.cmdproj.net/sshot0027.jpg)

</details>

## CircleStdLib
___
<details>
<summary>History</summary>

    3.0
        Added in options for XZ, YZ plane circles.
        Added option to let the script skip spawning items that would be underground.
            - Notes: May be bugged with items over a specific size.
        Added functions:
            setPlane()
    
    2.4
        Better documentation of variable names and usage.
        Added in feature to make all items spawn at the same height.
        Added in Center of circle marker option
    
    2.3
        -What did I do?-
    
    2.2
        Organized functions and variables to be in alphabetical order.
    
    2.1
        Added few safe gaurds to make sure objects actually spawn.
        Fixed seemingly random unit from existing by deleting it. (SetObject now deletes the object handed to it)
    
    2.0
        Created namespace `Circle`
        Rewrote great majority of code to fit into new namespace.
        Expanded functions.
            Created Cleanup()
            Created Init()
            Created SetCount()
            Created SetLocation()
            Created SetMode()
            Created SetObject()
            Created SetSize()
            Renamed `makeCircle()` to `SpawnCircle()`
    
    1.5
        Added Mode types, and correlating formulas
    
    1.0
        Basic circle creation.
        Added Offsets.
        Added custome objects.
    

</details>
<details>
<summary>Gallery</summary>

![Image](https://dump.cmdproj.net/unknown_%281%29.png)

</details>

## CubeStdLib
___
<details>
<summary>History</summary>

    1.0 
        Basic outlining ability.
        Core functionality built.
        Added Offsets.

</details>
<details>
<summary>Gallery</summary>

![Image](https://dump.cmdproj.net/unknown.png)

</details>

# Welcome to the Vehicle Depot for the Wilzuun Scripts Library!

```cs
##--------------------------- Main Classification
//
//  Turning off either of these will override all options below. Flyers are not
//    included due to them snapping to map locations, and being very hard to
//    predict, and to hit with weapons.
//
//    Examples: tanks = true; hercs = false; Terran = true;
//      Only Terran Tanks will spawn, no Terran hercs will show up.

//  $allow["Tanks"] : boolean
//    true : Enable AI tanks
$allow["Tanks"]     = true;

//  $allow["Hercs"] : boolean
//    true : Enable AI Hercs
$allow["Hercs"]     = true;

//  $allow["Drone"] : boolean
//    true : Enable AI Drones. (These typically have no weapons, so they're pretty much easy kills.
// $allow["Drone"]     = true; // This is Broken. Research required to fix.

// Flyers aren't included at all in this project.

##--------------------------- Faction Classification
//
//  These options matter if the above options are turned on.
//    Example: Tanks = true; hercs = false; Terran = true; Knight = false;
//      Only tanks will appear. Only Terran tanks will be spawned. Knight tanks
//      will not appear at all

//  $allow["Terran"]  : boolean
//    true : Will spawn in Terran vehicles.
$allow["Terran"]    = true;

//  $allow["Knight"]  : boolean
//    true : Will spawn in Knight vehicles.
$allow["Knight"]    = true;

//  $allow["Pirate"]  : boolean
//    true : Will spawn in Pirate vehicles.
$allow["Pirate"]    = true;

//  $allow["Rebel"]  : boolean
//    true : Will spawn in Rebel vehicles.
$allow["Rebel"]     = true;

//  $allow["Cybrid"]  : boolean
//    true : Will spawn in Cybrid vehicles.
$allow["Cybrid"]    = true;

//  $allow["Metagan"] : boolean
//    true : Will spawn in Metagan vehicles.
$allow["Metagan"]   = true;

//  $allow["Special"] : boolean
//    true : Will spawn in Special vehicles.
$allow["Special"]   = false;

##--------------------------- Specialty Classification
//
//  These last two categories are in respects to groups of vehicles that players
//    may find highly annoying, or undesirable for normal game play. They still 
//    require their Faction Classification to be enabled to show up.

//  $allow["Disruptors"] : boolean
//    true : Will spawn in Disruptor Tanks.
$allow["Disruptors"]= false;

//  $allow["Artillery"] : boolean
//    true : Will spawn in Artillery Tanks.
$allow["Artillery"] = false;
```



These Vehicles are default Configurations from how the Mission Editor drops the vehicles into the game.

They are named `vehId_{ID}.veh` where `{ID}` can be any viable vehicle ID. Table below for Quick reference.

Vehicles are sorted by Vehicle ID.

| Vehicle Name | Vehicle ID|
| ---- | ---- |
| Terran Apocalypse | 1 |
| Terran Minotaur | 2 |
| Terran Gorgon | 3 |
| Terran Talon | 4 |
| Terran Basilisk | 5 |
| Paladin Tank | 6 |
| Myrmidon Tank | 7 |
| Disruptor Tank | 8 |
| Knight's Apocalypse | 10 |
| Knight's Minotaur | 11 |
| Knight's Gorgon | 12 |
| Knight's Talon | 13 |
| Knight's Basilisk | 14 |
| Knight's Paladin | 15 |
| Knight's Myrmidon | 16 |
| Knight's Disruptor | 17 |
| Cybrid Seeker | 20 |
| Cybrid Goad | 21 |
| Cybrid Shepherd | 22 |
| Cybrid Adjudicator | 23 |
| Cybrid Executioner | 24 |
| Bolo Tank | 25 |
| Recluse Tank | 26 |
| Platinum Adjudicator (SP version, not selectable) | 27 |
| Platinum Executioner (SP version, not selectable) | 28 |
| Prometheus | 29 |
| Rebel Emancipator | 30 |
| Avenger Tank | 31 |
| Dreadnought Tank | 32 |
| Rebel Olympian | 33 |
| Metagen Seeker | 35 |
| Metagen Goad | 36 |
| Metagen Shepherd | 37 |
| Metagen Adjudicator | 38 |
| Metagen Executioner | 39 |
| Platinum Adjudicator 2 | 55 |
| Platinum Executioner 2 | 56 |
| Harabec's Apocalypse | 40 |
| Caanan's Basilisk | 42 |
| Harabec's Predator | 41 |
| Harabec's Super Predator | 45 |
| Pirate's Apocalypse | 50 |
| Pirate's Dreadlock | 51 |
| Pirate's Emancipator | 52 |
| Terran Empty Cargo | 60 |
| Terran Ammo Cargo | 61 |
| Terran Big Ammo Cargo | 62 |
| Terran Big Personnel Cargo | 63 |
| Terran Fuel Cargo | 64 |
| Terran Minotaur Cargo | 65 |
| Rebel Empty Cargo | 66 |
| Rebel Ammo Cargo | 67 |
| Rebel Big Cargo Transport | 68 |
| Rebel Bix Box Cargo Transport | 69 |
| Rebel Box Cargo Transport | 70 |
| Terran Utility Truck | 71 |
| Rebel Thumper | 72 |
| Terran Starefield | 73 |
| Cybrid Artillery | 90 |
| Cybrid Omnicrawler | 94 |
| Cybrid Protector | 95 |
| Cybrid Jamma | 96 |
| Nike Artillery | 133 |
| Supressor Tank | 134 |
| Rebel bike | 138 |
| Rebel Artillery | 137 |
| SUAV Bus | 150 |