const fs = require('fs');

try {
    const data = fs.readFileSync('input.txt', 'utf-8');
    const lines = data.split('\n');

    let sum = 0;
    for (let i = 0; i < lines.length; ++i) {
        const numbers = Array.from(lines[i]).filter((c) => c >= '0' && c <= '9');
        if (numbers.length > 0) {
            console.log(numbers[0], numbers[numbers.length - 1], parseInt(numbers[0] + numbers[numbers.length - 1]));
            sum += parseInt(numbers[0] + numbers[numbers.length - 1]);
        }
    }
    console.log(sum);
} catch (err) {
    console.error(err);
}
