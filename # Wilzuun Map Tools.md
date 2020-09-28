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

## Building Size Project
A project that is recording the size of the buildings in the game. This project mainly exists for the DynCityStdLib project.

## DynPathStdLib
A dynamic pathway system for players to follow on a mission. Randomly generates where the next point should be while players are concentrating on mission objectives.
The idea for this project came from a conversation about Warframe, and how it had a dynamic setup of where to go to do what. I thought I could at least snag the idea of dynamic pathways and give normal Starsiege objectives at each pathway marker.


## logStdLib
A log feature set, set aside for the other scripts in this collection.


## Game Types
A project of porting over a lot of the Warframe game types.

## Shapes
### Polygon
<details>
<summary>History</summary>

    1.0 - 04 Sept 2020
        Made the function that plotted out all the points of the shape's edge.
        Flushed out the minor bugs that had arisen with point plots.
        Flushed out the math to do rough line fills of the shape's edges.
        Added in common features from the other shape feature-sets.
        Added in a shape rotation.
        Added in plane direction. (Works with shape rotation.)
    
</details>

### Star
<details>
<summary>History</summary>

    1.0 - 04 Sept 2020
        Made the function that plotted out all the points of the shape's edge.
        Flushed out the minor bugs that had arisen with point plots.
        Flushed out the math to do rough line fills of the shape's edges.
        Added in common features from the other shape feature-sets.
        Added in a shape rotation.
        Added in plane direction. (Works with shape rotation.)
    
</details>

### Sphere
<details>
<summary>History</summary>

    2.0 - 04 Sept 2020
        Reset version history, as it was lost.
        Small math revisions - readability prettiness only.
        Merged from stand alone library into Shapes Library.
        Fixed some math errors, that prevented the sphere from spawning where a map maker would want it to appear.
    
</details>

### Circle
<details>
<summary>History</summary>

    2.0 - 04 Sept 2020
        Reset version history, as it was lost.
        Small math revisions - readability prettiness only.
        Merged from stand alone library into Shapes Library.
        From pervious versions, a lot of overhaul.
            - Removed a great majority of the global variables.
            - made it so the main function took parameters.
            - Removed functions that were unneeded.

</details>

## Vehicle Depot
A vehicle AI random spawn project of sorts. This is used in my game types for generating random vehicles for AI to pilot.
