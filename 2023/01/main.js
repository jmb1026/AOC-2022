const fs = require('fs');

try {
    const data = fs.readFileSync('input.txt', 'utf-8');
    const lines = data.split('\n');

    let sum = 0;
    for (let i = 0; i < lines.length; ++i) {
        sum += extractNumbers(lines[i]);
    }

    console.log(sum);
} catch (err) {
    console.error(err);
}

function extractNumbers(value) {
    const characters = Array.from(value);
    if (characters.length == 0) {
        return 0;
    }

    const numbers = [];

    for (let i = 0; i < characters.length; i++) {
        const c = characters[i];
        if (c >= '0' && c <= '9')
        {
            numbers.push({ pos: i, digit: c });
        }
    }

    const words = [
        { word: "one", digit: "1" },
        { word: "two", digit: "2" },
        { word: "three", digit: "3" },
        { word: "four", digit: "4" },
        { word: "five", digit: "5" },
        { word: "six", digit: "6" },
        { word: "seven", digit: "7" },
        { word: "eight", digit: "8" },
        { word: "nine", digit: "9" }
    ];

    for (let i = 0; i < words.length; i++) {
        let index = value.indexOf(words[i].word);
        while (index >= 0) {
            numbers.push({ pos: index, digit: words[i].digit });

            index = value.indexOf(words[i].word, index+1);
        }
    }

    numbers.sort((a, b) => a.pos - b.pos);

    const number = parseInt(numbers[0].digit + numbers[numbers.length-1].digit);
    console.log(value, numbers[0].digit, numbers[numbers.length-1].digit, number);

    return number;
}
