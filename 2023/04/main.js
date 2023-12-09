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

    const cards = [];

    for (let i = 0; i < lines.length-1; ++i) {
        console.log(lines[i]);
        cards.push(extractCardData(lines[i]));
    }

    let total = cards.length;

    for (let i = 0; i < cards.length; ++i) {
        const toProcess = [];
        const value = determineCardValue(cards[i]);
        console.log("Card", cards[i].cardId, "Value:", value);
        total += value;

        for (let j = 1; j <= value; ++j) {
            console.log("Adding Card", cards[i+j].cardId, "to list to process");
            toProcess.push(i+j);
        }

        while(toProcess.length > 0) {
            const index = toProcess.shift();
            const v = determineCardValue(cards[index]);
            console.log("Downstream Card", cards[index].cardId, "Value:", v);
            total += v;

            for (let j = 1; j <= v; ++j) {
                console.log("Adding Card", cards[index+j].cardId, "to list to process");
                toProcess.unshift(index+j);
            }
        }
    }

    console.log(total);
} catch (err) {
    console.error(err);
}

function determineCardValue(card) {
    let value = 0;
    card.potential.forEach(number => {
        if (card.winners.includes(number)) {
            value++;
        }
    });

    return value;
}

function extractCardData(value) {
    const parts = value.split(":");

    const cardIndex = parts[0].indexOf(" ");
    const cardId = parts[0].substr(cardIndex+1);

    const numbers = parts[1].split(" | ");
    const winners = numbers[0].split(" ").filter(x => x.length > 0);
    const potential = numbers[1].split(" ").filter(x => x.length > 0);

    return {
        cardId,
        winners,
        potential,
    };
}
