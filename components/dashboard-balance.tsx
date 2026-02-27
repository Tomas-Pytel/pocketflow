import { Card, CardContent } from "./ui/card";
import StatCard from "./ui/stat-card";

interface DashboardBalanceProps {
  totalBalance: number;
  income: number;
  expenses: number;
  savings: number;
}

export default function DashboardBalance({
  totalBalance,
  income,
  expenses,
  savings,
}: DashboardBalanceProps) {
  return (
    <Card className="h-full w-full bg-white dark:bg-gray-800 dark:border dark:border-gray-700 shadow-none">
      <CardContent className="flex flex-col h-full w-full p-6 gap-3">
        <div className="w-full">
          <p className="text-gray-800 dark:text-gray-300">Total Balance</p>
          <p className="font-bold text-5xl">${totalBalance}</p>
        </div>
        <div className="flex gap-3">
          <StatCard
            title="INCOME"
            value={`$${income}`}
            className="dark:bg-orange-500"
          />
          <StatCard
            title="EXPENSES"
            value={`$${expenses}`}
            className="dark:bg-orange-500"
          />
          <StatCard
            title="SAVINGS"
            value={`$${savings}`}
            className="dark:bg-orange-500"
          />
        </div>
        {/**bar chart with spendings */}
        {/**
 * <div className="flex w-full">
          <CategoryExpenseGraph />
        </div>
 */}
      </CardContent>
    </Card>
  );
}
