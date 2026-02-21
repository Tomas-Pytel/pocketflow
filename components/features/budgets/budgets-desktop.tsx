import { PageComponentProps } from "../dashboard/dashboard-mobile";

export default function BudgetsDesktop({ className }: PageComponentProps) {
  return (
    <div className={`p-6 ${className}`}>
      <h1 className="text-2xl font-bold mb-4">Budgets Desktop</h1>
      <p>This is the desktop version of the budgets page.</p>
    </div>
  );
}
