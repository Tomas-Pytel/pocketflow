import { CategoryExpenseGraph } from "./category-expense-graph";
import { Card, CardContent } from "./ui/card";
import StatCard from "./ui/stat-card";

export type Months =
  | "January"
  | "February"
  | "March"
  | "April"
  | "May"
  | "June"
  | "July"
  | "August"
  | "September"
  | "October"
  | "November"
  | "December";

interface SpendingsByMonth {
  month: Months;
  spendings: number;
}

interface DashboardBalanceProps {
  totalBalance: number;
  income: number;
  expenses: number;
  savings: number;
  spendingsByMonth: SpendingsByMonth[];
}

export default function DashboardBalance({
  totalBalance,
  income,
  expenses,
  savings,
  spendingsByMonth,
}: DashboardBalanceProps) {
  return (
    <Card className="h-full w-full bg-white dark:bg-gray-800 dark:border dark:border-gray-700">
      <CardContent className="flex flex-col h-full w-full p-6 gap-3">
        <div className="w-full">
          <p className="text-gray-800">Total Balance</p>
          <p className="font-bold text-5xl">${totalBalance}</p>
        </div>
        <div className="flex gap-3">
          <StatCard title="INCOME" value={`$${income}`} />
          <StatCard title="EXPENSES" value={`$${expenses}`} />
          <StatCard title="SAVINGS" value={`$${savings}`} />
        </div>
        {/**bar chart with spendings */}
        <div className="flex w-full">
          <CategoryExpenseGraph />
        </div>
      </CardContent>
    </Card>
  );
}
