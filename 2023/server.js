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

    for (let i = 0; i < lines.length; ++i) {
        console.log(lines[i]);
    }
} catch (err) {
    console.error(err);
}
