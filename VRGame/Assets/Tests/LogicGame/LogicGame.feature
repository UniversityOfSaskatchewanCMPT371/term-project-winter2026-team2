Feature: Drawing lines
Rule: Player must be able to draw lines properly

Scenario: Beginning of successful line draw
	Given coloured line is not drawn yet
	When player is aiming at coloured line starting position
	And player presses and holds trigger
	And player drags aim across adjacent tiles that are not blocked or occupied
	Then coloured line begins drawing
	And starting panel and adjacent panel update accordingly

Scenario: Continuing line draw
	Given player is currently drawing a coloured line that is valid
	When player’s aim shifts to an adjacent panel that is unoccupied
	Then line extends to adjacent panel
	And previous panel and adjacent panel update accordingly

Scenario: End of successful line draw
	Given player is currently drawing a coloured line that is valid
	When player releases trigger
	And player is aiming at an endpoint
	Then coloured line is drawn

Scenario: Unsuccessful line draw
	Given player is currently drawing a coloured line that is otherwise valid
	When player’s aim shifts to an adjacent panel that is a block or another coloured line’s segment or itself (if it isn’t the segment immediately before the player’s aim)
	Then coloured line is cleared and the associated panels reset
	And sound plays

Scenario: Reversing line draw
	Given player is currently drawing a coloured line that is valid
	When player’s aim shifts to the segment immediately before the current one
	Then current panel is reset

Scenario: Resetting line
	Given there is a coloured line drawn
	When player is aiming at a coloured line
	And player presses and releases trigger
	Then coloured line and the associated panels reset

Rule: Game must have proper completion handling

Scenario: Initialization
	Given player begins game
	Then no lines are drawn
	And game is not complete

Scenario: Completing line
	Given player completes line
	And not all lines are complete
	Then game is not complete

Scenario: Completing game
	Given player completes line
	And all lines are now complete
	Then game is completed
