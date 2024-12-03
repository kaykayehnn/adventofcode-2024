import { promises } from "fs";

const input = await promises.readFile(
  import.meta.dirname + "/input.txt",
  "utf-8"
);

const listA = [];
const listB = [];
let count = 0;

function isSafe(arr) {
  let safe = true;
  let dir = null;
  for (let i = 1; i < arr.length; i++) {
    if (Math.abs(arr[i] - arr[i - 1]) > 3) safe = false;
    if (arr[i] > arr[i - 1]) {
      if (dir === false) safe = false;
      dir = true;
    }
    if (arr[i] < arr[i - 1]) {
      if (dir === true) safe = false;
      dir = false;
    }
    if (arr[i] === arr[i - 1]) {
      safe = false;
    }
  }

  return safe;
}

input.split("\n").forEach((line) => {
  if (line.trim().length === 0) return;
  const arr = line.split(/\s+/).map((a) => +a);
  let safe = isSafe(arr);

  for (let i = 0; i < arr.length && !safe; i++) {
    safe |= isSafe(arr.filter((_, index) => index !== i));
  }

  if (safe) count++;
});

console.log(count);
