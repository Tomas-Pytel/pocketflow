import NotificationsBell from "@/components/notifications-bell";
import QuickActions from "@/components/quick-actions";
import RecentTransactionsList from "@/components/recent-transactions-list";
import SpendingBreakdownGraph from "@/components/spending-breakdown-graph";
import { ThemeSwitcher } from "@/components/theme-switcher";
import TopNavigation from "@/components/navigation/top-navigation";
import TotalBalanceCard from "@/components/total-balance-card";
import UserCard from "@/components/user-card";

export default function Dashboard() {
  return (
    <main className="min-h-screen flex flex-col items-center pt-16 pb-24 overflow-auto">
      <TopNavigation userCard={<UserCard name="Tomas" />}>
        <NotificationsBell />
        <ThemeSwitcher />
      </TopNavigation>
      <div className="flex flex-col max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 w-full gap-5">
        <TotalBalanceCard />
        <QuickActions />
        <SpendingBreakdownGraph />
        <RecentTransactionsList />
      </div>
    </main>
  );
}
