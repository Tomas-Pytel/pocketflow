import BudgetsDesktop from "@/components/features/budgets/budgets-desktop";
import BudgetsMobile from "@/components/features/budgets/budgets-mobile";

export default function BudgetsPage() {
  return (
    <>
      <BudgetsDesktop className="hidden md:block" />
      <BudgetsMobile />
    </>
  );
}
