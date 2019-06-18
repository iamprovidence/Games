// CONST
const LEFT_ARROW    = 37;
const UP_ARROW      = 38;
const RIGHT_ARROW   = 39;
const DOWN_ARROW    = 40;

// CONFIG
const ANIMATION_LENGTH = 0.08;
var INITIAL_TILE_COLOR = Math.floor(Math.random() * 360);

// GRID
let div_gameContainer = document.getElementById("game_container");

let gridSize = 4;
let tileWidth;

let score = 0;
let p_score = document.getElementById("score");

let grid = [];

function getGridSize(defaultValue = 4)
{
    let input_grid_size = document.getElementById("input_grid_size");

    let value = Number.parseInt(input_grid_size.value);

    let min = input_grid_size.getAttribute("min");
    let max = input_grid_size.getAttribute("max");

    let validatedValue = (function()
    {
        if (Number.isNaN(value)) return defaultValue;
        
        if (value < min) return min;
        if (value > max) return max;
        else return value;
    })();

    input_grid_size.value = validatedValue;

    return validatedValue;
}

function findFreePosition()
{
    let positions = [];
    for (let i = 0; i < gridSize; ++i) 
    {
        for (let j = 0; j < gridSize; ++j) 
        {
            if (grid[i][j] == undefined) 
            {
                positions.push({i, j});
            }
        }
    }

    return positions[Math.floor(Math.random() * positions.length)];
}

function addTile()
{
    let position = findFreePosition();

    let tile = new Tile(position);
    updateScore(tile.value);

    grid[position.i][position.j] = tile;

    div_gameContainer.appendChild(tile.div);
}
function updateScore(value)
{
    score += value;
    p_score.innerText = score;
}
function removeTile(position)
{    
    let tile = grid[position.i][position.j];
    grid[position.i][position.j] = undefined;

    setTimeout(() => 
    {
        div_gameContainer.removeChild(tile.div);
    }, 1000 * ANIMATION_LENGTH);
}


// TILE
class Tile 
{
    // CONSTRUCTORS
    constructor(position) 
    {
        this.createDiv();

        this.setPosition({p_x: position.i * tileWidth, p_y: position.j * tileWidth});

        this.value = Math.random() < 0.5 ? 2 : 4;
        this.level = 0;

        this.updateStyle();
    }

    createDiv()
    {        
        this.div = document.createElement("div");
        
        // create div
        this.div.style.width = tileWidth + "px";
        this.div.style.height = tileWidth + "px";
        this.div.style.position = "absolute";
        this.div.style.transition = ANIMATION_LENGTH + "s";
        this.div.style.textAlign = "center";
        
        // create text inside div
        this.text = document.createElement("p");
        this.text.style.display = "inline-block";
        this.text.style.lineHeight = tileWidth + "px";
        this.text.style.verticalAlign = "middle";
        this.text.style.fontSize = tileWidth/2 + "px";
        this.text.style.color = "rgb(250, 250, 250)";

        this.div.appendChild(this.text);
    }

    // METHODS
    levelUp() 
    {
        this.value *= 2;
        ++this.level;
    }

    updateStyle() 
    {
        this.div.style.backgroundColor = `hsl(${INITIAL_TILE_COLOR - this.level * 8}, 100%, 40%)`;

        this.text.innerText = this.value;
    }

    setPosition({p_x, p_y}) 
    {        
        this.div.style.left = p_x + "px";
        this.div.style.top = p_y + "px";
    }
    setX(p_x) 
    {
        this.div.style.left = p_x + "px";
    }
    setY(p_y) 
    {
        this.div.style.top = p_y + "px";
    }
}

// MOVING EVENT
function move_left ()
{
    let moves_done = 0;

    for (let j = 0; j < gridSize; ++j) 
    {
        let right = 0;
        let lastTile = undefined;

        for (let i = 0; i < gridSize; ++i) 
        {
            if (grid[i][j] != undefined) 
            {
                if (lastTile != undefined && lastTile.value == grid[i][j].value) 
                {
                    // combine
                    lastTile.levelUp();
                    lastTile.updateStyle();

                    // remove cell
                    let tile = grid[i][j];
                    tile.setX((right - 1) * tileWidth);
                    grid[i][j] = undefined;
                    setTimeout(() => 
                    {
                        div_gameContainer.removeChild(tile.div);
                    }, 1000 * ANIMATION_LENGTH);

                    ++moves_done;
                }
                else 
                {
                    let tile = grid[i][j];

                    // move left, to empty spot
                    if (right != i) 
                    {
                        tile.setX(right * tileWidth);
                        grid[i][j] = undefined;
                        grid[right][j] = tile;

                        ++moves_done;
                    }

                    ++right;
                    lastTile = tile;
                }
            }
        }
    }

    return moves_done;
}

