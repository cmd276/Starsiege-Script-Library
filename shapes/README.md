# Wilzuun Scripts Library
##### By ^TFW^ Wilzuun

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
