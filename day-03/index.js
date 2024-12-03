import { promises } from "fs";

const input = await promises.readFile(
  import.meta.dirname + "/input.txt",
  "utf-8"
);

let sum = 0;
input.split("\n").forEach((line) => {
  if (line.trim().length === 0) return;
  const arr = [...line.matchAll(/mul\((\d{1,3}),(\d{1,3})\)/g)];
  arr.forEach(([a, b, c]) => (sum += b * c));
});

console.log(sum);
