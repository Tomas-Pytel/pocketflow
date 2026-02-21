import BottomNavigation from "../navigation/bottom-navigation";

export default function MobileLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <div className="bg-gray-100 dark:bg-gray-900 min-h-screen flex flex-col">
      {children}
    </div>
  );
}
