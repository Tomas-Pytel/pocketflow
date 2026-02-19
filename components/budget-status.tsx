"use client";

import RadialProgress from "./radial-progress-bar";

export default function BudgetsStatus() {
  const today: Date = new Date();

  return (
    <div className="bg-white dark:bg-gray-800 dark:border dark:border-gray-700 rounded-xl p-4 flex justify-between items-center w-full">
      <div className="flex-1 flex flex-col gap-5 items-center">
        <span className="text-sm font-medium text-gray-500 dark:text-gray-400">
          {today.toLocaleString("en", { month: "long" }).toUpperCase()} BUDGET
          STATUS
        </span>
        <div className="h-40 w-40">
          <RadialProgress
            value={75}
            strokeWidth={8}
            label="75%"
            color="#fb923c"
          >
            <div className="flex flex-col justify-center items-center">
              <span className="text-2xl font-bold text-foreground">$750</span>
              <span className="text-xs text-muted-foreground">Remaining</span>
            </div>
          </RadialProgress>
        </div>
      </div>
    </div>
  );
}
