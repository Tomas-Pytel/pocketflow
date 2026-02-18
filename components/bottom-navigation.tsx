import NavigationLink from "./navigation-link";
import { IoHome } from "react-icons/io5";
import { MdWallet } from "react-icons/md";
import { FaPiggyBank } from "react-icons/fa6";
import { RiBarChart2Fill } from "react-icons/ri";

export default function BottomNavigation() {
  return (
    <nav className="fixed bottom-0 left-0 right-0 bg-white border-t border-gray-200 dark:bg-gray-800 dark:border-gray-700 z-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="w-full h-20">
          <div className="flex items-center justify-between h-full">
            <NavigationLink
              href="/dashboard"
              className="flex flex-col items-center space-y-2"
            >
              <IoHome className="h-5 w-5" />
              <span className="text-xs">Home</span>
            </NavigationLink>
            <NavigationLink
              href="/budgets"
              className="flex flex-col items-center space-y-2"
            >
              <MdWallet className="h-5 w-5" />
              <span className="text-xs">Budgets</span>
            </NavigationLink>
            <NavigationLink
              href="/savings"
              className="flex flex-col items-center space-y-2"
            >
              <FaPiggyBank className="h-5 w-5" />
              <span className="text-xs">Savings</span>
            </NavigationLink>
            <NavigationLink
              href="/analytics"
              className="flex flex-col items-center space-y-2"
            >
              <RiBarChart2Fill className="h-5 w-5" />
              <span className="text-xs">Analytics</span>
            </NavigationLink>
          </div>
        </div>
      </div>
    </nav>
  );
}
