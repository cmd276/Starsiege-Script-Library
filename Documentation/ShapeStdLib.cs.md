# ShapeStdLib.cs

Welcome to the Shapes Standard Library!

This is the project that started out the entire project of Mapping tools!

This is the project that started the thought process of the Dynamic Map tools.

This script requires the WilzuunStdLib.cs file.
This script also requires the trigonometry.cs file by [M.I.B] Sentinel.

This file contains all the settings for the Shapes library, along with all the functionality of the Shapes library.

# Variables 

```cs
// This determines whether we're using the terrain height at the center of the shape as the terrain height of all the objects in the shape.
// good for making sure all the objects are the same height in the shape. (IE If you were to make a ring out of bridges, this will help)
$Shape::zCenter     = true;

// This is the object we're using to place at all the points of the shape.
// In this example, we're using a projectile for the electro-magnetic pulse cannon.
$Shape::Object        = "pr_emp";

// Are we placing a marker at the center of the shape.
// TRUE / FALSE
$Shape::markCenter  = true;

// What shape is being placed at the center of the shape
// In this example it is a red beam, created by Drake.
$Shape::marker      = redBeam;

// Allow/Disallow the rotation of a building based on X Axis, or Z axis.
// These are not interconnected with each other.
// Both are TRUE / FALSE 
$Shape::xRotate     = false;
$Shape::zRotate     = false;

// Rotation Modifiers. Only used if the respective variable above is set to TRUE.
// These are FLOAT or INT values. They represent a degree rotation of the object.
$Shape::xRotMod     = 0;
$Shape::zRotMod     = 0;

// Allow forcing the spawn of an object if its base point is below ground
// TRUE / FALSE
// if false, if the game thinks that the shape's grab handle is underground, it will not spawn the object.
// This is useful to keep spawned items to a minimum.
$Shape::underground = true;

// The GroupName of the shape to be spawned.
// This is a historical variable, and is not longer highly important to the script, its a abritrary value now.
// This will spawn any shape in the library with the GroupName followed by a number. 
// EX: TheShape1; TheShape2; TheShape3; so on.
$Shape::GroupName   = "TheShape";
```

# Functions

```cs
// These options are for most shapes in the collection.
// %object is the Object to use for the shape's outline.
// %numberOfSides is the shape's number of sides.
// %radius is distance from middle of the shape to the corners of the shape.
// %distanceBetweenObjects is a measurement of meters between the objects making the shape's outline.
// %objectsPerSide is a number of objects per side. Will calculate (roughly) how much distance to put between objects.
// %xOffset is the X position of the shape.
// %yOffset is the Y position of the shape.
// %zOffset is how far off the ground the Shape will be placed.
// %rotationOfShape allows you to rotate the shape by s specific number of degrees.
// %shapePlane allows you to determine how the shape is spawned. 
//      (For use with Polygon, Polygon2, Star, and Star2.
//      Plane 1: xz plane. Shape "stands" facing north/south.
//            2: yz plane. Shape "stands" facing east/west.
//            3: XY plane, sloped Z going eash/west? (Needs verification.)
//            4: XY plane, sloped Z going north/south? (Needs verification.)
//             : All other options are XY planes. Horizontal, "laying" on the ground.
//      (For use with Circle)
//      Plane XY: A circle that lays on the ground.
//      Plane XZ: Circle that faces north/south. "Stands" up.
//      Plane YZ: Circle that faces east/west. "Stands" up.



// This creates a multi-sided shape. It is limited to shapes with 2 or more sides and shapes with less than 100 sides.
Shape::Polygon(%object, %numberOfSides, %radius, %distanceBetweenObjects, %xOffset, %yOffset, %zOffset, %rotationOfShape, %shapePlane);

// Creates a polygon based on how many Objects are wanted on each side, instead of doing a set distance between each object.
Shape::Polygon2(%object, %numberOfSides, %radius, %objectsPerSide, %xOffset, %yOffset, %zOffset, %rotationOfShape, %shapePlane);

// Creates a star shape pattern, based on a polygon count.
Shape::Star(%object, %numberOfSides, %radius, %distanceBetweenObjects, %xOffset, %yOffset, %zOffset, %rotationOfShape, %shapePlane);

// Creates a star shape pattern. Instead of giving the desired space between objects, you give it hte number of objects to put on each line of the star.
Shape::Star2(%object, %numberOfSides, %radius, %objectsPerSide, %xOffset, %yOffset, %zOffset, %rotationOfShape, %shapePlane);

// Creates a Sphere object.
// %hSpawn is how many `slices` a shape will have, or how many `rows` of objects will make the sphere
// %itemCount is how many Items per `slice`.
function Shape::Sphere(%object, %itemCount, %radius, %xOffset, %yOffset, %zOffset, %hSpawn)

// This creates a circle shape.
function Shape::Circle (%object, %itemCount, %radius, %xOffset, %yOffset, %zOffset, %plane)

// Used by the Shape functions above to add an Object to the center of each shape.
shape::AddCenter(%group, %xOffset, %yOffset, %zOffset);

// It exists, but not used... Yet. Will add an Object to a shape, at the %x %y %z location with a Z rotation of %zr and an X rotation of %xr
shape::AddItem(%group, %object, %x, %y, %z, %xr, %zr);

// Gets the next available ID for the shapes that need generated.
shape::getNextId(%groupName);

// Checks to see if an ID is already in use.
shape::__IdUsed(%groupName, %id);

// Deletes a shape from the map. If for some reason multiple shapes with the same ID exist, will only delete the first one found.
shape::Cleanup(%groupName, %id);

// Mass deletes shapes with the same Exact name.
shape::Cleanup(%groupName, %id);
```











