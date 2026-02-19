import { ReactNode } from "react";
import { bgColorsMapping } from "./colors";
import { Progress } from "@/components/ui/progress";

interface BudgetCardProps {
  id: number;
  description: string;
  amount: number;
  max: number;
  color?: string;
  icon?: ReactNode;
}

export default function BudgetCard({
  description,
  amount,
  max,
  icon,
  color,
}: BudgetCardProps) {
  return (
    <div className="bg-white dark:bg-gray-800 dark:border dark:border-gray-700 rounded-xl shadow p-4 flex flex-col gap-3 w-full">
      <div className="flex justify-between">
        <div className="flex items-center space-x-2">
          <div
            className={`p-2 ${color ? bgColorsMapping[color] : ""} rounded-md`}
          >
            {icon}
          </div>
          <div>
            <p className="text-sm font-bold text-gray-900 dark:text-white">
              {description}
            </p>
            <p className="text-xs text-gray-500 dark:text-gray-400">
              {Math.floor(Math.min(100, (amount / max) * 100))}% of limit used
            </p>
          </div>
        </div>
        <div className="flex flex-1 items-center justify-end gap-1">
          <span className="text-md font-bold">{`$${amount.toFixed(2)}`}</span>
          <span className="text-xs text-gray-500 dark:text-gray-400">
            {"/ "}${max.toFixed(0)}
          </span>
        </div>
      </div>
      <Progress
        value={Math.min(100, (amount / max) * 100)}
        className="h-2 bg-gray-200 [&>div]:bg-gradient-to-r [&>div]:from-orange-400 [&>div]:to-yellow-500"
      />
    </div>
  );
}
