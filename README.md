# `Sudoku Application`

### `Accessible`

Runs the app in the development mode.<br />
Open (https://sudoku-solver-alpha.azurewebsites.net) to view it in the browser.


### `Features`

Once the website ahs been opened you will see the sudoku puzzle aswell as 3 buttons either placed on the side or ontop of the puzzle.

- Clicking the `Genreate` button generates a new sudoku puzzle.
- Clicking the `Solution` button will find a solution to the current puzzle a along with any edits you have made to it.
- Clicking the `Submit` button will check if the changes you have made to the puzzle are correct.


### `API`

The application exposes 2 end points.

- /api/sudoku/get-solutuion used to find a solution to the sudoku puzzle.
- /api/sudoku/check-answer used to check if your solution to the puzzle is correct.

#### /api/sudoku/get-solutuion

Request Type: POST
Consumes: JSON
Body: {currentBoard:[9][9]}

Objects creating the 9x9 array should have this form {wasGiven:<bool>, value: <int>}

#### /api/sudoku/check-answer

Request Type: POST
Consumes: JSON
Body: {originalBoard:[9][9], edittedBoard:[9][9]}

Objects creating the 9x9 array should have this form {wasGiven:<bool>, value: <int>}

### `EXTERNAL API'S`

The application uses 1 external api to produce the sudoku puzzles.

- https://sugoku.herokuapp.com/board?difficulty=easy
