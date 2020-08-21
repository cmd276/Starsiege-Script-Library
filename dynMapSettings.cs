##------------------------------------------------------------------------------
## Dynamic Pathway settings.
##------------------------------------------------------------------------------
// Do we use this module?
$dyn::Path::Enabled = true; 
// Quantity of way points to spawn.
$dyn::Path::Links = 10; 
// Distance between two points. In meters. 
$dyn::Path::Distance = 750; 
// The allowable variation in distance. EX: A Jitter of 0.10 (10%) will allows for a 750 Distance to actually be randomly between 675m and 825m between each point.
$dyn::Path::Jitter = 0.10; 
// Variance allows for angle adjustment... its a science thing. Please keep this as a multiple of 360.
$dyn::Path::Variance = 1080; 

// Where to put the first way point
// r is radius. It makes a circle, then gets a random point in that circle for the first waypoint placement.
// if r is 0, will place at exactly the points given.
$dyn::Path::origin['x'] = 0; 
$dyn::Path::origin['y'] = 0; 
$dyn::Path::origin['r'] = 50; 

##------------------------------------------------------------------------------
## Dynamic City Settings.
##------------------------------------------------------------------------------
// Are we making cities?
$dyn::City::Enabled = true; 
// What shape city are we making?
//      'Random'   : Will select one of the follwing options.
//      'Square'   : Options for Rectangle to come....
//      'Triangle' : Only equilateral triangles exist in this script, sorry!
//      'Circle'   : A city that appears to go around a few times.
// You can add specific shapes, such as "Square" and "Random." Theoretically, it should give
// squares twice as much appearance rates. Or if you dont want one of the two shapes, an example
// would be to use 0 for "triangle" and use 1 for "circle" because square cities are #r00d.
// Shape0 *must* have a value!!!
$dyn::City::Shape0 = 'Random';
// $dyn::City::Shape1 = 'Square';
// What size city are we making? This would be one measurement of the shape picked.
// If circle, this is the diameter.
$dyn::City::Size = 1500;
// What location? The center of the city's location will be here.
$dyn::City::xOffset = 0;
$dyn::City::yOffset = 0;
// Included for the know-it-all mapper.... Use this with caution if new.
// Uncomment and give it a numerical value.
// $dyn::City::zOffset = false;

