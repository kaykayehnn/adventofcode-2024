import { promises } from "fs";

const input = await promises.readFile(
  import.meta.dirname + "/input.txt",
  "utf-8"
);

const listA = [];
const listB = [];

input.split("\n").forEach((line) => {
  if (line.trim().length === 0) return;

  const [_, numA, numB] = line.match(/(\d+)\s+(\d+)/);
  listA.push(+numA);
  listB.push(+numB);
});

listA.sort((a, b) => a - b);
listB.sort((a, b) => a - b);

let distance = 0;
for (let i = 0; i < listA.length; i++) {
  distance += Math.abs(listA[i] - listB[i]);
}

console.log(`Part one: ${distance}`);

// ------------------
// Part one ends here
// ------------------

const occurrencesB = {};
for (let i = 0; i < listB.length; i++) {
  const num = listB[i];
  if (occurrencesB[num] == null) occurrencesB[num] = 0;
  occurrencesB[num]++;
}

let similarityScore = 0;
for (let i = 0; i < listA.length; i++) {
  const num = listA[i];
  similarityScore += num * (occurrencesB[num] ?? 0);
}

console.log(`Part two: ${similarityScore}`);
