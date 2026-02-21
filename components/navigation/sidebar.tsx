import { FaPiggyBank } from "react-icons/fa";
import { IoHome } from "react-icons/io5";
import { MdWallet } from "react-icons/md";
import { RiBarChart2Fill } from "react-icons/ri";
import { NavLinkProps } from "./bottom-navigation";
import NavigationLink from "./navigation-link";

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

export default function Sidebar() {
  return (
    <nav className="hidden lg:flex lg:flex-col lg:w-64 bg-white border-r border-gray-200 dark:bg-gray-800 dark:border-gray-700">
      <div className="flex items-center justify-center border-b border-gray-200 dark:border-gray-700">
        <ul>
          {links.map((link, index) => (
            <li key={index}>
              <NavigationLink
                href={link.href}
                title={link.title}
                className="flex items-center space-x-3 px-4 py-2"
              >
                {link.children}
              </NavigationLink>
            </li>
          ))}
        </ul>
      </div>
    </nav>
  );
}
