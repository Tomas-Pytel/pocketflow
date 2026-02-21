import BudgetsStatus from "@/components/budget-status";
import BudgetsList from "@/components/budgets-list";
import TopNavigation from "@/components/navigation/top-navigation";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardDescription,
  CardTitle,
  CardContent,
} from "@/components/ui/card";
import { Plus } from "lucide-react";
import { Suspense } from "react";
import { SlOptions } from "react-icons/sl";

export default function BudgetsPage() {
  return (
    <main className="min-h-screen flex flex-col bg-white items-center pt-16 pb-24 overflow-auto">
      <TopNavigation title="Budgets" backButton={true}>
        <SlOptions className="h-5 w-5 text-gray-500 dark:text-gray-100 hover:text-gray-700" />
      </TopNavigation>
      <div className="flex flex-col max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 w-full gap-3">
        <Suspense fallback={null}>
          <BudgetsStatus />
        </Suspense>
        <div className="flex justify-between gap-3">
          <Card className="flex-1 bg-orange-100/50 border border-orange-200">
            <CardContent className="p-5 flex justify-start">
              <div className="flex flex-col items-start gap-1.5">
                <CardDescription>Total Budget</CardDescription>
                <CardTitle>$3,500</CardTitle>
              </div>
            </CardContent>
          </Card>
          <Card className="flex-1 bg-gray-50 border border-muted-200">
            <CardContent className="p-5 flex justify-start">
              <div className="flex flex-col items-start gap-1.5">
                <CardDescription>Spent so far</CardDescription>
                <CardTitle>$2,300</CardTitle>
              </div>
            </CardContent>
          </Card>
        </div>
        <BudgetsList />
        <Button
          variant="outline"
          className="w-full bg-gradient-to-r p-7 rounded-xl from-orange-500 to-yellow-500 text-white"
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
