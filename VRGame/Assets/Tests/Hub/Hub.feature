Feature: Hub and Scene

  Rule: The hub must load with all objects,
        and moving through doors should load the next scene

  Scenario: Hub loads with player at spawn point
    Given the game is starting
    When the hub scene loads
    Then the player should be at the spawn point
    And the brain model should be there
    And the doors to other rooms should be there

  Scenario: Player goes through a door to another room
    Given the player is in the hub and a door leads to the next room
    When the player walks through the door
    Then the next room scene should load
    And the player should end up at that room's spawn point
