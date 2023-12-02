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

    const inventory = {
        red: 12,
        green: 13,
        blue: 14
    };

    let sum = 0;

    for (let i = 0; i < lines.length-1; ++i) {
        console.log(lines[i]);
        const game = parseGame(lines[i]);
        
        const isPossible = (round) => {
            return (
                round.red <= inventory.red &&
                round.green <= inventory.green &&
                round.blue <= inventory.blue
            );
        };
        if (game.rounds.every(round => isPossible(round))) {
            console.log( "Game", game.id, "is possible");
            sum += game.id;
        } else {
            console.log( "Game", game.id, "is NOT possible");
        }
    }

    console.log(sum);
} catch (err) {
    console.error(err);
}

// Returns an object:
// {
//     id: <id>,
//     rounds: [{
//       red: <redCount>,
//       green: <greenCount>,
//       blue: <blueCount>
//     }, 
//     ...];
//  }
function parseGame(game) {
    if (!game.length) return;

    // Get the game id
    const spaceIndex = game.indexOf(" ");
    const colonIndex = game.indexOf(":");
    const id = parseInt(game.substr(spaceIndex, colonIndex));

    let result = { id, rounds: [] };

    const rounds = game.substr(colonIndex+1).split(";");
    rounds.forEach(round => { 
        let red = 0;
        let green = 0;
        let blue = 0;
        round = round.trim(); 
        const colors = round.split(",");
        let colorObj = { red: 0, green: 0, blue: 0 };
        colors.forEach(color => {
            color = color.trim();
            const countIndex = color.indexOf(" ");
            const count = parseInt(color.substring(0, countIndex));
            const colorType = color.substring(countIndex + 1).trim();

            switch(colorType) {
                case "red": colorObj = { ...colorObj, red: count }; break
                case "green": colorObj = { ...colorObj, green: count }; break;
                case "blue": colorObj = { ...colorObj, blue: count }; break;
                default: console.error("unknown color type");
            }
        });

        result.rounds = [...result.rounds, colorObj];
    });

    console.log(result);
    return result;
}
