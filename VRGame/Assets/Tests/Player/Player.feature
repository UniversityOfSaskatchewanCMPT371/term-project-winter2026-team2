Feature: Player

  Rule: The Player's name and ID should stay the same the whole time they're playing

  Scenario: Player initializes with default values
    Given the game is starting
    When the player GameObject is created
    Then the player should have name "Player"
    And the player should have ID 1
    And the player should be alive

  Scenario: Player data should stay the same across scenes
    Given the player has been initialized with name "Player" and ID 1
    When the player moves from one scene to another
    Then the player's name should still be "Player"
    And the player's ID should still be 1
    And the player should still be alive