import { Card } from "./ui/card";

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
  return <Card className="h-full w-full"></Card>;
}
