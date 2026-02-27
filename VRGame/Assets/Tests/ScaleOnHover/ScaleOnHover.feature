Feature: Scale on Hover

  Rule: Objects scale smoothly on hover and return to original size when not hovered.

  Scenario: Object scales up when pointed at
    Given an interactive object (e.g., a brain part) exists in the scene
    When the player points at the object
    Then the object's scale should gradually increase to a larger size
    And the scaling should be smooth (not instant)

  Scenario: Object scales back when player looks away
    Given the player is pointing at an interactive object and it has grown
    When the player points away from the object
    Then the object's scale should gradually return to its original size