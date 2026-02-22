import BottomNavigation from "@/components/navigation/bottom-navigation";
import Navbar from "@/components/navigation/navbar";
import Sidebar from "@/components/navigation/sidebar";

export default function AppLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <div className="flex h-screen flex-col md:flex-row bg-gray-100 dark:bg-gray-900">
      {/**SIDEBAR - visible only on tablet higher */}
      <Sidebar />

      {/**MAIN CONTENT */}
      <main className="flex flex-col flex-1">
        <div className="hidden md:block h-14">
          <Navbar />
        </div>
        <div className="flex-1 overflow-y-auto p-5 pb-24 md:pb-5">
          {children}
        </div>
      </main>

      {/**MOBILE NAVIGATION - visible only on mobile*/}
      <BottomNavigation />
    </div>
  );
}
