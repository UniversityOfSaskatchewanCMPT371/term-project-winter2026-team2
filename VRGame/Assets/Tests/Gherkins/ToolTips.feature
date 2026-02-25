Feature: ToolTips

  Rule: Tooltips appear only when pointing at a brain part 
        and show the correct title and description.

  Scenario: Tooltip appears with correct information when pointing at a brain part
    Given the frontal lobe has a title "Frontal Lobe"
    And the frontal lobe has a description "Responsible for decision making"
    When the player points at the frontal lobe
    Then a tooltip appears
    And the tooltip shows the title "Frontal Lobe"
    And the tooltip shows the description "Responsible for decision making"

  Scenario: Tooltip disappears when player points away
    Given the player is pointing at the frontal lobe and the tooltip is visible
    When the player points away from the brain part
    Then the tooltip disappears