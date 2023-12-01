const fs = require('fs');

try {
    const data = fs.readFileSync('input.txt', 'utf-8');
    const lines = data.split('\n');

    for (let i = 0; i < lines.length; ++i) {
        console.log(lines[i]);
    }
} catch (err) {
    console.error(err);
}
