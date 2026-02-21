import DashboardDesktop from "@/components/features/dashboard/dashboard-desktop";
import TopNavigationMobile from "@/components/navigation/top-navigation-mobile";
import NotificationsBell from "@/components/notifications-bell";
import QuickActions from "@/components/quick-actions";
import RecentTransactionsList from "@/components/recent-transactions-list";
import SpendingBreakdownGraph from "@/components/spending-breakdown-graph";
import { ThemeSwitcher } from "@/components/theme-switcher";
import TotalBalanceCard from "@/components/total-balance-card";
import UserCard from "@/components/user-card";

export default function Dashboard() {
  return (
    <>
      {/**----- MOBILE LAYOUT ----- */}
      <div className="flex flex-col gap-6 md:hidden pt-16">
        <TopNavigationMobile userCard={<UserCard name="Tomas" />}>
          <NotificationsBell />
          <ThemeSwitcher />
        </TopNavigationMobile>
        <div className="flex flex-col max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 w-full gap-5">
          <TotalBalanceCard />
          <QuickActions />
          <SpendingBreakdownGraph />
          <RecentTransactionsList />
        </div>
      </div>

      {/**----- DESKTOP LAYOUT ----- */}
      <div className="hidden md:grid grid-cols-12 gap-8"></div>
      <DashboardDesktop className="hidden md:block" />
    </>
  );
}
