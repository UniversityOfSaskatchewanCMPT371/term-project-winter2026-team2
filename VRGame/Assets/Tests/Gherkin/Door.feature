Feature: Door Interaction and Scene Transition

  Rule: The door must load the correct scene when interacted with, 
        and handle errors gracefully

Background:
    Given a door exists in the scene
    And the door has valid DoorData
    And the scene changer service is available

Scenario: Interacting with door loads the target scene
  Given a door with valid DoorData
  And a valid scene changer service
  And the door is not currently transitioning
  When the door is interacted with
  Then the scene changer should load the target scene

Scenario: Interacting multiple times only loads scene once
  Given a door with valid DoorData
  And a valid scene changer service
  And the door is not currently transitioning
  When the door is interacted with
  And the door is immediately interacted with again
  Then the scene changer should load the target scene only once

Scenario: Scene changer service is null
  Given a door with valid DoorData
  And the scene changer service is null
  When the door is interacted with
  Then no scene should be loaded
  And an error should be logged

Scenario: DoorData component is missing
  Given a door without DoorData
  When the door initializes
  Then an assertion error should occur