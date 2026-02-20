import { FaFileInvoiceDollar } from "react-icons/fa6";
import TransactionCard from "./transaction-card";
import { textColorsMapping } from "../lib/colors";
import { FaMoneyBills } from "react-icons/fa6";
import { FaCoffee } from "react-icons/fa";

export default function RecentTransactionsList() {
  const transactions = [
    {
      id: 1,
      description: "Grocery Store",
      amount: -54.23,
      date: "2024-06-15",
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
      date: "2024-06-01",
      icon: (
        <FaMoneyBills className={`h-5 w-5 ${textColorsMapping["green"]}`} />
      ),
      color: "green",
    },
    {
      id: 3,
      description: "Coffee Shop",
      amount: -5.75,
      date: "2024-06-14",
      icon: <FaCoffee className={`h-5 w-5 ${textColorsMapping["yellow"]}`} />,
      color: "yellow",
    },
  ];

  return (
    <div className="w-full">
      <div className="flex justify-between">
        <h2 className="text-sm font-bold text-gray-900 dark:text-white">
          Recent Transactions
        </h2>
        <button className="text-xs text-orange-500">View All</button>
      </div>

      <ul className="space-y-3 mt-4">
        {transactions.map((transaction) => (
          <li
            key={transaction.id}
            className="flex justify-between items-center"
          >
            <TransactionCard
              id={transaction.id}
              description={transaction.description}
              amount={transaction.amount}
              date={transaction.date}
              icon={transaction.icon}
              color={transaction.color}
            />
          </li>
        ))}
      </ul>
    </div>
  );
}
