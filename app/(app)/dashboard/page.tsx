import DashboardMobile from "@/components/features/dashboard/dashboard-mobile";
import DashboardDesktop from "@/components/features/dashboard/dashboard-desktop";

export default function Dashboard() {
  return (
    <>
      <DashboardDesktop className="hidden md:block" />
      <DashboardMobile className="block md:hidden" />
    </>
  );
}
