import Sidebar from "../navigation/sidebar";

export default function DesktopLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <div className="bg-gray-100 dark:bg-gray-900 min-h-screen flex flex-row">
      <Sidebar />
      <div className="flex-1">{children}</div>
    </div>
  );
}
