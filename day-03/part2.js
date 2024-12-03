import { promises } from "fs";

const input = await promises.readFile(
  import.meta.dirname + "/input.txt",
  "utf-8"
);

let sum = 0;
let doo = true;
input.split("\n").forEach((line) => {
  if (line.trim().length === 0) return;
  const arr = [
    ...line.matchAll(/mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)/g),
  ];
  arr.forEach(([a, b, c]) => {
    if (a === "do()") doo = true;
    else if (a === "don't()") doo = false;
    else {
      if (doo) {
        sum += b * c;
      }
    }
  });
});

console.log(sum);
