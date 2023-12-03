const fs = require('fs');

if (process.argv.length < 3) {
    console.error("Not enough arguments");
    return;
}

const file = process.argv[2];
console.log("Input file: ", file);

try {
    const data = fs.readFileSync(file, 'utf-8');
    const lines = data.split('\n');

    // determineSymbols(lines);
    const grid = createGrid(lines);
    const symbols = [ '-', '+', '*', '&', '/', '@', '%', '=', '$', '#' ];

    const isSymbol = (c) => symbols.includes(c);
    const isNumber = (c) => c >= '0' && c <= '9';
    const createAdjacencies = (i, j) => {
        let result = [
            { i: i-1, j: j },   //   up
            { i: i-1, j: j+1 }, //   up, right
            { i: i,   j: j+1 }, //       right
            { i: i+1, j: j+1 }, // down, right
            { i: i+1, j: j },   // down
            { i: i+1, j: j-1 }, // down, left
            { i: i,   j: j-1 }, //       left
            { i: i-1, j: j-1 }, //   up, left
        ];

        result = result.filter(adj => (
            adj.i >= 0 && adj.i < grid.length &&
            adj.j >= 0 && adj.j < grid[0].length // assume grid rows are all the same length
        ));
        
        return result;
    };

    let sum = 0;

    for (let i = 0; i < grid.length; ++i) {
        for (let j = 0; j < grid[i].length; ++j) {
            const symbol = grid[i][j];
            if (isSymbol(symbol)) {
                let adjacencies = createAdjacencies(i, j);

                const parts = [];

                while(adjacencies.length > 0) {
                    const adj = adjacencies.shift();
                    const c = grid[adj.i][adj.j];
                    if (isNumber(c)) {
                        // search left for the beginning of the number
                        let numberStart = adj.j;
                        while(numberStart-1 >= 0 && isNumber(grid[adj.i][numberStart-1])) {
                            numberStart--;
                        }

                        // search right for the end of the number
                        let numberEnd = adj.j;
                        while(numberEnd+1 < grid[adj.i].length && isNumber(grid[adj.i][numberEnd+1])) {
                            numberEnd++;
                        }

                        let numberString = "";
                        for (let k = numberStart; k <= numberEnd; k++) {
                            numberString += grid[adj.i][k];
                            
                            // Remove adjacencies that are part of this number
                            adjacencies = adjacencies.filter( x => x.i != adj.i || x.j != k);
                        }

                        // console.log(numberString, parseInt(numberString));
                        parts.push(parseInt(numberString));
                    }
                }

                // If this part is a gear
                if (parts.length == 2) {
                    sum += parts[0] * parts[1];
                }
            }
        }
    }

    console.log("Sum", sum);

} catch (err) {
    console.error(err);
}

function createGrid(lines) {
    const grid = [];
    for (let i = 0; i < lines.length-1; ++i) {
        const row = [];
        for (let j = 0; j < lines[i].length; ++j) {
            const c = lines[i][j];
            if (c != '\n') {
                row.push(c);
            }
        }
        grid.push(row);
    }

    return grid;
}

function determineSymbols(lines) {
    const symbols = [];

    for (let i = 0; i < lines.length; ++i) {
        console.log(lines[i]);
        for (let j = 0; j < lines[i].length; ++j) {
            const c = lines[i][j];
            debugger;
            if ((c < '0' || c > '9') && c != '.' && c != '\n') {
                //console.log("Symbol", c);
                if (!symbols.includes(c)) {
                    symbols.push(c);
                }
            }
        }
    }

    console.log("const symbols =", symbols);
}
