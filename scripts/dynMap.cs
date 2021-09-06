exec("dynMapSettings.cs");

function onMissionPreload ()
{
    // We know the map is setup like this. Its a built in requirement.
    %group = getGroupId("MissionGroup");
    %item = getNextObject(%group, 0);
    for(%count = 0; %item != 0; %count++)
    {
        // Don't remove these two groups, they're also required.
        if (getObjectName(%item) != "Volumes") && (getObjectName(%item) != "world") {}
        // delete everything else... we dont need it, we dont want it.
        else
        {
            deleteObject(%item);
        }
        %item = getNextObject(%group, %item);
    }

    exec("dynamicMap.cs");
}
