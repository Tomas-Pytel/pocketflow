import NavigationLink from "./navigation-link";
import { IoHome } from "react-icons/io5";
import { MdWallet } from "react-icons/md";
import { FaPiggyBank } from "react-icons/fa6";
import { RiBarChart2Fill } from "react-icons/ri";

export interface NavLinkProps {
  href: string;
  title: string;
  children: React.ReactNode;
  className?: string;
}

const links: NavLinkProps[] = [
  {
    href: "/dashboard",
    title: "Home",
    children: <IoHome className="h-5 w-5" />,
  },
  {
    href: "/budgets",
    title: "Budgets",
    children: <MdWallet className="h-5 w-5" />,
  },
  {
    href: "/savings",
    title: "Savings",
    children: <FaPiggyBank className="h-5 w-5" />,
  },
  {
    href: "/analytics",
    title: "Analytics",
    children: <RiBarChart2Fill className="h-5 w-5" />,
  },
];

export default function BottomNavigation() {
  return (
    <nav className="fixed bottom-0 left-0 right-0 bg-white border-t border-gray-200 dark:bg-gray-800 dark:border-gray-700 z-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="w-full h-20">
          <ul className="flex items-center justify-between h-full">
            {links.map((link, index) => (
              <li key={index}>
                <NavigationLink
                  key={link.href}
                  href={link.href}
                  title={link.title}
                  className="flex flex-col items-center space-y-2"
                >
                  {link.children}
                </NavigationLink>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </nav>
  );
}
