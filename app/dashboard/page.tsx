import NotificationsBell from "@/components/notifications-bell";
import QuickActions from "@/components/quick-actions";
import TopNavigation from "@/components/top-navigation";
import TotalBalanceCard from "@/components/total-balance-card";
import UserCard from "@/components/user-card";

export default function Dashboard() {
  return (
    <main className="min-h-screen flex flex-col items-center pt-16">
      <TopNavigation userCard={<UserCard name="Tomas" />}>
        <NotificationsBell />
      </TopNavigation>
      <div className="flex flex-col max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 w-full gap-2">
        <TotalBalanceCard />
        <QuickActions />
      </div>
    </main>
  );
}
