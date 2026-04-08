using ObjectMatchGame;
/// <summary>
/// This class's only job is to be have a static array of levelData objects that
/// can be accessed by the game model. Each one contains:
/// - The level number (level 0 MUST be the tutorial)
/// - The ID of the correct object for that level
/// - The IDs of all objects in the level (including the correct object and the target object, which is the object that the user is trying to match the correct object to)
/// - The maximum time allowed to get bonus points upon level completion
/// - The base points awarded for completing the level
/// 
/// This is placed in it's own class to separate it out - edits to the levels that
/// do not need or want to change any behaviour can do so here with no issues
/// </summary>
public class LevelData
{
    internal static levelData[] levels = new levelData[] {
        new levelData(0, "level0object2", new string[] {"level0object1", "level0object2", "level0object3", "level0targetObject"}, 60, 100),
        new levelData(1, "level1object1", new string[] {"level1object1", "level1object2", "level1object3", "level1targetObject"}, 60, 150),
        new levelData(2, "level2object2", new string[] {"level2object1", "level2object2", "level2object3", "level2targetObject"}, 60, 200),
        new levelData(3, "level3object3", new string[] {"level3object1", "level3object2", "level3object3", "level3targetObject"}, 60, 250),
    };
}



