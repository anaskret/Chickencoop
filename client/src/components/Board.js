export default class Board {
  constructor() {
    //define the board array
    this.cells = [
      ["", "", ""],
      ["", "", ""],
      ["", "", ""]
    ];
  }

  /**
   * @param x The x location
   * @param y The y location
   * @param player The player either 'x' or 'o'
   * @returns {boolean} True if successful, False if invalid move.
   */

  //check if move is possible to perform, if it is perform it, otherwise return false
  doMove(x, y, player) {
    if (this.cells[x][y] !== "") {
      return false;
    }

    this.cells[x][y] = player;
    return true;
  }

  //check for possible win conditions
  playerHas3InARow(player) {
    // Horizontal rows
    for (let i = 0; i < 3; i++) {
      if (
        this.cells[0][i] === player &&
        this.cells[1][i] === player &&
        this.cells[2][i] === player
      ) {
        return true;
      }
    }

    // Vertical rows
    for (let i = 0; i < 3; i++) {
      if (
        this.cells[i][0] === player &&
        this.cells[i][1] === player &&
        this.cells[i][2] === player
      ) {
        return true;
      }
    }

    // Diagonals
    if (
      this.cells[0][0] === player &&
      this.cells[1][1] === player &&
      this.cells[2][2] === player
    ) {
      return true;
    }
    if (
      this.cells[2][0] === player &&
      this.cells[1][1] === player &&
      this.cells[0][2] === player
    ) {
      return true;
    }

    return false;
  }

  //check if the game is over
  isGameOver() {
    return (
      this.getPossibleMoves().length === 0 ||
      this.playerHas3InARow("x") ||
      this.playerHas3InARow("o")
    );
  }

  clone() {
    let clone = new Board();

    for (let i = 0; i < 3; i++) {
      for (let j = 0; j < 3; j++) {
        clone.cells[i][j] = this.cells[i][j];
      }
    }

    return clone;
  }

  //crete a new board
  resetBoard() {
    this.cells = [
      ["", "", ""],
      ["", "", ""],
      ["", "", ""]
    ];
  }
}
