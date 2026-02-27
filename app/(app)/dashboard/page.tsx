import DashboardBalance from "@/components/dashboard-balance";
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
      <div className="hidden md:flex flex-col h-full">
        <div className="col-span-12 py-4 shrink-0">
          <p className="text-2xl font-bold">Dashboard Overview</p>
          <p className="text-md">Here is your quick summary</p>
        </div>

        <div className="grid grid-cols-12 flex-1 gap-4">
          <div className="col-span-9 space-y-4">
            <div className="h-52">
              <DashboardBalance
                totalBalance={12000}
                expenses={1500}
                savings={800}
                income={2000}
              />
            </div>
            <div className="h-42">
              <QuickActions />
            </div>
            <div className="h-64">
              <SpendingBreakdownGraph />
            </div>
          </div>

          {/**RIGTH PANEL */}
          <div className="col-span-3 bg-red-300">Savings goals</div>
        </div>
      </div>
    </>
  );
}
