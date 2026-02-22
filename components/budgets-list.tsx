import { FaFileInvoiceDollar } from "react-icons/fa6";
import { textColorsMapping } from "../lib/colors";
import { FaMoneyBills } from "react-icons/fa6";
import { FaCoffee } from "react-icons/fa";
import StatusCard from "./status-card";

export default function BudgetsList() {
  const budgets = [
    {
      id: 1,
      description: "Grocery Store",
      amount: 54.23,
      max: 500,
      icon: (
        <FaFileInvoiceDollar
          className={`h-5 w-5 ${textColorsMapping["blue"]}`}
        />
      ),
      color: "blue",
    },
    {
      id: 2,
      description: "Salary",
      amount: 2500.0,
      max: 5000,
      icon: (
        <FaMoneyBills className={`h-5 w-5 ${textColorsMapping["green"]}`} />
      ),
      color: "green",
    },
    {
      id: 3,
      description: "Coffee Shop",
      amount: 5.75,
      max: 100,
      icon: <FaCoffee className={`h-5 w-5 ${textColorsMapping["yellow"]}`} />,
      color: "yellow",
    },
  ];

  return (
    <div className="w-full">
      <div className="flex justify-between">
        <h2 className="text-sm font-bold text-gray-900 dark:text-white">
          Category budgets
        </h2>
        <button className="text-xs text-orange-500">View All</button>
      </div>

      <ul className="space-y-3 mt-4">
        {budgets.map((budget) => (
          <li key={budget.id} className="flex justify-between items-center">
            <StatusCard
              id={budget.id}
              description={budget.description}
              amount={budget.amount}
              max={budget.max}
              icon={budget.icon}
              color={budget.color}
            />
          </li>
        ))}
      </ul>
    </div>
  );
}
