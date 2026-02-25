import BudgetsStatus from "@/components/budget-status";
import BudgetsList from "@/components/budgets-list";
import TopNavigationMobile from "@/components/navigation/top-navigation-mobile";
import { Button } from "@/components/ui/button";
import { Plus } from "lucide-react";
import { Suspense } from "react";
import { SlOptions } from "react-icons/sl";
import StatCard from "@/components/ui/stat-card";

export default function BudgetsMobile() {
  return (
    <main
      className={`min-h-screen flex flex-col bg-white items-center pt-16 pb-24 overflow-auto`}
    >
      <TopNavigationMobile title="Budgets" backButton={true}>
        <SlOptions className="h-5 w-5 text-gray-500 dark:text-gray-100 hover:text-gray-700" />
      </TopNavigationMobile>
      <div className="flex flex-col max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 w-full gap-3">
        <Suspense fallback={null}>
          <BudgetsStatus />
        </Suspense>
        <div className="flex justify-between gap-3">
          <StatCard
            title="Total Budget"
            value={"$3,500"}
            className="bg-orange-100/50 border border-orange-200"
          />
          <StatCard title="Spent so far" value={"$2,400"} />
        </div>
        <BudgetsList />
        <Button
          variant="outline"
          className="w-full  p-7 rounded-xl bg-gradient-to-r from-orange-500 to-yellow-500 text-white"
        >
          <div className="bg-white flex rounded-full text-orange-500 font-bold">
            <Plus className="h-4 w-4" />
          </div>
          <span className="text-white font-semibold">Set new budget</span>
        </Button>
      </div>
    </main>
  );
}
