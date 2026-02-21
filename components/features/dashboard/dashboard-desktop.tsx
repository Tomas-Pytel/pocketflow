import { PageComponentProps } from "./dashboard-mobile";

export default function DashboardDesktop({ className }: PageComponentProps) {
  return (
    <div className={`p-6 ${className || ""}`}>
      <h1 className="text-2xl font-bold mb-4">Dashboard Desktop</h1>
      <p>This is the desktop version of the dashboard.</p>
    </div>
  );
}