function move_right() 
{
    let moves_done = 0;

    for (let j = 0; j < gridSize; ++j) 
    {
        let left = gridSize - 1;
        let lastTile = undefined;

        for (let i = gridSize - 1; i >= 0; --i) 
        {
            if (grid[i][j] != undefined) 
            {
                if (lastTile != undefined && lastTile.value == grid[i][j].value) 
                {
                    // combine
                    lastTile.levelUp();
                    lastTile.updateStyle();

                    // remove cell
                    grid[i][j].setX((left + 1) * tileWidth);
                    removeTile({i, j});

                    ++moves_done;
                }
                else 
                {
                    let tile = grid[i][j];

                    // move right to empty spot
                    if (left != i) 
                    {
                        tile.setX(left * tileWidth);
                        grid[i][j] = undefined;
                        grid[left][j] = tile;

                        ++moves_done;
                    }

                    --left;
                    lastTile = tile;
                }
            }
        }
    }
    return moves_done;
}

function move_up() 
{
    let moves_done = 0;
    for (let i = 0; i < gridSize; ++i) 
    {
        let bottom = 0;
        let lastTile = undefined;
        
        for (let j = 0; j < gridSize; ++j) 
        {
            if (grid[i][j] != undefined) 
            {
                if (lastTile != undefined && lastTile.value == grid[i][j].value) 
                {
                    // combine
                    lastTile.levelUp();
                    lastTile.updateStyle();

                    // remove cell
                    grid[i][j].setY((bottom - 1) * tileWidth);
                    removeTile({i, j});

                    ++moves_done;
                }
                else 
                {
                    let tile = grid[i][j];

                    // move up to empty spot
                    if (bottom != j) 
                    {
                        tile.setY(bottom * tileWidth);
                        grid[i][j] = undefined;
                        grid[i][bottom] = tile;

                        ++moves_done;
                    }

                    ++bottom;
                    lastTile = tile;
                }
            }
        }
    }

    return moves_done;
}

function move_down()
{
    let moves_done = 0;
    
    for (let i = 0; i < gridSize; ++i) 
    {
        let top = gridSize - 1;
        let lastTile = undefined;

        for (let j = gridSize - 1; j >= 0; --j) 
        {
            if (grid[i][j] != undefined) 
            {
                if (lastTile != undefined && lastTile.value == grid[i][j].value) 
                {
                    // combine
                    lastTile.levelUp();
                    lastTile.updateStyle();

                    // remove cell                    
                    grid[i][j].setY((top + 1) * tileWidth);
                    removeTile({i, j});

                    moves_done++;
                }
                else 
                {
                    let tile = grid[i][j];

                    // moves down to empty spot
                    if (top != j) 
                    {

                        tile.setY(top * tileWidth);
                        grid[i][j] = undefined;
                        grid[i][top] = tile;

                        ++moves_done;
                    }

                    --top;
                    lastTile = tile;
                }
            }
        }
    }
    return moves_done;
}

onkeydown = function (event) 
{
    // do moves
    let moves_done = null;

    if (event.keyCode == LEFT_ARROW) 
    {
        moves_done = move_left();
    }
    else if (event.keyCode == RIGHT_ARROW) 
    {
        moves_done = move_right();
    }
    else if (event.keyCode == UP_ARROW) 
    {
        moves_done = move_up();   
    }
    else if (event.keyCode == DOWN_ARROW)
    {
        moves_done = move_down();
    }

    // add new tile only when 
    // any tile has moved
    if (moves_done > 0)
    {
        this.setTimeout(addTile, 1000 * ANIMATION_LENGTH);
    }
};
// START GAME
function restart() 
{
    // reset config
    INITIAL_TILE_COLOR = Math.floor(Math.random() * 360);
    score = 0;

    // clean grid
    div_gameContainer.innerHTML = "";

    gridSize = getGridSize();
    tileWidth = div_gameContainer.getBoundingClientRect().width / gridSize;

    grid = new Array(gridSize);
    for (let i = 0; i < gridSize; ++i) 
    {
        grid[i] = new Array(gridSize).fill(undefined);
    }
    
    addTile();
    addTile();
}

// start game
document.getElementById("button_restart").onclick = restart;
restart();