import BottomNavigation from "@/components/navigation/bottom-navigation";
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
      <main className="flex-1 overflow-y-auto p-4 pb-20 md:pb-4">
        {children}
      </main>

      {/**MOBILE NAVIGATION - visible only on mobile*/}
      <BottomNavigation />
    </div>
  );
}
