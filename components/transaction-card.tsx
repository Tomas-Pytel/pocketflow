import { ReactNode } from "react";
import { bgColorsMapping } from "./colors";

interface TransactionCardProps {
  id: number;
  description: string;
  amount: number;
  date: string;
  color?: string;
  icon?: ReactNode;
}

export default function TransactionCard({
  description,
  amount,
  date,
  icon,
  color,
}: TransactionCardProps) {
  return (
    <div className="bg-white dark:bg-gray-800 dark:border dark:border-gray-700 rounded-xl shadow p-4 flex justify-between items-center w-full">
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
          <p className="text-xs text-gray-500 dark:text-gray-400">{date}</p>
        </div>
      </div>
      <p
        className={`text-sm font-bold ${amount < 0 ? "text-gray-900 dark:text-white" : "text-green-500"}`}
      >
        {amount < 0 ? "-" : "+"}${Math.abs(amount).toFixed(2)}
      </p>
    </div>
  );
}
